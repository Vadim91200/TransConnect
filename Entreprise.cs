using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace TransConnect
{
    public class Entreprise
    {
        private List<Client> Clients;
        private OrganizationChart salaries;
        private List<Vehicule> vehicules;
        private List<Commande> Commandes;

        public Entreprise()
        {
            this.Clients = new List<Client>();
            this.salaries = new OrganizationChart(new Salarie(1801191547687, "Dupont", "Jean", new DateTime(1980, 11, 14), "12 rue des oliviers Bures-sur-Yvette 91440", "jean.dupon@transconnect.com", 0647875421, new DateTime(2022, 01, 01), "CEO", 1000000, null));
            this.vehicules = new List<Vehicule>();
            this.Commandes = new List<Commande>();
        }
        public OrganizationChart Salaries { get => salaries; }
        public List<Vehicule> Vehicules { get => vehicules;}
        public void DisplayOrganisationchart()
        {
            this.Salaries.DisplayOrganizationChart(this.Salaries.CEO);
        }
        public void Fire(long NSS)
        {
            var linesList = File.ReadAllLines("../../../CompanyDetails/EmployeeList.csv").ToList();
            foreach(string e in linesList)
            {
                if (e.Contains(NSS.ToString()))
                {
                    linesList.Remove(e);
                    break;
                }
            }
            File.WriteAllLines("../../../CompanyDetails/EmployeeList.csv", linesList.ToArray());
            this.Salaries.FireEmployee(NSS);
        }

        public void AddClient(Client client)
        {
            this.Clients.Add(client);
        }
        public void SuppressionClient(long NSS)
        {          
            var linesList = File.ReadAllLines("../../../CompanyDetails/ClientsList.csv").ToList();
            linesList.RemoveAt(this.Clients.FindIndex(client => client.NSS == NSS));
            File.WriteAllLines("../../../CompanyDetails/ClientsList.csv", linesList.ToArray());
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
                    sWriter.Write(string.Format("{0};{1};{2};{3};{4}\n", co.CommandeID, co.Client.NSS, co.Chauffeur.NSS, co.Vehicule.Id, co.CommandeDate));

                }
                catch (Exception e)
                {
                    Console.WriteLine("Un error occured while trying to add an Order in a file");
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
                    Console.WriteLine("Un error occured while trying to Assign a Driver");
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
        public Vehicule FindVehicle(int vnumber)
        {
            return this.Vehicules.Find(vehicle => vehicle.Id == vnumber);
        }
        public void BuyVehicle(Vehicule v)
        {
            this.Vehicules.Add(v);
        }
        public Vehicule AssignVehicule()
        {
            do
            {
                Console.WriteLine("What type of vehicle do your order need ?");
                string input = Console.ReadLine();
                if (input.ToUpper() == "VOITURE")
                {
                    foreach (Vehicule x in this.Vehicules)
                    {
                        Voiture v = x as Voiture;
                        if (v != null)
                        {
                            return v;
                        }
                    }
                }
                else if (input.ToUpper() == "CAMIONNETTE")
                {
                    foreach (Vehicule v in this.Vehicules)
                    {
                        Camionnette c = v as Camionnette;
                        if (c != null)
                        {
                            return c;
                        }
                    }
                }
                else if (input.ToUpper() == "CAMMIONCITERNE")
                {
                    foreach (Vehicule v in this.Vehicules)
                    {
                        Camion_citerne c = v as Camion_citerne;
                        if (c != null)
                        {
                            return c;
                        }
                    }
                }
                else if (input.ToUpper() == "CAMMIONFRIGORIFIQUE")
                {
                    foreach (Vehicule v in this.Vehicules)
                    {
                        Camion_frigorifique c = v as Camion_frigorifique;
                        if (c != null)
                        {
                            return c;
                        }
                    }
                }
                else if (input.ToUpper() == "CAMMIONBENNE")
                {
                    foreach (Vehicule v in this.Vehicules)
                    {
                        Camion_benne c = v as Camion_benne;
                        if (c != null)
                        {
                            return c;
                        }
                    }
                }
            } while (true);
        }
        public void CloseDelivery(int CommandeID)
        {
            this.Commandes.Find(commande => commande.CommandeID == CommandeID).Deliverie.Paid();
        }
        public void DisplayClientsByAlphabeticalOrder()
        {
            this.Clients.Sort(Client.AlphabeticalOrderSort);
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
                Console.WriteLine();
            }
        }
        public void DisplayClientsByCity()
        {
            this.Clients.Sort(Client.CitySort);
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
                Console.WriteLine();
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
                Console.WriteLine();
            }
        }
        public void DisplayNumberOfDeliveriesMadePerDriver()
        {
            foreach (Salarie s in this.Salaries.GetAllEmployees(this.Salaries.CEO))
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
                    Console.WriteLine("Un error occured while trying to display the number of deliveries made per driver");
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
                    Console.WriteLine();
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
                sum += c.AmountOfPurchase();
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