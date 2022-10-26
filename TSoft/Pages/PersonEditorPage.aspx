<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonEditorPage.aspx.cs" Inherits="TSoft.Pages.PersonEditorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="ButtonPersonDataInfo" runat="server" Text="Button" ClientIDMode="Static" OnClick="ButtonPersonDataInfo_Click" />
        <asp:Button ID="ButtonPersonRoleInfo" runat="server" Text="Button" ClientIDMode="Static" OnClick="ButtonPersonRoleInfo_Click" />
        <%--<asp:Button ID="ButtonPersonList" runat="server" Text="Button" ClientIdMode="Static" OnClick="ButtonPersonList_Click"/>--%>
        <asp:Panel ID="PanelPersonDataInfo" runat="server">
            <div>
                Name<asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
                LastName<asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
                UserName<asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
                Password<asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                RepeatPassword<asp:TextBox ID="TextBoxRepeatPassword" runat="server"></asp:TextBox>
                IdCardNumber<asp:TextBox ID="TextBoxIdCardNumber" runat="server"></asp:TextBox>
                Email<asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                PhoneNumber<asp:TextBox ID="TextBoxPhoneNumber" runat="server"></asp:TextBox>
                Male<asp:RadioButton ID="RadioButtonMale" runat="server" GroupName="Gender" />
                Female<asp:RadioButton ID="RadioButtonFeMale" runat="server" GroupName="Gender" />
                <asp:Button ID="ButtonSubmit" runat="server" Text="submit" OnClick="ButtonSubmit_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelPersonRoleInfo" runat="server" Visible ="false">
            <asp:DropDownList ID="DropDownListPerson" runat="server" DataSource="<%#GetPersonData() %>" DataValueField="Id" DataTextField="UserName" ItemType="TSoft.Models.Person" ClientIDMode="Static" OnSelectedIndexChanged="DropDownListPerson_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:GridView ID="GridViewRole" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" DataSource="<%# GetRoleData() %>">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="ButtonDelete" runat="server"
                                Text="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="ButtonDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <%--<asp:Panel ID="PanelPersonGrid" runat="server" Visible="false">
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
        </asp:Panel>--%>
    </form>
</body>
</html>
