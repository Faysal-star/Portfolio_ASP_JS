using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portfolio_admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodeName"] == null || Session["Password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string codeName = Session["CodeName"].ToString();
                string password = Session["Password"].ToString();

                string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

                string query = "SELECT * FROM Admin WHERE CodeName = @CodeName AND Password = @Password";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CodeName", codeName);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    connection.Close();
                }
            }
        }
    }
}