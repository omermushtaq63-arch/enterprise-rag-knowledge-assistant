namespace EnterpriseRag;

public sealed record RagDocument(Guid Id, string Title, string Text, float[] Vector);
public sealed record SearchHit(Guid Id, string Title, string Text, double Score);

public sealed class RagStore
{
    private readonly List<RagDocument> _docs = new();

    public Guid Add(string title, string text, float[] vector)
    {
        var id = Guid.NewGuid();
        _docs.Add(new(id, title, text, vector));
        return id;
    }

    public List<SearchHit> Search(string q, EmbeddingService emb, int k)
    {
        var v = emb.Embed(q);
        return _docs.Select(d => new SearchHit(d.Id, d.Title, d.Text, Similarity(v, d.Vector)))
            .OrderByDescending(x => x.Score).Take(k).ToList();
    }

    private static double Similarity(float[] a, float[] b)
    {
        double dot = 0, na = 0, nb = 0;
        for (var i = 0; i < a.Length; i++) { dot += a[i]*b[i]; na += a[i]*a[i]; nb += b[i]*b[i]; }
        return na == 0 || nb == 0 ? 0 : dot / (Math.Sqrt(na) * Math.Sqrt(nb));
    }
}