﻿namespace RestaurantReservationCore.Db.DataModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public override string ToString()
        {
            return $"Order Id: {OrderId}, Order Date: {OrderDate}, Total Amount: {TotalAmount}, EmployeeId: {EmployeeId}, ReservationId: {ReservationId}";
        }
    }
}