using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Maintenance.Domain.DTO
{
    public class ContractDto
    {
        public string ContractID { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
