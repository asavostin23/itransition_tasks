using System.Text;

var f = new Org.BouncyCastle.Crypto.Digests.Sha3Digest(256);
string path = @"D:\Рабочий стол\Tasks\All2\task2";
string[] hashes = new string[256];
int i = 0;
foreach(string filepath in Directory.GetFiles(path))
{
    byte[] data = File.ReadAllBytes(filepath);
    f.BlockUpdate(data, 0, data.Length);
    byte[] filehash = new byte[32];
    f.DoFinal(filehash, 0);
    hashes[i++] = BitConverter.ToString(filehash).Replace("-", "").ToLowerInvariant();
}

hashes = hashes.OrderBy(x => x).ToArray();
StringBuilder sb = new StringBuilder();
foreach (string hash in hashes)
    sb.Append(hash);
File.WriteAllText(@"D:\Рабочий стол\Tasks\All2\hash.txt", sb.ToString());
