# Enterprise RAG Knowledge Assistant

A .NET 8 Web API demonstrating ingestion, embeddings, vector retrieval, and grounded-answer patterns for enterprise knowledge systems.

> Portfolio implementation based on professional experience and documented technology areas. It contains no proprietary customer code, data, credentials, or confidential assets.

## Stack
C# / .NET 8 / ASP.NET Core / Azure OpenAI-ready / embeddings / RAG / cosine vector search / REST API / Docker

## Run
```bash
dotnet restore
dotnet run --project src
```

The default implementation runs locally without cloud credentials. The local embedding implementation is deterministic so the retrieval flow can be demonstrated offline.

## API
- `GET /health`
- `POST /api/rag/ingest` with `{"title":"Azure","text":"Azure OpenAI provides..." }`
- `POST /api/rag/ask` with `{"question":"What does Azure OpenAI provide?"}`

See `docs/architecture.md`.

## Note
Azure OpenAI embeddings and Azure AI Search can replace the local provider behind the same service boundary for production deployments.

License: MIT.