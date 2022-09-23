using leducclement_m_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Controllers
{
    internal class CustomerController
    {
        public void InsertCustomer(string email, string firstName = "", string lastName = "")
        {
            Customer tempCustomer = new Customer(firstName, lastName, email);
            tempCustomer.Insert();
        }

        public void DisplayCustomer(int id)
        {
            Customer tempCustomer = Customer.GetById(id);
            this.DisplayCustomer(tempCustomer);
        }

        public void DisplayCustomer(Customer customer)
        {
            // Display a product to UI
        }

        public void UpdateCustomer(int id, string firstName, string lastName, string email)
        {
            Customer tempCustomer = Customer.GetById(id);
            tempCustomer.FirstName = firstName;
            tempCustomer.LastName = lastName;
            tempCustomer.Email = email;
            tempCustomer.Update();
        }

        public void DeleteCustomer(int id)
        {
            Customer tempCustomer = Customer.GetById(id);
            tempCustomer.Delete();
        }
    }
}
