// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using TransConnect;


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
            ClientModule();
            break;
        case "2":
            EmployeeModule();
            break;
        case "3":
            OrderModule();
            break;
        case "4":
            StatisticsModule();
            break;
        case "5":
            Othermodule();
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
Entreprise ConfigureTheCompany()
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
void GetClientsList(Entreprise TC)
{
    string file = "../../../CompanyDetails/ClientsList.csv";
    StreamReader sReader = null;
    try
    {
        sReader = new StreamReader(file);
        string line;
        while ((line = sReader.ReadLine()) != null)
        {
            string[] words = line.Split(';');
            TC.AddClient(words);
        }
    }
    catch (IOException e)
    {
        using (FileStream fs = File.Create(file)) ;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        if (sReader != null) { sReader.Close(); }
    }
}
void GetEmployeeList(Entreprise TC)
{
    string file = "../../../CompanyDetails/EmployeeList.csv";
    StreamReader sReader = null;
    try
    {
        sReader = new StreamReader(file);
        string line;
        while ((line = sReader.ReadLine()) != null)
        {
            string[] words = line.Split(';');
            TC.Hire(words);
        }
    }
    catch (IOException e)
    {
        using (FileStream fs = File.Create(file)) ;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        if (sReader != null) { sReader.Close(); }
    }
}
void GetVehiclesList(Entreprise TC)
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
            TC.BuyVehicle(words);
        }
    }
    catch (IOException e)
    {
        using (FileStream fs = File.Create(file)) ;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        if (sReader != null) { sReader.Close(); }
    }
}
void GetCommandesList(Entreprise TC)
{
    string file = "../../../CompanyDetails/CommandesList.csv";
    StreamReader sReader = null;
    try
    {
        sReader = new StreamReader(file);
        string line;
        while ((line = sReader.ReadLine()) != null)
        {
            string[] words = line.Split(';');
            TC.Commande(words);
        }
    }
    catch (IOException e)
    {
        using (FileStream fs = File.Create(file)) ;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        if (sReader != null) { sReader.Close(); }
    }
}
void ClientModule()
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
            string enterclient;
            do
            {
                Console.WriteLine("Enter the client detail separte by a ; (Social security number; Surname; Name,; Date Of Birth; Postal Adress format (number street name city zipcode); Email Adress; Phone");
                enterclient = Console.ReadLine();
            } while (IsAValidInput(enterclient, TypeCode.String, 6));
            TransConnect.AddClient(enterclient.Split(';'));
            break;
        case "2":
            string enterNSS;
            do
            {
                Console.WriteLine("Enter the client social security number you want to erase");
                enterNSS = Console.ReadLine();
            } while (IsAValidInput(enterNSS, TypeCode.Int64, 1000000000000));
            TransConnect.SuppressionClient(Int64.Parse(enterNSS));
            break;
        case "3":
            string someinfo;
            do
            {
                Console.WriteLine("Enter the client social security number you want to modify");
                someinfo = Console.ReadLine();
            } while  (IsAValidInput(someinfo, TypeCode.Int64, 1000000000000));
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
void EmployeeModule()
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
            string[] enteredemployee;
            do
            {
                Console.WriteLine("Enter the employee detail separte by a ; (Social security number; Surname; Name; Date Of Birth; Postal Adress format (number street name city zipcode); Email Adress; Phone; date of hiring; position; salary");
                enteredemployee = Console.ReadLine().Split(';');
                // s.ConvertTo()
            } while (enteredemployee.Length != 6);
            TransConnect.Hire(enteredemployee);
            break;
        case "3":
            int firedemployee;
            do
            {
                Console.WriteLine("Enter the employee social security number you want to fire");
            } while (!int.TryParse(Console.ReadLine(), out firedemployee));
            TransConnect.Fire(firedemployee);
            break;
        default:
            Console.WriteLine("Error index not reconised");
            break;
    }
}
void OrderModule()
{
    string reponse = "";
    do
    {
        Console.WriteLine("Select what you want to do with the order module: ");
        Console.WriteLine("1 : Create a new order");
        Console.WriteLine("2 : Modify an order");
        Console.WriteLine("3 : Calculate the price of an order");
        reponse = Console.ReadLine();
    } while (Int32.Parse(reponse) > 3);
    switch (reponse)
    {
        case "1":
            string[] enteredorder;
            do
            {
                Console.WriteLine("Enter the detail of the order separte by a ; ( City of departure; City of arrival; Delivery Date; SSN of the client");
                enteredorder = Console.ReadLine().Split(';');
            } while (enteredorder.Length != 6);
            TransConnect.Commande(enteredorder);
            break;
        case "2":
            int orderid;
            do
            {
                Console.WriteLine("Enter the id of the order you want to modify");
            } while (!int.TryParse(Console.ReadLine(), out orderid));
            TransConnect.ModifyOrder(orderid);
            break;
        case "3":
            int porderid;
            do
            {
                Console.WriteLine("Enter the id of the order you want to display the price");
            } while (!int.TryParse(Console.ReadLine(), out porderid));
            TransConnect.DisplayOrderStatus(porderid);
            break;
        default:
            Console.WriteLine("Error index not reconised");
            break;
    }
}
void StatisticsModule()
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
            do
            {
                Console.WriteLine("Enter the time period separated by ; ( Begining Date; End Date");
                TimePeriod = Console.ReadLine().Split(';');
            } while (TimePeriod.Length != 2);
            TransConnect.DisplayAllTheOrdersByTimePeriod(DateTime.Parse(TimePeriod[0]), DateTime.Parse(TimePeriod[1]));
            break;
        case "3":
            TransConnect.ViewAverageOrderPrices();
            break;
        case "4":
            TransConnect.DisplayAverageCustomerAccounts();
            break;
        case "5":
            int CustomerName;
            do
            {
                Console.WriteLine("Enter the social security number of the custumer");
            } while (!int.TryParse(Console.ReadLine(), out CustomerName));
            TransConnect.DisplayTheListOfOrdersForACustomer(CustomerName);
            break;
        default:
            Console.WriteLine("Error index not reconised");
            break;
    }
}
void Othermodule()
{
    string reponse = "";
    do
    {
        Console.WriteLine("Select what you want to do with the other module: ");
        Console.WriteLine("1 : Not implemented");
        Console.WriteLine("2 : Not implemented");
        Console.WriteLine("3 : Not implemented");
        Console.WriteLine("4 : Not implemented");
        reponse = Console.ReadLine();
    } while (IsAValidInput(reponse, TypeCode.Int64, 4));
    switch (reponse)
    {
        case "1":
            //TransConnect;
            break;
        case "2":
            //TransConnect;
            break;
        case "3":
            //TransConnect;
            break;
        case "4":
            //TransConnect;
            break;
        default:
            Console.WriteLine("Error index not reconised");
            break;
    }
}
bool IsAValidInput(string consoleinput, TypeCode type, long lenght)
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