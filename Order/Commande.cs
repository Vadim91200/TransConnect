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

        public Commande(Client client, Livraison livraison, Vehicule vehicule, Chauffeur chauffeur, DateTime date, int nbr)
        {
            this.client = client;
            this.deliverie = livraison;
            this.vehicule = vehicule;
            this.chauffeur = chauffeur;
            this.commandedate = date;
            this.commandeID = nbr;
        }
        public int Price { get => this.chauffeur.getTJ * deliverie.Distance; }
        public Client Client { get => client; }
        public int Id { get => commandeID; }
        public DateTime CommandeDate { get { return this.commandedate; } }
        public Livraison Deliverie { get => deliverie; }
        public Vehicule Vehicule { get => vehicule; }
        public Salarie Chauffeur { get => chauffeur; }
        public int CommandeID { get => commandeID; }
        public override string ToString()
        {
            return "The order " + this.CommandeID + "Made by " + this.Client.Surname + " for the delivery " + this.Deliverie.deliveredproduct + " with the vehicle " + this.Vehicule.ToString() + " and the driver " + this.Chauffeur.Surname + " on " + this.CommandeDate + " for a price of " + this.Price;
        }
        public void DisplayStatus()
        {
            if (this.deliverie.ispaid)
            {
                Console.WriteLine("The order nbr" + this.commandeID + " for" + this.deliverie.deliveredproduct + " is paid");
            }
            else
            {
                Console.WriteLine("The order " + this.commandeID + "    for the client " + this.client.Name + " who ordered " + this.deliverie.deliveredproduct + " is not paid" + " the delivery date is " + this.deliverie.Deliverydate);
            }
        }
    }
}
