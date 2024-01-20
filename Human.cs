using NPOI.POIFS.Crypt;
using NPOI.SS.Formula.Functions;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using tictactoe.RaylibManager;
using tictactoe.State;

public class Human
{
    public int? symbol;
    public readonly List<char> keys;
    public State state;
    RaylibManager raylibManager;
    public Human(RaylibManager raylibManager_)
    {
        symbol = null;
        keys = new List<char> { 'q', 'w', 'e', 'a', 's', 'd', 'z', 'x', 'c' };
        state = null;
        raylibManager = raylibManager_;    
    }
    public void Reset()
    {
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
    public void GetKeyFromButton(float posX, float posY)
    {
        var i = (int)(posY / (Raylib.GetScreenHeight() / 3));
        var j = (int)(posX / (Raylib.GetScreenWidth() % 3));
    }
    public State GetState()
    {
        return state;
    }
    public State GetSymbol()
    {
        return state;
    }
    public static implicit operator T(Human v)
    {
        throw new NotImplementedException();
    }
}
