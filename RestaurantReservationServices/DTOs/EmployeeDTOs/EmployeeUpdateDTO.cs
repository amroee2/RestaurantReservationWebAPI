﻿namespace RestaurantReservationServices.DTOs.EmployeeDTOs
{
    public class EmployeeUpdateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int RestaurantId { get; set; }
    }
}
