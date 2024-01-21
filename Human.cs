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
/// <summary>
/// Represents a human player in the Tic Tac Toe game.
/// </summary>
public class Human
{
    public int? symbol;
    public readonly List<char> keys;
    public State state;
    RaylibManager raylibManager;
    public Human(RaylibManager raylibManager_)
    {
        symbol = null;
        state = null;
        raylibManager = raylibManager_;    
    }
    /// <summary>
    /// Sets the current state of the game for the human player.
    /// </summary>
    public void SetState(State state)
    {
        this.state = state;
    }
    /// <summary>
    /// Sets the symbol of the human player.
    /// </summary>
    public void SetSymbol(int symbol)
    {
        this.symbol = symbol;
    }
    public static implicit operator T(Human v)
    {
        throw new NotImplementedException();
    }
}
