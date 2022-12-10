namespace TransConnect
{
    public class Camion_citerne : Poids_Lourds
    {
        private Cuve cuve;
        public Camion_citerne(string type, Cuve cuve) : base(type)
        {
            this.cuve = cuve;
        }
    }
}

