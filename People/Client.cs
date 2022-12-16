namespace TransConnect
{
    public class Client: Personne
    {
        private List<Livraison> Livraisons;
        public Client(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.Livraisons = new List<Livraison>();
        }
    }
}