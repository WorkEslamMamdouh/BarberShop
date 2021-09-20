using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Home
{
    public interface IHomeServices
    {
        Table_Hagz GetById(int id);
        List<Table_Hagz> GetAll();
        List<Table_Hagz> GetAll(Expression<Func<Table_Hagz, bool>> predicate);
        Table_Hagz Insert(Table_Hagz entity);
        Table_Hagz Update(Table_Hagz entity);
        void Delete(int id);
        void UpdateList(List<Table_Hagz> Lstservice);
    }
}
