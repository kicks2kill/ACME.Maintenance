﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACME.Maintenance.Domain.Enums;
using ACME.Maintenance.Domain.Exceptions;
using ACME.Maintenance.Domain;

namespace ACME.Maintenance.Domain
{
    public class OrderService
    {
        public Order CreateOrder(Contract contract)
        {

            if (contract.ExpirationDate < DateTime.Now)
                throw new ExpiredContractException();

            var order = new Order
            { OrderId = Guid.NewGuid()
            .ToString(),
                Status =  OrderStatus.New};
            return order;
        }
        public OrderItem CreateOrderItem(Part part, int quantity)
        {
            var orderItem = new OrderItem
            {
                Part = part,
            Quantity = quantity,
            Price = part.Price,
            LineTotal = quantity * part.Price
            };
            return orderItem;
        }
       
    }
}
