using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.PreTradeRegistration.DAL;
using ECX.PreTradeRegistration.BE;

namespace ECX.PreTradeRegistration.BLL
{
    public class LookUpBLL
    {
        public DataTable GetAllActiveCommodites()
        {
            try
            {
                return new LookUpDAL().GetAllActiveCommodites();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommodityGrade> GetAllActiveCommodityGradesByCommodity(Guid CommodityId)
        {
            return new LookUpDAL().GetAllActiveCommodityGradesByCommodity(CommodityId);
        }
    }
}
