using Logika;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    internal class ModelBall : IModelBall, INotifyPropertyChanged
    {
        private double _x { get; set; }
        private double _y { get; set; }
        private double _r { get; set; }

        public ModelBall(double x, double y, double r)
        {
            this._x = x;
            this._y = y;
            this._r = r;
        }


        public override double X
        { get => _x; set { _x = value; OnPropertyChanged(); } }
        public override double Y
        { get => _y; set { _y = value; OnPropertyChanged(); } }
        public override double R 
        { get => _r; }

        public override void Update(object obj, PropertyChangedEventArgs args)
        {
            IBall ball = (IBall)obj;

            if (args.PropertyName == "X")
            {
                X = ball.X;
            }
            else if (args.PropertyName == "Y")
            {
                Y = ball.Y;
            }
        }
        public override event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}