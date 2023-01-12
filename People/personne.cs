namespace TransConnect
{
    public abstract class Personne : IConvert
    {
        public long Nss { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        private DateTime dateNaissance;
        public string Postaladdress { get; set; }
        private string AdresseMail { get; set; }
        private int phone;
        public Personne(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone)
        {
            this.Nss = NSS;
            this.Surname = nom;
            this.Name = prenom;
            this.dateNaissance = dateNaissance;
            this.Postaladdress = adressePostale;
            this.AdresseMail = adresseMail;
            this.phone = telephone;
        }
        public string City { get => this.Postaladdress.Split(' ')[3]; }
        public override string ToString()
        {
            return this.Surname + " " + this.Name + " with the SSN " + this.Nss + " is born the " + this.dateNaissance + " live at " + this.Postaladdress + ". Is phone number is " + this.phone + " and email adress is " + this.AdresseMail;
        }
    }
}