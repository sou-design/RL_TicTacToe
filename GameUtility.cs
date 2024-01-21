using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tictactoe.State;

static class GameUtility
{
    /// <summary>
    /// Recursively generates all possible game states.
    /// </summary>
    static void GetStates(State currentState, int currentSymbol, Dictionary<int, Tuple<State, bool>> allStates)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (currentState.Data[i, j] == 0)
                {
                    State newState = currentState.NextState(i, j, currentSymbol);
                    int newHash = newState.Hash();

                    if (!allStates.ContainsKey(newHash))
                    {
                        bool isEnd = newState.IsEnd();
                        allStates[newHash] = new Tuple<State, bool>(newState, isEnd);

                        if (!isEnd)
                        {
                            GetStates(newState, -currentSymbol, allStates);
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// Generates all possible game states and stores them in a dictionary.
    /// </summary>
    /// <returns>A dictionary containing all possible game states.</returns>
    public static Dictionary<int, Tuple<State, bool>> GetAllStates()
    {
        int currentSymbol = 1;
        State currentState = new State();
        var allStates = new Dictionary<int, Tuple<State, bool>>();
        allStates[currentState.Hash()] = new Tuple<State, bool>(currentState, currentState.IsEnd());
        GetStates(currentState, currentSymbol, allStates);
        return allStates;
    }

    private static Random random = new Random();
    /// <summary>
    /// Shuffles the elements of a list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}