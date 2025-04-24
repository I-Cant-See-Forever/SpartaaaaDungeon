using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class TitleLayout
    {
        public Rectangle Title { get; private set; }
        public Rectangle Image { get; private set; }
        public Rectangle Menu { get; private set; }
        public TitleLayout()
        {
            Title = new(0, 0, Console.WindowWidth, 8);

            Menu = new(0, Title.Height + 1, 30, Console.WindowHeight - Title.Height);

            Image = new(Menu.Width + 1, Title.Height + 1, Console.WindowHeight - Menu.Width, Console.WindowHeight - Title.Height);
        }
    }

    public class MenuInfoLayout
    {
        public Rectangle Left { get; private set; }
        public Rectangle Right { get; private set; }
        public MenuInfoLayout()
        {
            Left = new(0, 0, 30, Console.WindowHeight);
            Right = new(Left.Width + 1, 0, Console.WindowWidth - Left.Width, Console.WindowHeight);
        }
    }

    public class DungeonLayout
    {
        public Rectangle MonsterInfo { get; private set; }
        public Rectangle PlayerInfo { get; private set; }
        public Rectangle MonsterImage { get; private set; }
        public Rectangle BattleInfo { get; private set; }
        public DungeonLayout()
        {
            MonsterInfo = new(0, 0, 30, 20);
            PlayerInfo = new(0, 20, 30, 10);

            MonsterImage = new(31, 0, 89, 20);
            BattleInfo = new(31, 20, 89, 6);
        }
    }

}
