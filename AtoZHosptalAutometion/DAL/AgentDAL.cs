using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.Models;
using AtoZHosptalAutometion.UI;
using Microsoft.ApplicationInsights.Web;

namespace AtoZHosptalAutometion.DAL
{
    public class AgentDAL
    {
        Entities db = new Entities();
        public bool Register(Agent oAgent)
        {
            int affected = 0;
            try
            {
                db.Agents.Add(oAgent);
                affected = db.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return affected != 0;
        }

        public string GetLastCode()
        {
            try
            {
                using (var db = new Entities())
                {
                   return db.Agents.ToList().OrderByDescending(a => a.Id).Select(a => a.Code).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException.Message);
            }
        }

        public int? GetAgentIdFromCode(string agentCode)
        {
            try
            {
                using (var db = new Entities())
                {
                    int id = db.Agents.Where(a => a.Code == agentCode).Select(a => a.Id).FirstOrDefault();
                    return id;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException?.Message);
            }
        }

        public void SaveOnorariam(Honorarium oHonorarium)
        {
            try
            {
                using (var db = new Entities())
                {
                    db.Honoraria.Add(oHonorarium);
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.InnerException?.Message);
            }
        }

        public List<HonorariumPayment> PayAgent(int agentId)
        {
            List<HonorariumPayment> oPayAgents = new List<HonorariumPayment>();
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select a.Code, a.Name, a.Phone, a.Address, sum(h.Paid) as Paid, sum(h.Honorarium) as Honorarium from Honorarium h inner join Agent a on h.AgentId = a.Id where a.Id = @AgentId Group by a.Code, a.Name, a.Phone, a.Address";
                        cmd.Parameters.AddWithValue("@AgentId", agentId);
                        cmd.Connection = conn;
                        conn.Open();
                        List<string> names = new List<string>();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                HonorariumPayment oPayment = new HonorariumPayment();
                                oPayment.Code = sdr["Code"].ToString();
                                oPayment.Name = sdr["Name"].ToString();
                                oPayment.Phone = sdr["Phone"].ToString();
                                oPayment.Address = sdr["Address"].ToString();
                                //string due = sdr["Paid"].ToString();
                                oPayment.Paid = sdr["Paid"].ToString() == "" ? 0 :Convert.ToDecimal(sdr["Paid"].ToString());
                                oPayment.Honorarium = sdr["Honorarium"].ToString() == "" ? 0 : Convert.ToDecimal(sdr["Honorarium"].ToString());
                                oPayAgents.Add(oPayment);
                            }
                        }
                        conn.Close();
                        return oPayAgents;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.InnerException.Message);
            }
        }

        public bool AgentDuePayment(int agentId, int amount, int userId)
        {
            try
            {
                using (var db = new Entities())
                {
                    List<Honorarium> oHonorariums = db.Honoraria.ToList();
                    Honorarium oHonorarium = oHonorariums.FirstOrDefault(h => h.AgentId == agentId);
                    oHonorarium.Paid = (oHonorarium.Paid == null ? 0 : Convert.ToInt32(oHonorarium.Paid)) + amount;
                    oHonorarium.UpdatedBy = userId;
                    int affected = db.SaveChanges();
                    return affected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}