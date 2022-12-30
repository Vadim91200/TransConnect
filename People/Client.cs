namespace TransConnect
{
    public class Client : Personne
    {
        private List<Livraison> deliveries;
        private int TotalAmount;
        public Client(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
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
    }
}