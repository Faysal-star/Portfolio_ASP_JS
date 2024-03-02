<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Skills.aspx.cs" Inherits="portfolio_admin.Skills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/skills.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="skillG">
        <div class="skillG1">
            <asp:TextBox ID="txtSkillName" runat="server" placeholder="Skill Name" CssClass="sinp"></asp:TextBox>
            <asp:TextBox ID="txtSkillLevel" runat="server" placeholder="Skill Level" CssClass="sinp"></asp:TextBox>
        </div>
        <div class="skillG2">
            <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" OnClick="btnAddSkill_Click" CssClass="sbtn" />
        </div>
    </div>


    <asp:GridView ID="gvSkill" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvSkill_RowDeleting" OnRowEditing="gvSkill_RowEditing" OnRowUpdating="gvSkill_RowUpdating" OnRowCancelingEdit="gvSkill_RowCancelingEdit" CssClass="table" >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="skillID" runat="server" Value='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Skill" HeaderText="Skill" />
            <asp:BoundField DataField="Progress" HeaderText="Level" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="True" HeaderText="Customize" />
        </Columns>
    </asp:GridView>


</asp:Content>
