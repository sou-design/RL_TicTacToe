using System.Drawing;
using Spectre.Console;
using Color = Spectre.Console.Color;
using Raylib_cs;
using tictactoe.State;
using System.Numerics;
public class Program
{
        static void Main(string[] args)
        {

            var all_states = GameUtility.GetAllStates();
            Console.WriteLine(all_states.Count);
            Train(100000, 500);
            Compete(1000);
            const int screenWidth = 600;
            const int screenHeight = 600;

            Raylib.InitWindow(screenWidth, screenHeight, "RL Tic Tac Toe");
        // Define button size and spacing
            int buttonSize = 200;
            int buttonSpacing = 10;

            while (!Raylib.WindowShouldClose())
            {
                // Detect mouse position
                Vector2 mousePosition = Raylib.GetMousePosition();

                // Draw the tic-tac-toe board with buttons
                DrawBoard(buttonSize, buttonSpacing, mousePosition);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib_cs.Color.RAYWHITE);

                // Draw other game elements

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
            }
    static void DrawBoard(int buttonSize, int buttonSpacing, Vector2 mousePosition)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                // Calculate button position
                int xPos = col * (buttonSize + buttonSpacing);
                int yPos = row * (buttonSize + buttonSpacing);

                // Check if the mouse is over the button
                bool isMouseOver = Raylib.CheckCollisionPointRec(mousePosition, new Raylib_cs.Rectangle(xPos, yPos, buttonSize, buttonSize));

                // Draw the button
                Raylib.DrawRectangle(xPos, yPos, buttonSize, buttonSize, isMouseOver ? Raylib_cs.Color.GREEN : Raylib_cs.Color.LIGHTGRAY);

                // Draw 'X' or 'O' on the button (modify this based on your game logic)
                string buttonText = isMouseOver ? "Hovered" : "Button";
                Raylib.DrawText(buttonText, xPos + buttonSize / 4, yPos + buttonSize / 3, 20, Raylib_cs.Color.BLACK);
            }
        }
    }
    static void DrawX(int posX, int posY, Vector2 mousePosition)
    {
        bool isMouseOver = Raylib.CheckCollisionPointRec(mousePosition, new Raylib_cs.Rectangle(posX, posY, 200, 200));
        string buttonText = "X";
        Raylib.DrawText(buttonText, posX + 200 / 4, posY + 200 / 3, 20, Raylib_cs.Color.BLACK);
    }
    static void DrawO(int posX, int posY, Vector2 mousePosition)
    {
        
        string buttonText = "O";
        Raylib.DrawText(buttonText, posX + 200 / 4, posY + 200 / 3, 20, Raylib_cs.Color.BLACK);
    }
    static void Train(int epochs, int printEveryN = 500)
        {
            Machine player1 = new Machine(0.01);
            Machine player2 = new Machine(0.01);
            GameManager judger = new GameManager(player1, player2,null,0);
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
            GameManager judger = new GameManager(player1, player2,null,0);
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

        static void Play()
        {
            while (true)
            {
                Human player1 = new Human();
                Machine player2 = new Machine(0);
                GameManager judger = new GameManager(null, player2,player1,1);
                player2.LoadPolicy();
                int winner = judger.PlayHuman();

                if (winner == player2.symbol)
                {
                    Console.WriteLine("You lose!");
                }
                else if (winner == player1.symbol)
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("It is a tie!");
                }
            }
        }

    }
