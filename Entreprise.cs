namespace TransConnect
{
    /// <summary>
    /// Represents a transportation company.
    /// </summary>
    public class Entreprise
    {
        private List<Client> Clients;
        private OrganizationChart salaries;
        private List<Vehicule> vehicules;
        private List<Commande> Commandes;
        /// <summary>
        /// Initializes a new instance of the <see cref="Entreprise"/> class.
        /// </summary>
        public Entreprise()
        {
            this.Clients = new List<Client>();
            this.salaries = new OrganizationChart(new Salarie(1801191547687, "Dupont", "Jean", new DateTime(1980, 11, 14), "12 rue des oliviers Bures-sur-Yvette 91440", "jean.dupon@transconnect.com", 0647875421, new DateTime(2022, 01, 01), "CEO", 1000000, null));
            this.vehicules = new List<Vehicule>();
            this.Commandes = new List<Commande>();
        }
        /// <summary>
        /// Gets the organization chart of the company.
        /// </summary>
        public OrganizationChart Salaries { get => salaries; }
        /// <summary>
        /// Gets the list of vehicles of the company.
        /// </summary>
        public List<Vehicule> Vehicules { get => vehicules; }
        /// <summary>
        /// Displays the organization chart of the company.
        /// </summary>
        public void DisplayOrganisationchart()
        {
            this.Salaries.DisplayOrganizationChart(this.Salaries.CEO);
        }
        /// <summary>
        /// Fires an employee from the company.
        /// </summary>
        /// <param name="NSS">The national social security number of the employee to fire.</param>
        public void Fire(long NSS)
        {
            var linesList = File.ReadAllLines("../../../CompanyDetails/EmployeeList.csv").ToList();
            foreach (string e in linesList)
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
        /// <summary>
        /// Adds a new client to the company.
        /// </summary>
        /// <param name="client">The new client to add.</param>
        public void AddClient(Client client)
        {
            this.Clients.Add(client);
        }
        /// <summary>
        /// Removes a client from the company's list of clients.
        /// </summary>
        /// <param name="NSS">The National Social Security number of the client to remove.</param>
        public void SuppressionClient(long NSS)
        {
            var linesList = File.ReadAllLines("../../../CompanyDetails/ClientsList.csv").ToList();
            linesList.RemoveAt(this.Clients.FindIndex(client => client.NSS == NSS));
            File.WriteAllLines("../../../CompanyDetails/ClientsList.csv", linesList.ToArray());
            this.Clients.Remove(this.Clients.Find(client => client.NSS == NSS));
        }
        /// <summary>
        /// Modifies the information of a client in the company's list of clients.
        /// </summary>
        /// <param name="NSS">The National Social Security number of the client to modify.</param>
        public void ModifierClient(long NSS)
        {

        }
        /// <summary>
        /// Displays the information of all clients in the company's list of clients.
        /// </summary>
        public void AfficherClient()
        {
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
            }
        }
        /// <summary>
        /// Adds a new order to the company's list of orders and writes it to a file.
        /// </summary>
        /// <param name="co">The order to add.</param>
        /// <param name="insert">A boolean value indicating whether the order should be written to the file or not.</param>
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
        /// <summary>
        /// Assigns a driver to a delivery.
        /// </summary>
        /// <param name="DateLivraison">The date of the delivery.</param>
        /// <returns>A driver that is available on the specified date, or null if no such driver is found.</returns>
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
        /// <summary>
        /// Finds a client with the specified National Social Security number in the company's list of clients.
        /// </summary>
        /// <param name="clientNSS">The National Social Security number of the client to find.</param>
        /// <returns>The found client, or a new client if no such client is found.</returns>
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
        /// <summary>
        /// Finds a vehicle in the list of vehicles owned by the company.
        /// </summary>
        /// <param name="vnumber">The id number of the vehicle to find.</param>
        /// <returns>The vehicle with the specified id number, or null if no such vehicle was found.</returns>
        public Vehicule FindVehicle(int vnumber)
        {
            return this.Vehicules.Find(vehicle => vehicle.Id == vnumber);
        }
        /// <summary>
        /// Adds a new vehicle to the list of vehicles owned by the company.
        /// </summary>
        /// <param name="v">The vehicle to add to the list.</param>
        public void BuyVehicle(Vehicule v)
        {
            this.Vehicules.Add(v);
        }
        /// <summary>
        /// Assigns a vehicle to fulfill an order based on the type of vehicle requested by the customer.
        /// </summary>
        /// <returns>The assigned vehicle, or null if no suitable vehicle was found.</returns>
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
        /// <summary>
        /// Closes a delivery by marking it as paid.
        /// </summary>
        /// <param name="CommandeID">The ID number of the order associated with the delivery.</param>
        public void CloseDelivery(int CommandeID)
        {
            this.Commandes.Find(commande => commande.CommandeID == CommandeID).Deliverie.Paid();
        }
        /// <summary>
        /// Displays the company's list of clients sorted in alphabetical order.
        /// </summary>
        public void DisplayClientsByAlphabeticalOrder()
        {
            this.Clients.Sort(Client.AlphabeticalOrderSort);
            AfficherClient();
        }

        /// <summary>
        /// Displays the company's list of clients sorted by city.
        /// </summary>
        public void DisplayClientsByCity()
        {
            this.Clients.Sort(Client.CitySort);
            AfficherClient();
        }
        /// <summary>
        /// Modifies the delivery date of an order.
        /// </summary>
        /// <param name="orderNumber">The ID number of the order to modify.</param>
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
        /// <summary>
        /// Displays the status of an order.
        /// </summary>
        /// <param name="orderid">The ID number of the order to display.</param>
        public void DisplayOrderStatus(int orderid)
        {
            this.Commandes.Find(Commande => Commande.Id == orderid).DisplayStatus();
        }
        /// <summary>
        /// Displays the clients sorted by amount of purchase in ascending order.
        /// </summary>
        public void DisplayClientsByAmountOfPurchase()
        {
            this.Clients.Sort(Client.AmountOfPurchaseSort);
            AfficherClient();
        }
        /// <summary>
        /// Displays the number of deliveries made by each driver.
        /// </summary>
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
        /// <summary>
        /// Displays all the orders made within a given time period.
        /// </summary>
        /// <param name="start">The start date of the time period.</param>
        /// <param name="end">The end date of the time period.</param>
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
        /// <summary>
        /// Displays the average price of all the orders.
        /// </summary>
        public void ViewAverageOrderPrices()
        {
            double sum = 0;
            foreach (Commande c in this.Commandes)
            {
                sum += c.Price;
            }
            Console.WriteLine("The average price of the orders is " + sum / this.Commandes.Count);
        }
        /// <summary>
        /// Displays the average amount of purchase made by all the clients.
        /// </summary>
        public void DisplayAverageCustomerAccounts()
        {
            double sum = 0;
            foreach (Client c in this.Clients)
            {
                sum += c.AmountOfPurchase();
            }
            Console.WriteLine("The average price of the orders is " + sum / this.Clients.Count);
        }
        /// <summary>
        /// Displays the list of orders made by a particular client.
        /// </summary>
        /// <param name="NSS">The National Social Security Number of the client.</param>
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