using EnterpriseRag;

public class EmbeddingTests
{
    [Fact]
    public void ProducesFixedDimensionVector()
    {
        var e = new EmbeddingService();
        Assert.Equal(64, e.Embed("azure openai rag").Length);
    }
}