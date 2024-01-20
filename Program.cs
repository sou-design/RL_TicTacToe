using System.Drawing;
using Spectre.Console;
using Color = Spectre.Console.Color;
using Raylib_cs;
using tictactoe.State;
using System.Numerics;
using tictactoe.RaylibManager;
using AngleSharp.Html.Dom.Events;
using MouseButton = Raylib_cs.MouseButton;

public class Program
{
    public static Vector2 mousePosition=Vector2.Zero;
    public static State gameState = new State();
    public static RaylibManager raylibManager;
    public static Human human;
    public static Machine machine;
    static void Main(string[] args)
    {   
        raylibManager = RaylibManager.Instance;
        int buttonSize = 200;
        int buttonSpacing = 10;
        Train(100000, 500);
        Compete(1000);

        raylibManager.InitWindow(600, 600, "RL Tic Tac Toe");
        // Define button size and spacing

        bool flag = false;
        var all_states = GameUtility.GetAllStates();
        List<MouseEvent> mouseEvents = new List<MouseEvent>();
        bool roundIsRuning = true; bool is_end = false;
        while (!Raylib.WindowShouldClose())
        {
            all_states = GameUtility.GetAllStates();
            flag = false;
            // Draw other game elements

            human = new Human(raylibManager);
            machine = new Machine(0);
            human.SetSymbol(1);
            machine.SetSymbol(-1,all_states); 
            var ispressed = false;           
            machine.LoadPolicy();


            Console.WriteLine("clicked");
            var alternator = Alternate2().GetEnumerator();
            if (machine != null)
            {
                machine.Reset();
            }
            State currentState = new State();
            human.SetState(currentState);
            machine.SetState(currentState);
            raylibManager.BeginDrawing();
            raylibManager.resetDraws();
            raylibManager.ClearBackground(Raylib_cs.Color.RAYWHITE);
            raylibManager.DrawBoard(buttonSize, buttonSpacing, mousePosition);
            
            raylibManager.EndDrawing();
            is_end = false;
            roundIsRuning = true;
            while (roundIsRuning)
            {               
                raylibManager.BeginDrawing();
                raylibManager.ClearBackground(Raylib_cs.Color.RAYWHITE);
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) && !flag)
                { 
                    flag = true;
                }
                if (flag)
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
                        int xPos = j * (buttonSize + buttonSpacing);
                        int yPos = i * (buttonSize + buttonSpacing);
                        var text = "O";
                        raylibManager.draws[i, j] = "O";
                        Console.WriteLine("O");
                        symbol = -1;
                        flag = false;
                    }
                    catch (InvalidCastException)
                    {                       
                        var mouse = raylibManager.MousePosition;
                        var humanPlayer = (Human)c;
                        j = (int)(mouse.X / (600 / 3));
                        i = (int)(mouse.Y / (600 / 3));
                        raylibManager.draws[i, j] = "X";
                        symbol = 1;
                        int xPos = j * (buttonSize + buttonSpacing);
                        int yPos = i * (buttonSize + buttonSpacing);
                        Console.WriteLine("X");
                    }
                        var nextHash = currentState.NextState(i, j, symbol).Hash();
                        (currentState, is_end) = all_states[nextHash];
                        human.SetState(currentState);
                        machine.SetState(currentState);

                    if (is_end)
                    {
                        raylibManager.EndDrawing();
                        roundIsRuning = false;
                        Console.WriteLine("end");
                        if (currentState.Winner == machine.symbol)
                        {
                            Console.WriteLine("You lose!");
                        }
                        else if (currentState.Winner == human.symbol)
                        {
                            Console.WriteLine("You win!"); 
                        }
                        else
                        {
                            Console.WriteLine("It is a tie!"); 
                        }
                    }
            
                }
                raylibManager.DrawBoard(buttonSize, buttonSpacing, mousePosition);
                raylibManager.EndDrawing();
            }
        }   
    }
        static void Train(int epochs, int printEveryN = 500)
        {
            Machine player1 = new Machine(0.01);
            Machine player2 = new Machine(0.01);
            GameManager judger = new GameManager(player1, player2, null, 0, raylibManager);
            double player1Win = 0.0;
            double player2Win = 0.0;

            for (int i = 1; i <= epochs; i++)
            {
                int winner = judger.Play(false);

                if (winner == 1)
                    player1Win += 1;
                if (winner == -1)
                    player2Win += 1;

                if (i % printEveryN == 0)
                {
                    Console.WriteLine($"Epoch {i}, player 1 winrate: {player1Win / i:F2}, player 2 winrate: {player2Win / i:F2}");
                }

                player1.Backup();
                player2.Backup();
                judger.Reset();
            }

            player1.SavePolicy();
            player2.SavePolicy();
        }
        static void Compete(int turns)
        {
            Machine player1 = new Machine(0);
            Machine player2 = new Machine(0);
            GameManager judger = new GameManager(player1, player2, null, 0, raylibManager);
            player1.LoadPolicy();
            player2.LoadPolicy();
            double player1Win = 0.0;
            double player2Win = 0.0;

            for (int i = 0; i < turns; i++)
            {
                int winner = judger.Play();

                if (winner == 1)
                    player1Win += 1;
                if (winner == -1)
                    player2Win += 1;

                judger.Reset();
            }

            Console.WriteLine($"{turns} turns, player 1 win {player1Win / turns:F2}, player 2 win {player2Win / turns:F2}");
        }
        public static IEnumerable<object> Alternate2()
        {
            while (true)
            {
                yield return human;
                yield return machine;
            }
        }
    }


