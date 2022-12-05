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

            List<Stack<char>> stacks_9000 = new List<Stack<char>>();
            List<Stack<char>> stacks_9001 = new List<Stack<char>>();
            foreach (List<char> l  in stacks_tmp)
            {
                l.Reverse();
                stacks_9000.Add(new Stack<char>(l));
                stacks_9001.Add(new Stack<char>(l));
            }

            int amount, origin, target;
            List<char> moving = new List<char>();
            for (int i = move_start; i < input.Length; i++)
            {
                string[] move = input[i].Split(new char[] { ' ' });
                amount = int.Parse(move[1]);
                origin = int.Parse(move[3]) - 1;
                target = int.Parse(move[5]) - 1;

                for(int a = 0; a < amount; a++)
                {
                    if(stacks_9000[origin].Count > 0)
                    {
                        stacks_9000[target].Push(stacks_9000[origin].Pop());
                    }
                }

                if(stacks_9001[origin].Count >= amount)
                {
                    moving.Clear();
                    for(int a = 0; a < amount; a++)
                    {
                        moving.Add(stacks_9001[origin].Pop());
                    }
                    moving.Reverse();
                    for (int a = 0; a < amount; a++)
                    {
                        stacks_9001[target].Push(moving[a]);
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach(Stack<char> sc in stacks_9000)
            {
                sb.Append(sc.Peek());
            }
            Console.WriteLine($"Top crates (1): {sb}");
            sb.Clear();
            foreach (Stack<char> sc in stacks_9001)
            {
                sb.Append(sc.Peek());
            }
            Console.WriteLine($"Top crates (2): {sb}");

            Console.ReadLine();
        }
    }
}
