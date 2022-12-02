using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day_02
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            1. col enemy - A for Rock, B for Paper, and C for Scissors
            2. col own   - X for Rock, Y for Paper, and Z for Scissors

            The score for a single round is the score for the shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors) 
            plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won)
             */

            Dictionary<char, int> own_choice = new Dictionary<char, int>() { {'X', 1 }, {'Y', 2}, {'Z', 3} };

            string[] strat = File.ReadAllLines("input.txt");
            int score = 0;
            char own;
            char enemy;

            foreach(string round in strat)
            {
                enemy = round[0];
                own = round[2];

                score += own_choice[own];
                score += RPC_WinLoss_Choice(enemy, own);
            }

            Console.WriteLine($"Score (1): {score}");

            /*X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win. Good luck!"*/
            Dictionary<char, int> own_result = new Dictionary<char, int>() { { 'X', 0 }, { 'Y', 3 }, { 'Z', 6 } };
            score = 0;
            foreach (string round in strat)
            {
                enemy = round[0];
                own = round[2];

                score += own_result[own];
                score += own_choice[RPC_WinLoss_ResultMove(enemy, own)];
            }

            Console.WriteLine($"Score (2): {score}");
            Console.ReadLine();
        }

        static int RPC_WinLoss_Choice(char en, char ow)
        {
            if ((en=='A'&&ow=='X')|| (en == 'B' && ow == 'Y') || (en == 'C' && ow == 'Z')) return 3;
            if ((en=='A'&&ow=='Y') || (en == 'B' && ow == 'Z') || (en == 'C' && ow == 'X') ) return 6;
            return 0;
        }

        static char RPC_WinLoss_ResultMove(char en, char ow)
        {
            /*2. col own   - X for Rock, Y for Paper, and Z for Scissors*/
            switch (ow)
            {
                case 'X'/*lose*/:
                    switch (en)
                    {
                        case 'A'/*rock*/: return 'Z';
                        case 'B'/*paper*/: return 'X';
                        case 'C'/*scissors*/: return 'Y';
                    }
                    break;
                case 'Y'/*draw*/:
                    switch (en)
                    {
                        case 'A'/*rock*/: return 'X';
                        case 'B'/*paper*/: return 'Y';
                        case 'C'/*scissors*/: return 'Z';
                    }
                    break;
                case 'Z'/*win*/:
                    switch (en)
                    {
                        case 'A'/*rock*/: return 'Y';
                        case 'B'/*paper*/: return 'Z';
                        case 'C'/*scissors*/: return 'X';
                    }
                    break;
            }
            return 'Y';
        }
    }
}
