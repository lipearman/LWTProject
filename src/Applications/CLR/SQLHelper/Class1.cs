using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;



public static class SQLHelper
{
    [SqlFunction()]
    public static string ThaiDateValidation(string input)
    {

        string output = "";
        var txtDate = input.Split('/');
        try
        {

            int dd = Convert.ToInt32(txtDate[0]);
            int mm = Convert.ToInt32(txtDate[1]);
            int yyyy = Convert.ToInt32(txtDate[2]);


            if (yyyy > 2500) {
                yyyy = yyyy - 543;
            }

            DateTime MyDate = new DateTime(yyyy, mm, dd);

            output = MyDate.ToString("dd/MM/yyyy");
        }
        catch (Exception)
        {

            //throw;
        }


        return output;
    }

}
