<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="portfolio_admin.Projects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/projects.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="formItem">
         <div class="g1">
             <div class="t1">
             <p>Title</p>
             <asp:TextBox ID="txtProjectTitle" runat="server" CssClass="inp"></asp:TextBox>
             </div>
             
            <div class="t2">
                 <p>Description</p>
                <asp:TextBox ID="txtProjectDescription" runat="server" CssClass="inp"></asp:TextBox>
             </div>
             
            <div class="t3">
                <asp:FileUpload ID="fileProjectImage" runat="server" CssClass="upload" />
             </div>
           </div>
         
        <div class="g2">
            <div class="t4">
                 <p>Link</p>
                 <asp:TextBox ID="txtProjectURL" runat="server" CssClass="inp"></asp:TextBox>
             </div>
            
            <div class="t5">
                 <p>Tags</p>
                 <asp:TextBox ID="txtTags" runat="server" CssClass="inp"></asp:TextBox>
             </div>
            
            <asp:Button ID="btnAddProject" runat="server" Text="Add Project" OnClick="btnAddProject_Click" CssClass="submitBtn" />
        </div>
    </div>
    
    <asp:GridView ID="gvProjects" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvProjects_RowDeleting" OnRowEditing="gvProjects_RowEditing" OnRowUpdating="gvProjects_RowUpdating" OnRowCancelingEdit="gvProjects_RowCancelingEdit" OnRowDataBound="gvProjects_RowDataBound" OnSelectedIndexChanged="gvProjects_SelectedIndexChanged" CssClass="table">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="hfProjectID" runat="server" Value='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Title" HeaderText="Title"/>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ProjectDescription" />
            <asp:TemplateField HeaderText="Image">
                <EditItemTemplate>
                    <asp:FileUpload ID="editImage" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image" runat="server" CssClass="image"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="URL" HeaderText="URL" />
            <asp:BoundField DataField="Tags" HeaderText="Tags" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Customize" />
        </Columns>
    </asp:GridView>

</asp:Content>
