﻿using Core.Entities.Abstract;

namespace Entities.DTOs.Order
{
    public class GetOrderDetailDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int AmountPaid { get; set; }
        public int RefundPaid { get; set; }
    }
}
