﻿// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using TransConnect;

namespace TransConnect;
internal class Program
{
    static void Main(string[] args)
    {
        Entreprise TransConnect = ConfigureTheCompany();
        string theresponce = "";
        while (theresponce.ToUpper() != "EXIT")
        {
            do
            {
                Console.WriteLine("Hello, welcome to the transconnect internal application. Select what action you want to perform: ");
                Console.WriteLine("1 : Client Module");
                Console.WriteLine("2 : Employee Module");
                Console.WriteLine("3 : Order Module");
                Console.WriteLine("4 : Statistics Module");
                Console.WriteLine("5 : Other module");
                Console.WriteLine("6 : Quit the application");
                theresponce = Console.ReadLine();
            } while (IsAValidInput(theresponce, TypeCode.Int64, 6));
            switch (theresponce)
            {
                case "1":
                    ClientModule(TransConnect);
                    break;
                case "2":
                    EmployeeModule(TransConnect);
                    break;
                case "3":
                    OrderModule(TransConnect);
                    break;
                case "4":
                    StatisticsModule(TransConnect);
                    break;
                case "5":
                    Othermodule(TransConnect);
                    break;
                case "6":
                    Console.WriteLine("Enter exit to quit the application");
                    theresponce = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Error index not reconised");
                    break;
            }
        }
    }

