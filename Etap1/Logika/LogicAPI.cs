using Dane;
using System;
using System.Runtime.ExceptionServices;

namespace Logika
{
    public abstract class AbstractLogicAPI
    {
        public static AbstractLogicAPI CreateAPI(AbstractDaneAPI abstractDaneAPI = null)
        {
            return new LogicAPI(abstractDaneAPI);
        }
        public abstract void CreateField(int height, int width);
        public abstract void CreateBalls(int kulaAmount, int kulaRadius);
        public abstract List<IBall> GetAllBalls();
        public abstract void TurnOff();
        public abstract void TurnOn();
        public abstract bool IsRunning();


        internal sealed class LogicAPI : AbstractLogicAPI
        {
            private AbstractDaneAPI _dataAPI;
            private Field _Field;
            private List<Task> _Tasks = new List<Task>();
            private SemaphoreSlim _Semaphore = new SemaphoreSlim(1);


            public LogicAPI(AbstractDaneAPI abstractDaneAPI = null)
            {
                if (abstractDaneAPI == null)
                {
                    _dataAPI = AbstractDaneAPI.CreateApi();
                }
                else
                {
                    _dataAPI = abstractDaneAPI;
                }
            }
            private class Unsubscriber : IDisposable
            {
                private IObserver<int> _observer;

                public Unsubscriber(IObserver<int> observer)
                {
                    this._observer = observer;
                }

                public void Dispose()
                {
                    _observer = null;
                }
            }


            public override void CreateField(int height, int width)
            {
                _Field = new Field(height, width);
            }
            public override void CreateBalls(int ballsAmount, int ballRadius)
            {
                _Field.CreateBallsList(ballsAmount, ballRadius);
                ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

                for (int i = 0; i < ballsAmount; i++) 
                {
                    IBall ball = _Field.GetBalls().ElementAt(i);
                    Task task = new Task(async () =>
                    { 
                        while (this.IsRunning())
                        {
                            await _Semaphore.WaitAsync();
                            ball.XMovement = random.Value.Next(-10000, 10000) % 5;
                            ball.YMovement = random.Value.Next(-10000, 10000) % 5;

                            if (0 > (ball.X + ball.XMovement - ball.R) ||
                                _Field.Width < (ball.X + ball.XMovement + ball.R))
                            {
                                ball.XMovement = -ball.XMovement;
                            }
                            if (0 > (ball.Y + ball.YMovement - ball.R) ||
                                _Field.Height < (ball.Y + ball.YMovement + ball.R))
                            {
                                ball.YMovement = -ball.YMovement;
                            }

                            ball.MakeMove();
                            Task.Delay(10);
                            _Semaphore.Release();
                        }
                    });
                    _Tasks.Add(task);
                };

            }

            public override void TurnOff()
            {
                _Field.IsRunning = false;
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
                _Field.ClearBalls();
            }
            public override void TurnOn()
            {
                _Field.IsRunning = true;
                foreach (Task task in _Tasks)
                {
                    task.Start();
                }
            }


            public override bool IsRunning()
            {
                return _Field.IsRunning;
            }
            public override List<IBall> GetAllBalls()
            {
                return _Field.GetBalls();
            }

        }
    }
}