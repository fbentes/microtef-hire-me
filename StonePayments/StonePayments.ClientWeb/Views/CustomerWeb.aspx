<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerWeb.aspx.cs" Inherits="StonePayments.ClientWeb.CustomerWebView" %>

<%@ Register Assembly="StonePayments.Util" Namespace="StonePayments.Util.ViewWeb" TagPrefix="cc1" %>
<%@ Register Assembly="DataObjectLayer.View.Web" Namespace="DataObjectLayer.View.Web" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table runat="server" id="table" class="dsds">
          <tr>
              <td colspan="3">
                <asp:Label ID="lblTitutlo"  runat="server" Text="Cadastro de Clientes" Font-Bold="true" Font-Size="Medium"></asp:Label>
              </td>
          </tr>
          <tr>
             <td  style="width:15px; height: 28px;">
                 <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                 <asp:Label ID="lblName" runat="server" Text="Name: "></asp:Label>
                 <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
             </td>
          </tr>
          <tr>
             <td style="width:15px">
               <asp:Label ID="lblCreditLimit" runat="server" Text="Credit Limit: "></asp:Label>
               <asp:TextBox ID="txtCreditLimit" runat="server"></asp:TextBox>
             </td>
          </tr>     
              <tr>
              </tr>
              <tr>
              </tr>
              <tr>
                  <td colspan="2">
                      <asp:Button ID="btnSalvar" runat="server" Text="Save" />
                  </td>
              </tr>
              <tr>
              </tr>
              <tr>
              </tr>
              <tr>
                <td colspan="2">   

                    <cc1:GridViewEntityModel ID="gridViewCustomer" runat="server"></cc1:GridViewEntityModel>
                </td>
              </tr>                    
      </table>
    </div>
    </form>
</body>
</html>
