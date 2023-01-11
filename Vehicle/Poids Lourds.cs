namespace TransConnect
{
    /// <summary>
    /// Abstract class representing a heavy vehicle
    /// </summary>
    public abstract class Poids_Lourds : Vehicule
    {
        private string type;
        /// <summary>
        /// Initializes a new instance of the <see cref="Poids_Lourds"/> class.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="nbrplace">The number of seats in the vehicle.</param>
        /// <param name="type">The type of the heavy vehicle</param>
        public Poids_Lourds(string model, int nbrplace, string type) :base(model, nbrplace)
        {
            this.type = type;
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object, in the format: "model is a type"</returns>
        public override string ToString()
        {
            return base.ToString() + " is a " + this.type;
        }
    }
}

