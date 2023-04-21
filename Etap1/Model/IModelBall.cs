using System.ComponentModel;

namespace Model
{
    public abstract class IModelBall
    {
        public static IModelBall CreateModelBall(double x, double y, double r)
        {
            return new ModelBall(x, y, r);
        }

        public abstract double X { get; set; }
        public abstract double Y { get; set; }
        public abstract double R { get; }

        public abstract void Update(Object s, PropertyChangedEventArgs e);

        public abstract event PropertyChangedEventHandler PropertyChanged;

    }
}
