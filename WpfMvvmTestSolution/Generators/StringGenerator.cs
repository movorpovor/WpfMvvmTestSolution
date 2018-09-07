using System;
using System.Linq;

namespace InvendTest.Generators
{
    class StringGenerator:IGenerator
    {
        public GeneratedResult Generate(int count)
        {
            var strings = new string[count];
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var length = 3;
            var randomizer = new Random();
            for (int i = 0; i < count; i++)
            {
                strings[i] = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[randomizer.Next(s.Length)]).ToArray());
            }

            return new GeneratedResult()
            {
                Lines = strings,
                Type = GeneratedType.stringType
            };
        }
    }
}
