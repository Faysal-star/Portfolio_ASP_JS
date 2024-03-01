<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Education.aspx.cs" Inherits="portfolio_admin.Education" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/education.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="eduG">
        <div class="eduG1">
            <asp:TextBox ID="txtInstitution" placeholder="Institution" runat="server" CssClass="einp"></asp:TextBox>
            <asp:TextBox ID="txtDegree" runat="server" placeholder="Degree" CssClass="einp"></asp:TextBox>
        </div>
        <div class="eduG2">
            <asp:TextBox ID="txtGraduationDate" runat="server" placeholder="Graduation Date" CssClass="einp"></asp:TextBox>
            <asp:Button ID="btnAddEducation" runat="server" Text="Add" CssClass="ebtn" OnClick="btnAddEducation_Click" />
        </div>
    </div>

    <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvEducation_RowDeleting" OnRowEditing="gvEducation_RowEditing" OnRowUpdating="gvEducation_RowUpdating" OnRowCancelingEdit="gvEducation_RowCancelingEdit" CssClass="table" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="eduID" runat="server" Value='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Institution" HeaderText="Institution" />
            <asp:BoundField DataField="Degree" HeaderText="Degree" />
            <asp:BoundField DataField="Graduation" HeaderText="Graduation Date" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="True" HeaderText="Customize" />
        </Columns>
    </asp:GridView>


</asp:Content>
