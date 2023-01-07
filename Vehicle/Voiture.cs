namespace TransConnect
{
    public class Voiture : Vehicule
    {

        public Voiture(string model, int nbrplace) :base(model, nbrplace)
        {

        }
        public override string ToString()
        {
            return base.ToString() + " is a car";
        }
    }
}
