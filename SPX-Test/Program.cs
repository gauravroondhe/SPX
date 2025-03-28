using SPX_Test.Extensions;
using SPX_Test.Helper;
using SPX_Test.Implementation;
using SPX_Test.Model;

namespace SPX_Test
{
    internal class Program
    {
        static RouteHelper routeHelper = new RouteHelper();
        static void Main(string[] args)
        {            
            routeHelper.GetRoutes();

            // Calculate Distance
            Console.WriteLine(string.Format("Test #1: The distance of the route A=>B=>C is " + routeHelper.CalculateDistance("A,B,C")));
            Console.WriteLine(string.Format("Test #2: The distance of the route A=>D is " + routeHelper.CalculateDistance("A,D")));
            Console.WriteLine(string.Format("Test #3: The distance of the route A=>D=>C is " + routeHelper.CalculateDistance("A,D,C")));
            Console.WriteLine(string.Format("Test #4: The distance of the route A=>E=>B=>C=>D is " + routeHelper.CalculateDistance("A,E,B,C,D")));

            // Check if path exists
            Console.WriteLine(string.Format("Test #5: Route A=>E=>D " + routeHelper.CheckRouteDistance("A,E,D")));

            // Check number of trips
            Console.WriteLine(string.Format("Test #6: Number of trips from C to C with maximum 3 stops is " + CheckNumberOfTripsWithMaximumStops("C,C", 3)));

            // Check number of trips with exact stops
            Console.WriteLine(string.Format("Test #7: Number of trips from A to C with exactly 4 stops is " + CheckNumberOfTripsWithExactStops("A,C", 4)));

            // Check for shortest route
            Console.WriteLine(string.Format("Test #8: The length of the shortest route from A to C is " + ShortestRoute("A,C")));
            Console.WriteLine(string.Format("Test #9: The length of the shortest route from B to B is " + ShortestRoute("B,B")));

            // Check length of Shortest Route
            Console.WriteLine(string.Format("Test #10: The number of trips from C to C with distance less than 30 is " + LengthOfShortestRoute("C,C", 30)));

            Console.ReadLine();
        }

        private static int ShortestRoute(string routePath)
        {
            return new ShortestRouteStrategy().Calculate(routeHelper, routePath.Split(",")[0], routePath.Split(",")[1], 0);
        }

        private static int LengthOfShortestRoute(string routePath, int maxDistance)
        {
            return new MaxDistanceStrategy().Calculate(routeHelper, routePath.Split(",")[0], routePath.Split(",")[1], maxDistance);
        }

        public static int CheckNumberOfTripsWithExactStops(string routePath, int exactStops)
        {
            return new ExactStopsStrategy().Calculate(routeHelper, routePath.Split(",")[0], routePath.Split(",")[1], exactStops);
        }

        private static int CheckNumberOfTripsWithMaximumStops(string routePath, int maxTrips)
        {
            return new MaxStopsStrategy().Calculate(routeHelper, routePath.Split(",")[0], routePath.Split(",")[1], maxTrips);
        }

    }
}
