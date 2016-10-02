using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace AtoZHosptalAutometion
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {


        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetGroupList(string groupName)
        {
            String cnString = System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;
            
            SqlConnection con = new SqlConnection(cnString);
            SqlCommand cmd = new SqlCommand("SELECT name FROM groups", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@groupName", groupName);
            cmd.Parameters.Add(parameter);
            List<string>  groups = new List<string>();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string group = reader["Name"].ToString();
                    groups.Add(group);
                }
                reader.Close();
            }
            con.Close();

            return groups;
        }
    }
}
