using Dane;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Logika
{
    public abstract class AbstracDataAPI
    {
        public static AbstracDataAPI CreateAPI(AbstractDataAPI abstractDataAPI = default)
        {
            return new LogicAPI(abstractDataAPI ?? AbstractDataAPI.CreateApi());
        }
        public abstract void CreateField(int height, int width);
        public abstract void CreateBalls(int kulaAmount, int kulaRadius);
        public abstract List<ILogicBall> GetAllBalls();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();



        internal sealed class LogicAPI : AbstracDataAPI
        {
            private AbstractDataAPI _dataAPI;
            private List<Task> _Tasks = new List<Task>();
            private List<ILogicBall> _logicBalls = new List<ILogicBall>();
            private int ballRadius;
            private object _ballListLock = new object();


            public LogicAPI(AbstractDataAPI abstractDataAPI)
            {
                _dataAPI = abstractDataAPI;
            }

            public override void CreateField(int height, int width)
            {
                _dataAPI.CreateScene(height, width);
            }
            public override void CreateBalls(int ballsAmount, int ballRadius)
            {
                this.ballRadius = ballRadius;
                int x;
                int y;
                Random random = new Random();
                for (int i = 0; i < ballsAmount; i++)
                {

                    x = random.Next(ballRadius, _dataAPI.GetSceneWidth() - ballRadius);
                    y = random.Next(ballRadius, _dataAPI.GetSceneHeight() - ballRadius);

                    _dataAPI.CreateBall(new Point(x, y));
                    
                    IBall ball = _dataAPI.GetAllBalls().ElementAt(i);
                    do
                    {
                        ball.XMovement = random.Next(-10000, 10000) % 3;
                        ball.YMovement = random.Next(-10000, 10000) % 3;
                    } while (ball.XMovement == 0 || ball.YMovement == 0);


                    ILogicBall logicBall = ILogicBall.CreateLogicBall(ball.Position.X, ball.Position.Y);
                    ball.PropertyChanged += logicBall.Update!;
                    ball.PropertyChanged += WallColission!;
                    ball.PropertyChanged += CheckCollision!;
                    

                    _logicBalls.Add(logicBall);
                }
            }


            public override void TurnOff()
            {
                _dataAPI.TurnOff();
                _logicBalls.Clear();
            }
            public override void TurnOn()
            {
                _dataAPI.TurnOn();
            }
            public override bool IsRunning()
            {
                return _dataAPI.IsRunning();
            }
            public override List<ILogicBall> GetAllBalls()
            {
                return _logicBalls;
            }


            private void WallColission(Object o, PropertyChangedEventArgs args)
            {
                IBall ball = (IBall)o;

                if (0 > (ball.Position.X + ball.XMovement) ||
                    _dataAPI.GetSceneWidth() < (ball.Position.X + ball.XMovement + ballRadius))
                {
                    ball.XMovement = -ball.XMovement;
                }
                if (0 > (ball.Position.Y + ball.YMovement) ||
                    _dataAPI.GetSceneHeight() < (ball.Position.Y + ball.YMovement + ballRadius))
                {
                    ball.YMovement = -ball.YMovement;
                }
            }

            private void BallCollision(IBall ball, IBall otherBall)
            {
                int weight = 1;
                if (otherBall != ball)
                {

                    int xDistance = ball.Position.X + ball.XMovement - otherBall.Position.X - otherBall.XMovement;
                    int yDistance = ball.Position.Y + ball.YMovement - otherBall.Position.Y - otherBall.YMovement;
                    double distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

                    if (distance <= (ballRadius))
                    {
                        int newXMovement = (2 * weight * ball.XMovement) / (2 * weight);
                        ball.XMovement = (2 * weight * otherBall.XMovement) / (2 * weight);
                        otherBall.XMovement = newXMovement;

                        int newYMovement = (2 * weight * ball.YMovement) / (2 * weight);
                        ball.YMovement = (2 * weight * otherBall.YMovement) / (2 * weight);
                        otherBall.YMovement = newYMovement;

                    }
                }
                
               
            }

            private void CheckCollision(Object o, PropertyChangedEventArgs args)
            {

                IBall ball = (IBall)o;
                foreach (IBall otherBall in _dataAPI.GetAllBalls().ToArray())
                {
                    if (otherBall != ball)
                    {
                        int xDistance = ball.Position.X - otherBall.Position.X;
                        int yDistance = ball.Position.Y - otherBall.Position.Y;
                        double distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

                        if (distance <= (ballRadius))
                        {
                            lock (ball)
                            {
                                BallCollision(ball, otherBall);
                            }
                        }
                    }
                }
                
                
                
            }


        }
    }
}