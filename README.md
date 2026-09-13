# Will AI Replace .NET Developers?

AI can generate a .NET API in seconds. But can it tell us whether that API is correct, secure, scalable, maintainable, and right for the problem?

This is a 14-day build-and-learn-in-public challenge about understanding what happens inside a .NET API: from an incoming HTTP request to a deployed service.

It is not an anti-AI challenge. AI is part of the workflow now, and I want to become better at using it. The point is that fundamentals are what let engineers ask better questions, evaluate generated code, spot trade-offs, and make responsible decisions.

**Start here:** [.NET API Foundations](docs/00-dotnet-api-foundations.md) — a Day 0 prerequisite refresher before Day 1 begins.

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

- [ ] Day 1 — ASP.NET Core request pipeline and middleware
- [ ] Day 2 — Dependency injection and service lifetimes
- [ ] Day 3 — API structure, DTOs, and clean boundaries
- [ ] Day 4 — Validation and error handling
- [ ] Day 5 — EF Core and database migrations
- [ ] Day 6 — EF Core tracking and query performance
- [ ] Day 7 — Authentication and authorization
- [ ] Day 8 — Async/await in APIs
- [ ] Day 9 — Caching
- [ ] Day 10 — Logging and observability
- [ ] Day 11 — Unit and integration testing
- [ ] Day 12 — Docker
- [ ] Day 13 — AWS deployment
- [ ] Day 14 — The complete request journey and final recap

## How to follow along

GitHub is the source of truth for this challenge. Each day will add one focused update with that day's notes, experiments, code where relevant, diagrams, and tests.

The detailed lessons will live in [`docs/`](docs/). As the series progresses, each roadmap item above will link to its corresponding daily note. I’ll also share one short post on X/Twitter each day to point people to that day's GitHub update.

## Planned repository layout

```text
.
├── docs/       # Detailed daily lessons
├── diagrams/   # Request-flow and architecture diagrams
├── src/        # Small .NET API built as supporting evidence
└── tests/      # Unit and integration tests
```

The API is deliberately not here yet. Day 1 starts tomorrow with the ASP.NET Core request pipeline and middleware.

## Publishing workflow

`main` will remain the public, readable record of the complete journey. Each day will be developed on a short-lived branch, then squash-merged into `main` as one meaningful commit—for example:

```text
day 01: document the ASP.NET Core request pipeline
```

That same commit will update the roadmap and link to the completed lesson.

---

Starting tomorrow. One useful concept at a time.
