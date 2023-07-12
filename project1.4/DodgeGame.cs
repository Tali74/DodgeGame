using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace project1._4
{
    internal class DodgeGame// הצגת הלוגיקה של המשחק
    {
        private BoardGame _Brd;// נגדיר את עמוד הבורד גיים
        public BoardGame Brd
        {
            get { return _Brd; }
            set { _Brd = value; }
        }

        private DispatcherTimer _timer;// בשביל תנועת הדמויות, הפעלת ועצירת המשחק נגדיר טיימר
        public DispatcherTimer timer
        {
            get { return _timer; }
            set { _timer = value; }
        }


        bool isUp, isDown, isLeft, isRight;// נגיר את כיווני התנועה

        public DodgeGame(Grid TheGrid) //הקונסטרקטור קשור לגריד הראשי שכולל את שאר הגרידים 
        {
            _Brd = new BoardGame(TheGrid);
            isUp = isDown = isLeft = isRight = false;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;//נםעיל את פעולת לחיצת המקש
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;//נפעיל את פעולת הרמת המקש לאחר הלחיצה
            _timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);// נגדיר שבכל 10 מילי שניות תהיה תנועה 
            timer.Tick += Timer_Tick;
            timer.Start();// התחלת השעון
        }

        private void Timer_Tick(object sender, object e)// כאשר השעון מתחיל לעשות אז יש תנועה של כל הדמויות ואם יש מגע בין אויבים אז אחד מהם נעלם ויש תליך של איסוף המדוזות
        {
            PlayerMove();
            EnemyMove();
            Touch();
        }


        private void Touch()// מטוד שמגדיר מה קןרה אם יש מגע בין שחקן לאויב, בין אויב לאויב ובין שחקן למדוזה
        {
            if (Brd.SameLoc(Brd.player, Brd.Enemies, out GamePiece a))//אם האויב משיג את החשקן ויש מגע 
            {
                GameOver();//יורד חיים אחד עד שהחיים שווים לאפס ואז אתה מפסיד
                Brd.SetLoc(Brd.player);//אם החיים לא שווים לאפס אז המשחק ממשיך והשחקן קופץ לנקודה אחרת על המסך
            }


            for (int i = 0; i < Brd.Enemies.Count; i++)
            {
                if (Brd.SameLoc(Brd.Enemies[i], Brd.Enemies, out a))//אם יש 2 אויבים באותה נקודה
                {
                    Brd.cnvs.Children.Remove(Brd.Enemies[i].Shape);//מורידים את התמונה של האויב
                    Brd.Enemies.Remove(Brd.Enemies[i]);//מורידי אויב אחד מהרשימה
                    if (Brd.Enemies.Count <= 1)//אם מספר האויבים שווה או קטן מ1
                    {
                        PopMes("Game Over", "You Win!! :)"); //אתה ניצחת
                    }
                }

            }



            if (Brd.SameLoc(Brd.player, Brd.Jelly, out GamePiece target))
            {
                Brd.cnvs.Children.Remove(target.Shape);//לאחר המגע בין השחקן למדוזה מורידים את התמונה של המדוזה
                Brd.Jelly.Remove(target);//מורידים מדוזה אחת מהרשימה
                Brd.player.Lives++;//חיים מתווספים לשחקן
                if (Brd.Jelly.Count <= 0)// אם לא נשארו מדוזות אז השחקן ניצח
                {
                    PopMes("Game Over", "You Win!! :)");
                }
            }


        }
        public static async void HowToPlay()// כפתור במסך הראשי שמסביר את מטרת המשחק והכללים
        {
            await new MessageDialog("Help spongebob to run away from his enemies, you have 3 lives.\n" +
                 "If you want more lives collect the jellyfish, if you collect them all you will win.\n" +
                 "If there will be only 1 enemy left you will also win", "How to Play").ShowAsync();
        }


        private async void PopMes(string head, string text)//כתיבת הודעה- קודפ כותרת ואז טקסט
        {
            timer.Stop();//כאשר קופצת הודעה השכון נעצר
            MessageDialog message = new MessageDialog(text, head);
            await message.ShowAsync();//ההודעה מופיעה ומחכה לתגובה
        }

        private void GameOver()
        {
            Brd.player.Lives--;//כל הפסד יורד חיים אחד
            if (Brd.player.Lives == 0)//אם החיים שווים לאפס
            {
                PopMes("Game Over", "BAAAHH You Lose :(");//קופצת הודעה של הפסד
            }
        }

        private void EnemyMove()//תנועת האויב
        {
            foreach (Enemy enemy in Brd.Enemies)//כל אויב ברשימת האויבים
            {
                double goX = Brd.player.X - enemy.X;//נע בכיוון איקס לפי השחקן
                double goY = Brd.player.Y - enemy.Y;//נע ביוון ווי לפי השחקן

                if (goX > 0)//אם התנועה בציר איקס קטנה מאפס אז האויב יצא מהמסך, אז אנחנו מונעים את זה
                {
                    enemy.X += enemy.Speed;
                }
                else
                {
                    enemy.X -= enemy.Speed;
                }
                if (goY > 0)//אם התנועה בציר ווי קטנה מאפס אז האויב יצא מהמסך, אז אנחנו מונעים את זה
                {
                    enemy.Y += enemy.Speed;
                }
                else
                {
                    enemy.Y -= enemy.Speed;
                }
            }
        }

        private void PlayerMove()//תנועת השחקן תהיה בהתאם למקש שנלחץ בגבולות המסך
        {
            if (isUp)
            {
                Brd.player.Y -= Brd.player.Speed;
            }
            if (isDown)
            {
                Brd.player.Y += Brd.player.Speed;
            }
            if (isLeft)
            {
                Brd.player.X -= Brd.player.Speed;
            }
            if (isRight)
            {
                Brd.player.X += Brd.player.Speed;
            }
            Brd.WallsPlayer();
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)// הגדרת הספקת כיוון תנועת הדמות אם מפסיקים ללחוץ על המקש
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    isUp = false;
                    break;
                case Windows.System.VirtualKey.Down:
                    isDown = false;
                    break;
                case Windows.System.VirtualKey.Left:
                    isLeft = false;
                    break;
                case Windows.System.VirtualKey.Right:
                    isRight = false;
                    break;
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)// הגדרת תנועת הדמות בהתאם לחיצת המקש
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Up:
                    isUp = true;
                    break;
                case Windows.System.VirtualKey.Down:
                    isDown = true;
                    break;
                case Windows.System.VirtualKey.Left:
                    isLeft = true;
                    break;
                case Windows.System.VirtualKey.Right:
                    isRight = true;
                    break;
            }


        }

        public async void SaveFile()// תהליך שמירת הקובץ שומר את מיקומי כל הדמויות והמהירויות שלהם
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;//הגדרת תקייה לשמירה
            StorageFile sampleFile = await storageFolder.CreateFileAsync("SaveGame.txt", CreationCollisionOption.ReplaceExisting);//אם יש משהו שמור כבר אז שומרים עליו
            foreach (Jellyfish jellyfish in Brd.Jelly)
            {
                await FileIO.AppendTextAsync(sampleFile, $"{jellyfish.X}|{jellyfish.Y}|0{Environment.NewLine}");//שמירת את כל המידע הנדרש של מיקום של המדוזות
            }
            await FileIO.AppendTextAsync(sampleFile,"*");//הפרדה של נתוני שמירה בין המדוזות לאויבים
            foreach (Enemy enemy in Brd.Enemies)
            {
                await FileIO.AppendTextAsync(sampleFile, $"{enemy.X}|{enemy.Y}|{enemy.Speed}{Environment.NewLine}");//שמירת כל המידע של מיקום ומהירות של האויבים
            }
            
            await FileIO.AppendTextAsync(sampleFile, $"{Brd.player.X}|{Brd.player.Y}|{Brd.player.Speed}|{Brd.player.Lives}");//שמירת כל המידע של השחקן
            PopMes("Game Saved", "Close to Continue");//קפיצת הופעה של המשחק נשמר
            //timer.Start();//תחילת משחק
        }

        public async void LoadGame()//המשך משחק שנשמר
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;//פניה לתקייה שבה זה נשמר
            StorageFile sampleFile = await storageFolder.GetFileAsync("SaveGame.txt");//שומרים רת הקובץ בתקייה
            string allText = await FileIO.ReadTextAsync(sampleFile);
            ReadFile(allText);
            PopMes("Game Loaded", "Close To Continue");//קפיצת הודעה שהמחשק עולה
            timer.Start();//תחילת המשחק
        }

        void ReadFile(string allText)// בפתיחת משחק שמור צריך לקרוא את כל הנתונים השמורים ככה שזה יעלה את הנתונים המדוייקים
        {
            string[] partstring = allText.Split("*");//ההפרדה בין נתוני המדוזה לנתוני האויב
            string[] JellyLines = partstring[0].Split("\n");
            Brd.cnvs.Children.Clear();//מוחקים את כל הנתונים
            Brd.Jelly.Clear();
            Brd.Enemies.Clear();

            for (int i = 0; i < JellyLines.Length- 1; i++)
            {
                string[] JellyLine = JellyLines[i].Split("|");//קראת נתונים שנשמרו
                Jellyfish jellyfish1 = new Jellyfish();
                ReCreatePiece(JellyLine,jellyfish1);//יצירת מדוזות
                Brd.Jelly.Add(jellyfish1);
            }
            string[] lines = partstring[1].Split("\n");
            string[] line;

          
            for (int i = 0; i < lines.Length - 1; i++)
            {
                line = lines[i].Split("|");
                Enemy enemy1 = new Enemy();//יצירת אויבים
                ReCreatePiece(line, enemy1);
                Brd.Enemies.Add(enemy1);
              
            }

           line = lines[lines.Length - 1].Split("|");
            Brd.player.Lives = int.Parse(line[3]);//יצירת שמקן ונתוני החיים
            ReCreatePiece(line, Brd.player);
        }

        private void ReCreatePiece(string[] line, GamePiece piece)//יצירת כל הדמויות מחדש לפי המיקומים שהיו להם והמהירויות שהיו להם
        {
            piece.X = double.Parse(line[0]);
            piece.Y = double.Parse(line[1]);
            piece.Speed = double.Parse(line[2]);

            Brd.cnvs.Children.Add(piece.Shape);
        }
    }
}

