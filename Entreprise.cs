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
        public void Hire(string[] EmployeeInformation)
        {
            this.Salaries.Add(new Salarie(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire));
        }

        public void Fire(int NSS)
        {
            this.Salaries.Remove(this.Salaries.Find(salarie => salarie.NSS == NSS));
        }
        public void AddClient(string[] ClientInformation)
        {
            this.Clients.Add(new Client(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone));
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

        public void Commande(string[] OrderInformation)
        {
            try
            {

                Livraison l = new Livraison(départ, arrivé, DateLivraison);

                Commande co = new Commande(FindClient(clientNSS), l, Vehicules[0], AssignDriver(DateLivraison), DateTime.Now);
                this.Commandes.Add(co);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private Chauffeur AssignDriver(DateTime DateLivraison)
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
        private Client FindClient(string clientNSS)
        {
            foreach (Client c in Clients)
            {
                if (c.NSS == Int32.Parse(clientNSS)) ;
                {
                    return c;
                }
            }
            string[] info;
            do
            {
                Console.WriteLine("Creating new client enter all the information separte by a ';' (surname, name, date of birth, postal adresse, email, telephone");
                info = Console.ReadLine().Split(';');
            } while (info.Length != 6);
            return new Client(Int32.Parse(clientNSS), info[0], info[1], Convert.ToDateTime(info[2]), info[3], info[4], info[5]);
        }
        public void BuyVehicle(string[] VehicleInformation)
        {
            this.Salaries.Add(new Vehicule(VehicleInformation));
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