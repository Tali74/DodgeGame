using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace project1._4
{
    public class GamePiece
    {
        //מדבור בקלאס שממנו כל השחקנים ירשו את התכונות שהם צריכים
        //את המיקום של המשחקנים נגדיר באיקס ווי עם קישור לשייפ שיופיע בקנבס
        private double _x;

        public double X
        {
            get { return _x; }
            set { _x = value; Canvas.SetLeft(Shape, _x); }
        }
        private double _y;
        public double Y
        {
            get { return _y; }
            set { _y = value; Canvas.SetTop(Shape, _y); }
        }
        //לשחקנים יש מהירות

        private double _Speed;

        public double Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        //לשחקנים יש תמונות

        private Image _Shape;
        public Image Shape
        {
            get { return _Shape; }
            set { _Shape = value; }
        }

        //נגדיר בקונסטרקטור את רוחב והאורך של כל שחקן בשביל אחידות
        public GamePiece()
        {
            _x = X;
            _y = Y;
            _Speed = Speed;
            _Shape = new Image();
            _Shape.Height = 50;
            _Shape.Width = 50;
        }
    }
}
