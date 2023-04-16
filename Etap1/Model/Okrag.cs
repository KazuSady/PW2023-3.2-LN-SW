using Logika;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class Okrag : INotifyPropertyChanged
    {
        private double x;
        private double y;
        private double r;

        public Okrag(Kula kula)
        {
            this.x = kula.X;
            this.y = kula.Y;
            this.r = kula.R;
            kula.PropertyChanged += Update;
        }

        private void Update(object _object, PropertyChangedEventArgs args)
        {
            Kula kula = (Kula)_object;
            if (args.PropertyName == "X")
            {
                this.X = kula.X;
                OnPropertyChanged("X");
            }
            if (args.PropertyName == "Y")
            {
                this.Y = kula.Y;
                OnPropertyChanged("Y");
            }
            if (args.PropertyName == "R")
            {
                this.R = kula.R;
                OnPropertyChanged("R");
            }
        }

        public double X
        { get { return x; } set { x = value; OnPropertyChanged(); } }
        public double Y
        { get { return y; } set { y = value; OnPropertyChanged(); } }
        public double R
        { get { return r; } set { r = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}