namespace TransConnect
{
    public abstract class Personne
    {
        private int NSS;
        private string nom { get; set; }
        private string prenom { get; set; }
        private DateTime dateNaissance;
        private string adressePostale { get; set; }
        private string adresseMail { get; set; }
        private string telephone { get; set; }
        public Personne(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone)
        {
            this.NSS = NSS;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.adressePostale = adressePostale;
            this.adresseMail = adresseMail;
            this.telephone = telephone;
        }
    }
}