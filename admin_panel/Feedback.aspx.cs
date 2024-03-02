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
    public partial class Feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFeedback();
            }

        }

        protected void BindFeedback()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "SELECT * FROM Feedback";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                gvFeedback.DataSource = command.ExecuteReader();
                gvFeedback.DataBind();
                connection.Close();
            }
        }

    }
}