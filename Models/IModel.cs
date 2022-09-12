using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leducclement_m_LAB_07449_420_DA3_AS.Models
{
    public interface IModel<TModel> where TModel : IModel<TModel> {

        TModel Insert();

        TModel GetById();

        TModel Update();

        void Delete();
    }
}
