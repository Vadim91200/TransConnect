namespace TransConnect
{
    public class Salarie: Personne
    {
        private DateTime dateEntree;
        private string poste { get; set; }
        private int salaire { get; set; }

        public Salarie(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone, DateTime dateEntree, string poste, int salaire) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }
    }
}