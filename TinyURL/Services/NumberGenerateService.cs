using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using TinyURL.Options;
using TinyURL.Services.Interfaces;

namespace TinyURL.Services
{
    public class NumberGenerateService : RandomNumberGenerator, INumberGenerateService
    {
        private readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();
        private int max { get; set; }
        private int min { get; set; }

        public NumberGenerateService(IOptions<RandomNumberOptions> options)
        {
            max = options.Value.Max;
            min = options.Value.Min;
        }
        public int Next()
        {
            var data = new byte[sizeof(int)];
            rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
        }

        public int GenerateInt()
        {
            return Next(min, max);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (int)Math.Floor((minValue + ((double)maxValue - minValue) * NextDouble()));
        }

        public double NextDouble()
        {
            var data = new byte[sizeof(uint)];
            rng.GetBytes(data);
            var randUint = BitConverter.ToUInt32(data, 0);
            return randUint / (uint.MaxValue + 1.0);
        }

        public override void GetBytes(byte[] data)
        {
            rng.GetBytes(data);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            rng.GetNonZeroBytes(data);
        }
    }
}
