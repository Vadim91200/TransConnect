namespace TransConnect
{
    public class Commande
    {
        private Client client;
        private Livraison livraison;
        private double prix;
        private Vehicule vehicule;
        private Chauffeur chauffeur;
        private DateTime date;

        public Commande(Client client, Livraison livraison, Vehicule vehicule, Chauffeur chauffeur, DateTime date)
        {
            this.client = client;
            this.livraison = livraison;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.date = date;
        }
        public int Prix
        {
            get { return this.chauffeur.getTJ * 3; }
        }
    }
}
