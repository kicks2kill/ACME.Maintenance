using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Maintenance.Domain.DTO;

namespace ACME.Maintenance.Domain.Interfaces
{
    public interface IContractRepository
    {
        ContractDto GetById(string contractId);
    }
}
