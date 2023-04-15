using Dane;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logika
{
    internal class LogicKula : INotifyPropertyChanged
    {
        private Kula kula;
        public LogicKula(Kula kula)
        {
            this.kula = kula;
            kula.PropertyChanged += Update;
        }

        private void Update(object _object, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "X")
            {
                OnPropertyChanged("X");
            }
            if (args.PropertyName == "Y") 
            {  
                OnPropertyChanged("Y");
            }
            if (args.PropertyName == "R")
            {
                OnPropertyChanged("R");
            }
        }

        public double X
        { get { return kula.X; } set { kula.X = value; OnPropertyChanged("X"); } }
        public double Y
        { get { return kula.Y; } set { kula.Y = value; OnPropertyChanged("Y"); } }
        public double R
        { get { return kula.R; } set { kula.R = value; OnPropertyChanged("R"); } }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
