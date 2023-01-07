namespace TransConnect
{
    public abstract class Poids_Lourds : Vehicule
    {
        private string type;

        public Poids_Lourds(string model, int nbrplace, string type) :base(model, nbrplace)
        {
            this.type = type;
        }
        public override string ToString()
        {
            return base.ToString() + " is a " + this.type;
        }
    }
}

