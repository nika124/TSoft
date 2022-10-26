<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleEditor.aspx.cs" Inherits="TSoft.Pages.RoleEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            RoleName:<asp:TextBox ID="TextBoxRoleName" runat="server"></asp:TextBox>
            <asp:Button ID="ButtonAddRole" runat="server" Text="Add" OnClick="ButtonAddRole_Click" />
            <asp:DropDownList ID="DropDownListRoles" runat="server" DataSource="<%#GetRoleNames() %>" DataValueField="Id" DataTextField="Name" ItemType="TSoft.Models.Roles" AutoPostBack="true" OnSelectedIndexChanged="DropDownListRoles_SelectedIndexChanged"></asp:DropDownList>
            <asp:GridView ID="GridViewAppObjects" runat="server" AutoGenerateColumns="false" ClientIDMode="Static" DataSource="<%#GetAppObjectsData() %>">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="ButtonAppObjectDelete" runat="server"
                                Text="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="ButtonAppObjectDelete_Click"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:DropDownList ID="DropDownListAppObjects" runat="server" ItemType="TSoft.Models.AppObjects" DataSource="<%#GetAppObjectNames() %>" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
            <asp:DropDownList ID="DropDownListAppObjectType" runat="server"></asp:DropDownList>
            <asp:Button ID="ButtonAddAppObject" runat="server" Text="Add" ClientIDMode="Static" OnClick="ButtonAddAppObject_Click"/>
        </div>
    </form>
</body>
</html>
