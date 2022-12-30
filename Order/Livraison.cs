namespace TransConnect
{
    public class Livraison
    {
        private string PDepart;
        private string PArrive;
        private int price;
        private bool ispaid;
        private DateTime duration;
        private DateTime deliverydate;
        private int distance;

        public DateTime Deliverydate { get => deliverydate; set => deliverydate = value; }
        public Livraison(string PDepart, string PArrive, int price, DateTime datelivraison)
        {
            this.PDepart = PDepart;
            this.PArrive = PArrive;
            this.price = price;
            this.ispaid = false;
            this.duration = new DateTime();
            this.deliverydate = datelivraison;
        }
        public int Distance
        {
            get { return this.distance; }
        }
        public int Price { get => price;}
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
            while (tmpDeparture != (this.PDepart))
            {
            }
        }
    }
}
