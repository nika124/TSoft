<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/SiteMaster.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="TSoft.Pages.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridViewPersonList" runat="server" AutoGenerateColumns="false" DataSource="<%# GetPersonDataGrid() %>" ClientIDMode="Static">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="UserName" HeaderText="Name" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="ButtonEdit" runat="server" CommandArgument='<%# Eval("Id") %>' Text="EDIT" OnClick="ButtonEdit_Click" ClientIDMode="Static" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="ButtonPersonDelete" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Delete" OnClick="ButtonPersonDelete_Click" ClientIDMode="Static" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GridViewRoles" runat="server" AutoGenerateColumns="false" ClientIDMode="Static" DataSource="<%#GetRolesData() %>">
                <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="ButtonRoleEdit" runat="server" CommandArgument='<%# Eval("Id") %>' Text="EDIT" ClientIDMode="Static" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:Button ID="ButtonRoleDelete" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Delete" ClientIDMode="Static" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
