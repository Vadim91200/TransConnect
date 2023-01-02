using System.Xml.Linq;

namespace TransConnect
{
    public class Salarie: Personne
    {
        private DateTime dateEntree;
        private string title;
        private List<Salarie> directreports;
        private Salarie manager;
        private int salaire { get; set; }
        public Salarie(long NSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, int telephone, DateTime dateEntree, string poste, int salaire, Salarie manager) : base(NSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            this.dateEntree = dateEntree;
            this.title = poste;
            this.salaire = salaire;
            this.directreports = new List<Salarie>();
            this.manager = manager;
            this.manager.directreports.Add(this);
        }
        public string Title { get => title;}
        public List<Salarie> DirectReports { get => directreports; }
        public Salarie Manager { get => manager; set => manager = value; }
        public static Salarie CreateEmployeeFromInput(Dictionary<string, Salarie> employees)
        {
            string[] EmployeeDetails;
            Salarie EnteredEmployee = null;
            do
            {
                Console.WriteLine("Enter the employee detail separte by a ; (Social security number; Surname; Name; Date Of Birth; Postal Adress format (number street name city zipcode); Email Adress; Phone; date of hiring; position; salary; Manager Surname;'Leave empty'");
                EmployeeDetails = Console.ReadLine().Split(';');
                try
                {
                    EnteredEmployee = ParseFromArrayString(EmployeeDetails, employees);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (EnteredEmployee == null);
            return EnteredEmployee;
        }
        public static Salarie ParseFromArrayString(string[] ObjectDetails, Dictionary<string, Salarie> employees)
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
            string ManagerName = IConvert.ConvertTo<string>(ObjectDetails[10]);
            string[] directReportNames = IConvert.ConvertTo<string[]>(ObjectDetails.Skip(11).ToList());
            Salarie Manager = null;
            if (ManagerName != "")
            {
                Manager = employees[ManagerName];
            }
            Salarie employee = new Salarie(SSN, Surname, Name, DateBirth, PostalAdress, EmailAdress, Phone, DateHiring, Position, Salary, Manager);
            foreach (var directReportName in directReportNames)
            {
                Salarie directReport = employees[directReportName];
                directReport.Manager = employee;
                employee.DirectReports.Add(directReport);
            }
            employees[Surname] = employee;
            return employee;
        }
    }
}