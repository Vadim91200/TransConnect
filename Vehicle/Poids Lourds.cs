namespace TransConnect
{
    public abstract class Poids_Lourds : Vehicule
    {
        private string type;

        public Poids_Lourds(string type)
        {
            this.type = type;
        }
    }
}
