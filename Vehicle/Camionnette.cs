namespace TransConnect
{
    public class Camionnette : Vehicule
    {
        private string Usage;
        public Camionnette(string model, int nbrplace, string Usage): base(model, nbrplace)
        {
            this.Usage = Usage;
        }
    }
}
