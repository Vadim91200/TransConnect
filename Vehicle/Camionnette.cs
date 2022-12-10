namespace TransConnect
{
    public class Camionnette : Vehicule
    {
        private string Usage;
        public Camionnette(string Usage)
        {
            this.Usage = Usage;
        }
        // {
        //Camionnette: dont l�usage est � pr�ciser. Exemple les artisans dans la vitrerie ont besoin d�un cadre sp�cifique 
        //pour transporter les verres.Seul l�usage sera � d�finir et pas les accessoires mis en cause.

    }
}
