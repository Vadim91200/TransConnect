using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    public class Entreprise
    {
        private List<Client> Clients;
        private OrganizationChart salaries;
        private List<Vehicule> Vehicules;
        private List<Commande> Commandes;

        public Entreprise()
        {
            this.Clients = new List<Client>();
            this.salaries = new OrganizationChart(new Salarie(1801191547687, "Dupont", "Jean", new DateTime(1980,11,14), "12 rue des oliviers Bures-sur-Yvette 91440", "jean.dupon@transconnect.com", 0647875421,  new DateTime(2022,01,01), "CEO", 1000000, null));
            this.Vehicules = new List<Vehicule>();
            this.Commandes = new List<Commande>();
        }
        public OrganizationChart Salaries { get => salaries; }
        public void DisplayOrganisationchart()
        {
            this.Salaries.DisplayOrganizationChart(this.Salaries.CEO);
        }
        public void Fire(long NSS)
        {
            this.Salaries.FireEmployee(NSS);
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

        public void PlaceOrder(Commande co, bool insert)
        {
            this.Commandes.Add(co);
            if (insert)
            {
                StreamWriter sWriter = null;
                try
                {
                    FileStream fileStream = new FileStream("../../../CompanyDetails/CommandesList.csv", FileMode.Append, FileAccess.Write);

                    sWriter = new StreamWriter(fileStream);
                    sWriter.Write(string.Format("{0};{1};{2};{3}\n", co.CommandeID, co.Client.NSS, co.Chauffeur.NSS, co.CommandeDate));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sWriter != null) sWriter.Close();
                }
            }
        }
        public Chauffeur AssignDriver(DateTime DateLivraison)
        {
            foreach (Salarie s in this.Salaries.GetAllEmployees(this.Salaries.CEO))
            {
                try
                {
                    Chauffeur c = s as Chauffeur;
                    if (c != null)
                    {
                        if (c.EstLibre(DateLivraison))
                        {
                            return c;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return null;
        }
        public Client FindClient(long clientNSS)
        {
            foreach (Client c in this.Clients)
            {
                if (c.NSS == clientNSS)
                {
                    return c;
                }
            }
            Console.WriteLine("Client not found, creating new client");
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
            foreach (Salarie s in this.Salaries.CEO.DirectReports)
            {
                try
                {
                    Chauffeur c = s as Chauffeur;
                    if (c != null)
                    {
                        Console.WriteLine(c.Name + " as done " + c.NumberOfDeliveries + " deliveries");
                    }
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
        public void DisplayTheListOfOrdersForACustomer(long NSS)
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