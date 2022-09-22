using leducclement_m_LAB_07449_420_DA3_AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Controllers
{
    internal class ProductController : IController
    {
        public void CreateProductById()
        {
            Product newProduct = new Product();
        }

        public void CreateProductAndInsertInDB(long gtinCode, int qtyInStock, string name, string description)
        {
            Product newProduct = new Product(0L, qtyInStock, name, description);
            newProduct.Insert();
        }
    }
}
