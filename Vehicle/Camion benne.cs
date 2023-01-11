namespace TransConnect
{
    /// <summary>
    /// Represents a Camion_benne, a type of Poids_Lourds vehicle.
    /// </summary>
    public class Camion_benne : Poids_Lourds
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Camion_benne"/> class.
        /// </summary>
        /// <param name="model">The model of the Camion_benne.</param>
        /// <param name="nbrplace">The number of seats in the Camion_benne.</param>
        /// <param name="type">The type of Poids_Lourds vehicle.</param>
        public Camion_benne(string model, int nbrplace, string type) : base(model, nbrplace, type)
        {
        }
    }
}
