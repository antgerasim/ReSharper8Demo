using System;
using System.Deployment.Internal;

namespace AnotherWeb.Models
{
    public class Customer
    {
        private int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfLastContact { get; set; }
        public int Priority { get; set; }

        public Customer()
        {
            
        }
    }
}