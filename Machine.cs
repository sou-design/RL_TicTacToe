using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using tictactoe.State;

public class Machine
{
    public Dictionary<int, double> estimations;
    public double stepSize;
    public double epsilon;
    public List<State> states;
    public List<bool> greedy;
    public int symbol;

    public Machine(double stepSize = 0.1, double epsilon = 0.1)
    {
        estimations = new Dictionary<int, double>();
        this.stepSize = stepSize;
        this.epsilon = epsilon;
        states = new List<State>();
        greedy = new List<bool>();
        symbol = 0;
    }
    public void Reset()
    {
        states.Clear();
        greedy.Clear();
    }
    public void SetState(State state)
    {
        states.Add(state);
        greedy.Add(true);
    }
    public void SetSymbol(int symbol, Dictionary<int, Tuple<State, bool>> all_states)
    {
        this.symbol = symbol;
        
        foreach (var hashVal in all_states.Keys)
        {
            var (state, isEnd) = all_states[hashVal];
            if (isEnd)
            {
                if (state.Winner == this.symbol)
                {
                    estimations[hashVal] = 1.0;
                }
                else if (state.Winner == 0)
                {
                    estimations[hashVal] = 0.5;
                }
                else
                {
                    estimations[hashVal] = 0;
                }
            }
            else
            {
                estimations[hashVal] = 0.5;
            }
        }
    }
    public void Backup()
    {
        var states = this.states.ConvertAll(state => state.Hash());

        for (int i = states.Count - 2; i >= 0; i--)
        {
            var state = states[i];
            var tdError = greedy[i] ? (estimations[states[i + 1]] - estimations[state]) : 0.0;
            estimations[state] += stepSize * tdError;
        }
    }
    public (int i, int j, int symbol) Act()
    {
        var state = states[states.Count - 1];
        var nextStates = new List<int>();
        var nextPositions = new List<List<int>>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (state.Data[i, j] == 0)
                {
                    nextPositions.Add(new List<int> { i, j });
                    nextStates.Add(state.NextState(i, j, symbol).Hash());
                }
            }
        }

        if (new Random().NextDouble() < epsilon)
        {
            var randomIndex = new Random().Next(nextPositions.Count);
            var action = nextPositions[randomIndex];
            action.Add(symbol);
            greedy[greedy.Count - 1] = false;
            return (action[0], action[1], action[2]); // Return a tuple
        }   

        var values = new List<(double, List<int>)>();
        foreach (var (hash_val, pos) in nextStates.Zip(nextPositions, (a, b) => (a, b)))
        {           
           values.Add((estimations[hash_val], pos));       
        }
        GameUtility.Shuffle(values); // Shuffle the list randomly

        values.Sort((x, y) => y.Item1.CompareTo(x.Item1));

        // Sort in descending order
        var chosenAction = values[0].Item2;
        if (values.Count > 0)
        {            
            chosenAction.Add(symbol); 
        }
        else
        {
            // Handle the case when values is empty, e.g., choose a default action
        }
        
        return (chosenAction[0], chosenAction[1], chosenAction[2]); // Return a tuple
    }
    public void SavePolicy()
    {
        var fileName = string.Format("policy_{0}.json", symbol == 1 ? "first" : "second");
        var jsonString = JsonSerializer.Serialize(estimations);
        File.WriteAllText(fileName, jsonString);
    }
    public void LoadPolicy()
    {
        var fileName = string.Format("policy_{0}.json", symbol == 1 ? "first" : "second");
        if (File.Exists(fileName))
        {
            var jsonString = File.ReadAllText(fileName);
            estimations = JsonSerializer.Deserialize<Dictionary<int, double>>(jsonString);
        }
    }
    public static implicit operator T(Machine v)
    {
        throw new NotImplementedException();
    }
}