    /// <summary>
    /// This function creates a new Entreprise object and populates it with data from various files in the "CompanyDetails" directory.
    /// If the directory does not exist, it is created.
    /// </summary>
    /// <returns>Returns a fully configured Entreprise object.</returns>
    private static Entreprise ConfigureTheCompany()
    {
        Entreprise TC = new Entreprise();
        string path = "../../../CompanyDetails";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        GetClientsList(TC);
        GetEmployeeList(TC);
        GetVehiclesList(TC);
        GetCommandesList(TC);
        return TC;
    }
    /// <summary>
    /// This function reads from a file named "ClientsList.csv" and creates Client objects using the data in the file. 
    /// These objects are then added to the "TC" Entreprise object. If the file does not exist, it creates the file.
    /// </summary>
    /// <param name="TC">The Entreprise object where the newly created Client objects will be added to.</param>
    private static void GetClientsList(Entreprise TC)
    {
        string file = "../../../CompanyDetails/ClientsList.csv";
        StreamReader sReader = null;
        try
        {
            sReader = new StreamReader(file);
            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                TC.AddClient(Client.ParseFromArrayString(line.Split(';')));
            }
        }
        catch (IOException e)
        {
            using (FileStream fs = File.Create(file)) ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Un error occured while trying to add Client from a file");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sReader != null) { sReader.Close(); }
        }
    }
    /// <summary>
    /// This function reads from a file named "EmployeeList.csv" and creates Employee objects using the data in the file.
    /// These objects are added to the "employeesDictionary" dictionary and the "TC" Entreprise object. If the file does not exist, it creates the file.
    /// </summary>
    /// <param name="TC">The Entreprise object where the newly created Employee objects will be added to.</param>
    private static void GetEmployeeList(Entreprise TC)
    {
        string file = "../../../CompanyDetails/EmployeeList.csv";
        StreamReader sReader = null;
        try
        {
            sReader = new StreamReader(file);
            string line;
            Dictionary<string, Salarie> employeesDictionary = new Dictionary<string, Salarie>();
            employeesDictionary.Add(TC.Salaries.Ceo.Surname, TC.Salaries.Ceo);
            while ((line = sReader.ReadLine()) != null)
            {
                Salarie.ParseFromArrayString(line.Split(';'), employeesDictionary);
            }
        }
        catch (IOException e)
        {
            using (FileStream fs = File.Create(file)) ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Un error occured while trying to add an Employee from a file");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sReader != null) { sReader.Close(); }
        }
    }
    /// <summary>
    /// This function reads from a file named "VehiclesList.csv" and creates Vehicle objects using the data in the file. 
    /// These objects are then added to the "TC" Entreprise object. If the file does not exist, it creates the file.
    /// </summary>
    /// <param name="TC">The Entreprise object where the newly created Vehicle objects will be added to.</param>
    private static void GetVehiclesList(Entreprise TC)
    {
        string file = "../../../CompanyDetails/VehiclesList.csv";
        StreamReader sReader = null;
        try
        {
            sReader = new StreamReader(file);
            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                string[] words = line.Split(';');
                TC.BuyVehicle(Vehicule.ParseFromArrayString(words, true));
            }
        }
        catch (IOException e)
        {
            using (FileStream fs = File.Create(file)) ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Un error occured while trying to add Vehicle from a file");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sReader != null) { sReader.Close(); }
        }
    }
    /// <summary>
    /// This function reads from a file named "DeliveryList.csv" and searches for a delivery with the specified OrderID. 
    /// If a matching delivery is found, it is returned. If the file does not exist, it creates the file.
    /// </summary>
    /// <param name="OrderID">The ID of the delivery being searched for.</param>
    /// <returns>Returns a Livraison object with the specified OrderID if it is found, or null if no matching delivery is found.</returns>
    private static Livraison GetDeliveryList(int OrderID)
    {
        string file = "../../../CompanyDetails/DeliveryList.csv";
        StreamReader sReader = null;
        try
        {
            sReader = new StreamReader(file);
            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                Livraison livraison = Livraison.ParseFromArrayString(line.Split(';'), true);
                if (livraison.DeliveryID == OrderID)
                {
                    return livraison;
                }
            }
        }
        catch (IOException e)
        {
            using (FileStream fs = File.Create(file)) ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Un error occured while trying to parse à Delivery from a file");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sReader != null) { sReader.Close(); }
        }
        return null;
    }
    /// <summary>
    /// This function reads from a file named "CommandesList.csv" and creates Order objects using the data in the file. 
    /// These objects are then added to the "TC" Entreprise object. If the file does not exist, it creates the file.
    /// </summary>
    /// <param name="TC">The Entreprise object where the newly created Order objects will be added to.</param>
    private static void GetCommandesList(Entreprise TC)
    {
        string file = "../../../CompanyDetails/CommandesList.csv";
        StreamReader sReader = null;
        try
        {
            sReader = new StreamReader(file);
            string line;
            while ((line = sReader.ReadLine()) != null)
            {
                string[] OrderDetails = line.Split(';');
                Commande c = new Commande(TC.FindClient(Int64.Parse(OrderDetails[1])), GetDeliveryList(Int32.Parse(OrderDetails[0])), TC.FindVehicle(Int32.Parse(OrderDetails[3])), (Chauffeur)TC.Salaries.FindEmployeeBySocialSecurityNumber(Int64.Parse(OrderDetails[2]), TC.Salaries.Ceo), IConvert.ConvertTo<DateTime>(OrderDetails[4]), Int32.Parse(OrderDetails[0]));
                TC.PlaceOrder(c, false);
            }
        }
        catch (IOException e)
        {
            using (FileStream fs = File.Create(file)) ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Un error occured while trying to add an Order from a file");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sReader != null) { sReader.Close(); }
        }
    }
    /// <summary>
    /// This function provides the user with a menu to interact with the client module of the TransConnect Entreprise object.
    /// The user can add, delete, modify, and display clients in various ways.
    /// </summary>
    /// <param name="TransConnect">The Entreprise object containing the clients to be manipulated.</param>
    private static void ClientModule(Entreprise TransConnect)
    {
        string reponse = "";
        do
        {
            Console.WriteLine("Select what you want to do with the client module: ");
            Console.WriteLine("1 : Add a client");
            Console.WriteLine("2 : Delete a client");
            Console.WriteLine("3 : Modify a client");
            Console.WriteLine("4 : Display all the client by alphabetical order");
            Console.WriteLine("5 : Display all the client by city");
            Console.WriteLine("6 : Display all the client by amount of purchase");
            Console.WriteLine("7 : Display all the client by all the criters");
            reponse = Console.ReadLine();
        } while (IsAValidInput(reponse, TypeCode.Int64, 7));
        switch (reponse)
        {
            case "1":
                TransConnect.AddClient(Client.CreateClientFromInput());
                break;
            case "2":
                string enterNSS;
                do
                {
                    Console.WriteLine("Enter the client social security number you want to erase");
                    enterNSS = Console.ReadLine();
                } while (!IsAValidInput(enterNSS, TypeCode.Int64, 1000000000000));
                TransConnect.SuppressionClient(Int64.Parse(enterNSS));
                break;
            case "3":
                string someinfo;
                do
                {
                    Console.WriteLine("Enter the client social security number you want to modify");
                    someinfo = Console.ReadLine();
                } while (!IsAValidInput(someinfo, TypeCode.Int64, 1000000000000));
                TransConnect.ModifierClient(Int64.Parse(someinfo));
                break;
            case "4":
                TransConnect.DisplayClientsByAlphabeticalOrder();
                break;
            case "5":
                TransConnect.DisplayClientsByCity();
                break;
            case "6":
                TransConnect.DisplayClientsByAmountOfPurchase();
                break;
            case "7":
                TransConnect.DisplayClientsByAlphabeticalOrder();
                TransConnect.DisplayClientsByCity();
                TransConnect.DisplayClientsByAmountOfPurchase();
                break;
            default:
                Console.WriteLine("Error index not reconised");
                break;
        }
    }
    /// <summary>
    /// Employee module of the program. 
    /// Allows the user to display the organisation chart of the company, hire an employee or dismiss an employee.
    /// </summary>
    /// <param name="TransConnect">The current instance of the Entreprise class.</param>
    private static void EmployeeModule(Entreprise TransConnect)
    {
        string reponse = "";
        do
        {
            Console.WriteLine("Select what you want to do with the employee module: ");
            Console.WriteLine("1 : Display an organisation chart of the company");
            Console.WriteLine("2 : Hire an employee");
            Console.WriteLine("3 : dismiss an employee");
            reponse = Console.ReadLine();
        } while (IsAValidInput(reponse, TypeCode.Int64, 3));
        switch (reponse)
        {
            case "1":
                TransConnect.DisplayOrganisationchart();
                break;
            case "2":
                Dictionary<string, Salarie> employeesDictionary = new Dictionary<string, Salarie>();
                employeesDictionary.Add(TransConnect.Salaries.Ceo.Surname, TransConnect.Salaries.Ceo);
                TransConnect.Salaries.Ceo.Directreports.ForEach(x => employeesDictionary.Add(x.Surname, x));
                Salarie.CreateEmployeeFromInput(employeesDictionary);
                break;
            case "3":
                string firedemployee;
                do
                {
                    Console.WriteLine("Enter the employee social security number you want to fire");
                    firedemployee = Console.ReadLine();
                } while (!IsAValidInput(firedemployee, TypeCode.Int64, 1000000000000));
                TransConnect.Fire(Int64.Parse(firedemployee));
                break;
            default:
                Console.WriteLine("Error index not reconised");
                break;
        }
    }
    /// <summary>
    /// This function provides the user with a menu to interact with the order module of the TransConnect Entreprise object.
    /// The user can create, modify, and calculate the price of orders.
    /// </summary>
    /// <param name="TransConnect">The Entreprise object containing the orders to be manipulated.</param>
    private static void OrderModule(Entreprise TransConnect)
    {
        string reponse = "";
        do
        {
            Console.WriteLine("Select what you want to do with the order module: ");
            Console.WriteLine("1 : Create a new order");
            Console.WriteLine("2 : Modify an order");
            Console.WriteLine("3 : Calculate the price of an order");
            reponse = Console.ReadLine();
        } while (IsAValidInput(reponse, TypeCode.Int64, 3));
        switch (reponse)
        {
            case "1":
                Livraison l = Livraison.CreateDeliveryFromInput();
                string clientNSS;
                do
                {
                    Console.WriteLine("Enter the SSN of the client");
                    clientNSS = Console.ReadLine();
                } while (!IsAValidInput(clientNSS, TypeCode.Int64, 1000000000000));
                TransConnect.PlaceOrder(new Commande(TransConnect.FindClient(Int64.Parse(clientNSS)), l, TransConnect.AssignVehicule(), TransConnect.AssignDriver(l.Deliverydate), DateTime.Now, l.DeliveryID), true);
                break;
            case "2":
                string orderid;
                do
                {
                    Console.WriteLine("Enter the id of the order you want to modify");
                    orderid = Console.ReadLine();
                } while (IsAValidInput(orderid, TypeCode.Int64, 999999));
                TransConnect.ModifyOrder(Int32.Parse(orderid));
                break;
            case "3":
                string porderid;
                do
                {
                    Console.WriteLine("Enter the id of the order you want to display the price");
                    porderid = Console.ReadLine();
                } while (IsAValidInput(porderid, TypeCode.Int64, 999999));
                TransConnect.DisplayOrderStatus(Int32.Parse(porderid));
                break;
            default:
                Console.WriteLine("Error index not reconised");
                break;
        }
    }
    /// <summary>
    /// This function provides the user with a menu to interact with the statistics module of the TransConnect Entreprise object.
    /// The user can view the number of deliveries made per driver, display all orders within a time period, view average order prices, display average customer accounts, and view a list of orders for a customer.
    /// </summary>
    /// <param name="TransConnect">The Entreprise object containing the orders and customers to be analyzed.</param>
    private static void StatisticsModule(Entreprise TransConnect)
    {
        string reponse = "";
        do
        {
            Console.WriteLine("Select what you want to do with the statistics module: ");
            Console.WriteLine("1 : Display the number of deliveries made per driver");
            Console.WriteLine("2 : Display all the orders by time period");
            Console.WriteLine("3 : View average order prices");
            Console.WriteLine("4 : Display average customer accounts");
            Console.WriteLine("5 : Display the list of orders for a customer");
            reponse = Console.ReadLine();
        } while (Int32.Parse(reponse) > 5);
        switch (reponse)
        {
            case "1":
                TransConnect.DisplayNumberOfDeliveriesMadePerDriver();
                break;
            case "2":
                string[] TimePeriod;
                Nullable<DateTime> Beginning = null;
                Nullable<DateTime> End = null;
                do
                {
                    Console.WriteLine("Enter the time period separated by ; ( Beginning Date; End Date )");
                    TimePeriod = Console.ReadLine().Split(';');
                    try
                    {
                        Beginning = (DateTime)Convert.ChangeType(TimePeriod[0], TypeCode.DateTime);
                        End = (DateTime)Convert.ChangeType(TimePeriod[1], TypeCode.DateTime);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in the convertion of the dates");
                        Console.WriteLine(e.Message);
                    }
                } while (Beginning == null || End == null);
                TransConnect.DisplayAllTheOrdersByTimePeriod((DateTime)Beginning, (DateTime)End);
                break;
            case "3":
                TransConnect.ViewAverageOrderPrices();
                break;
            case "4":
                TransConnect.DisplayAverageCustomerAccounts();
                break;
            case "5":
                string CustomerSSN;
                do
                {
                    Console.WriteLine("Enter the social security number of the custumer");
                    CustomerSSN = Console.ReadLine();
                } while (!IsAValidInput(CustomerSSN, TypeCode.Int64, 1000000000000));
                TransConnect.DisplayTheListOfOrdersForACustomer(Int64.Parse(CustomerSSN));
                break;
            default:
                Console.WriteLine("Error index not reconised");
                break;
        }
    }
    /// <summary>
    /// This function provides various options for interacting with the "other" module of the TransConnect program.
    /// Users can add a new vehicle, display a list of vehicles, display a list of available destinations, add a new destination,
    /// or mark an order as paid.
    /// </summary>
    /// <param name="TransConnect">The current instance of the TransConnect program being used.</param>
    private static void Othermodule(Entreprise TransConnect)
    {
        string reponse = "";
        do
        {
            Console.WriteLine("Select what you want to do with the other module: ");
            Console.WriteLine("1 : Add a new vehicle");
            Console.WriteLine("2 : Display the list of vehicles");
            Console.WriteLine("3 : Display the list of available destination");
            Console.WriteLine("4 : Add a new destination");
            Console.WriteLine("5 : Mark an order as paid");
            reponse = Console.ReadLine();
        } while (IsAValidInput(reponse, TypeCode.Int64, 5));
        switch (reponse)
        {
            case "1":
                TransConnect.BuyVehicle(Vehicule.CreateVehicleFromInput());
                break;
            case "2":
                Console.WriteLine("The application has the following vehicles: ");
                TransConnect.Vehicules.ForEach(x => Console.WriteLine(x.ToString()));
                break;
            case "3":
                FileInteraction.DisplayDistination();
                break;
            case "4":
                FileInteraction.AddNewCity();
                break;
            case "5":
                string OrderID;
                do
                {
                    Console.WriteLine("Enter the id of the order you want to mark as paid");
                    OrderID = Console.ReadLine();
                } while (IsAValidInput(OrderID, TypeCode.Int64, 999999));
                TransConnect.CloseDelivery(Int32.Parse(OrderID));
                break;
            default:
                Console.WriteLine("Error index not reconised");
                break;
        }
    }
    /// <summary>
    /// This function checks whether the input provided by the user is a valid input or not.
    /// </summary>
    /// <param name="consoleinput">The input provided by the user as a string.</param>
    /// <param name="type">The expected type of the input. Can be either a string or an integer.</param>
    /// <param name="lenght">The length of the input. This can be either the number of elements in a string separated by semicolons, or the maximum value allowed for an integer input.</param>
    /// <returns>True if the input is valid, false otherwise.</returns>
    private static bool IsAValidInput(string consoleinput, TypeCode type, long lenght)
    {
        if (type == TypeCode.Int64)
        {
            return (Int64.TryParse(consoleinput, out long tmp) && Int64.Parse(consoleinput) > lenght);
        }
        else if (type == TypeCode.String)
        {
            return (consoleinput.Split(';').Length == lenght);
        }
        return false;
    }
}