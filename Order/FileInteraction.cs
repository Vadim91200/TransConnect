using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace TransConnect
{
    public abstract class FileInteraction
    {    
        public FileInteraction()
        {
            
        }
        public static List<List<string>> AccessFile()
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
                return Path;
            }
            catch (IOException e)
            {
                Console.WriteLine("Un error occured while trying to open Distances.csv file");
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) { sReader.Close(); }
            }
            return null;
        }
        public static List<string> CalculateDistance(Livraison l)
        {
            try
            {
                return FindTheSortestPath(AccessFile(), l);
            }
            
            catch (Exception e)
            {
                Console.WriteLine("Un error occured while trying to CalculateDistance");
                Console.WriteLine(e.Message);
            }
            
            return null;
        }
        private static List<string> FindTheSortestPath(List<List<string>> routes, Livraison l)
        {
            //Dijkstra algorithm
            // Dictionary to store the distances from the start city to each city
            var distances = new Dictionary<string, int>();

            // Dictionary to store the previous city in the shortest path to each city
            var previous = new Dictionary<string, string>();

            // List of cities that have not yet been processed
            var unprocessedCities = new List<string>();

            // Initialize the distances and previous values
            foreach (var route in routes)
            {
                var start = route[0];
                var end = route[1];
                var distance = int.Parse(route[2]);

                if (!distances.ContainsKey(start))
                {
                    distances[start] = int.MaxValue;
                    previous[start] = null;
                }

                if (!distances.ContainsKey(end))
                {
                    distances[end] = int.MaxValue;
                    previous[end] = null;
                }

                unprocessedCities.Add(start);
                unprocessedCities.Add(end);
            }

            // Set the distance from the start city to itself to 0
            distances[l.Departure] = 0;

            // Loop through the unprocessed cities until all cities have been processed
            while (unprocessedCities.Count > 0)
            {
                // Find the city with the smallest distance from the start city
                var currentCity = unprocessedCities.OrderBy(x => distances[x]).First();
                
                // Remove the current city from the unprocessed cities list
                unprocessedCities.Remove(currentCity);

                // If the current city is the end city, we have found the shortest path
                if (currentCity == l.Arrival)
                {
                    break;
                }

                // Find all of the routes from the current city
                var currentRoutes = routes.Where(x => x[0] == currentCity).ToList();

                // Update the distances to the neighboring cities
                foreach (var route in currentRoutes)
                {
                    var end = route[1];
                    var distance = int.Parse(route[2]);

                    var newDistance = distances[currentCity] + distance;
                    if (newDistance < distances[end])
                    {
                        distances[end] = newDistance;
                        previous[end] = currentCity;
                    }
                }
            }

            // Build the list of cities in the shortest path by starting at the end city and following the previous cities back to the start city
            var path = new List<string>();
            var current = l.Arrival;
            while (current != null)
            {
                path.Add(current);
                current = previous[current];
            }

            // Reverse the list so it starts at the start city
            path.Reverse();
            l.Distance = distances[l.Arrival];
            return path;
        }
        public static void AddNewCity()
        {
            StreamWriter sWriter = null;
            try
            {
                Console.WriteLine("Enter the new Start City");
                string CDeparture = IConvert.ConvertTo<string>(Console.ReadLine());
                Console.WriteLine("Enter the new Destination City");
                string CArrival = IConvert.ConvertTo<string>(Console.ReadLine());
                Console.WriteLine("Enter the distance betwen the two city");
                int distance = IConvert.ConvertTo<int>(Console.ReadLine());
                Console.WriteLine("Enter the duration of the route");
                string Durationv = IConvert.ConvertTo<string>(Console.ReadLine());
                FileStream fileStream = new FileStream("../../../Distances.csv", FileMode.Append, FileAccess.Write);

                sWriter = new StreamWriter(fileStream);
                sWriter.Write(string.Format("{0},{1},{2},{3}\n", CDeparture, CArrival, distance, Durationv));

            }
            catch (Exception e)
            {
                Console.WriteLine("Un error occured while trying to add new city");
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sWriter != null) sWriter.Close();
            }
        }
        public static void DisplayDistination()
        {
            AccessFile().ForEach(x => Console.WriteLine("Departure " + x[0] + " Destination " + x[1]));
        }
    }
}
