# Architecture

Client -> ASP.NET Core API -> ingestion/retrieval services -> vector store abstraction -> grounded response.

The local embedding implementation is deterministic for offline demos. A production deployment can replace it with Azure OpenAI embeddings and Azure AI Search while preserving the API boundary.

No proprietary customer data or implementation is included.