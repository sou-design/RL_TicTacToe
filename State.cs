using NPOI.SS.Formula.Functions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe.State
{
    public class State
    {

        public int[,] Data { get; set; }
        public int Winner { get; set; }
        public object HashVal { get; set; }
        public bool End { get; set; }

        public State()
        {
            Data = new int[3, 3];

            // Initialize each element with zero
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Data[i, j] = 0;
                }
            }
            Winner = 0;
            HashVal = null;
            End = false;
        }

        public int Hash()
        {
            if (HashVal is null)
            {
                //HashVal = (int)HashVal;
                HashVal = 0;
                // Assuming Data is a 2D array of integers
                foreach (var item in Data)
                {
                    HashVal = (int)HashVal * 3 + item + 1;
                }
            }

            return (int)HashVal;
        }


        public bool IsEnd()
        {
            if (End)
            {
                return true;
            }

            var results = new List<int>();

            // check row
            for (int i = 0; i < 3; i++)
            {
                int rowSum = 0;
                for (int j = 0; j < 3; j++)
                {
                    rowSum += Data[i, j];
                }
                results.Add(rowSum);
            }

           
            for (int i = 0; i < 3; i++)
            {
                int colSum = 0;
                for (int j = 0; j <3; j++)
                {
                    colSum += Data[j, i];
                }
                results.Add(colSum);
            }

           
            int trace = 0;
            int reverseTrace = 0;
            for (int i = 0; i < 3; i++)
            {
                trace += Data[i, i];
                reverseTrace += Data[i, 3 - 1 - i];
            }
            results.Add(trace);
            results.Add(reverseTrace);

            foreach (var result in results)
            {
                if (result == 3)
                {
                    Winner = 1;
                    End = true;
                    return true;
                }
                if (result == -3)
                {
                    Winner = -1;
                    End = true;
                    return true;
                }
            }

   
            int sumValues = 0;
            foreach (var value in Data)
            {
                sumValues += Math.Abs(value);
            }

            if (sumValues == 9)
            {
                Winner = 0;
                End = true;
                return true;
            }
            End = false;
            return false;
        }

        public State NextState(int i, int j, int symbol)
        {
            State newState = new State();
            Array.Copy(Data, newState.Data, Data.Length); 
            //newState.Data = (int[,])Data.Clone();
            newState.Data[i, j] = symbol;
            return newState;
        }

        public void PrintState()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-------------");
                string outStr = "| ";
                for (int j = 0; j < 3; j++)
                {
                    char token;
                    if (Data[i, j] == 1)
                        token = '*';
                    else if (Data[i, j] == -1)
                        token = 'x';
                    else
                        token = '0';
                    outStr += token + " | ";
                }
                Console.WriteLine(outStr);
            }
            Console.WriteLine("-------------");
        }
    }
    
}
