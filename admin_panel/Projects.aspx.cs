using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace portfolio_admin
{
    public partial class Projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjects();
                bindMode();
            }
        }

        protected void btnAddProject_Click(object sender, EventArgs e)
        {
            byte[] projectImage = null;
            using (var binaryReader = new BinaryReader(fileProjectImage.PostedFile.InputStream))
            {
                projectImage = binaryReader.ReadBytes(fileProjectImage.PostedFile.ContentLength);
            }
            string title = txtProjectTitle.Text;
            string description = txtProjectDescription.Text;
            string url = txtProjectURL.Text;
            string tags = txtTags.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "INSERT INTO Projects (Title, Description, Image, URL, Tags) VALUES (@Title, @Description, @Image, @URL, @Tags)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Image", projectImage);
                command.Parameters.AddWithValue("@URL", url);
                command.Parameters.AddWithValue("@Tags", tags);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            txtProjectTitle.Text = "";
            txtProjectDescription.Text = "";
            txtProjectURL.Text = "";
            txtTags.Text = "";
            fileProjectImage = null;

            BindProjects();

            return;
        }

        private void BindProjects()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

           using(SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Projects", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    /*foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        byte[] bytes = (byte[])row["Image"];
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        row["Image"] = "data:image/png;base64," + base64String;
                    }*/

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvProjects.DataSource = ds;
                        gvProjects.DataBind();
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

        protected void gvProjects_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvProjects.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("hfProjectID");

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "DELETE FROM Projects WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", hiddenField.Value);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            BindProjects();
        }

        protected void gvProjects_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvProjects.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("hfProjectID");
            TextBox txtTitle = (TextBox)row.Cells[1].Controls[0];
            TextBox txtDescription = (TextBox)row.Cells[2].Controls[0];
            FileUpload fileImage = (FileUpload)row.FindControl("editImage");
            TextBox txtURL = (TextBox)row.Cells[4].Controls[0];
            TextBox txtTags = (TextBox)row.Cells[5].Controls[0];

            byte[] projectImage = null;
            using (var binaryReader = new BinaryReader(fileImage.PostedFile.InputStream))
            {
                projectImage = binaryReader.ReadBytes(fileImage.PostedFile.ContentLength);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "UPDATE Projects SET Title = @Title, Description = @Description, Image = @Image, URL = @URL, Tags = @Tags WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", hiddenField.Value);
                command.Parameters.AddWithValue("@Title", txtTitle.Text);
                command.Parameters.AddWithValue("@Description", txtDescription.Text);
                command.Parameters.AddWithValue("@Image", projectImage);
                command.Parameters.AddWithValue("@URL", txtURL.Text);
                command.Parameters.AddWithValue("@Tags", txtTags.Text);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            gvProjects.EditIndex = -1;
            BindProjects();
            
        }

        protected void gvProjects_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProjects.EditIndex = -1;
            BindProjects();
        }

        protected void gvProjects_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                return;
            }
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
            {
                byte[] bytes = (byte[])DataBinder.Eval(e.Row.DataItem, "Image");
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                (e.Row.FindControl("Image") as Image).ImageUrl = "data:image/png;base64," + base64String;
            }
        }

        protected void gvProjects_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProjects.EditIndex = e.NewEditIndex;
            BindProjects();
        }

        protected void gvProjects_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void updateMode_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "UPDATE Mode SET [Mode] = @Mode WHERE ID = 1";

            string mode = portfolioState.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Mode", mode);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            bindMode();
        }

        protected void bindMode()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT [Mode] FROM Mode WHERE ID = 1", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        portfolioState.Text = ds.Tables[0].Rows[0]["Mode"].ToString();
                        currentMode.Text = ds.Tables[0].Rows[0]["Mode"].ToString();
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
    }
}