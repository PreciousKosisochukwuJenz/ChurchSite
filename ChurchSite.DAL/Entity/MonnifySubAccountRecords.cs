using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class MonnifySubAccountRecords
    {
        public int Id { get; set; }
        public string SubAccountCode { get; set; }
        public int SplitPercentage { get; set; }
        public bool FeeBearer { get; set; }
        public double FeePercentage { get; set; }
        public bool IsDeleted { get; set; }
    }
}
