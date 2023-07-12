using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace project1._4
{
    //הקלאס של השחקן יורש מהקלאס גיים פיס

    public class Player : GamePiece
    {
        TextBlock LivesTbl;//נקרא לטקסט בלוק שעשינו לספירת החיים בקסאמל 
        private int _lives;//נגדיר את התנאים של החיים

        public int Lives
        {
          get { return _lives; }
          set { _lives = value; LivesTbl.Text = "Lives : " + _lives; } 
        }
        //נגדיר בקונסטרקטור את החיים והמהירות
        public Player(Grid TheGrid)
        {
            LivesTbl = (TextBlock)TheGrid.FindName("LivesTbl");
            Shape.Source = new BitmapImage(new Uri("ms-appx:///Assets/spongebob.png"));
            Speed = 5;
            Lives = 3;
        }
    }
}
