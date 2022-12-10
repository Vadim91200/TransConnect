namespace TransConnect
{
    public class Livraison
    {
        private string PDepart;
        private string PArrive;
        private bool Estpaye;
        public Livraison(string PDepart, string PArrive, bool Estpaye)
        {
            this.PDepart = PDepart;
            this.PArrive = PArrive;
            this.Estpaye = Estpaye;
        }
    }
}
