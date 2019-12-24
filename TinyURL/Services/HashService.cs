using Microsoft.Extensions.Options;
using System;
using TinyURL.Options;
using TinyURL.Services.Interfaces;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TinyURL.Services
{
    public class HashService : IHashService
    {
        private readonly INumberGenerateService numberGenerateService;
        private char[] map { get; set; }
        public HashService(INumberGenerateService numberGenerateService ,IOptions<HashOptions> options)
        {
            map = options.Value.Map.ToCharArray();
            this.numberGenerateService = numberGenerateService;
        }
        public async Task<string> GenerateHash(string url)
        {
            var arr = url.ToCharArray();
            Array.Reverse(arr);
            var reverseUrl = new string(arr);
            byte[] encrypted;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.GenerateKey();
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                           await swEncrypt.WriteAsync(reverseUrl);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            string hash = ToBase64String(encrypted);
            return hash;
        }

        private string ToBase64String(byte[] url)
        {
            var fullHash = Convert.ToBase64String(url);
            var startIndex = numberGenerateService.Next(fullHash.Length - 8);
            var hash = fullHash.Substring(startIndex, 8).ToLower().Replace('/', RandChar()).Replace('=', RandChar()).Replace('+', RandChar());
            return hash;
        }

        private char RandChar()
        {
            var randomCharIndex = numberGenerateService.GenerateInt();
            return map[randomCharIndex];
        }
    }
}
