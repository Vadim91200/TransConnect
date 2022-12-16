namespace TransConnect
{
    public class Entreprise
    {
        private List<Client> Clients;
        private List<Chauffeur> Salaries;
        private List<Vehicule> Vehicules;
        private List<Commande> Commandes;

        public Entreprise()
        {
            this.Clients = new List<Client>();
            this.Salaries = new List<Chauffeur>();
            this.Vehicules = new List<Vehicule>();
            this.Commandes = new List<Commande>();
        }

        public void Embauche(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone, DateTime dateEntree, string poste, int salaire)
        {
            this.Salaries.Add(new Chauffeur(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire));
        }

        public void Virée(int NSS)
        {
            this.Salaries.Remove(this.Salaries.Find(salarie => salarie.NSS == NSS));
        }
        public void AjoutClient(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone)
        {
            this.Clients.Add(new Client(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone));
        }
        public void SuppressionClient(int NSS)
        {
            this.Clients.Remove(this.Clients.Find(client => client.NSS == NSS));
        }
        public void ModifierClient()
        {

        }
        public void AfficherClient()
        {
            foreach (Client client in this.Clients)
            {
                Console.WriteLine(client.ToString());
            }
        }

        public void Commande(Client c, string départ, string arrivé)
        {
            try
            {
                Livraison l = new Livraison(départ, arrivé);

                Commande co = new Commande(c, l, Vehicules[0], Salaries[0], DateTime.Now);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}