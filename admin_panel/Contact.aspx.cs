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
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindContact();
            }
        }

        private void BindContact()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "SELECT * FROM Contact";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                gvContact.DataSource = command.ExecuteReader();
                gvContact.DataBind();
                connection.Close();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string mobile = txtMobile.Text;
            string type = txtType.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "INSERT INTO Contact (Address, Email, Mobile, Type) VALUES (@Address, @Email, @Mobile, @Type)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Type", type);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtType.Text = "";

            BindContact();
            
            
        }

        protected void gvContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvContact.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("contactID");
            int contactId = Convert.ToInt32(hiddenField.Value);

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "DELETE FROM Contact WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", contactId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            BindContact();

        }

        protected void gvContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvContact.EditIndex = -1;
            BindContact();

        }

        protected void gvContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvContact.EditIndex = e.NewEditIndex;
            BindContact();
        }

        protected void gvContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvContact.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("contactID");
            int contId = Convert.ToInt32(hiddenField.Value);
            TextBox tAddress = (TextBox)row.Cells[1].Controls[0];
            TextBox tEmail = (TextBox)row.Cells[3].Controls[0];
            TextBox tMobile = (TextBox)row.Cells[2].Controls[0];
            TextBox tType = (TextBox)row.Cells[4].Controls[0];

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "UPDATE Contact SET Address = @Address, Email = @Email, Mobile = @Mobile, Type = @Type WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Address", tAddress.Text);
                command.Parameters.AddWithValue("@Email", tEmail.Text);
                command.Parameters.AddWithValue("@Mobile", tMobile.Text);
                command.Parameters.AddWithValue("@Type", tType.Text);
                command.Parameters.AddWithValue("@ID", contId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            gvContact.EditIndex = -1;
            BindContact();

        }
    }
}