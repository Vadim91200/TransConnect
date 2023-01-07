namespace TransConnect
{
    public class Chauffeur : Salarie
    {
        private int Tarif_Journalié;
        private List<Livraison> deliverieslist;
        public Chauffeur(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire, Salarie manager) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire, manager)
        {
            this.deliverieslist = new List<Livraison>();
        }
        public int getTJ
        {
            get { return this.Tarif_Journalié; }
        }
        public int NumberOfDeliveries
        {
            get { return this.deliverieslist.Count; }
        }
        public override string ToString()
        {
            return base.ToString() + "he has " + this.deliverieslist.Count + " deliveries" + " and a daily rate of " + this.Tarif_Journalié;
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