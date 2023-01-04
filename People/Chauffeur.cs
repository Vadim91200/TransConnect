namespace TransConnect
{
    public class Chauffeur : Salarie
    {
        private int Tarif_Journalié;
        private List<Livraison> deliverieslist;
        public Chauffeur(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire, Salarie manager) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire, manager)
        {
        }
        public int getTJ
        {
            get { return this.Tarif_Journalié; }
        }
        public int NumberOfDeliveries
        {
            get { return this.deliverieslist.Count; }
        }
        public bool EstLibre(DateTime d)
        {
            if (this.deliverieslist.Find(Livraison => Livraison.Deliverydate == d) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}