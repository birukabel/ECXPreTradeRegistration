using ECX.PreTradeRegistration.BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.DAL
{
    public class LookUpDAL
    {
        public DataTable GetAllActiveCommodites()
        {
            try
            {
                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["LookupConnectionString"].ConnectionString, "dbo", "spPreTradeGetAllActiveCommodites", ref errormesg);
                return dt;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommodityGrade> GetAllActiveCommodityGradesByCommodity(Guid CommodityId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@CommodityId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(CommodityId);
             
                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["LookupConnectionString"].ConnectionString, "dbo", "spGetAllActiveCommodityGradesByCommodity", paramName, paramVal, ref errormesg);
                return  DataAccessProvider.ConvertDataTable<CommodityGrade>(dt);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
        
    }
}
