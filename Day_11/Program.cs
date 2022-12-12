using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_11
{
    class Program
    {
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
            }

            for(int round = 1; round <= 20; round++)
            {
                foreach(Monkey m in monkeys)
                {
                    foreach (Tuple<int, Int64> item in m.InspectAllItems())
                    {
                        monkeys[item.Item1].Items.Enqueue(item.Item2);
                    }
                }

                Console.WriteLine($"Round {round} result:");
                for(int i = 0; i < monkeys.Count; i++)
                {
                    Console.Write($"Monkey {i}: ");
                    foreach(var item in monkeys[i].Items.ToList())
                    {
                        Console.Write($"{item}, ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            List<Monkey> monkeys_sorted = monkeys.OrderByDescending(x => x.ItemsInspected).ToList();

            Console.WriteLine($"Monkey business: {(monkeys_sorted[0].ItemsInspected * monkeys_sorted[1].ItemsInspected)}");

            Console.ReadLine();
        }
    }

    class Monkey
    {
        public Queue<Int64> Items;
        MonkeyOperation Operation;
        MonkeyTest Test;
        public int ItemsInspected;

        public Monkey(IEnumerable<Int64> items, MonkeyOperation mo, MonkeyTest mt)
        {
            Items = new Queue<Int64>(items);
            Operation = mo;
            Test = mt;
            ItemsInspected = 0;
        }

        public List<Tuple<int, Int64>> InspectAllItems()
        {
            List<Tuple<int, Int64>> _return = new List<Tuple<int, Int64>>();
            Int64 _currentItem;
            
            while(Items.Count > 0)
            {
                //Get Item
                _currentItem = Items.Dequeue();
                //Perform Operation
                _currentItem = Operation.Resolve(_currentItem);
                //Reduce worry
                _currentItem = _currentItem / 3;
                //Asses target and return
                _return.Add(new Tuple<int, Int64>(Test.Resolve(_currentItem), _currentItem));
                ItemsInspected++;
            }
            return _return;
        }

        public static Monkey FromDescription(string[] description)
        {
            List<Int64> items = new List<Int64>();
            string[] itemList = description[1].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string i in itemList)
            {
                items.Add(Int64.Parse(i));
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
        int? ValueA;
        MonkeyOperand Operand;
        int? ValueB;

        public MonkeyOperation(int? a, MonkeyOperand o, int? b)
        {
            ValueA = a;
            Operand = o;
            ValueB = b;
        }

        public Int64 Resolve(Int64 value)
        {
            Int64 a = ValueA.HasValue ? (Int64)ValueA.Value : value;
            Int64 b = ValueB.HasValue ? (Int64)ValueB.Value : value;
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
        int TestDivisor;
        int TrueTarget;
        int FalseTarget;

        public MonkeyTest(int d, int t, int f)
        {
            TestDivisor = d;
            TrueTarget = t;
            FalseTarget = f;
        }

        public int Resolve(Int64 value)
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
