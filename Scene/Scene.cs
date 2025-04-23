using System;
using System.Collections.Generic;
using System.Drawing;
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
            string subText = colorText.Substring(1);

            return Enum.TryParse<ConsoleColor>(subText, true, out var color) ?
                 color :
                 ConsoleColor.Gray;
            // 입력된 문자가 잘못입력되어 매칭되는 색상 없으면 Gray
            // 위 true 매개변수가 대소문자 상관없게 하는거
        }


        //사용 방법 : 
        // 《x1》 x숫자 = 커서 x 위치
        // 《y1》 y숫자 = 커서 y 위치
        // 《tYellow》 t색상 = 텍스트 색상
        // 《bYellow》 b색상 = 배경 색상
        // 《l1》 l숫자 = 해당 문자열 반복 횟수(기본1)
        // x,y,t,b,l 처럼 식별자는 꼭 소문자!
        // 컬러는 대소문자 상관은 없는데 첫글자 대문자 뒤 소문자 쓰십셔
        protected void DrawString(string input)
        {
            int inputIndex = 0;

            while (inputIndex < input.Length)
            {
                if (input[inputIndex] == '《')
                {
                    int endIndex = input.IndexOf('》', inputIndex);

                    if (endIndex < 0) continue; // 괄호 제대로 안닫히면 아래 무시


                    string content = input.Substring(inputIndex + 1, endIndex - inputIndex - 1);
                    string[] parts = content.Split(',');


                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black; //색상 기본값 초기화

                    var position = Console.GetCursorPosition(); //현위치

                    int posX = position.Left;
                    int posY = position.Top;
                    int loopCount = 1;

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i].Length > 0) //《》빈괄호면 패스
                        {
                            switch (parts[i][0])
                            {
                                case 't': Console.ForegroundColor = ConvertColor(parts[i]); break;
                                case 'b': Console.BackgroundColor = ConvertColor(parts[i]); break;
                                case 'x': posX = int.Parse(parts[i].Substring(1)); break;
                                case 'y': posY = int.Parse(parts[i].Substring(1)); break;
                                case 'l': loopCount = int.Parse(parts[i].Substring(1)); break;
                            }
                        }
                    }
                    Console.SetCursorPosition(posX, posY);



                    int drawStart = endIndex + 1;

                    int drawEnd = input.Length; //뒤에 더 괄호가 없을 때를 대비한 초기화

                    inputIndex = drawEnd; //뒤에 더없다면 반복문 탈출

                    for (int j = drawStart; j < input.Length; j++)
                    {
                        if (input[j] == '《')
                        {
                            drawEnd = j;

                            inputIndex = drawEnd; //다음 괄호가 반복문 시작 인덱스
                            break;
                        }
                    }

                    string drawLenght = drawEnd == input.Length ?
                        input.Substring(drawStart, input.Length - drawStart) : //뒤에 괄호가 없다면 끝까지 출력
                        input.Substring(drawStart, drawEnd - drawStart); // 있다면 다음괄호 전까지 짤라서 출력

                    for (int i = 0; i < loopCount; i++)
                    {
                        Console.Write(drawLenght);
                    }
                }
                else
                {
                    Console.Write(input[inputIndex]);
                    inputIndex++;
                }
            }
        }


        protected void DrawRemoveRect(Rectangle rectangle)
        {
            string removeString = "";

            for (int y = rectangle.Top; y <= rectangle.Height; y++)
            {
                for (int x = rectangle.Left; x <= rectangle.Width; x++)
                {
                    removeString += $"《x{x},y{y}》 ";
                }
            }

            DrawString(removeString);
        }
    }
}
