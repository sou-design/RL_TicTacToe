using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tictactoe.State;

public class Human
{
    public int? symbol;
    public readonly List<char> keys;
    public State state;

    public Human()
    {
        symbol = null;
        keys = new List<char> { 'q', 'w', 'e', 'a', 's', 'd', 'z', 'x', 'c' };
        state = null;
    }

    public void Reset()
    {
        // No reset needed for HumanPlayer
    }

    public void SetState(State state)
    {
        this.state = state;
    }

    public void SetSymbol(int symbol)
    {
        this.symbol = symbol;
    }

    public (int, int, int) Act()
    {
        state.PrintState();
        Console.Write("Input your position:");
        var key = Console.ReadKey().KeyChar;
        Console.WriteLine();

        if (keys.Contains(key))
        {
            var data = keys.IndexOf(key);
            var i = data / 3;
            var j = data % 3;
            return (i, j, symbol.Value);
        }
        else
        {
            Console.WriteLine("Invalid input. Please use the keys: q, w, e, a, s, d, z, x, c.");
            return Act();
        }
    }

    public static implicit operator T(Human v)
    {
        throw new NotImplementedException();
    }
}
