using leducclement_m_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Controllers
{
    internal class ShoppingCartController
    {
        public void InsertShoppingCart(int customerId, string billingAddress, string shippingAddress)
        {
            Customer customer = Customer.GetById(customerId);
            ShoppingCart tempShoppingCart = new ShoppingCart(customer, billingAddress, shippingAddress);
            tempShoppingCart.Insert();
        }

        public void DisplayShoppingCart(int id)
        {
            ShoppingCart tempShoppingCart = ShoppingCart.GetById(id);
            this.DisplayShoppingCart(tempShoppingCart);
        }

        public void DisplayShoppingCart(ShoppingCart shoppingCart)
        {
            // Display shoppingCart to UI
        }

        public void UpdateShoppingCart(int shoppingCartId, int customerId, string billingAddress, string shippingAddress)
        {
            ShoppingCart tempShoppingCart = ShoppingCart.GetById(shoppingCartId);
            Customer tempCustomer = Customer.GetById(customerId);
            tempShoppingCart.Customer = tempCustomer;
            tempShoppingCart.ShippingAddress = shippingAddress;
            tempShoppingCart.BillingAddress = billingAddress;
            tempShoppingCart.Update();
        }

        public void DeleteShoppingCart(int id)
        {
            ShoppingCart tempShoppingCart = ShoppingCart.GetById(id);
            tempShoppingCart.Delete();
        }
    }
}
