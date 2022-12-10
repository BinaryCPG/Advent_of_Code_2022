using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_09
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<int, int> a = new Tuple<int, int>(1, 2);
            Tuple<int, int> b = new Tuple<int, int>(0, 0);
            Tuple<int, int> c = GetMovementNew(a, b);

            string[] input = File.ReadAllLines("input.txt");

            int moveAmount;
            Tuple<int, int> t_pos = new Tuple<int, int>(0,0), h_pos = new Tuple<int, int>(0,0);
            List<Tuple<int, int>> tl = new List<Tuple<int, int>>(10);
            for(int i = 0; i < tl.Capacity; i++) { tl.Add(new Tuple<int, int>(0, 0)); }
            char moveDir;
            List<string> visited_1 = new List<string>();
            List<string> visited_2 = new List<string>();
            visited_1.Add($"{t_pos.Item1}_{t_pos.Item2}");
            visited_2.Add($"{t_pos.Item1}_{t_pos.Item2}");
            foreach (string s in input)
            {
                string[] movement = s.Split(new char[] { ' ' });
                moveAmount = int.Parse(movement[1]);
                moveDir = movement[0][0];
                for(int i = 0; i < moveAmount; i++)
                {
                    //Move Head
                    switch (moveDir)
                    {
                        case 'R': 
                            h_pos = new Tuple<int, int>(h_pos.Item1 + 1, h_pos.Item2); 
                            tl[0] = new Tuple<int, int>(tl[0].Item1 + 1, tl[0].Item2);
                            break;
                        case 'L': 
                            h_pos = new Tuple<int, int>(h_pos.Item1 - 1, h_pos.Item2);
                            tl[0] = new Tuple<int, int>(tl[0].Item1 - 1, tl[0].Item2);
                            break;
                        case 'U': 
                            h_pos = new Tuple<int, int>(h_pos.Item1, h_pos.Item2 + 1);
                            tl[0] = new Tuple<int, int>(tl[0].Item1, tl[0].Item2 + 1);
                            break;
                        case 'D': 
                            h_pos = new Tuple<int, int>(h_pos.Item1, h_pos.Item2 - 1);
                            tl[0] = new Tuple<int, int>(tl[0].Item1, tl[0].Item2 - 1);
                            break;
                    }

                    //###PART 1
                    //Move Tail
                    t_pos = GetMovement(h_pos, t_pos);

                    if (!visited_1.Contains($"{t_pos.Item1}_{t_pos.Item2}"))
                    {
                        visited_1.Add($"{t_pos.Item1}_{t_pos.Item2}");
                    }

                    //###PART2
                    //Propagate Movement
                    for(int segment = 1; segment < tl.Count; segment++)
                    {
                        tl[segment] = GetMovementNew(tl[segment - 1], tl[segment]);
                    }

                    if (!visited_2.Contains($"{tl.Last().Item1}_{tl.Last().Item2}"))
                    {
                        visited_2.Add($"{tl.Last().Item1}_{tl.Last().Item2}");
                    }
                }
            }

            Console.WriteLine($"Unique visited locations(1): {visited_1.Count}");
            Console.WriteLine($"Unique visited locations(2): {visited_2.Count}");

            Console.ReadLine();
        }

        static Tuple<int,int> GetMovementNew(Tuple<int, int> head, Tuple<int, int> tail)
        {
            int t_x = tail.Item1, t_y = tail.Item2, h_x = head.Item1, h_y = head.Item2;
            int d_x = t_x - h_x;
            int d_y = t_y - h_y;

            if (Math.Abs(d_x) > 1)
            {
                t_x = t_x < h_x ? t_x+1 : t_x - 1;
                if(Math.Abs(d_y) == 1)
                {
                    t_y = t_y < h_y ? t_y + 1 : t_y - 1;
                }
            }
            if (Math.Abs(d_y) > 1)
            {
                t_y = t_y < h_y ? t_y+1 : t_y - 1;
                if (Math.Abs(d_x) == 1)
                {
                    t_x = t_x < h_x ? t_x + 1 : t_x - 1;
                }
            }

            return new Tuple<int, int>(t_x, t_y);
        }

        static Tuple<int, int> GetMovement(Tuple<int, int> head, Tuple<int, int> tail)
        {
            int t_x = tail.Item1, t_y = tail.Item2, h_x = head.Item1, h_y = head.Item2;
            int d_x = t_x - h_x;
            int d_y = t_y - h_y;

            if (Math.Abs(t_x - h_x) > 1 || Math.Abs(t_y - h_y) > 1)
            {
                //Pure x Movement
                if (t_y - h_y == 0)
                {
                    if (t_x < h_x)
                    {
                        t_x++;
                    }
                    else
                    {
                        t_x = t_x - 1;
                    }
                }
                //Pure y Movement
                else if (t_x - h_x == 0)
                {
                    if (t_y < h_y)
                    {
                        t_y++;
                    }
                    else
                    {
                        t_y = t_y - 1;
                    }
                }
                else if (Math.Abs(t_x - h_x) == 2 && Math.Abs(t_y - h_y) == 1)
                {
                    t_y = h_y;
                    if (t_x < h_x)
                    {
                        t_x++;
                    }
                    else
                    {
                        t_x = t_x - 1;
                    }
                }
                else if (Math.Abs(t_x - h_x) == 1 && Math.Abs(t_y - h_y) == 2)
                {
                    t_x = h_x;
                    if (t_y < h_y)
                    {
                        t_y++;
                    }
                    else
                    {
                        t_y = t_y - 1;
                    }
                }
                else
                {
                    Console.WriteLine($"Erroneous movement: H({h_x}|{h_y}) vs T({t_x}|{t_y})");
                    throw new Exception($"Erroneous movement: H({h_x}|{h_y}) vs T({t_x}|{t_y})");
                }
            }

            return new Tuple<int, int>(t_x, t_y);
        }
    }
}
