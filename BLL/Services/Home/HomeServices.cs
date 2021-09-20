using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebUl.DAL.Repository;

namespace BLL.Services.Home
{

   public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeServices(IUnitOfWork _unitOfWork)
        {

            this.unitOfWork = _unitOfWork;

        }


        #region Nationality Services
        public Table_Hagz GetById(int id)
        {

            return unitOfWork.Repository<Table_Hagz>().GetById(id);

        }

        public List<Table_Hagz> GetAll()
        {
            return unitOfWork.Repository<Table_Hagz>().GetAll();
        }

        public List<Table_Hagz> GetAll(Expression<Func<Table_Hagz, bool>> predicate)
        {
            return unitOfWork.Repository<Table_Hagz>().Get(predicate);
        }

        public Table_Hagz Insert(Table_Hagz entity)
        {
            var memb = unitOfWork.Repository<Table_Hagz>().Insert(entity);
            unitOfWork.Save();
            return memb;
        }

        public Table_Hagz Update(Table_Hagz entity)
        {

            var memb = unitOfWork.Repository<Table_Hagz>().Update(entity);
            unitOfWork.Save();
            return memb;
        }

        public void Delete(int id)
        {
            unitOfWork.Repository<Table_Hagz>().Delete(id);
            unitOfWork.Save();
        }

        public void UpdateList(List<Table_Hagz> Lstservice)
        {

            var insertedRecord = Lstservice.Where(x => x.StatusFlag == "i");
            var updatedRecord = Lstservice.Where(x => x.StatusFlag == "u");
            var deletedRecord = Lstservice.Where(x => x.StatusFlag == "d");

            if (updatedRecord.Count() > 0)
                unitOfWork.Repository<Table_Hagz>().Update(updatedRecord);

            if (insertedRecord.Count() > 0)
                unitOfWork.Repository<Table_Hagz>().Insert(insertedRecord);


            if (deletedRecord.Count() > 0)
            {
                foreach (var entity in deletedRecord)
                    unitOfWork.Repository<Table_Hagz>().Delete(entity.ID);
            }

            unitOfWork.Save();

        }
        #endregion
    }
}
