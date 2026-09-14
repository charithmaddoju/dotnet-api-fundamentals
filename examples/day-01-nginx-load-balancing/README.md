# Day 1 demo: two Kestrel instances behind Nginx

Supporting project for [Day 1](../../docs/day-01-dotnet-fundamentals-and-hosting.md) (section 13.5). Runs the same ASP.NET Core Minimal API twice, on two ports, and puts Nginx in front of both doing round-robin load balancing — so the reverse-proxy/load-balancer concepts in the doc can be observed instead of just read about.

```text
curl
  ↓
Nginx  (listens on :8080, round-robins)
  ├──→ Kestrel instance A (:5001)
  └──→ Kestrel instance B (:5002)
```

`InstanceApi` has one endpoint, `GET /`, returning which instance and port answered:

```json
{ "instance": "A", "port": 5001, "time": "2026-..." }
```

## Prerequisites

- .NET SDK (net10.0)
- Nginx (macOS: `brew install nginx`)

## Run it

Open three terminals from this folder.

**Terminal 1 — instance A:**

```bash
cd InstanceApi
dotnet run --launch-profile Instance-A
```

**Terminal 2 — instance B:**

```bash
cd InstanceApi
dotnet run --launch-profile Instance-B
```

**Terminal 3 — Nginx, using the config in this folder:**

```bash
nginx -c "$(pwd)/nginx.conf" -p "$(pwd)"
```

`-p` sets Nginx's working directory so the relative `logs/` paths in `nginx.conf` resolve here instead of the Homebrew install location.

## What Terminal 3 actually did

`nginx -c "$(pwd)/nginx.conf" -p "$(pwd)"` starts Nginx itself — a real, general-purpose web server/reverse proxy, the same Nginx covered in section 8.3 of the Day 1 doc — as a foreground process on your machine, telling it to use *this folder's* `nginx.conf` instead of the default one Homebrew installed. Once it's running, Nginx is listening on a port of its own (8080 — see below) and is not running your API code at all. It only knows how to do one thing with a request that arrives: look at the config, and forward it somewhere.

At that point you have three processes running side by side:

```text
Instance A  → Kestrel, listening on :5001, running InstanceApi
Instance B  → Kestrel, listening on :5002, running InstanceApi
Nginx       → listening on :8080, running nothing but the reverse-proxy config below
```

Nginx sits in front of the other two. It never runs your C# code — it just decides, for each incoming request, which of the two Kestrel processes should handle it, then forwards the request there and relays the response back to whoever called it. That's the reverse-proxy/load-balancer role from section 8 of the Day 1 doc, made concrete.

## Understanding `nginx.conf`

Walking through the actual file, top to bottom:

```nginx
worker_processes 1;
daemon off;
pid logs/nginx.pid;
```

- `worker_processes 1;` — Nginx normally runs one or more worker processes to handle connections; one is plenty for a local demo.
- `daemon off;` — by default Nginx forks itself into the background and hands your terminal back immediately. This keeps it running in the foreground instead, which is why Terminal 3 stays "stuck" on this command — that's Nginx actively running, not a hang. It's also why `Ctrl+C` in that terminal is how you stop it.
- `pid logs/nginx.pid;` — where Nginx writes its own process ID, purely bookkeeping.

```nginx
events {
    worker_connections 64;
}
```

Baseline networking settings — how many simultaneous connections one worker can hold open. 64 is far more than this demo needs.

```nginx
http {
    access_log logs/access.log;
    error_log  logs/error.log;

    upstream dotnet_backend {
        server 127.0.0.1:5001;
        server 127.0.0.1:5002;
    }
```

This is the important part. An `upstream` block just names a *group of backend servers* — here called `dotnet_backend` — and lists their addresses: `127.0.0.1:5001` (instance A) and `127.0.0.1:5002` (instance B). That's the whole upstream group Nginx is allowed to send requests to.

**This is also the entire round-robin configuration** — there's no separate "enable round-robin" setting to find, because round-robin is Nginx's *default* behavior for an `upstream` block the moment it contains more than one `server` line. Every request that gets proxied to `dotnet_backend` goes to the next server in the list, cycling back to the top after the last one — which is exactly the alternating `A, B, A, B, ...` you saw. (Section 13.4 of the Day 1 doc lists the other strategies — least-connections, weighted, health-checked — any of which you'd configure by adding a directive inside this same `upstream` block instead.)

```nginx
    server {
        listen 8080;

        location / {
            proxy_pass http://dotnet_backend;
            proxy_set_header Host $host;
            proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
            proxy_set_header X-Forwarded-Proto $scheme;
        }
    }
}
```

This `server` block is Nginx's actual public-facing listener — the thing a client talks to.

- **`listen 8080;`** is exactly where `localhost:8080` comes from. It's not a .NET port, not something Kestrel picked, and not a default anyone assumes — it's this one line, telling Nginx "accept connections on port 8080." When you `curl http://localhost:8080/`, you are talking to *Nginx*, on the port this line opened, not to either Kestrel instance directly. (Homebrew's nginx defaults to 8080 rather than 80 so it can run without `sudo` — this demo just reuses that same port rather than picking a new one.)
- `location / { ... }` says "for any request path, do the following."
- `proxy_pass http://dotnet_backend;` is the actual forwarding step: take the request that just arrived on :8080, and send it on to whichever server the `dotnet_backend` upstream group (and its round-robin rotation) picks next — instance A or instance B.
- The three `proxy_set_header` lines forward along the original client info (`Host`, real IP, and whether the original request was HTTPS) as headers, since from Kestrel's point of view every request now appears to come from Nginx on `localhost`, not from your `curl`. This is the same `X-Forwarded-*` mechanism section 8.3 of the Day 1 doc describes.

So end to end: `curl` → hits Nginx on `:8080` (because of `listen 8080;`) → Nginx's `location /` rule fires → `proxy_pass` sends it into the `dotnet_backend` upstream group → round-robin (the default, from having two `server` lines) alternates between `:5001` and `:5002` → that Kestrel instance answers → Nginx relays the response back to `curl`.

## Observe the load balancing

In a fourth terminal:

```bash
for i in 1 2 3 4; do curl -s http://localhost:8080/; echo; done
```

Responses alternate between `"instance":"A"` and `"instance":"B"` — that's Nginx's default round-robin strategy (section 13.4 of the Day 1 doc) picking a different upstream each time.

## Stop everything

- Nginx: `Ctrl+C` in its terminal (it's running in the foreground via `daemon off;`)
- Each `dotnet run`: `Ctrl+C` in its terminal

## Next step (not done yet)

Add a local certificate to `nginx.conf` and re-run this to see TLS termination happen at the proxy — the handshake mechanics from section 13.1 of the Day 1 doc, this time with Nginx as the server the client actually negotiates with.
