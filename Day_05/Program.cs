using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_05
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int stack_amount = (int)((input[0].Length + 1) / 4);
            List<List<char>> stacks_tmp = new List<List<char>>();

            for (int i = 0; i < stack_amount; i++)
            {
                stacks_tmp.Add(new List<char>());
            }

            int move_start = 0;
            while(move_start < input.Length && input[move_start][1] != '1')
            {
                for(int i = 0; i < stack_amount; i++)
                {
                    if(input[move_start][i*4 + 1] != ' ')
                    {
                        stacks_tmp[i].Add(input[move_start][i * 4 + 1]);
                    }
                }
                move_start++;
            }
            move_start += 2;

            List<Stack<char>> stacks = new List<Stack<char>>();
            foreach(List<char> l  in stacks_tmp)
            {
                l.Reverse();
                stacks.Add(new Stack<char>(l));
            }

            int amount, origin, target;
            for (int i = move_start; i < input.Length; i++)
            {
                string[] move = input[i].Split(new char[] { ' ' });
                amount = int.Parse(move[1]);
                origin = int.Parse(move[3]) - 1;
                target = int.Parse(move[5]) - 1;

                for(int a = 0; a < amount; a++)
                {
                    if(stacks[origin].Count > 0)
                    {
                        stacks[target].Push(stacks[origin].Pop());
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach(Stack<char> sc in stacks)
            {
                sb.Append(sc.Peek());
            }
            Console.WriteLine($"Top crates: {sb}");
            
            Console.ReadLine();
        }
    }
}
