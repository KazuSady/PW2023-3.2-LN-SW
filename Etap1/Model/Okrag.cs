using Logika;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class Okrag
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
            Kula kula = (Kula) _object;
            if (args.PropertyName == "X")
            {
                this.x = kula.X - kula.R;
            }
            if (args.PropertyName == "Y")
            {
                this.y = kula.Y - kula.R;
            }
            if (args.PropertyName == "Radius")
            {
                this.r = kula.R;
            }
        }

        public double X
        { get { return x; } set { x = value; OnPropertyChanged("X"); } }
        public double Y
        { get { return y; } set { y = value; OnPropertyChanged("Y"); } }
        public double R
        { get { return r; } set { r = value; OnPropertyChanged("R"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}