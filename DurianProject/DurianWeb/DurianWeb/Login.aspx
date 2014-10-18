<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/SiteDefault.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DurianWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<style type="text/css">
        #<%= btLogin.ClientID%> {padding: 0.4em 1em 0.1em 25px;text-decoration: none;position: relative;color:White;font-size:0.8em;}
	    #<%= btLogin.ClientID%> span.ui-icon {margin: 0 5px 0 0;position: absolute;left: .2em;top: 50%;margin-top: -8px;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        $('input[type=checkbox],input[type=radio]').prettyCheckboxes();
        $('#<%= btLogin.ClientID%>').hover(
					function () { $(this).addClass('ui-state-hover'); },
					function () { $(this).removeClass('ui-state-hover'); }
			);
        $("#<%=TxUsername.ClientID %>,#<%=TxPassword.ClientID %>").keydown(function (event) {
            if (event.keyCode == 13) {
                eval($("#<%=btLogin.ClientID %>").attr('href'));
            }
        });
    });
	</script>
<div class="grid" id="dvReport" style="width: 400px;margin:100px auto;">
                        <div class="top"><asp:Image ID="Image1" runat="server" ImageUrl="~/images/icon/lock.png" />
                                เข้าสู่ระบบ
                        </div>
                        <div class="mid">
                            <table style="width: 350px;" class="formFields">
                                <tr>
                                    <td style="font-weight: bold; text-align: right; width: 110px;">
                                        ชื่อผู้ใช้:&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxUsername" runat="server" Width="220px" CssClass="textEntry"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; text-align: right">
                                        รหัสผ่าน:&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxPassword" runat="server" TextMode="Password" Width="220px" 
                                            CssClass="textEntry"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <br />
                                        <asp:LinkButton ID="btLogin" runat="server" 
                                            CssClass="ui-state-default ui-corner-all" ValidationGroup="xxxx" 
                                            onclick="btLogin_Click"><span class="ui-icon ui-icon-unlocked"></span>เข้าสู่ระบบ</asp:LinkButton>
                                        
                                       
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Label ID="LbErr" runat="server" ForeColor="Red"></asp:Label>
                                        
                                       
                                    </td>
                                </tr>
                                </table>
                        </div>
                        
                    </div>
                    
</asp:Content>
