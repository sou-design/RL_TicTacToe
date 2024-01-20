using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe.RaylibManager
{
    public class RaylibManager
    {

        public string[,] draws = { { "*", "*", "*" }, { "*", "*", "*" }, { "*", "*", "*" } };
        public Vector2 MousePosition => Raylib.GetMousePosition();
        public void InitWindow(int width, int height, string title)
        {
            Raylib.InitWindow(width, height, title);
        }
        public void resetDraws()
        {
            Raylib.WaitTime(0.5);

            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    draws[i, j] = "*";
                }
            }
        }
        public bool IsMouseButtonPressed(MouseButton button)
        {
            return Raylib.IsMouseButtonPressed(button);
        }
        public void wait(MouseButton button)
        {
            while (!Raylib.IsMouseButtonPressed(button))
            { 
                Raylib.WaitTime(2); 
            } 
        }
        public void BeginDrawing()
        {
            Raylib.BeginDrawing();
        }
        public void EndDrawing()
        {
            Raylib.EndDrawing();
        }
        public void DrawBoard(int buttonSize, int buttonSpacing, Vector2 mousePosition)
        {
            //gameState.PrintState();
            buttonSize = buttonSize - 10;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {                    
                    int xPos = col * (buttonSize + buttonSpacing)+5;
                    int yPos = row * (buttonSize + buttonSpacing)+5;

                    // Draw the button
                    Raylib.DrawRectangle(xPos, yPos, buttonSize, buttonSize, Raylib_cs.Color.DARKBLUE);
                    //int symbol = gameState.Data[row, col];
                    string buttonText = draws[row, col];
                    if(buttonText == "X")
                    {
                        Raylib.DrawText(buttonText, xPos + 80, yPos + 70, 64, Raylib_cs.Color.YELLOW);
                    }
                    else if((buttonText == "O"))
                    {
                        Raylib.DrawText(buttonText, xPos + 80, yPos + 70, 64, Raylib_cs.Color.ORANGE);
                    }
                    else 
                    { 
                        Raylib.DrawText(buttonText, xPos + 80, yPos + 70, 64, Raylib_cs.Color.WHITE); 
                    }
                }
            }
        }
        public void DrawText(string text, int posX, int posY, int fontSize, Color color)
        {
            Raylib.DrawText(text, posX, posY, fontSize, color);
        }
        public void ClearBackground(Color color)
        {
            Raylib.ClearBackground(color);
        }

        private static RaylibManager instance;

        private RaylibManager() { }

        public static RaylibManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RaylibManager();
                }
                return instance;
            }
        }
    }
}
