namespace TransConnect
{
    public class Commande
    {
        private Client client;
        private Livraison deliverie;
        private double price;
        private Vehicule vehicule;
        private Chauffeur chauffeur;
        private DateTime commandedate;
        private int commandeID;

        public Commande(Client client, Livraison livraison, Vehicule vehicule, Chauffeur chauffeur, DateTime date)
        {
            this.client = client;
            this.deliverie = livraison;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.commandedate = date;
            this.commandeID = new Random().Next(100000, 999999);
        }
        public int Price
        {
            get { return this.chauffeur.getTJ * deliverie.Distance; }
        }
        public Client Client { get => client; }
        public int Id { get => commandeID; }
        public DateTime CommandeDate { get { return this.commandedate; } }
        public Livraison Deliverie { get => deliverie; }
        public void DisplayStatus()
        {
            Console.WriteLine("The order " + this.commandeID + " is in progress");
        }
    }
}
