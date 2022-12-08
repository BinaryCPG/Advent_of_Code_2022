using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_08
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int[,] forest = new int[input.Length, input[0].Length];
            bool[,] treeVis = new bool[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[0].Length; j++)
                {
                    forest[i, j] = int.Parse($"{input[i][j]}");
                    if(i == 0 || j==0 || i==input.Length-1 || j == input[0].Length - 1)
                    {
                        treeVis[i, j] = true;
                    }
                }
            }

            bool visLeft, visRight, visUp, visDown;
            for (int i = 1; i < input.Length-1; i++)
            {
                for (int j = 1; j < input[0].Length-1; j++)
                {
                    //Reset
                    visLeft = true;
                    visRight = true;
                    visUp = true;
                    visDown = true;

                    //Row
                    for(int _i = 0; _i < i; _i++)
                    {
                        if(forest[_i,j] >= forest[i, j])
                        {
                            visLeft = false;
                            break;
                        }
                    }
                    for (int _i = i+1; _i < input.Length; _i++)
                    {
                        if (forest[_i, j] >= forest[i, j])
                        {
                            visRight = false;
                            break;
                        }
                    }

                    //Column
                    for (int _j = 0; _j < j; _j++)
                    {
                        if (forest[i, _j] >= forest[i, j])
                        {
                            visUp = false;
                            break;
                        }
                    }
                    for (int _j = j+1; _j < input[0].Length; _j++)
                    {
                        if (forest[i, _j] >= forest[i, j])
                        {
                            visDown = false;
                            break;
                        }
                    }

                    treeVis[i, j] = visLeft || visRight || visUp || visDown;
                }
            }

            int sum = 0;
            for (int i = 0; i < treeVis.GetLength(0); i++)
            {
                for (int j = 0; j < treeVis.GetLength(1); j++)
                {
                    sum += treeVis[i, j] ? 1 : 0;
                }
            }
            Console.WriteLine($"Trees visible: {sum}");

            int[,] treeScore = new int[input.Length, input[0].Length];
            int scoreLeft, scoreRight, scoreUp, scoreDown;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    //Reset
                    scoreLeft = 0;
                    scoreRight = 0; 
                    scoreUp = 0; 
                    scoreDown = 0;

                    //Row
                    if (i == 0)
                    {
                        scoreLeft = 0;
                    }
                    else
                    {
                        for (int _i = i - 1; _i >= 0; _i = _i - 1)
                        {
                            scoreLeft++;
                            if (forest[_i, j] >= forest[i, j])
                            {
                                break;
                            }
                        }
                    }

                    if(i == input.Length - 1)
                    {
                        scoreRight = 0;
                    }
                    else
                    {
                        for (int _i = i + 1; _i < input.Length; _i++)
                        {
                            scoreRight++;
                            if (forest[_i, j] >= forest[i, j])
                            {
                                break;
                            }
                        }
                    }

                    //Column
                    if(j == 0)
                    {
                        scoreUp = 0;
                    }
                    else
                    {
                        for (int _j = j - 1; _j >= 0; _j = _j - 1)
                        {
                            scoreUp++;
                            if (forest[i, _j] >= forest[i, j])
                            {
                                break;
                            }
                        }
                    }
                    if(j == input[0].Length - 1)
                    {
                        scoreDown = 0;
                    }
                    else
                    {
                        for (int _j = j + 1; _j < input[0].Length; _j++)
                        {
                            scoreDown++;
                            if (forest[i, _j] >= forest[i, j])
                            {
                                break;
                            }
                        }
                    }

                    treeScore[i, j] = scoreLeft * scoreRight * scoreUp * scoreDown;
                }
            }

            int maxScore = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    maxScore = treeScore[i, j] > maxScore ? treeScore[i, j] : maxScore;
                }
            }
            Console.WriteLine($"Max score: {maxScore}");

            Console.ReadLine();
        }
    }
}
