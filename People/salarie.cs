namespace TransConnect
{
    public class Salarie: Personne
    {
        private DateTime dateEntree;
        private string poste { get; set; }
        private int salaire { get; set; }

        public Salarie(int NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.dateEntree = dateEntree;
            this.poste = poste;
            this.salaire = salaire;
        }
        public static Salarie CreateEmployeeFromInput()
        {
            string[] EmployeeDetails;
            Salarie EnteredEmployee = null;
            do
            {
                Console.WriteLine("Enter the employee detail separte by a ; (Social security number; Surname; Name; Date Of Birth; Postal Adress format (number street name city zipcode); Email Adress; Phone; date of hiring; position; salary");
                EmployeeDetails = Console.ReadLine().Split(';');
                try
                {
                    ParseFromArrayString(EmployeeDetails);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (EnteredEmployee == null);
            return EnteredEmployee;
        }
        public static Salarie ParseFromArrayString(String[] ObjectDetails)
        {
            int SSN = IConvert.ConvertTo<int>(ObjectDetails[0]);
            string Surname = IConvert.ConvertTo<string>(ObjectDetails[1]);
            string Name = IConvert.ConvertTo<string>(ObjectDetails[2]);
            DateTime DateBirth = IConvert.ConvertTo<DateTime>(ObjectDetails[3]);
            string PostalAdress = IConvert.ConvertTo<string>(ObjectDetails[4]);
            string EmailAdress = IConvert.ConvertTo<string>(ObjectDetails[5]);
            int Phone = IConvert.ConvertTo<int>(ObjectDetails[6]);
            DateTime DateHiring = IConvert.ConvertTo<DateTime>(ObjectDetails[7]);
            string Position = IConvert.ConvertTo<string>(ObjectDetails[8]);
            int Salary = IConvert.ConvertTo<int>(ObjectDetails[9]);
            return new Salarie(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone, DateHiring, Position, Salary);

        }
    }
}