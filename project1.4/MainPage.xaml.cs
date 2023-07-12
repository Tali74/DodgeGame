using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace project1._4
{
    public sealed partial class MainPage : Page
    {
        private DodgeGame game { get; set; }//נקשר ללוגיקה

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Save_Tapped(object sender, TappedRoutedEventArgs e)//נגדיר שכפתור השמירה שומר לפי המתודה הרלוונטית ועוצר את המשחק
        {
            game.SaveFile();
            game.timer.Stop();
        }

        private void Pause_Tapped(object sender, TappedRoutedEventArgs e)//נגדיר שכאשר הכפתור עצוב לחוץ אז המשחק עוצר ויש כפתור המשך משחק וההפך
        {
            if ((string)PauseBtn.Content == "Pause")
            {
                PauseBtn.Content = "Resume";
            }
            else
            {
                PauseBtn.Content = "Pause";
            }
            if ((string)PauseBtn.Content == "Pause")
            {
                game.timer.Start();
            }
            else if ((string)PauseBtn.Content == "Resume")
            {
                game.timer.Stop();
            }
        }

        private void New_Tapped(object sender, TappedRoutedEventArgs e)//כפתור משחק חדש מוחק את מה שיש במשחק ויוצר משחק חדש
        {
            cnvs.Children.Clear();
            game = new DodgeGame(TheGrid);
        }

        private void Load_Tapped(object sender, TappedRoutedEventArgs e)// כפתור הטעינה טוען משחק ששמרנו מקודם
        {
            game.LoadGame();
            game.timer.Stop();
        }

        private void Home_Tapped(object sender, TappedRoutedEventArgs e)//כפתור הבית מסתיר את עמוד המשחק ומחזיר את עמוד הבית
        {
            StartGrid.Visibility = Visibility.Visible;
        }

        private void Start_Tapped(object sender, TappedRoutedEventArgs e)//כפתור התחל אשר מסתיר את זה מה שיש בעמוד הבית ומציג את כל מה שיש לנו במשחק
        {
            StartGrid.Visibility = Visibility.Collapsed;
            cnvs.Children.Clear();
            game = new DodgeGame(TheGrid);
        }

        private void How_Tapped(object sender, TappedRoutedEventArgs e)//מקפיץ הודעה עם הוראות המשחק
        {
            DodgeGame.HowToPlay();
        }
    }
}
