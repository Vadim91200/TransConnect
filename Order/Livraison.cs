namespace TransConnect
{
    public class Livraison : IConvert
    {
        private string departure;
        private string arrival;
        private int price;
        private bool ispaid;
        private TimeSpan duration;
        private DateTime deliverydate;
        private int distance;
        private DateTime Startingdate;
        public DateTime Deliverydate { get => deliverydate; set => deliverydate = value; }
        public Livraison(string PDepart, string PArrive, int price, DateTime datelivraison)
        {
            this.departure = PDepart;
            this.arrival = PArrive;
            this.price = price;
            this.ispaid = false;
            this.Startingdate = DateTime.Now;
            this.deliverydate = datelivraison;
        }
        public int Distance
        {
            get { return this.distance; }
        }
        public int Price { get => price;}
        public TimeSpan Duration { get => this.Startingdate - DateTime.Now; }
        public DateTime DeliveryDate { get => this.deliverydate; }
        public void Paid()
        {
            this.ispaid = true;
            this.duration = Startingdate - DateTime.Now;
        }
        public void CalculateDistance()
        {
            StreamReader sReader = null;
            try
            {
                sReader = new StreamReader("../../../Distances.csv");
                string line;
                List<List<string>> Path = new List<List<string>>(); 
                while ((line = sReader.ReadLine()) != null)
                {
                    Path.Add(line.Split(',').ToList());

                }
                FindTheSortestPath(Path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) { sReader.Close(); }
            }
        }
        private void FindTheSortestPath(List<List<string>> Path)
        {
            Console.WriteLine(Path.ToString);
            string Destination = Path[0][0];
            string tmpDeparture = Path[0][1];
            while (tmpDeparture != (this.departure))
            {
            }
        }
        public static Livraison CreateDeliveryFromInput()
        {
            string[] DeliveryDetails;
            Livraison EnteredDelivery= null;
            do
            {
                Console.WriteLine("Enter the detail of the delivery separte by a ; ( City of departure; City of arrival; Delivery Date");
                DeliveryDetails = Console.ReadLine().Split(';');
                try
                {
                    string departure = IConvert.ConvertTo<string>(DeliveryDetails[0]);
                    string arrival = IConvert.ConvertTo<string>(DeliveryDetails[1]);
                    int price = IConvert.ConvertTo<int>(DeliveryDetails[2]);
                    bool ispaid = IConvert.ConvertTo<bool>(DeliveryDetails[3]);
                    DateTime duration = IConvert.ConvertTo<DateTime>(DeliveryDetails[4]);
                    DateTime deliverydate = IConvert.ConvertTo<DateTime>(DeliveryDetails[5]);
                    int distance = IConvert.ConvertTo<int>(DeliveryDetails[6]);
                    EnteredDelivery = new Livraison(departure, arrival, price, deliverydate);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (EnteredDelivery == null);
            return EnteredDelivery;
        }
    }
}
