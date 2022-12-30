namespace TransConnect
{
    public abstract class Personne : IConvert
    {
        private long nss;
        private string name;
        private string surname;
        private DateTime dateNaissance;
        private string postaladdress;
        private string adresseMail { get; set; }
        private int phone;
        public Personne(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone)
        {
            this.nss = NSS;
            this.surname = nom;
            this.name = prenom;
            this.dateNaissance = dateNaissance;
            this.postaladdress = adressePostale;
            this.adresseMail = adresseMail;
            this.phone = telephone;
        }
        public long NSS
        {
            get { return this.nss; }
        }
        public string Name
        {
            get { return this.name; }
        }
        public string Surname
        {
            get { return this.surname; }
        }
        public string City
        {
            get { return this.postaladdress.Split(' ')[3]; }
        }
        public string PostalAddress
        {
            get { return this.postaladdress; }
        }
    }
}