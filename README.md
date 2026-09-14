# Will AI Replace .NET Developers?

AI can generate a .NET API in seconds. But can it tell us whether that API is correct, secure, scalable, maintainable, and right for the problem?

This is a 14-day build-and-learn-in-public challenge about understanding what happens inside a .NET API: from an incoming HTTP request to a deployed service.

It is not an anti-AI challenge. AI is part of the workflow now, and I want to become better at using it. The point is that fundamentals are what let engineers ask better questions, evaluate generated code, spot trade-offs, and make responsible decisions.

**Start here:** [Day 1 — .NET API Foundations](docs/day-01-dotnet-fundamentals-and-hosting.md).

## Why I'm doing this

I'm a software engineer working with .NET, React, SQL, and AWS. This is a personal learning project, not a job-search announcement.

Over the next 14 days, I want to:

- strengthen my backend engineering fundamentals;
- understand .NET API internals more deeply;
- learn to use AI as a more effective learning partner;
- practise communicating technical ideas clearly; and
- leave behind useful, credible public notes for others learning alongside me.

## How I'll learn

I'll use AI to ask questions, explore options, and unblock myself. I'll also refer to official documentation and other useful resources along the way.

But I won't treat generated answers as the final word. I'll verify what I learn through small experiments, code, tests, and evidence. This series is about learning together—not pretending to be an expert teaching from above.

## The 14-day roadmap

- [x] Day 1 — .NET fundamentals, hosting, and a practical Nginx load-balancing demo
- [ ] Day 2 — ASP.NET Core request pipeline and middleware
- [ ] Day 3 — Dependency injection and service lifetimes
- [ ] Day 4 — API structure, DTOs, and clean boundaries
- [ ] Day 5 — Validation and error handling
- [ ] Day 6 — EF Core and database migrations
- [ ] Day 7 — EF Core tracking and query performance
- [ ] Day 8 — Authentication and authorization
- [ ] Day 9 — Async/await in APIs
- [ ] Day 10 — Caching
- [ ] Day 11 — Logging and observability
- [ ] Day 12 — Unit and integration testing
- [ ] Day 13 — Docker
- [ ] Day 14 — AWS deployment
- [ ] Day 15 — The complete request journey and final recap

## How to follow along

GitHub is the source of truth for this challenge. Each day will add one focused update with that day's notes, experiments, code where relevant, diagrams, and tests.

The detailed lessons will live in [`docs/`](docs/). As the series progresses, each roadmap item above will link to its corresponding daily note. I’ll also share one short post on X/Twitter each day to point people to that day's GitHub update.

## Planned repository layout

```text
.
├── docs/       # Detailed daily lessons
├── diagrams/   # Request-flow and architecture diagrams
├── examples/   # Small supporting demo projects (e.g., Day 1's Nginx load-balancing exercise)
├── src/        # Small .NET API built as supporting evidence
└── tests/      # Unit and integration tests
```

The main `src/` API is deliberately not here yet — that starts with Day 2's request pipeline and middleware work. A small standalone demo already lives under `examples/` to support Day 1's practical exercise.

## Publishing workflow

`main` will remain the public, readable record of the complete journey. Each day will be developed on a short-lived branch, then squash-merged into `main` as one meaningful commit—for example:

```text
day 01: document .NET fundamentals and hosting
```

That same commit will update the roadmap and link to the completed lesson.

---

One useful concept at a time.
