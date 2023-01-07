namespace TransConnect
{
    public abstract class Poids_Lourds : Vehicule
    {
        private string type;

        public Poids_Lourds(string model, int nbrplace, string type) :base(model, nbrplace)
        {
            this.type = type;
        }
    }
}

