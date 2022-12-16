namespace TransConnect
{
    public class Livraison
    {
        private string PDepart;
        private string PArrive;
        private bool Estpaye;
        private DateTime durée;
        public Livraison(string PDepart, string PArrive)
        {
            this.PDepart = PDepart;
            this.PArrive = PArrive;
            this.Estpaye = false;
            this.durée = new DateTime();
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
            while (tmpDeparture != (this.PDepart))
            {
            }
        }
    }
}
