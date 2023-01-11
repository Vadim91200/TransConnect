namespace TransConnect
{
    /// <summary>
    /// Represents a tanker truck
    /// </summary>
    public class Camion_citerne : Poids_Lourds
    {
        private Cuve cuve;
        /// <summary>
        /// Initializes a new instance of the <see cref="Camion_citerne"/> class.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="nbrplace">The number of seats in the vehicle.</param>
        /// <param name="type">The type of the heavy vehicle</param>
        /// <param name="cuve">The Tanker tank of the truck</param>
        public Camion_citerne(string model, int nbrplace, string type, Cuve cuve) : base(model, nbrplace, type)
        {
            this.cuve = cuve;
        }
        public override string ToString()
        {
            return base.ToString() + " with a " + this.cuve.ToString();
        }
    }
}

