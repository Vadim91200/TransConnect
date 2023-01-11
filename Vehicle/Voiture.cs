namespace TransConnect
{
    /// <summary>
    /// Voiture class is a representation of a car and inherits from Vehicule class.
    /// </summary>
    public class Voiture : Vehicule
    {
        /// <summary>
        /// Creates a new instance of the Voiture class.
        /// </summary>
        /// <param name="model">The model of the car.</param>
        /// <param name="nbrplace">The number of seats in the car.</param>
        public Voiture(string model, int nbrplace) :base(model, nbrplace)
        {

        }
        /// <summary>
        /// Overrides the ToString() method to return a string that includes the information from the base class, as well as the fact that it is a car.
        /// </summary>
        /// <returns>A string that includes the model and number of seats in the car, as well as the fact that it is a car.</returns>
        public override string ToString()
        {
            return base.ToString() + " is a car";
        }
    }
}
