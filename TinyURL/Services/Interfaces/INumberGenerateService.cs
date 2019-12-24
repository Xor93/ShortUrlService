using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyURL.Services.Interfaces
{
    public interface INumberGenerateService
    {
        int Next();
        int GenerateInt();
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
        double NextDouble();

    }
}
