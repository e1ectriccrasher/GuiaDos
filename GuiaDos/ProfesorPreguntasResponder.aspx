<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfesorPreguntasResponder.aspx.cs" Inherits="GuiaDos.ProfesorPreguntasResponder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Preguntas respondidas
        </div>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <p>
            Preguntas sin responder
        </p>
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="340px" TextMode="MultiLine" Width="486px"></asp:TextBox>
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Cerrar Sesión" />
    </form>
</body>
</html>
