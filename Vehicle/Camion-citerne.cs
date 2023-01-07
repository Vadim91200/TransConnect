namespace TransConnect
{
    public class Camion_citerne : Poids_Lourds
    {
        private Cuve cuve;
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

