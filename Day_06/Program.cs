using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string buffer = input[0];

            Queue<char> start_buf = new Queue<char>();
            start_buf.Enqueue(buffer[0]);
            start_buf.Enqueue(buffer[1]);
            start_buf.Enqueue(buffer[2]);
            start_buf.Enqueue(buffer[3]);
            int charOverhead_start = 3;
            while(charOverhead_start < buffer.Length-1 && !IsPacketStart(start_buf))
            {
                charOverhead_start++;
                start_buf.Dequeue();
                start_buf.Enqueue(buffer[charOverhead_start]);
            }

            Console.WriteLine($"Chars to process (1): {charOverhead_start+1}");


            Queue<char> message_buf = new Queue<char>();
            for (int i = 0; i < 14; i++)
            {
                message_buf.Enqueue(buffer[i]);
            }
            int charOverhead_message = 13;
            while (charOverhead_message < buffer.Length - 1 && !IsMessageStart(message_buf))
            {
                charOverhead_message++;
                message_buf.Dequeue();
                message_buf.Enqueue(buffer[charOverhead_message]);
            }

            Console.WriteLine($"Chars to process (2): {charOverhead_message + 1}");

            Console.ReadLine();
        }

        static bool IsMessageStart(Queue<char> buf)
        {
            if (buf.Count == 14)
            {
                List<char> tmp_buf = new List<char>(buf);
                tmp_buf.Sort();
                for(int i = 0; i < tmp_buf.Count-1; i++)
                {
                    if(tmp_buf[i] == tmp_buf[i + 1])
                    {
                        return false;
                    }
                }
                return true;
            }
            throw new ArgumentException($"Expected buffer of size 14; instead got {buf.Count} - {buf}");
        }

        static bool IsPacketStart(Queue<char> buf)
        {
            if(buf.Count == 4)
            {
                List<char> tmp_buf = new List<char>(buf);
                tmp_buf.Sort();
                return tmp_buf[0] != tmp_buf[1] && tmp_buf[1] != tmp_buf[2] && tmp_buf[2] != tmp_buf[3];
            }
            throw new ArgumentException($"Expected buffer of size 4; instead got {buf.Count} - {buf}");
        }
    }
}
