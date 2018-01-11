using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using ACME.Maintenance.Domain.Interfaces;
using ACME.Maintenance.Domain.DTO;
using ACME.Maintenance.Domain.Exceptions;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class PartServiceTest
    {
        private const string ValidPartId = "VALIDPARTID";
        private const string InvalidPartId = "INVALIDPARTID";
        private const double ValidPartAmount = 50.0;

        private IPartServiceRepository _partRepository;

        [TestInitialize]
        public void Initialize()
        {
            _partRepository = A.Fake<IPartServiceRepository>();

            A.CallTo(() => _partRepository.GetById(ValidPartId))
                .Returns(new DTO.PartDto {PartId = ValidPartId, Price = ValidPartAmount});

            A.CallTo(() => _partRepository.GetById(InvalidPartId)).Throws(new PartNotFoundException());

            AutoMapper.Mapper.CreateMap<PartDto, Part>();
        }

        [TestMethod, ExpectedException(typeof(PartNotFoundException))]
        public void GetPartById_InValidId_ReturnsPart()
        {
            var partService = new PartService(_partRepository);
            var part = partService.GetById(InvalidPartId);

           

        }
    }
}
