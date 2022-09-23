using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Models
{
    internal class CartProduct
    {
        private string _cartId;
        private string _productId;
        private string _productQuantity;

        public string CartId
        {
            get { return this._cartId; }
            set
            {
                if(this._cartId != value)
                this._cartId = value; 
            }
        }
    }
}
