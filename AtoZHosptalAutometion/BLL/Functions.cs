using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AtoZHosptalAutometion.DAL;
using AtoZHosptalAutometion.Models;

namespace AtoZHosptalAutometion.BLL
{
    public class Functions
    {

        public string GeneratedCode(string code, int point)
        {
            int partOne = Convert.ToInt32(code.Substring(point)) + 1;
            string strPart = code.Substring(0, point);
            return strPart + partOne;
        }

        public List<InvoiceType> GetInvoiceType()
        {
            CoreDAL oCoreDal = new CoreDAL();
            return oCoreDal.GetInvoiceType();
        }

        public string NumberToWord(int num)
        {
            if (num == 0)
                return "Zero";

            if (num < 0)
                return "Not supported";

            var words = "";
            string[] strones = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] strtens = { "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };


            int crore = 0, lakhs = 0, thousands = 0, hundreds = 0, tens = 0, single = 0;


            crore = num / 10000000; num = num - crore * 10000000;
            lakhs = num / 100000; num = num - lakhs * 100000;
            thousands = num / 1000; num = num - thousands * 1000;
            hundreds = num / 100; num = num - hundreds * 100;
            if (num > 19)
            {
                tens = num / 10; num = num - tens * 10;
            }
            single = num;

            if (crore > 0)
            {
                if (crore > 19)
                    words += NumberToWord(crore) + "Crore ";
                else
                    words += strones[crore - 1] + " Crore ";
            }

            if (lakhs > 0)
            {
                if (lakhs > 19)
                    words += NumberToWord(lakhs) + "Lakh ";
                else
                    words += strones[lakhs - 1] + " Lakh ";
            }

            if (thousands > 0)
            {
                if (thousands > 19)
                    words += NumberToWord(thousands) + "Thousand ";
                else
                    words += strones[thousands - 1] + " Thousand ";
            }

            if (hundreds > 0)
                words += strones[hundreds - 1] + " Hundred ";

            if (tens > 0)
                words += strtens[tens - 2] + " ";

            if (single > 0)
                words += strones[single - 1] + " ";

            return words;
        }

        public int SaveDiposit(Voucher oVoucher)
        {
            CoreDAL oCoreDal = new CoreDAL();
            return oCoreDal.SaveDiposit(oVoucher);
        }

        public DataSet GetDipositData(int id)
        {
            CoreDAL oCoreDal = new CoreDAL();
            return oCoreDal.GetDipositData(id);
        }
    }
}