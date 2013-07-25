<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Login</h2>
    
    <%=Html.ValidationSummary()%>

	<fieldset title="Login">
    <%Html.BeginLoginForm();%>
    
    <%=Html.Hidden("returnUrl", Request["returnUrl"])%>
    
    <label for="userName">User :</label>
    <%=Html.TextBox("userName")%>
    <%=Html.ValidationMessage("*")%>
    
    <label for="password">Password :</label>
    <%=Html.Password("password")%>
    <%=Html.ValidationMessage("*") %>
    
    <label for="rememberMe">Remember Me</label>
    <%=Html.CheckBox("rememberMe")%>
    
    <input type="submit" value="Login" />
    
    <%Html.EndForm();%>
    </fieldset>    

</asp:Content>
