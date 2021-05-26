﻿using EdProject.DAL.Entities.Enums;

namespace EdProject.BLL.Models.Orders
{
    public class OrderItemModel
    {
        public long EditionId { get; set; }
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public int ItemsCount { get; set; }
        public CurrencyTypes Currency { get; set; }
    }
}
