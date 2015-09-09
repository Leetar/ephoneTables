using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ephoneTables
{
    class GlobVar
    {
        public static Match fileCME { get; set; }
        public static Match buttonMatchGlob { get; set;  }
        public static Match macMatch { get; set;  }
        public static string serverUri { get; set;  } 
    }
}
