namespace TransConnect
{
    /// <summary>
    /// Represent the base class for all types of Cuve
    /// </summary>
    public abstract class Cuve
    {
        private string Cuvetype;
        /// <summary>
        /// Initializes a new instance of the <see cref="Cuve"/> class.
        /// </summary>
        /// <param name="type">The type of Cuve</param>
        public Cuve(string type)
        {
            this.Cuvetype = type;
        }
        /// <summary>
        /// Get the string representation of the Cuve
        /// </summary>
        /// <returns>Returns the Cuve type</returns>
        public override string ToString()
        {
            return this.Cuvetype;
        }
    }
}
