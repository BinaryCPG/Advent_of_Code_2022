using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace Day_11
{
    class Program
    {
        public static bool PART1 = true;
        //https://www.reddit.com/r/adventofcode/comments/zizi43/comment/iztt8mx/?utm_source=share&utm_medium=web2x&context=3
        public static BigInteger cycleLength = 1;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            
            List<Monkey> monkeys = new List<Monkey>();
            for(int i = 0; i < input.Length; i += 7)
            {
                monkeys.Add(Monkey.FromDescription(
                    new string[] {
                        input[i + 0],
                        input[i + 1],
                        input[i + 2],
                        input[i + 3],
                        input[i + 4],
                        input[i + 5]
                    }
                    ));

                //https://www.reddit.com/r/adventofcode/comments/zizi43/comment/iztt8mx/?utm_source=share&utm_medium=web2x&context=3
                cycleLength *= monkeys.Last().Test.TestDivisor;
            }

            PART1 = false;

            for(int round = 1; round <= (PART1 ? 20 : 10000); round++)
            {
                foreach(Monkey m in monkeys)
                {
                    
                    foreach (Tuple<int, BigInteger> item in m.InspectAllItems())
                    {
                        monkeys[item.Item1].Items.Enqueue(item.Item2);
                    }
                }

                if (PART1)
                {
                    Console.WriteLine($"Round {round} result:");
                    for (int i = 0; i < monkeys.Count; i++)
                    {
                        Console.Write($"Monkey {i}: ");
                        foreach (var item in monkeys[i].Items.ToList())
                        {
                            Console.Write($"{item}, ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"\rRound {round}/10000     ");
                }
            }

            Console.WriteLine();

            List <Monkey> monkeys_sorted = monkeys.OrderByDescending(x => x.ItemsInspected).ToList();

            Console.WriteLine($"Monkey business: {(monkeys_sorted[0].ItemsInspected * monkeys_sorted[1].ItemsInspected)}");

            Console.ReadLine();
        }
    }

    class Monkey
    {
        public Queue<BigInteger> Items;
        public MonkeyOperation Operation;
        public MonkeyTest Test;
        public BigInteger ItemsInspected;

        public Monkey(IEnumerable<BigInteger> items, MonkeyOperation mo, MonkeyTest mt)
        {
            Items = new Queue<BigInteger>(items);
            Operation = mo;
            Test = mt;
            ItemsInspected = 0;
        }

        public List<Tuple<int, BigInteger>> InspectAllItems()
        {
            List<Tuple<int, BigInteger>> _return = new List<Tuple<int, BigInteger>>();
            BigInteger _currentItem;
            
            while(Items.Count > 0)
            {
                //Get Item
                _currentItem = Items.Dequeue();
                //Perform Operation
                _currentItem = Operation.Resolve(_currentItem);
                if (Program.PART1)
                {
                    //Reduce worry
                    _currentItem = _currentItem / 3;
                }
                else
                {
                    //https://www.reddit.com/r/adventofcode/comments/zizi43/comment/iztt8mx/?utm_source=share&utm_medium=web2x&context=3
                    /*
                    while (_currentItem > Program.cycleLength)
                    {
                        _currentItem -= Program.cycleLength;
                    }
                    */
                    _currentItem = _currentItem % Program.cycleLength;
                }
                //Asses target and return
                _return.Add(new Tuple<int, BigInteger>(Test.Resolve(_currentItem), _currentItem));
                ItemsInspected++;
            }
            return _return;
        }

        public static Monkey FromDescription(string[] description)
        {
            List<BigInteger> items = new List<BigInteger>();
            string[] itemList = description[1].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in itemList)
            {
                items.Add(BigInteger.Parse(i));
            }

            return new Monkey(
                items,
                MonkeyOperation.FromString(description[2]),
                MonkeyTest.FromString(description[3], description[4], description[5])
            );
        }
    }

    enum MonkeyOperand
    {
        PLUS,MULTIPL
    }

    class MonkeyOperation
    {
        public int? ValueA;
        public MonkeyOperand Operand;
        public int? ValueB;

        public MonkeyOperation(int? a, MonkeyOperand o, int? b)
        {
            ValueA = a;
            Operand = o;
            ValueB = b;
        }

        public BigInteger Resolve(BigInteger value)
        {
            BigInteger a = ValueA.HasValue ? (BigInteger)ValueA.Value : value;
            BigInteger b = ValueB.HasValue ? (BigInteger)ValueB.Value : value;
            switch (Operand)
            {
                case (MonkeyOperand.PLUS): return a + b;
                case (MonkeyOperand.MULTIPL): return a * b;
            }
            return value;
        }

        public static MonkeyOperation FromString(string operationLine)
        {
            string[] line = operationLine.Split(new char[] { ' ' });
            string a, o, b;
            b = line[line.Length - 1];
            o = line[line.Length - 2];
            a = line[line.Length - 3];
            return new MonkeyOperation(
                a == "old" ? (int?)null : (int?)int.Parse(a),
                o[0]=='+' ? MonkeyOperand.PLUS : MonkeyOperand.MULTIPL,
                b == "old" ? (int?)null : (int?)int.Parse(b)
            );
        }
    }

    class MonkeyTest
    {
        public int TestDivisor;
        public int TrueTarget;
        public int FalseTarget;

        public MonkeyTest(int d, int t, int f)
        {
            TestDivisor = d;
            TrueTarget = t;
            FalseTarget = f;
        }

        public int Resolve(BigInteger value)
        {
            return (value % TestDivisor) == 0 ? TrueTarget : FalseTarget;
        }

        public static MonkeyTest FromString(string d, string t, string f)
        {
            return new MonkeyTest(
                int.Parse(d.Split(new char[] { ' ' }).Last()),
                int.Parse(t.Split(new char[] { ' ' }).Last()),
                int.Parse(f.Split(new char[] { ' ' }).Last())
            );
        }
    }
}
