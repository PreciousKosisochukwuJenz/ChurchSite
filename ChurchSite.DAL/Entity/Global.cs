using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchSite.DAL.Entity
{
    public class Global
    {
        public static int AuthenticatedUserID { get; set; }
    }
    public enum BonusType
    {
        Book = 1,
        Article,
        Author
    }

    public enum Intensions { 
        [System.ComponentModel.Description("Repose of soul")]
        Repose_of_souls = 1,
        [System.ComponentModel.Description("Total healing")]
        Total_healing,
        [System.ComponentModel.Description("Freedom from sins")]
        Freedom_from_sins,
        [System.ComponentModel.Description("Abundant blessings")]
        Abundant_blessings,
        [System.ComponentModel.Description("Happy family and good life partner")]
        Happy_family_and_good_life_partner,
        [System.ComponentModel.Description("Thanksgiving")]
        Thanksgiving,
        [System.ComponentModel.Description("Prosperity")]
        Prosperity,
        [System.ComponentModel.Description("Fruit of the womb")]
        Fruit_of_the_womb,
        [System.ComponentModel.Description("Job security")]
        Job_security,
        [System.ComponentModel.Description("Promotion")]
        Promotion,
        [System.ComponentModel.Description("Break through")]
        Breakthrough
    }
    public enum Mass
    {
        [System.ComponentModel.Description("Sunday masses")]
        Sunday_masses = 1,
        [System.ComponentModel.Description("Morning masses")]
        Morning_masses
        
    }

}
