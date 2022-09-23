using leducclement_m_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Controllers
{
    internal class ProductController
    {
        public void InsertProduct(int qtyInStock, string name, string description = "", long gtinCode = 0L)
        {
            Product tempProduct = new Product(0L, qtyInStock, name, description);
            tempProduct.Insert();
        }

        public void DisplayProduct(int id)
        {
            Product product = Product.GetById(id);
            this.DisplayProduct(product);
        }

        public void DisplayProduct(Product product)
        {
            // Display a product to UI
        }

        public void UpdateProduct(int id, long gtinCode, int qtyInStock, string name, string description)
        {
            Product tempProduct = Product.GetById(id);
            tempProduct.GTINCode = gtinCode;
            tempProduct.Name = name;
            tempProduct.Description = description;
            tempProduct.QtyInStock = qtyInStock;
            tempProduct.Update();
        }

        public void DeleteProduct(int id)
        {
            Product product = Product.GetById(id);
            product.Delete();
        }      
    }
}
