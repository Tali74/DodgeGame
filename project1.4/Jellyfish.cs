using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace project1._4
{
    //הקלאס של המדוזה יורש מהקלאס גיים פיס

    internal class Jellyfish : GamePiece
    
    {
        public Jellyfish()
        {
            Random rnd = new Random(); //המדוזה מופיעה רנדומלית
            int a = rnd.Next(1, 3);// יש לנו 2 סוגי מדוזות
            Shape.Source = new BitmapImage(new Uri("ms-appx:///Assets/jelly" + a + ".png"));// הגדרת תמונות המדוזות

        }
    }
}
