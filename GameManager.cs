using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tictactoe.State;

    public class GameManager
    {
        private Machine p1;
        private Machine p2;
        private Human p3;
        private Machine currentPlayer;
        private int p1Symbol = 1;
        private int p2Symbol = -1;
        private State currentState;

    public Dictionary<int, Tuple<State, bool>> all_states;
    public GameManager(Machine player1, Machine player2,Human player2_,int type)
        {
            p2 = player2;
            all_states = GameUtility.GetAllStates();
            if (type == 0)
            { 
                p1 = player1;
                p1.SetSymbol(p1Symbol,all_states);
            }
            else 
            {
                p3 = player2_;
                p3.SetSymbol(p1Symbol);
            }
            
            currentPlayer = null;
            p2.SetSymbol(p2Symbol, all_states);
            
            currentState = new State();
        }
        public void Reset()
        {
        if (p1 != null)
        { p1.Reset(); }
        if (p2!=null)
        {
            p2.Reset();
        }   
        }

        public IEnumerable<Machine> Alternate()
        {
            while (true)
            {
                yield return p1;
                yield return p2;
            }
        }
    public IEnumerable<object> Alternate2()
    {
        while (true)
        {
            yield return p3;
            yield return p2;
        }
    }

    public int Play(bool printState = false)
    {
        var alternator = Alternate().GetEnumerator();
        Reset();
        currentState = new State();
        p1.SetState(currentState);
        p2.SetState(currentState);
        if (printState)
        {
            currentState.PrintState();
        }

        while (true)
        {
            currentPlayer = alternator.MoveNext() ? alternator.Current : null;
            if (currentPlayer == null)
            {
                break;
            }
            
            var (i, j, symbol) = currentPlayer.Act();
            var nextHash = currentState.NextState(i, j, symbol).Hash();
            (currentState, var isEnd) = all_states[nextHash];
            p1.SetState(currentState);
            p2.SetState(currentState);
            if (printState)
            {
                currentState.PrintState();
            }

            if (isEnd)
            {
                return currentState.Winner;
            }
        }

        // This point should not be reached
        return 0;

    }


    public int PlayHuman(bool printState = false)
    {
        
        var alternator = Alternate2().GetEnumerator();
        Reset();
        currentState = new State();
        p3.SetState(currentState);
        p2.SetState(currentState);
        if (printState)
        {
            currentState.PrintState();
        }

        while (true)
        {
            var (i, j, symbol) = (-99, -99, -99);
            var c = alternator.MoveNext() ? alternator.Current : null;
            if (c == null)
            {
                break;
            }

            try
            {
                var player = (Machine)c;
                (i, j, symbol) = player.Act();
            }
            catch (InvalidCastException)
            {
                var humanPlayer = (Human)c;
                (i, j, symbol) = humanPlayer.Act();
            }
            var nextHash = currentState.NextState(i, j, symbol).Hash();
            //State s = currentState.NextState(i, j, symbol);
            //int nextHash = s.Hash();
            (currentState, var isEnd) = all_states[nextHash];
            p3.SetState(currentState);
            p2.SetState(currentState);
            if (printState)
            {
                currentState.PrintState();
            }

            if (isEnd)
            {
                return currentState.Winner;
            }
        }

        return 0;
    }
}

