using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Xml;
using System;

namespace Dane
{
    internal class BallLogger : AbstractBallLogger
    {
        private string _filePath;
        private bool isRunning;
        private ConcurrentQueue<JObject> _ballsQueue;
        private Mutex _writeMutex = new Mutex();
        private Mutex _enterQueueMutex = new Mutex();

        public BallLogger()
        {
            string path = Path.GetTempPath();
            _filePath = Path.Combine(path, "DataBallsLog.json");
            _ballsQueue = new ConcurrentQueue<JObject>();
            if (!File.Exists(_filePath))
            {
                FileStream file = File.Create(_filePath);
                file.Close();
            }
            isRunning = true;
            Task.Run(writeDataToLogFile);
        }

        public override void writeSceneSizeToLogFile(int sceneHeigth, int sceneWidth)
        {
            JObject sceneSizeObject = new JObject();
            sceneSizeObject["Scene Height: "] = sceneHeigth;
            sceneSizeObject["Scene Width: "] = sceneWidth;
            sceneSizeObject["Time"] = DateTime.Now.ToString("HH:mm:ss");
            _writeMutex.WaitOne();
            try
            {
                File.AppendAllText(_filePath, JsonConvert.SerializeObject(sceneSizeObject, Newtonsoft.Json.Formatting.Indented));
            }
            finally
            {
                _writeMutex.ReleaseMutex();
            }
        }

        public override void addBallToQueue(IBall ball)
        {
            _enterQueueMutex.WaitOne();
            try
            {
                if (_ballsQueue.Count < 50)
                {
                    JObject logObject = JObject.FromObject(ball);
                    logObject["Time:"] = DateTime.Now.ToString("HH:mm:ss");
                    _ballsQueue.Enqueue(logObject);
                }
            }
            finally
            {
                _enterQueueMutex.ReleaseMutex();
            }

        }

        private void writeDataToLogFile()
        {
            while (this.isRunning)
            {
                while (_ballsQueue.TryDequeue(out JObject ball))
                {
                    string data = JsonConvert.SerializeObject(ball, Newtonsoft.Json.Formatting.Indented);
                    _writeMutex.WaitOne();
                    try
                    {
                        File.AppendAllText(_filePath, data);
                    }
                    finally
                    {
                        _writeMutex.ReleaseMutex();
                    }
                }
            }
        }

        public override void Dispose()
        {
            this.isRunning = false;
            _ballsQueue.Clear();
        }

        ~BallLogger()
        {
            this.Dispose();
        }
    }
}
