using Dane;
using Microsoft.VisualBasic;
using System;
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
        public abstract void ClearBalls();
        public abstract List<ILogicBall> GetAllBalls();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();



        internal sealed class LogicAPI : AbstracDataAPI
        {
            private AbstractDataAPI _dataAPI;
            private List<Task> _Tasks = new List<Task>();
            private List<ILogicBall> _logicBalls = new List<ILogicBall>();
            private readonly object locked = new object();


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



                    _logicBalls.Add(ILogicBall.CreateLogicBall(ball.Position.X, ball.Position.Y));
                    ball.PropertyChanged += _logicBalls.ElementAt(i).Update!;

                    Task task = new Task(async () =>
                    {
                        startMovement(ball, ballRadius);
                    });
                    _Tasks.Add(task);
                }
            }
            public override void ClearBalls()
            {
                _dataAPI.ClearBalls();
                _logicBalls.Clear();
            }


            public override void TurnOff()
            {
                _dataAPI.TurnOff();
                bool isAllTasksCompleted = false;

                while (!isAllTasksCompleted)
                {
                    isAllTasksCompleted = true;
                    foreach (Task task in _Tasks)
                    {
                        if (!task.IsCompleted)
                        {
                            isAllTasksCompleted = false;
                            break;
                        }
                    }
                }

                foreach (Task task in _Tasks)
                {
                    task.Dispose();
                }
                _Tasks.Clear();
                this.ClearBalls();
            }
            public override void TurnOn()
            {
                _dataAPI.TurnOn();
                
                foreach (Task task in _Tasks)
                {
                    task.Start();
                }
                
            }
            public override bool IsRunning()
            {
                return _dataAPI.IsRunning();
            }
            public override List<ILogicBall> GetAllBalls()
            {
                return this._logicBalls;
            }

            private async void startMovement(IBall ball, int ballRadius)
            {
                ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
                while (_dataAPI.IsRunning())
                {
                    ballCollision(ball, ballRadius);
                    wallColission(ball, ballRadius);
                    lock (ball)
                    {
                        ball.MakeMove();
                    }
                    Task.Delay(50).Wait();
                }
            }
            private void wallColission (IBall ball, int ballRadius)
            {
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
            private void ballCollision(IBall ball, int ballRadius)
            {
                int weight = 1;
                foreach (IBall otherBall in _dataAPI.GetAllBalls())
                {
                    if (otherBall != ball)
                    {
                        int xDistance = ball.Position.X - otherBall.Position.X;
                        int yDistance = ball.Position.Y - otherBall.Position.Y;
                        double distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

                        if ( distance <= (ballRadius) )
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

            }

        }
    }
}