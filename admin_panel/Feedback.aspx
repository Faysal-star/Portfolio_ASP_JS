<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="portfolio_admin.Feedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles/feedback.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h2>Feedbacks From Visitors</h2>
    <div class="sum">
        <div class="time">
            <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="slabel"></asp:Label>
        </div>
        <div class="total">
            <asp:Label ID="lblTotal" runat="server" Text="Total" CssClass="slabel2"></asp:Label>
        </div>
    </div>

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
