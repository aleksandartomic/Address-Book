using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        public int CustomerID { get; set; }
        public string Description { get; set; }
    }

    public static class Company
    {
        static Company()
        {
            Customers = new List<Customer>
            {new Customer {ID = 0, Name = "Jovan" },
             new Customer {ID = 1, Name = "Misa" },
             new Customer {ID = 2, Name = "Zoki" }
            };

            Orders = new List<Order>
            {new Order {CustomerID = 0, Description = "Jabuka" },
             new Order {CustomerID = 1, Description = "Kruska" },
             new Order {CustomerID = 2, Description = "Jagoda" }
            };
        }

         public static List<Customer> Customers  { get; set; }
         public static List<Order> Orders { get; set; }

}

    class Program
    {
        static void Main(string[] args)
        {           
            var customerOrders = from cust in Company.Customers
                                 join ord in Company.Orders
                                 on cust.ID equals ord.CustomerID
                                 select new
                                 {
                                     ID = cust.ID,
                                     Customer = cust.Name,
                                     Item = ord.Description
                                 };

            foreach (var custord in customerOrders)
            {
                Console.WriteLine(custord);
            }

           
        }
    }
}
