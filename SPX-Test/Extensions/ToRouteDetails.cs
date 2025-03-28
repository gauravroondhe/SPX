using SPX_Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SPX_Test.Extensions
{
    public static class ToRouteDetails
    {
        public static RouteModel ToRouteDetail(this string[] splitRouteLine)
        {
            return new RouteModel 
            { 
                Source = splitRouteLine[0],
                Destination = splitRouteLine[1],
                Distance = Convert.ToInt32(splitRouteLine[2])
            };
        }

        public static List<DistanceRouteModel> ToDistanceRouteDetail(this string[] splitRoute)
        {
            List<DistanceRouteModel> distances = new List<DistanceRouteModel>();

            for (int i = 0; i < splitRoute.Length; i++)
            {
                if ((splitRoute.Length - 1) != i)
                {
                    distances.Add(new DistanceRouteModel()
                    {
                        Source = splitRoute[i],
                        Destination = splitRoute[i + 1],
                    });
                }
            }

            return distances;
        }
    }
}
