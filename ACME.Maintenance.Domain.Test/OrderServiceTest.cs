using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using ACME.Maintenance.Domain.Interfaces;
using ACME.Maintenance.Domain.Exceptions;

namespace ACME.Maintenance.Domain.Test
{
    [TestClass]
    public class OrderServiceTest
    {
        private IContractRepository _contractRepository;
        private ContractService _contractService;

        private IPartServiceRepository _partServiceRepository;

        private const string ValidContractId = "CONTRACTID";
        private const string ExpiredContractId = "EXPIREDCONTRACTID";
        private const string ValidPartId = "PARTID";
        private const double ValidPartPrice = 50.0;

        [TestInitialize]
        public void Initialize()
        {

            //Initialize serves as the "Composition Root"

            _contractRepository = A.Fake<IContractRepository>();

            _partServiceRepository = A.Fake<IPartServiceRepository>();
            A.CallTo(() => _contractRepository.GetById(ValidContractId))
                .Returns(new DTO.ContractDto
                {
                    ContractID = ValidContractId,
                    ExpirationDate = DateTime.Now.AddDays(1)
                });

            A.CallTo(() => _contractRepository.GetById(ExpiredContractId))
                .Returns(new DTO.ContractDto
                {
                    ContractID = ExpiredContractId,
                    ExpirationDate = DateTime.Now.AddDays(-1)
                });


            A.CallTo(() => _partServiceRepository.GetById(ValidPartId))
                .Returns(new DTO.PartDto
                { PartId = ValidPartId,
                    Price = ValidPartPrice
                });

            AutoMapper.Mapper.CreateMap<DTO.ContractDto, Contract>();
            AutoMapper.Mapper.CreateMap<DTO.PartDto, Part>();

            _contractService = new ContractService(_contractRepository);
        }


        [TestMethod]
        public void CreateOrder_ValidContract_CreatesNewOrder()
        {
            //Arrange
            var orderService = new OrderService();

            var contractService = new ContractService(_contractRepository);
            var contract = contractService.GetById(ValidContractId);

            //Act
            var newOrder = orderService.CreateOrder(contract);


            //Assert
            Assert.IsInstanceOfType(newOrder, typeof(Order));

            Guid guidOut;
            Assert.IsTrue(Guid.TryParse(newOrder.OrderId, out guidOut));
           // Assert.AreEqual(newOrder.OrderId, OrderId);


            Assert.AreEqual(newOrder.Status, Enums.OrderStatus.New);
            Assert.IsInstanceOfType(newOrder.Items, typeof(IReadOnlyList<OrderItem>));
        }


        [TestMethod, ExpectedException(typeof(ExpiredContractException))]
        public void CreateOrder_ExpiredContract_ThrowsException()
        {
            //Arrange
            var orderService = new OrderService();

            var contractService = new ContractService(_contractRepository);
            var contract = contractService.GetById(ExpiredContractId);

            //Act
            var newOrder = orderService.CreateOrder(contract);

            //Assert


        }
        
        [TestMethod]
        public void CreateOrderItem_ValidPart_AddsCreatesOrderItem()
        {
            //Arrange
            var orderService = new OrderService();

            var contractService = new ContractService(_contractRepository);
            var contract = contractService.GetById(ValidContractId);
            var order = orderService.CreateOrder(contract);

            var partService = new PartService(_partServiceRepository);
            var part = partService.GetById(ValidPartId);

            var quantity = 1;
            //Act
           var orderItem=  orderService.CreateOrderItem(part,quantity);

            //Assert
            Assert.AreEqual(orderItem.Part, part);
            Assert.AreEqual(orderItem.Quantity, quantity);
            Assert.AreEqual(orderItem.Price, ValidPartPrice);
            Assert.AreEqual(orderItem.LineTotal, quantity * ValidPartPrice);
        }
        
    }
}
