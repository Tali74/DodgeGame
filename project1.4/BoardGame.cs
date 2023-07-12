using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace project1._4
{
    internal class BoardGame
    {

        private Player _player;// הצגת השחקן
        public Player player
        {
            get { return _player; }
            set { _player = value; }
        }

       

        private Grid _TheGrid; //הצגת הגריד הראשי שכולל את שאר הגרידים
        public Grid TheGrid
        {
            get { return _TheGrid; }
            set { _TheGrid = value; }
        }

        private Canvas _cnvs;//המגת הקנבס
        public Canvas cnvs
        {
            get { return _cnvs; }
            set { _cnvs = value; }
        }
        public List<GamePiece> Enemies { get; set; }//יצירת רשימת של 10 אויבים
        int numberOfEnemies = 10;

        public List<GamePiece> Jelly { get; set; } //יצירת רשימה של 5 מדוזות
        int numberOfJelly = 5;

        public BoardGame(Grid TheGrid)
        {
            _TheGrid = TheGrid;
            cnvs = (Canvas)TheGrid.FindName("cnvs");
            Enemies = new List<GamePiece>();
            Jelly = new List<GamePiece>();//יצירת מדוזות לפי הכמות הרשומה

            for (int i = 0; i < numberOfEnemies; i++)//יצירת אויבים לפי הכמות הרשומה
            {
                Enemy enemy1 = new Enemy();
                CreatePiece(enemy1);
                Enemies.Add(enemy1);
            }

            for (int i = 0; i < numberOfJelly; i++)//יצירת המדוזות לפי הכמות הרצויה
            {
                Jellyfish jelly1 = new Jellyfish();
                CreatePiece(jelly1);
                Jelly.Add(jelly1);
            }

            player = new Player(TheGrid);//יצירת השחקן
            CreatePiece(player);
        }

        private void CreatePiece(GamePiece piece)//מטוד לייצירת דמות
        {
            SetLoc(piece);//ממקמים את הדמויות
            if (SameLoc(piece, Enemies, out GamePiece a) || SameLoc(piece, Jelly, out a) )//אם יש דמויות באותו מקום
            {
                CreatePiece(piece);// יוצרים שוב את הדמות
            }
            else
            {
                cnvs.Children.Add(piece.Shape);// אם הם לא באותו מקום אז מוסיפים ומשאירים את הדמות
            }
        }

        public bool SameLoc(GamePiece piece, List<GamePiece> pawns, out GamePiece target)// נבדוק האם לפי הקורדינטות הדמות היא לא באותו מקום ואם כן אז ניצור אותה במקום אחר
        {
            target = null;
            foreach (GamePiece pawn in pawns)//נאחד את רשימת המדוזות ורשימת האוייבים לרשימה אחת כי כולם צריכים להיות לא אחד על השני ללא קשר למה הם
            {
                if (piece == pawn)
                {
                    continue;
                }
                if (Math.Abs(pawn.X - piece.X) < piece.Shape.Width * 0.65 && Math.Abs(pawn.Y - piece.Y) < piece.Shape.Height * 0.65)
                {
                    target = pawn;
                    return true;
                }
            }
            return false;
        }
           


        public void SetLoc(GamePiece piece) //הדמויות מופיעות באופן רנגומלי
        {
            Random rnd = new Random();
            piece.X = rnd.Next((int)(cnvs.ActualWidth - piece.Shape.Width));
            piece.Y = rnd.Next((int)(cnvs.ActualHeight - piece.Shape.Height));

        }

        //נגדיר שאין איקס ווי שיליליים בשביל שהשחקן לא יוכל לצאת מגבולות המסך ונגדיר גם שהוא יעצר כשעדיין רואים את התמונה ולא אחרי שהתמונה עבברה את הגבול
        public void WallsPlayer ()
        {

            if (player.X < 0)
            {
                player.X = 0;
            }
            if (player.X > cnvs.ActualWidth - player.Shape.Width)
            {
                player.X = cnvs.ActualWidth - player.Shape.Width;
            }
            if (player.Y < 0)
            {
                player.Y = 0;
            }
            if (player.Y > cnvs.ActualHeight - player.Shape.Height)
            {
                player.Y = cnvs.ActualHeight - player.Shape.Height;
            }
        }
       

    }
}
