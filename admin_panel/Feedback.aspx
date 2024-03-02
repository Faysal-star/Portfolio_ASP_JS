﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="portfolio_admin.Feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Feedback collection : Email , name , Feedback -->
    <asp:GridView ID="gvFeedback" runat="server" AutoGenerateColumns="false" CssClass="table">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HiddenField ID="feedID" runat="server" Value='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Feedback" HeaderText="Feedback" />
        </Columns>
    </asp:GridView>

</asp:Content>