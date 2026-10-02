using EnterpriseRag;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RagStore>();
builder.Services.AddSingleton<EmbeddingService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.MapGet("/health", () => Results.Ok(new { status = "healthy", service = "enterprise-rag" }));
app.MapPost("/api/rag/ingest", (IngestRequest req, RagStore store, EmbeddingService emb) =>
{
    if (string.IsNullOrWhiteSpace(req.Text)) return Results.BadRequest("Text is required.");
    var id = store.Add(req.Title ?? "Untitled", req.Text, emb.Embed(req.Text));
    return Results.Ok(new { id });
});
app.MapPost("/api/rag/ask", (AskRequest req, RagStore store, EmbeddingService emb) =>
{
    if (string.IsNullOrWhiteSpace(req.Question)) return Results.BadRequest("Question is required.");
    var hits = store.Search(req.Question, emb, 3);
    var answer = hits.Count == 0 ? "No relevant context was found."
        : $"Relevant context: {string.Join(" ", hits.Select(h => h.Text))}";
    return Results.Ok(new { answer, citations = hits.Select(h => new { h.Id, h.Title, h.Score }) });
});
app.Run();

public record IngestRequest(string? Title, string Text);
public record AskRequest(string Question);