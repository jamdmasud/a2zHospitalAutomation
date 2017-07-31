using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using AtoZHosptalAutometion.Models;

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
        [WebMethod]
        public void GetPatientList()
        {
            String cnString = System.Configuration.ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;

            SqlConnection con = new SqlConnection(cnString);
            SqlCommand cmd = new SqlCommand("Select Code, Name, Sex, Phone,fatherOhusbandName, presentAddress from Patient", con);
            
            List<Patient> patients = new List<Patient>();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Patient oPatient = new Patient();
                    oPatient.Code = reader["Code"].ToString();
                    oPatient.Name = reader["Name"].ToString();
                    oPatient.Sex = reader["Sex"].ToString();
                    oPatient.Phone = reader["Phone"].ToString();
                    oPatient.fatherOhusbandName = reader["fatherOhusbandName"].ToString();
                    oPatient.presentAddress = reader["presentAddress"].ToString();
                    patients.Add(oPatient);
                }
                reader.Close();
            }
            con.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(patients));
        }
    }
}
