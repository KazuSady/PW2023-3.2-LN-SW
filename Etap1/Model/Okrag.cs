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
            this.x = kula.X - kula.R;
            this.y = kula.Y - kula.R;
            this.r = kula.R;
            kula.PropertyChanged += Update;
        }

        private void Update(object _object, PropertyChangedEventArgs args)
        {
            Kula kula = (Kula)_object;
            if (args.PropertyName == "X")
            {
                this.x = kula.X - kula.R;
                OnPropertyChanged(nameof(X));
            }
            if (args.PropertyName == "Y")
            {
                this.y = kula.Y - kula.R;
                OnPropertyChanged(nameof(Y));
            }
            if (args.PropertyName == "R")
            {
                this.r = kula.R;
                OnPropertyChanged(nameof(R));
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