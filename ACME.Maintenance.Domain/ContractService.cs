using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Maintenance.Domain.Interfaces;

namespace ACME.Maintenance.Domain
{
    public class ContractService
    {

        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        
        public Contract GetById(string contractId)
        {
            //1. Call an instance of my persistence layer
          //  var contractRepository = new ContractRepository();


            //2. Call the FindById method of the persistence layer
            // and pass the contractId
            var contractDto = _contractRepository.GetById(contractId);
            //3. Receive the data back from that function and populate
            //my properties... similar to this
            //but with REAL data;
            //var contract = new Contract();          
            // contract.ContractId = contractDto.ContractID; 
            // contract.ExpirationDate = contractDto.ExpirationDate; 

            var contract = AutoMapper.Mapper.Map<DTO.ContractDto, Contract>(contractDto);
            return contract;
        }
         
    }
}
