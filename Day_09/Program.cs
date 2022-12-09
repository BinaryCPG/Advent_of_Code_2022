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
            string[] input = File.ReadAllLines("input.txt");

            int t_x = 0, t_y = 0, h_x = 0, h_y = 0, moveAmount;
            char moveDir;
            List<string> visited = new List<string>();
            visited.Add($"{t_x}_{t_y}");
            foreach(string s in input)
            {
                string[] movement = s.Split(new char[] { ' ' });
                moveAmount = int.Parse(movement[1]);
                moveDir = movement[0][0];
                for(int i = 0; i < moveAmount; i++)
                {
                    //Move Head
                    switch (moveDir)
                    {
                        case 'R': h_x++; break;
                        case 'L': h_x = h_x - 1; break;
                        case 'U': h_y++; break;
                        case 'D': h_y = h_y - 1; break;
                    }

                    //Move Tail
                    if(Math.Abs(t_x-h_x)>1 || Math.Abs(t_y-h_y) >1)
                    {
                        //Pure x Movement
                        if(t_y - h_y == 0)
                        {
                            if(t_x < h_x)
                            {
                                t_x++;
                            }
                            else
                            {
                                t_x = t_x - 1;
                            }
                        }
                        //Pure y Movement
                        else if (t_x - h_x==0)
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
                        else if(Math.Abs(t_x - h_x) == 2 && Math.Abs(t_y - h_y) == 1)
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
                        else if(Math.Abs(t_x - h_x) == 1 && Math.Abs(t_y - h_y)== 2)
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

                        if (!visited.Contains($"{t_x}_{t_y}"))
                        {
                            visited.Add($"{t_x}_{t_y}");
                        }
                    }
                }
            }

            Console.WriteLine($"Unique visited locations: {visited.Count}");

            Console.ReadLine();
        }
    }
}
