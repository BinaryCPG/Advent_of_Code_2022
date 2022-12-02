using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
            string[] input = File.ReadAllLines("input.txt");
            Console.WriteLine($"{input.Length} lines read");
            List<int> elvesCals = new List<int>();
            int sum = 0;
            foreach(string item in input)
            {
                if (string.IsNullOrEmpty(item))
                {
                    elvesCals.Add(sum);
                    sum = 0;
                }
                else
                {
                    sum += Int32.Parse(item);
                }
            }
            elvesCals.Sort();
            Console.WriteLine($"Max: {elvesCals.Last()}\n");

            Console.WriteLine($"Top three:\n1.: {elvesCals[elvesCals.Count-1]}\n2.: {elvesCals[elvesCals.Count - 2]}\n3.: {elvesCals[elvesCals.Count - 3]}");
            Console.WriteLine($"Together: {(elvesCals[elvesCals.Count - 1] + elvesCals[elvesCals.Count - 2] + elvesCals[elvesCals.Count - 3])}");

            Console.ReadLine();
        }
    }
}
