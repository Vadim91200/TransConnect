using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    public class Entreprise
    {
        private List<Client> Clients;
        private List<Salarie> Salaries;
        private List<Vehicule> Vehicules;
        private List<Commande> Commandes;

        public Entreprise()
        {
            this.Clients = new List<Client>();
            this.Salaries = new List<Salarie>();
            this.Vehicules = new List<Vehicule>();
            this.Commandes = new List<Commande>();
        }
        public void DisplayOrganisationchart()
        {
            Console.WriteLine("Not yey implemented");
        }
        public void Hire(Salarie s)
        {
            this.Salaries.Add(s);
        }

        public void Fire(int NSS)
        {
            this.Salaries.Remove(this.Salaries.Find(salarie => salarie.NSS == NSS));
        }
        
        public void AddClient(Client client)
        {
            this.Clients.Add(client);
        }
        public void SuppressionClient(long NSS)
        {
            this.Clients.Remove(this.Clients.Find(client => client.NSS == NSS));
        }
        public void ModifierClient(long NSS)
        {

        }
        public void AfficherClient()
        {
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
            }
        }

        public void PlaceOrder(Commande co)
        {
            this.Commandes.Add(co);
        }
        public Chauffeur AssignDriver(DateTime DateLivraison)
        {
            foreach (Salarie s in Salaries)
            {
                try
                {
                    Chauffeur c = s as Chauffeur;
                    if (c.EstLibre(DateLivraison))
                    {
                        return c;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }
        public Client FindClient(int clientNSS)
        {
            foreach (Client c in Clients)
            {
                if (c.NSS == clientNSS) ;
                {
                    return c;
                }
            }
            Console.WriteLine("Client not found");
            return Client.CreateClientFromInput();
        }
        public void BuyVehicle(string[] VehicleInformation)
        {
            //this.Salaries.Add(new Vehicule(VehicleInformation));
        }
        public void DisplayClientsByAlphabeticalOrder()
        {
            this.Clients.Sort(Client.AlphabeticalOrderSort);
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
            }
        }
        public void DisplayClientsByCity()
        {
            this.Clients.Sort(Client.CitySort);
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
            }
        }
        public void ModifyOrder(int orderNumber)
        {
            Commande c = this.Commandes.Find(commande => commande.Id == orderNumber);
            if (c != null)
            {
                Console.WriteLine("Enter the new date of delivery");
                c.Deliverie.Deliverydate = Convert.ToDateTime(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Order not found");
            }
        }
        public void DisplayOrderStatus(int orderid)
        {
            this.Commandes.Find(Commande => Commande.Id == orderid).DisplayStatus();
        }
        public void DisplayClientsByAmountOfPurchase()
        {
            this.Clients.Sort(Client.AmountOfPurchaseSort);
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
            }
        }
        public void DisplayNumberOfDeliveriesMadePerDriver()
        {
            foreach (Salarie s in this.Salaries)
            {
                try
                {
                    Chauffeur c = s as Chauffeur;
                    Console.WriteLine(c.Name + " as done " + c.NumberOfDeliveries + " deliveries");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void DisplayAllTheOrdersByTimePeriod(DateTime start, DateTime end)
        {
            foreach (Commande c in this.Commandes)
            {
                if (c.CommandeDate >= start && c.CommandeDate <= end)
                {
                    Console.WriteLine(c.ToString());
                }
            }
        }
        public void ViewAverageOrderPrices()
        {
            double sum = 0;
            foreach (Commande c in this.Commandes)
            {
                sum += c.Price;
            }
            Console.WriteLine("The average price of the orders is " + sum / this.Commandes.Count);
        }
        public void DisplayAverageCustomerAccounts()
        {
            double sum = 0;
            foreach (Client c in this.Clients)
            {
                sum += c.AmountOfPurchase;
            }
            Console.WriteLine("The average price of the orders is " + sum / this.Clients.Count);
        }
        public void DisplayTheListOfOrdersForACustomer(int NSS)
        {
            Console.WriteLine("The customer " + this.Clients.Find(client => client.NSS == NSS).Name + " has made the following orders :");
            foreach (Commande c in this.Commandes)
            {
                if (c.Client.NSS == NSS)
                {
                    Console.WriteLine(c.ToString());
                }
            }
        }
    }
}