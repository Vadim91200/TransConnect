namespace TransConnect
{
    public class Camionnette : Vehicule
    {
        private string Usage;
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
