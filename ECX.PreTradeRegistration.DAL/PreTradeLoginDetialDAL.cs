using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.PreTradeRegistration.BE;
using System.Data;


namespace ECX.PreTradeRegistration.DAL
{
    public class PreTradeLoginDetialDAL
    {
        #region memberVariables

        #endregion

        #region memberMethods

        public bool SavePreTradeLoginRecord(Guid Id,Guid UserId,string	IpAddress)        
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@Id");
                paramName.Add("@UserId");
                paramName.Add("@IpAddress");              

                ArrayList paramVal = new ArrayList();

                paramVal.Add(Id);
                paramVal.Add(UserId);
                paramVal.Add(IpAddress);               

                string errormesg = "";
                return DataAccessProvider.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "SpSavePreTradeLoginRecord", paramName, paramVal, ref errormesg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePreTradeLogOutRecord(Guid UserId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@UserId");              

                ArrayList paramVal = new ArrayList();
              
                paramVal.Add(UserId);               

                string errormesg = "";
                return DataAccessProvider.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "SpUpdatePreTradeLogOutRecord", paramName, paramVal, ref errormesg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckRepIsMapped(Guid RepId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@RepId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(RepId);

                string errormesg = "";
                return Convert.ToBoolean(DataAccessProvider.ExecuteScalar(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "fCheckRepIsMapped", paramName, paramVal, ref errormesg));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckRepIsActive(Guid RepId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@RepId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(RepId);

                string errormesg = "";
               
                return Convert.ToBoolean(DataAccessProvider.ExecuteScalar(ConfigurationManager.ConnectionStrings["MembershipConnectionString"].ConnectionString, "dbo", "fCheckRepIsActive", paramName, paramVal, ref errormesg));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMemberIdByRepId(Guid RepId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@RepId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(RepId);

                string errormesg = "";
              
                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["MembershipConnectionString"].ConnectionString, "dbo", "spGetMemberIdByRepId", paramName, paramVal, ref errormesg);

                if (dt != null)
                {
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRepIdByADId(Guid AdId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@ADId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(AdId);

                string errormesg = "";
              
                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetRepIdByADId", paramName, paramVal, ref errormesg);

                if (dt != null)
                {
                    return dt;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
