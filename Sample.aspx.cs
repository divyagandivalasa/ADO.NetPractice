using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADOSample
{
    public partial class Sample : System.Web.UI.Page
    {
        private string cs;
        private SqlCommand cmd;
        private SqlDataReader reader;
        public Sample()
        {
            cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //string cs = @"data source=(localdb)\MyInstance;database=Practice;integrated security=SSPI";
            SqlConnection con = new SqlConnection(cs);
            cmd = new SqlCommand("",con);
            using (con)
            {
                con.Open();
                GridView1.DataSource = Sample_ExecuteReader(cmd);
                GridView1.DataBind();
                reader.Close();
                int noofRecords = Sample_ExecuteScalar(cmd);
                Response.Write("No of records Present"+ noofRecords);
                reader.Close();
                int message = Sample_ExecuteNonQuery(cmd);
                Response.Write("\n"+"No of records Inserted" + message);
            }
        }

        public SqlDataReader Sample_ExecuteReader(SqlCommand command)
        {
            command.CommandText = "select * from student";
            reader = cmd.ExecuteReader();
            return reader;
        }

        public int Sample_ExecuteScalar(SqlCommand command)
        {
            command.CommandText = "select Count(*) from student";
            int value = (int)cmd.ExecuteScalar();
            return value;
        }

        public int Sample_ExecuteNonQuery(SqlCommand command)
        {
            command.CommandText = "Insert into Student values(9,'Manya',20)";
            int value = cmd.ExecuteNonQuery();
            return value;
        }
    }
}