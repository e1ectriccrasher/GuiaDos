<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="GuiaDos.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>
        etsa es la pagina para hcer log in
    </h1>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            CU/cProf:
            <asp:TextBox ID="TextBoxCU" runat="server"></asp:TextBox>
        </p>
        <p>
            Contraseña:
            <asp:TextBox ID="TextBoxPWD" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="loginAlumno" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="login Profesor" />
        </p>
        <p>
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </p>
    </form>

