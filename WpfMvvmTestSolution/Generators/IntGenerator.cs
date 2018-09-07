using System;
using System.Collections.Generic;
using System.Linq;

namespace InvendTest.Generators
{
    class IntGenerator:IGenerator
    {
        public GeneratedResult Generate(int count)
        {
            var randomizer = new Random();
            var numbers = new List<int>();
            while (numbers.Count < count)
            {
                var newNumber = randomizer.Next(0, 255);
                if (!numbers.Contains(newNumber))
                    numbers.Add(newNumber);
            }

            return new GeneratedResult()
            {
                Lines = numbers.Select(x => x.ToString()).ToArray(),
                Type = GeneratedType.intType
            };
        }
    }
}
