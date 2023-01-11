namespace TransConnect
{
    /// <summary>
    /// Represents a van
    /// </summary>
    public class Camionnette : Vehicule
    {
        private string Usage;
        /// <summary>
        /// Initializes a new instance of the <see cref="Camionnette"/> class.
        /// </summary>
        /// <param name="model">The model of the vehicle.</param>
        /// <param name="nbrplace">The number of seats in the vehicle.</param>
        /// <param name="Usage">The usage of the van</param>
        public Camionnette(string model, int nbrplace, string Usage): base(model, nbrplace)
        {
            this.Usage = Usage;
        }
        public override string ToString()
        {
            return base.ToString() + " is a van that is use for " + this.Usage;
        }
    }
}
