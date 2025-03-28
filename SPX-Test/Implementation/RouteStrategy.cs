using SPX_Test.Helper;
using SPX_Test.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPX_Test.Implementation
{
    public class MaxStopsStrategy : IRouteStrategy
    {
        public int Calculate(RouteHelper routeHelper, string source, string destination, int maxStops)
        {
            return routeHelper.CountTrips(source, destination, 0, maxStops, true);
        }
    }

    public class ExactStopsStrategy : IRouteStrategy
    {
        public int Calculate(RouteHelper routeHelper, string source, string destination, int exactStops)
        {
            return routeHelper.CountTripsExact(source, destination, 0, exactStops);
        }
    }

    public class MaxDistanceStrategy : IRouteStrategy
    {
        public int Calculate(RouteHelper routeHelper, string source, string destination, int maxDistance)
        {
            return routeHelper.CountTripsByDistance(source, destination, 0, maxDistance);
        }
    }

    public class ShortestRouteStrategy : IRouteStrategy
    {
        public int Calculate(RouteHelper routeHelper, string source, string destination, int _)
        {
            return routeHelper.ShortestRouteLength(source, destination);
        }
    }
}
