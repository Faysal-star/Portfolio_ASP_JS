using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portfolio_admin
{
    public partial class Education : System.Web.UI.Page    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEducation();
            }
        }

           

        protected void gvEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEducation.Rows[e.RowIndex];
            HiddenField hfEducationID = (HiddenField)row.FindControl("eduID");

            int educationID = Convert.ToInt32(hfEducationID.Value);

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "DELETE FROM Education WHERE ID = @EducationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EducationID", educationID);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            BindEducation();

        }

        protected void gvEducation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEducation.EditIndex = e.NewEditIndex;
            BindEducation();
        }

        protected void gvEducation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEducation.Rows[e.RowIndex];
            HiddenField hfEducationID = (HiddenField)row.FindControl("eduID");
            TextBox txtInstitution = (TextBox)row.Cells[1].Controls[0];
            TextBox txtDegree = (TextBox)row.Cells[2].Controls[0];
            TextBox txtGraduationDate = (TextBox)row.Cells[3].Controls[0];

            int educationID = Convert.ToInt32(hfEducationID.Value);
            string institution = txtInstitution.Text;
            string degree = txtDegree.Text;
            string graduationDate = txtGraduationDate.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "UPDATE Education SET Institution = @Institution, Degree = @Degree, Graduation = @GraduationDate WHERE ID = @EducationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EducationID", educationID);
                command.Parameters.AddWithValue("@Institution", institution);
                command.Parameters.AddWithValue("@Degree", degree);
                command.Parameters.AddWithValue("@GraduationDate", graduationDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            gvEducation.EditIndex = -1;
            BindEducation();

        }

        protected void gvEducation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEducation.EditIndex = -1;
            BindEducation();

        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            string institution = txtInstitution.Text;
            string degree = txtDegree.Text;
            string graduationDate = txtGraduationDate.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "INSERT INTO Education (Institution, Degree, Graduation) VALUES (@Institution, @Degree, @GraduationDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Institution", institution);
                command.Parameters.AddWithValue("@Degree", degree);
                command.Parameters.AddWithValue("@GraduationDate", graduationDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            txtInstitution.Text = "";
            txtDegree.Text = "";
            txtGraduationDate.Text = "";

            BindEducation();
        }

        private void BindEducation()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Education", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);
                    gvEducation.DataSource = dataTable;
                    gvEducation.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}