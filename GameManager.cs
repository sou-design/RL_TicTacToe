using Raylib_cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tictactoe.RaylibManager;
using tictactoe.State;
/// <summary>
/// Manages the Tic Tac Toe game.
/// </summary>
public class GameManager
    {
        private Machine p1;
        private Machine p2;
        private Human p3;
        private Machine currentPlayer;
        private int p1Symbol = 1;
        private int p2Symbol = -1;
        private State currentState;
        RaylibManager raylibManager;
        public Dictionary<int, Tuple<State, bool>> all_states;
        public GameManager(Machine player1, Machine player2,Human player2_,int type,RaylibManager raylibManager_)
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
            raylibManager = raylibManager_;
            currentState = new State();
        }
        /// <summary>
        /// Resets the game by resetting player states.
        /// </summary>
        public void Reset()
        {
            if (p1 != null)
            { 
                p1.Reset(); 
            }
            if (p2!=null)
            {
                p2.Reset();
            }   
        }
        /// <summary>
        /// Alternates between players indefinitely.
        /// </summary>
        public IEnumerable<Machine> Alternate()
        {
            while (true)
            {
                yield return p1;
                yield return p2;
            }
        }
        /// <summary>
        /// Plays the Tic Tac Toe game by coordinating player moves and checking for a winner.
        /// </summary>
        public int Play(bool printState = false)
        {
            var alternator = Alternate().GetEnumerator();
            Reset();
            currentState = new State();
            p1.SetState(currentState);
            p2.SetState(currentState);
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
                if (isEnd)
                {
                    return currentState.Winner;
                }
            }
            return 0;
        }
}

