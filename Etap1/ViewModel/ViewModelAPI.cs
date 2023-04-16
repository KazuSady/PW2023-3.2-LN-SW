using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelAPI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AbstractModelAPI modelAPI;
        private int ballsAmount = 1;
        private int ballR = 3;
        private ObservableCollection<Okrag> okregi;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Balls
        {
            get { return Convert.ToString(ballsAmount); }
            set 
            {
                try
                {
                    ballsAmount = Convert.ToInt32(value);
                }
                catch (System.FormatException)
                {
                    ballsAmount = 0;
                }
                // Jeżeli wprowadzona wartość inna niż liczby, automatycznie ustaw liczbe kul na 0 
            }
        }
        public ObservableCollection<Okrag> Okregi 
        {
            get => okregi;
            set 
            {
                if (value.Equals(okregi)) return;
                okregi = value;
                OnPropertyChanged("Okregi");
                // Event informujący ItemsControl w View o zmianie wartości w okręgu z kolekcji Okregi
            }
        }
        
        public ViewModelAPI()
        {
            EnableAction = new Akcja(TurnOn);
            DisableAction = new Akcja(TurnOff);
            this.modelAPI = AbstractModelAPI.CreateAPI();

        }

        private void TurnOn()
        { 

            modelAPI.CreateObszar(500, 666, ballsAmount, ballR);
            modelAPI.CreateKule();
            modelAPI.TurnOn();
            Okregi = modelAPI.GetOkragList();
        }

        private void TurnOff()
        {
            modelAPI.TurnOff();
        }

        public ICommand EnableAction { get; set; }
        public ICommand DisableAction { get; set;}
    }
}
