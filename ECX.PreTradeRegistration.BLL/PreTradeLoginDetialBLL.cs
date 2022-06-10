using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.PreTradeRegistration.DAL;
using ECX.PreTradeRegistration.BE;
using System.Data;

namespace ECX.PreTradeRegistration.BLL
{
    public class PreTradeLoginDetialBLL
    {
        #region memberVariables

        #endregion

        #region memberMethods

        public bool SavePreTradeLoginRecord(Guid Id, Guid UserId, string IpAddress)
        {
            return new PreTradeLoginDetialDAL().SavePreTradeLoginRecord(Id, UserId, IpAddress);
        }

        public bool UpdatePreTradeLogOutRecord(Guid UserId)
        {
            return new PreTradeLoginDetialDAL().UpdatePreTradeLogOutRecord(UserId);
        }

        public bool CheckRepIsMapped(Guid RepId)
        {
            return new PreTradeLoginDetialDAL().CheckRepIsMapped(RepId);
        }

        public bool CheckRepIsActive(Guid RepId)
        {
            return new PreTradeLoginDetialDAL().CheckRepIsActive(RepId);
        }

        public DataTable GetMemberIdByRepId(Guid RepId)
        {
            return new PreTradeLoginDetialDAL().GetMemberIdByRepId(RepId); 
        }

        public DataTable GetRepIdByADId(Guid AdId)
        {
            return new PreTradeLoginDetialDAL().GetRepIdByADId(AdId);
        }

        #endregion
    }
}
