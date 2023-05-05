using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    internal class BallList
    {
        private List<IBall> _ballList;

        public BallList() 
        { 
            _ballList = new List<IBall>();
        }

        public void addBall(Point posistion)
        {
            _ballList.Add(new Ball(posistion.X, posistion.Y));
        }

        public List<IBall> getAllBalls()
        {
            return _ballList;
        }

        public void clearBalls()
        {
            _ballList.Clear();
        }

    }
}
