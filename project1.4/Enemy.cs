using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace project1._4
{
    //הקלאס של האויב יורש מהקלאס גיים פיס
    public class Enemy : GamePiece
    {
        public Enemy()
        { 
        Random rnd = new Random(); //השחקנים יופיעו בצורה רנדומלית כשיש לנו 4 סוגי אויבים
        int a = rnd.Next(1, 5);
        Shape.Source = new BitmapImage(new Uri("ms-appx:///Assets/enemy" + a + ".png"));//נגדיר את האיורים של האויבים
            Speed = 1;//נגדיר את מהירויות של האויבים
            }
    }
}

