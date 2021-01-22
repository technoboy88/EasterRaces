namespace EasterRaces.Models.Cars.Entities
{
    public class SportsCar : Car
    {
        private const double SPORTSCAR_CUBICCENTIMETERS = 3000;
        private const int SPORTSCAR_MINHORSEPOWER = 250;
        private const int SPORTSCAR_MAXHORSEPOWER = 450;

        public SportsCar(string model, int horsePower) 
            : base(model, horsePower, SPORTSCAR_CUBICCENTIMETERS, SPORTSCAR_MINHORSEPOWER, SPORTSCAR_MAXHORSEPOWER)
        {

        }
    }
}
