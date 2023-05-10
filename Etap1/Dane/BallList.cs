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

        public void AddBall(Point posistion)
        {
            _ballList.Add(new Ball(posistion.X, posistion.Y));
        }

        public List<IBall> GetAllBalls()
        {
            return _ballList;
        }

        public void ClearBalls()
        {
            foreach (IBall ball in _ballList)
            {
                ball.IsRunning = false;
                ball.TurnOff();
            }
            _ballList.Clear();
        }

    }
}
