using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_04
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int count = 0, count_all = 0;

            string[] _pairs, _r1, _r2;
            int r1a, r1b, r2a, r2b;

            foreach(string a in input)
            {
                _pairs = a.Split(new char[] { ',' });
                _r1 = _pairs[0].Split(new char[] { '-' });
                _r2 = _pairs[1].Split(new char[] { '-' });
                r1a = int.Parse(_r1[0]);
                r1b = int.Parse(_r1[1]);
                r2a = int.Parse(_r2[0]);
                r2b = int.Parse(_r2[1]);

                if (((r1a >= r2a) && (r1b <= r2b)) || ((r1a <= r2a) && (r1b >= r2b)))
                {
                    count++;
                    count_all++;
                }
                else if ( ((r1a <= r2a) && (r2a <= r1b)) || ((r1a <= r2b) && (r2b <= r1b)) || ((r2a <= r1a) && (r1a <= r2b)) || ((r2a <= r1b) && (r1b <= r2b)))
                {
                    count_all++;
                }
            }

            Console.WriteLine($"Count (1): {count}");
            Console.WriteLine($"Count (2): {count_all}");

            Console.ReadLine();
        }
    }
}
