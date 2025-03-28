using SPX_Test.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPX_Test.Interface
{
    public interface IRouteStrategy
    {
        int Calculate(RouteHelper routeHelper, string source, string destination, int constraint);
    }
}
