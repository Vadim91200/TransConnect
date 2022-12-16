using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    public class Entreprise
    {
        private List<Client> Clients;
        private List<Salarie> Salaries;
        private List<Vehicule> Vehicules;
        private List<Commande> Commandes;

        public Entreprise()
        {
            this.Clients = new List<Client>();
            this.Salaries = new List<Salarie>();
            this.Vehicules = new List<Vehicule>();
            this.Commandes = new List<Commande>();
        }

        public void Embauche(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone, DateTime dateEntree, string poste, int salaire)
        {
            this.Salaries.Add(new Salarie(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire));
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

        public void Commande(Client c, string départ, string arrivé, DateTime DateLivraison)
        {
            try
            {
                Livraison l = new Livraison(départ, arrivé, DateLivraison);

                Commande co = new Commande(c, l, Vehicules[0], AssignDriver(DateLivraison), DateTime.Now);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Chauffeur AssignDriver(DateTime DateLivraison)
        {
            foreach ( Salarie s in Salaries)
            {
                try
                {
                    Chauffeur c = s as Chauffeur;
                    c.EstLibre(DateLivraison) ? return c : continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }
    }
}