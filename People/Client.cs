using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    /// <summary>
    /// The class represents a client with additional functionality such as a list of deliveries and a total amount of purchases
    /// </summary>
    public class Client : Personne
    {
        private List<Livraison> deliveries;
        private int totalAmount;
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class
        /// </summary>
        /// <param name="NSS">The Social Security Number of the client</param>
        /// <param name="nom">The last name of the client</param>
        /// <param name="prenom">The first name of the client</param>
        /// <param name="dateNaissance">The date of birth of the client</param>
        /// <param name="adressePostale">The postal address of the client</param>
        /// <param name="adresseMail">The email address of the client</param>
        /// <param name="telephone">The telephone number of the client</param>
        public Client(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.deliveries = new List<Livraison>();
            this.totalAmount = 0;
        }
        /// <summary>
        /// Returns a string that represents the current client
        /// </summary>
        /// <returns>A string containing the client's number of deliveries and total order amount</returns>
        public override string ToString()
        {
            return base.ToString() + " and has " + this.deliveries.Count + " deliveries" + " and a total order amount of " + AmountOfPurchase();
        }
        /// <summary>
        /// Gets the total amount of purchases made by the client
        /// </summary>
        /// <returns>The total amount of purchases</returns>
        public int AmountOfPurchase()
        {
            this.totalAmount = 0;
            this.deliveries.ForEach(elem => totalAmount += elem.Price);
            return totalAmount;
        }
        /// <summary>
        /// Sorts a list of clients in alphabetical order by last name, then first name
        /// </summary>
        /// <param name="a">The first client to compare</param>
        /// <param name="b">The second client to compare</param>
        /// <returns>1 if a is greater than b, -1 if b is greater than a, 0 if they are equal</returns>
        public static int AlphabeticalOrderSort(Client a, Client b)
        {
            if (a.Surname.CompareTo(b.Surname) > 0)
            {
                return 1;
            }
            else if (a.Surname.CompareTo(b.Surname) == 0)
            {
                return a.Name.CompareTo(b.Name);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Sorts a list of clients by city.
        /// </summary>
        /// <param name="a">The first client to compare</param>
        /// <param name="b">The second client to compare</param>
        /// <returns>1 if a is greater than b, -1 if b is greater than a, 1 if they are equal </returns>
        public static int CitySort(Client a, Client b)
        {
            if (a.City.CompareTo(b.City) > 0)
            {
                return 1;
            }
            else if (a.City.CompareTo(b.City) == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Sorts a list of clients by total amount of purchases made.
        /// </summary>
        /// <param name="a">The first client to compare</param>
        /// <param name="b">The second client to compare</param>
        /// <returns>1 if a is greater than b, -1 if b is greater than a, 1 if they are equal </returns>
        public static int AmountOfPurchaseSort(Client a, Client b)
        {
            if (a.AmountOfPurchase().CompareTo(b.AmountOfPurchase()) > 0)
            {
                return 1;
            }
            else if (a.AmountOfPurchase().CompareTo(b.AmountOfPurchase()) == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// Creates a Client object by prompting the user to enter client details and then appends that information to a CSV file.
        /// </summary>
        /// <returns>Returns the created Client object.</returns>
        public static Client CreateClientFromInput()
        {
            string[] ClientDetails;
            Client EnteredClient = null;
            do
            {
                Console.WriteLine("Enter the client detail separte by a ; (Social security number; Surname; Name; Date Of Birth(Same format as your computer date); Postal Adress format (number street name city zipcode); Email Adress; Phone");
                ClientDetails = Console.ReadLine().Split(';');
                StreamWriter sWriter = null;
                try
                {
                    EnteredClient = ParseFromArrayString(ClientDetails);
                    FileStream fileStream = new FileStream("../../../CompanyDetails/ClientsList.csv", FileMode.Append, FileAccess.Write);

                    sWriter = new StreamWriter(fileStream);
                    sWriter.Write(string.Format("{0};{1};{2};{3};{4};{5};{6} \n", ClientDetails[0], ClientDetails[1], ClientDetails[2], ClientDetails[3], ClientDetails[4], ClientDetails[5], ClientDetails[6]));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Un error occured while trying to add Client in a file");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sWriter != null) sWriter.Close();
                }
            } while (EnteredClient == null);
            return EnteredClient;
        }
        /// <summary>
        /// Parses an array of strings and creates a Client object from it.
        /// </summary>
        /// <param name="ClientDetails">An array of strings containing the client's social security number, surname, name, date of birth, postal address, email address, and phone number.</param>
        /// <returns>Returns a Client object created from the given array of strings.</returns>
        public static Client ParseFromArrayString(String[] ClientDetails)
        {
            long SSN = IConvert.ConvertTo<long>(ClientDetails[0]);
            string Surname = IConvert.ConvertTo<string>(ClientDetails[1]).Trim();
            string Name = IConvert.ConvertTo<string>(ClientDetails[2]).Trim();
            DateTime DateBirth = IConvert.ConvertTo<DateTime>(ClientDetails[3]);
            string PostalAdress = IConvert.ConvertTo<string>(ClientDetails[4]).Trim();
            string EmailAdress = IConvert.ConvertTo<string>(ClientDetails[5]).Trim();
            int Phone = IConvert.ConvertTo<int>(ClientDetails[6]);
            return new Client(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone);
        }
    }
}