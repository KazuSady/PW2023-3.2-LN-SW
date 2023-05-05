using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class ILogicBall
    {
        public static ILogicBall CreateLogicBall(int x, int y)
        {
            return new LogicBall(x, y);
        }

        public abstract Point Position { get; set; }
        public abstract void Update(object obj, PropertyChangedEventArgs args);
           
        public abstract event PropertyChangedEventHandler PropertyChanged;
    }
}
