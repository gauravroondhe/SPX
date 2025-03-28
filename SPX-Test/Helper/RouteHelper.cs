using SPX_Test.Extensions;
using SPX_Test.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPX_Test.Helper
{
    public class RouteHelper
    {
        List<RouteModel> routes = new List<RouteModel>();

        public List<RouteModel> GetRoutes()
        {
            try
            {
                StreamReader sr = new StreamReader("Input.txt");

                string line = sr.ReadLine();

                while (line != null)
                {
                    RouteModel routeModel = GetRouteDetails(line.Split(','));
                    routes.Add(routeModel);
                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return routes;
        }

        public int CountTripsByDistance(string source, string destination, int distance, int maxDistance)
        {
            if (distance >= maxDistance) return 0;
            int count = (source == destination && distance > 0) ? 1 : 0;

            foreach (var route in routes.Where(r => r.Source == source))
            {
                count += CountTripsByDistance(route.Destination, destination, distance + route.Distance, maxDistance);
            }
            return count;
        }

        public int CountTripsExact(string current, string end, int stops, int exactStops)
        {
            if (stops > exactStops) return 0;
            if (stops == exactStops && current == end) return 1;

            int count = 0;
            foreach (var route in routes.Where(r => r.Source == current))
            {
                count += CountTripsExact(route.Destination, end, stops + 1, exactStops);
            }
            return count;
        }

        public int CountTrips(string source, string destination, int stops, int maxStops, bool allowLess)
        {
            if (stops > maxStops) return 0;
            int count = (source == destination && stops > 0) ? 1 : 0;

            foreach (var route in routes.Where(a => a.Source.Equals(source)))
            {
                count += CountTrips(route.Destination, destination, stops + 1, maxStops, allowLess);
            }

            return count;
        }

        public int CalculateDistance(string routePath)
        {
            try
            {
                if (string.IsNullOrEmpty(routePath))
                {
                    return -1;
                }

                List<DistanceRouteModel> distances = routePath.Split(',').ToDistanceRouteDetail();

                var totalDistance = 0;
                foreach (DistanceRouteModel distance in distances)
                {
                    RouteModel route = GetRoute(distance.Source, distance.Destination);
                    if (route != null)
                        totalDistance += route.Distance;
                }

                return totalDistance;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public string CheckRouteDistance(string routePath)
        {
            try
            {
                if (string.IsNullOrEmpty(routePath))
                {
                    return "Please enter route path";
                }

                List<DistanceRouteModel> distances = routePath.Split(',').ToDistanceRouteDetail();
                List<bool> bools = new List<bool>();
                foreach (DistanceRouteModel distance in distances)
                {
                    RouteModel route = GetRoute(distance.Source, distance.Destination);
                    if (route != null)
                        bools.Add(true);
                    else
                        bools.Add(false);
                }

                if (bools.Contains(false))
                    return "Route does not exists";
                else
                    return "Route exists";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Exception occured";
            }
        }

        public int ShortestRouteLength(string source, string destination)
        {
            var distances = new Dictionary<string, int>();
            var priorityQueue = new SortedSet<(int distance, string town)>();
            var shortestDistance = int.MaxValue;

            // Initialize distances
            foreach (var route in routes)
            {
                distances[route.Source] = int.MaxValue;
                distances[route.Destination] = int.MaxValue;
            }

            distances[source] = 0;
            priorityQueue.Add((0, source));

            while (priorityQueue.Count > 0)
            {
                var (currentDist, currentTown) = priorityQueue.Last();
                priorityQueue.Remove(priorityQueue.Last());

                // If we found a cycle back to the start node, update the shortestDistance
                if (currentTown == destination && currentDist > 0)
                {
                    shortestDistance = Math.Min(shortestDistance, currentDist);
                    break;
                }
                
                var minDistance = routes.Where(a => a.Source == currentTown).OrderBy(a => a.Distance).FirstOrDefault();
                int newDist = currentDist + minDistance.Distance;

                if ((newDist < distances[minDistance.Destination]) ||
                    (newDist > distances[currentTown]))
                {
                    priorityQueue.Remove((distances[minDistance.Destination], minDistance.Destination));
                    distances[minDistance.Destination] = newDist;
                    priorityQueue.Add((newDist, minDistance.Destination));
                }
            }
                        
            return shortestDistance == int.MaxValue ? -1 : shortestDistance;
        }

        #region Private Methods

        private RouteModel GetRouteDetails(string[] splitRouteLine)
        {
            return splitRouteLine.ToRouteDetail();
        }

        private RouteModel GetRoute(string source, string destination)
        {
            return routes.Where(a => a.Destination.Equals(destination) && a.Source.Equals(source)).FirstOrDefault();
        }

        #endregion

    }
}
