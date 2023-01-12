namespace TransConnect
{
    /// <summary>
    /// The Commande class represents a transportation order made by a client for a delivery of a product.
    /// It contains information about the client, delivery, vehicle, and chauffeur assigned to the order, as well as the order date and ID.
    /// </summary>
    public class Commande
    {
        /// <summary>
        /// The client who made the order.
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// The delivery being made as part of the order.
        /// </summary>
        public Livraison Deliverie { get; set; }
        /// <summary>
        /// The price of the order.
        /// </summary>
        public double price;
        /// <summary>
        /// The vehicle assigned to the order.
        /// </summary>
        public Vehicule Vehicule { get; set; }
        /// <summary>
        /// The chauffeur assigned to the order.
        /// </summary>
        public Chauffeur Chauffeur { get; set; }
        /// <summary>
        /// The date the order was made.
        /// </summary>
        public DateTime Commandedate { get; set; }
        /// <summary>
        /// The ID of the order.
        /// </summary>
        public int CommandeID { get; set; }
        /// <summary>
        /// Initializes a new instance of the Commande class with the specified client, delivery, vehicle, chauffeur, date, and order ID.
        /// </summary>
        /// <param name="client">The client who made the order.</param>
        /// <param name="livraison">The delivery being made as part of the order.</param>
        /// <param name="vehicule">The vehicle assigned to the order.</param>
        /// <param name="chauffeur">The chauffeur assigned to the order.</param>
        /// <param name="date">The date the order was made.</param>
        /// <param name="nbr">The ID of the order.</param>
        public Commande(Client client, Livraison livraison, Vehicule vehicule, Chauffeur chauffeur, DateTime date, int nbr)
        {
            this.Client = client;
            this.Deliverie = livraison;
            this.Vehicule = vehicule;
            this.Chauffeur = chauffeur;
            this.Commandedate = date;
            this.CommandeID = nbr;
        }
        /// <summary>
        /// Gets the price of the order, calculated as the daily rate of the assigned chauffeur multiplied by the distance of the delivery.
        /// </summary>
        public int Price { get => this.Chauffeur.Daily_rate * Deliverie.Distance; }
        /// <summary>
        /// Returns a string that represents the current Commande object.
        /// </summary>
        /// <returns>A string that represents the current Commande object.</returns>
        public override string ToString()
        {
            return "The order " + this.CommandeID + "Made by " + this.Client.Surname + " for the delivery " + this.Deliverie.Deliveredproduct + " with the vehicle " + this.Vehicule.ToString() + " and the driver " + this.Chauffeur.Surname + " on " + this.Commandedate + " for a price of " + this.Price;
        }
        /// <summary>
        /// Displays the current status of the order, including whether or not the order has been paid and the delivery date.
        /// </summary>
        public void DisplayStatus()
        {
            if (this.Deliverie.ispaid)
            {
                Console.WriteLine("The order nbr" + this.CommandeID + " for" + this.Deliverie.Deliveredproduct + " is paid");
            }
            else
            {
                Console.WriteLine("The order " + this.CommandeID + "    for the client " + this.Client.Name + " who ordered " + this.Deliverie.Deliveredproduct + " is not paid" + " the delivery date is " + this.Deliverie.Deliverydate);
            }
        }
    }
}
