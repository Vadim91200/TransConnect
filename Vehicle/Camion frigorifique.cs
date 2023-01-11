namespace TransConnect
{
    /// <summary>
    /// Represents a refrigerated truck
    /// </summary>
    public class Camion_frigorifique : Poids_Lourds
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Camion_frigorifique"/> class.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="nbrplace">The number of seats in the vehicle.</param>
        /// <param name="type">The type of the heavy vehicle</param>
        public Camion_frigorifique(string model, int nbrplace, string type) : base(model, nbrplace, type)
        {
        }
    }
}

