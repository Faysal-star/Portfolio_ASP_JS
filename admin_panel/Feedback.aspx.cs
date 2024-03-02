using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                bindSummary();
            }

        }

        protected void bindSummary()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;
            
            string query = "SELECT TOP 1 * FROM FeedbackSummary ORDER BY ID DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblTime.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                        lblTotal.Text = ds.Tables[0].Rows[0]["Summary"].ToString();
                    }
                    else
                    {
                        lblTime.Text = "0";
                        lblTotal.Text = "No feedback yet";
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return;
            
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