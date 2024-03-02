<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="portfolio_admin.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/contact.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="conG">
        <div class="conG1">
            <asp:TextBox ID="txtAddress" runat="server" CssClass="cinp" placeholder="Address" ></asp:TextBox>
            <asp:TextBox ID="txtMobile" runat="server" CssClass="cinp" placeholder="Mobile"></asp:TextBox>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="cinp" placeholder="Email"></asp:TextBox>
        </div>

        <div class="conG2">
            <asp:TextBox ID="txtType" runat="server" CssClass="cinp" placeholder="Type"></asp:TextBox>

            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="cbtn" OnClick="btnAdd_Click" />
        </div>
    </div>

<!-- Show Contact Adress : Address, Mobile , Email, Type -->
    <asp:GridView ID="gvContact" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvContact_RowDeleting" OnRowCancelingEdit="gvContact_RowCancelingEdit" OnRowEditing="gvContact_RowEditing" OnRowUpdating="gvContact_RowUpdating" CssClass="table">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="contactID" runat="server" Value='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Address" HeaderText="Address" />
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Type" HeaderText="Type" />
            <asp:CommandField ShowEditButton="true" ShowDeleteButton="True" HeaderText="Customize" />
        </Columns>

    </asp:GridView>

</asp:Content>
