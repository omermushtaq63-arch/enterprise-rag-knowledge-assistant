using System.Security.Cryptography;
using System.Text;

namespace EnterpriseRag;

public sealed class EmbeddingService
{
    public float[] Embed(string text)
    {
        const int n = 64;
        var v = new float[n];
        foreach (var token in text.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
            var idx = BitConverter.ToUInt16(hash, 0) % n;
            v[idx] += 1;
        }
        var norm = MathF.Sqrt(v.Sum(x => x*x));
        return norm == 0 ? v : v.Select(x => x/norm).ToArray();
    }
}