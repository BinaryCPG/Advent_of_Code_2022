using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string[] inst;
            char[,] screen = new char[6, 40];
            List<int> signal = new List<int>();
            int X = 1, cycle = 1, val;
            foreach(var instline in input)
            {
                inst = instline.Split(new char[] { ' ' });
                if(inst[0] == "noop")
                {
                    signal = CheckSignal(signal, cycle, X);
                    screen = DrawPixel(screen, cycle, X);
                    cycle++;
                }
                else if (inst[0] == "addx")
                {
                    signal = CheckSignal(signal, cycle, X);
                    screen = DrawPixel(screen, cycle, X);
                    cycle++;
                    signal = CheckSignal(signal, cycle, X);
                    screen = DrawPixel(screen, cycle, X);
                    cycle++;
                    val = int.Parse(inst[1]);
                    X += val;
                }
            }

            int signalsum = 0;
            foreach(int se in signal)
            {
                signalsum += se;
            }
            Console.WriteLine($"Signal: {signalsum}");
            Console.WriteLine("Screen:");
            for(int r = 0; r < screen.GetLength(0); r++)
            {
                for(int c = 0; c < screen.GetLength(1); c++)
                {
                    Console.Write(screen[r,c]);
                }
                Console.Write("\n");
            }

            Console.ReadLine();
        }

        static char[,] DrawPixel(char[,] screen, int cycle, int X)
        {

            int col = ((cycle-1) % 40);
            int row = (int)((cycle-1) / 40);

            if( col == X-1 || col == X || col == X+1)
            {
                screen[row, col] = '#';
            }
            else{
                screen[row, col] = '.';
            }

            return screen;
        }

        static List<int> CheckSignal(List<int> signal, int cycle, int X)
        {
            if ((cycle - 20) % 40 == 0)
            {
                signal.Add(cycle * X);
            }
            return signal;
        }
    }
}
