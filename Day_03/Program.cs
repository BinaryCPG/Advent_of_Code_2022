using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_03
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string test = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            for(int i = 0; i < test.Length; i++)
            {
                Console.Write($"{test[i]} ({ItemPrio(test[i])}) ");
                if (i % 2 == 1)
                {
                    Console.Write("\n");
                }
            }
            Console.ReadLine();
            */

            string[] rucksacks = File.ReadAllLines("input.txt");
            int sum = 0;
            foreach(string rs in rucksacks)
            {
                string c1 = rs.Substring(0, rs.Length / 2);
                for(int i = (rs.Length / 2); i < rs.Length; i++)
                {
                    if (c1.Contains($"{rs[i]}"))
                    {
                        sum += ItemPrio(rs[i]);
                        break;
                    }
                    /*
                    foreach(char c2 in c1)
                    {
                        if(c2 == rs[i])
                        {
                            sum += ItemPrio(c2);
                            i = rs.Length;
                            break;
                        }
                    }
                    */
                }
            }
            Console.WriteLine($"Sum(1): {sum}");

            sum = 0;
            for(int i = 0; i < rucksacks.Length; i += 3)
            {
                foreach(char item in rucksacks[i])
                {
                    if(rucksacks[i+1].Contains($"{item}") && rucksacks[i + 2].Contains($"{item}"))
                    {
                        sum += ItemPrio(item);
                        break;
                    }
                }
            }
            Console.WriteLine($"Sum(2)" +
                $"" +
              : {sum}");
            Console.ReadLine();
        }

        static int ItemPrio(char c)
        {
            string sc = $"{c}";
            if(sc.ToLower() != sc)
            {
                //Capital
                return (int)c - (int)'A' + 27;
            }
            else
            {
                //LowerCase
                return ((int)c - (int)'a') + 1;
            }
        }
    }
}
