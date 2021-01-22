namespace EasterRaces.Models.Cars.Entities
{
    public class MuscleCar : Car
    {
        private const double MUSCLECAR_CUBICCENTIMETERS = 5000;
        private const int MUSCLECAR_MINHORSEPOWER = 400;
        private const int MUSCLECAR_MAXHORSEPOWER = 600;

        public MuscleCar(string model, int horsePower) 
            : base(model, horsePower, MUSCLECAR_CUBICCENTIMETERS, MUSCLECAR_MINHORSEPOWER, MUSCLECAR_MAXHORSEPOWER)
        {

        }
    }
}
