using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.BusinessLogicLayer
{
    class donorBLL
    {
        public int DonorId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Bloodgrooup { get; set; }
        public DateTime Added_date { get; set; }
        public string Image_name { get; set; }
        public int Added_by{ get; set; }
    }
}
