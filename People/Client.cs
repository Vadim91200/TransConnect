using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    public class Client : Personne
    {
        private List<Livraison> deliveries;
        private int totalAmount;
        public Client(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.deliveries = new List<Livraison>();
            this.totalAmount = 0;
        }
        public override string ToString()
        {
            return base.ToString() + " and has " + this.deliveries.Count + " deliveries" + " and a total order amount of " + AmountOfPurchase();
        }
        public int AmountOfPurchase()
        {
            this.totalAmount = 0;
            this.deliveries.ForEach(elem => totalAmount += elem.Price);
            return totalAmount;
        }
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