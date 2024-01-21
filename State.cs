using NPOI.SS.Formula.Functions;
using Raylib_cs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe.State
{
    /// <summary>
    /// State : Represents the state of a Tic Tac Toe game.
    /// Data : gets or sets the 3x3 grid of the Tic Tac Toe game.
    /// Winner : Gets or sets the winner of the game (1 for 'X', -1 for 'O', 0 for a tie, initially set to 0).
    /// HashVal : Gets or sets the hash value of the current state.
    /// End : Gets or sets a value indicating whether the game has ended.
    /// </summary>
    public class State
    {
        public int[,] Data { get; set; }
        public int Winner { get; set; }
        public object HashVal { get; set; }
        public bool End { get; set; }
        
        public State()
        {
            Data = new int[3, 3];

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
        /// <summary>
        /// Computes a hash value for the current state based on the contents of Data.
        /// </summary>
        /// <returns>The computed hash value.</returns>
        public int Hash()
        {
            if (HashVal is null)
            {
                HashVal = 0;
                foreach (var item in Data)
                {
                    HashVal = (int)HashVal * 3 + item + 1;
                }
            }

            return (int)HashVal;
        }

        /// <summary>
        /// Checks if the game has ended by examining rows, columns, and diagonals for a winner or a tie.
        /// </summary>
        /// <returns>True if the game has ended; else returns false.</returns>
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
        /// <summary>
        /// Generates the next state by copying the current state and updating the specified position with the given symbol.
        /// </summary>
        /// <param name="i">The row index.</param>
        /// <param name="j">The column index.</param>
        /// <param name="symbol">The symbol to update the position with (1 for 'X', -1 for 'O').</param>
        /// <returns>The next state.</returns>
        public State NextState(int i, int j, int symbol)
        {
            State newState = new State();
            Array.Copy(Data, newState.Data, Data.Length); 
            newState.Data[i, j] = symbol;
            return newState;
        }
        public void PrintState()
        {
            const int cellSize = 200;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string outStr = "";

                    if (Data[i, j] == 1)
                    {
                        outStr = "X";
                    }
                    else if (Data[i, j] == -1)
                    {
                        outStr = "O";
                    }
                    else
                    {
                        outStr = " ";
                    }
                    int xPos = j * cellSize + cellSize / 4;
                    int yPos = i * cellSize + cellSize / 2;
                    Raylib.DrawText(outStr, xPos, yPos, 48, Raylib_cs.Color.BLACK);
                }
            }
        }
    } 
}
