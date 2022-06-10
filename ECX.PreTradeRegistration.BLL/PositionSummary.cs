using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BLL
{
    public class PositionSummary
    {
        public DataTable GetPreTradePositionSummary(Guid memberId)
        {
            return new DAL.PreTradeOrderDAL().GetPositionSummary(memberId);
        }
        public DataTable GetAvailableBalance(Guid whrId)
        {
            return new DAL.PreTradeOrderDAL().GetAvailableBalance(whrId);
        }
    }
}
