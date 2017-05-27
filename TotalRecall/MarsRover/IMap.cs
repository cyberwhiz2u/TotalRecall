using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public interface IMap
    {
        int XBoundary { get; set; }

        int YBoundary { get; set; }
    }
}
