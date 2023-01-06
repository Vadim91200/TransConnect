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
        public long NSS { get => nss; }
        public string Name { get => this.name; }
        public string Surname { get => this.surname; }
        public string City { get => this.postaladdress.Split(' ')[3]; }
        public string PostalAddress { get => this.postaladdress; }
    }
}