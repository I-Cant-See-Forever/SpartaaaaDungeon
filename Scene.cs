using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public abstract class Scene
    {
        protected SceneController controller;


        public Scene(SceneController controller)
        {
            this.controller = controller;
        }


        public abstract void Start();
        public abstract void Update();
        public abstract void End();


        protected ConsoleColor ConvertColor(string colorText)
        {
            return Enum.TryParse<ConsoleColor>(colorText, true, out var color)
                ? color : ConsoleColor.Gray;
        }

        protected void DrawText(string input)
        {
            int inputIndex = 0;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            while (inputIndex < input.Length)
            {
                if (input[inputIndex] == '(')
                {
                    int endIndex = input.IndexOf(')', inputIndex);

                    if (endIndex < 0) continue;


                    string content = input.Substring(inputIndex + 1, endIndex - inputIndex - 1);
                    string[] parts = content.Split(',');

                    if (parts.Length <= 1) continue;


                    if (parts.Length >= 2)
                    {
                        Console.SetCursorPosition(int.Parse(parts[0]), int.Parse(parts[1]));
                    }

                    if(parts.Length >= 3)
                    {
                        Console.ForegroundColor = ConvertColor(parts[2]);
                    }

                    if (parts.Length >= 4)
                    {
                        Console.BackgroundColor = ConvertColor(parts[3]);
                    }



                    int drawStart = endIndex + 1;

                    int drawEnd = input.Length;

                    inputIndex = input.Length;

                    for (int j = drawStart; j < input.Length; j++)
                    {
                        if (input[j] == '(')
                        {
                            drawEnd = j;

                            inputIndex = drawEnd;
                            break;
                        }
                    }

                    string drawLenght;

                    if (drawEnd == input.Length)
                    {
                        drawLenght = input.Substring(drawStart, input.Length - drawStart);
                    }
                    else
                    {
                        drawLenght = input.Substring(drawStart, drawEnd - drawStart);
                    }


                    Console.Write(drawLenght);
                }
            }
        }
    }
}
