using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;
using AtoZHosptalAutometion.UI;

namespace AtoZHosptalAutometion.BLL
{
    public class AgentBLL
    {
        public string Register(Agent oAgent)
        {
            Functions functions = new Functions();
            AgentDAL oAgentDal = new AgentDAL();

            oAgent.UpdatedDate = DateTime.Now;
            string lastCode = oAgentDal.GetLastCode();
            oAgent.Code = functions.GeneratedCode(lastCode, 3);
            string agentCode = null;
            if (oAgentDal.Register(oAgent))
            {
                agentCode = oAgentDal.GetLastCode();
            }
            return agentCode;
        }

        public int GetAgentIdByCode(string text)
        {
            AgentDAL oAgentDal = new AgentDAL();
            return (int) oAgentDal.GetAgentIdFromCode(text);
        }

        public List<UI.HonorariumPayment> PayAgent(int agentId)
        {
            AgentDAL oAgentDal = new AgentDAL();
            return oAgentDal.PayAgent(agentId);
            ;
        }

        public bool AgentDuePayment(int agentId, int amount, int userId)
        {
            AgentDAL oAgentDal = new AgentDAL();
            return oAgentDal.AgentDuePayment(agentId, amount, userId);
        }
    }
}