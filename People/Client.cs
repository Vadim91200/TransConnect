namespace TransConnect
{
    public class Client : Personne
    {
        private List<Livraison> deliveries;
        private int TotalAmount;
        public Client(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.deliveries = new List<Livraison>();
            this.TotalAmount = 0;
        }
        public int AmountOfPurchase
        {
            set { this.deliveries.ForEach(elem => TotalAmount += elem.Price); }
            get { return TotalAmount; }
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
            if (a.AmountOfPurchase.CompareTo(b.AmountOfPurchase) > 0)
            {
                return 1;
            }
            else if (a.AmountOfPurchase.CompareTo(b.AmountOfPurchase) == 0)
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
                Console.WriteLine("Enter the client detail separte by a ; (Social security number; Surname; Name,; Date Of Birth(American format); Postal Adress format (number street name city zipcode); Email Adress; Phone");
                ClientDetails = Console.ReadLine().Split(';');
                try
                {
                    ParseFromArrayString(ClientDetails);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (EnteredClient == null);
            return EnteredClient;
        }
        public static Client ParseFromArrayString(String[] ClientDetails)
        {
            long SSN = IConvert.ConvertTo<long>(ClientDetails[0]);
            string Surname = IConvert.ConvertTo<string>(ClientDetails[1]);
            string Name = IConvert.ConvertTo<string>(ClientDetails[2]);
            DateTime DateBirth = IConvert.ConvertTo<DateTime>(ClientDetails[3]);
            string PostalAdress = IConvert.ConvertTo<string>(ClientDetails[4]);
            string EmailAdress = IConvert.ConvertTo<string>(ClientDetails[5]);
            int Phone = IConvert.ConvertTo<int>(ClientDetails[6]);
            return new Client(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone);
        }
    }
}