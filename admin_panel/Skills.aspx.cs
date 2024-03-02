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
    public partial class Skills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSkills();
            }
        }

        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            string skill = txtSkillName.Text;
            string level = txtSkillLevel.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "INSERT INTO Skills (Skill, Progress) VALUES (@Skill, @Level)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Skill", skill);
                command.Parameters.AddWithValue("@Level", level);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            // clear form
            txtSkillName.Text = "";
            txtSkillLevel.Text = "";

            BindSkills();
            
        }

        private void BindSkills()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "SELECT * FROM Skills";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                gvSkill.DataSource = command.ExecuteReader();
                gvSkill.DataBind();
                connection.Close();
            }
        }

        protected void gvSkill_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvSkill.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("skillID");
            int skillId = Convert.ToInt32(hiddenField.Value);

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "DELETE FROM Skills WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", skillId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            BindSkills();

        }

        protected void gvSkill_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSkill.EditIndex = e.NewEditIndex;
            BindSkills();

        }

        protected void gvSkill_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvSkill.Rows[e.RowIndex];
            HiddenField hiddenField = (HiddenField)row.FindControl("skillID");
            TextBox txtSkill = (TextBox)row.Cells[1].Controls[0];
            TextBox txtLevel = (TextBox)row.Cells[2].Controls[0];

            string connectionString = ConfigurationManager.ConnectionStrings["PortfolioDB"].ConnectionString;

            string query = "UPDATE Skills SET Skill = @Skill, Progress = @Level WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Skill", txtSkill.Text);
                command.Parameters.AddWithValue("@Level", txtLevel.Text);
                command.Parameters.AddWithValue("@Id", hiddenField.Value);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            gvSkill.EditIndex = -1;
            BindSkills();
        }

        protected void gvSkill_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSkill.EditIndex = -1;
            BindSkills();
        }
    }
}