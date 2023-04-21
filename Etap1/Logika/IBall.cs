using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class IBall
    {
        public static IBall CreateBall(double x, double y, double r)
        {
            return new Ball(x, y, r);
        }

        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double R { get; set; }
        public abstract double XMovement { get; set; }
        public abstract double YMovement { get; set; }
        public abstract void MakeMove();

        public abstract event PropertyChangedEventHandler PropertyChanged;
    }
}
