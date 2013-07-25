<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Corporate>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Edition du compte</title>
    <style type="text/css">
		div.form_elements p label{ width:45%;}
		div.field-validation-error{ width:19%;}
	</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		<%Html.RenderPartial("~/views/account/managemenu.ascx", User.GetUserPrincipal().CurrentUser);%>
    </div>
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>Vos informations soci�t�</span>
          </h2>
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span><b>Vos informations soci�t�</b>
        </div> 
     <br class="clear"/>
		<%Html.BeginEditCorporateForm(); %>
        <div class="form_elements">
        <fieldset class="form">
        	<legend>informations soci�t�</legend>
                <p>
                   <label for="name">Nom de la soci�t�:</label>
                   <span class="form_element_input"> 
                   <%= Html.TextBox("name") %></span>
                    <%= Html.ValidationMessage("name")%>
                </p>
                <p>
                    <label for="socialstatus">Statut de la soci�t� (sarl, sa, sas, eurl):</label>
                    <span class="form_element_input"><%= Html.TextBox("SocialStatus")%></span>
                    <%= Html.ValidationMessage("SocialStatus")%>
                </p>
                <p>
                    <label for="email">Email:</label>
                   <span class="form_element_input"> <%= Html.TextBox("email")%></span>
                    <%= Html.ValidationMessage("email")%>
                </p>
                <p>
                    <label for="phonenumber">Num�ro de t�l�phone:</label>
                    <span class="form_element_input"><%= Html.TextBox("phonenumber")%></span>
                    <%= Html.ValidationMessage("phonenumber")%>
                </p>
                <p>
                    <label for="faxnumber">Num�ro de t�l�phone:</label>
                   <span class="form_element_input"> <%= Html.TextBox("faxnumber")%></span>
                    <%= Html.ValidationMessage("faxnumber")%></
                </p>
                <p>
                    <label for="website">Site web officiel:</label>
                    <span class="form_element_input"><%= Html.TextBox("website")%></span>
                    <%= Html.ValidationMessage("website")%>
                </p>
                <p>
                    <label for="nafcode">Code NAF Version 2008:</label>
                    <span class="form_element_input"><%= Html.TextBox("nafcode")%></span>
                    <%= Html.ValidationMessage("nafcode")%>
                </p>
                <p>
                    <label for="siretnumber">Num�ro Siret:</label>
                    <span class="form_element_input"><%= Html.TextBox("siretnumber")%></span>
                    <%= Html.ValidationMessage("siretnumber")%>
                </p>
                <p>
                    <label for="rcsnumber">N� Registre du commerce:</label>
                    <span class="form_element_input"><%= Html.TextBox("rcsnumber")%></span>
                    <%= Html.ValidationMessage("rcsnumber")%>
                </p>
                <p>
                    <label for="tvanumber">Num�ro TVA:</label>
                    <span class="form_element_input"><%= Html.TextBox("vatnumber")%></span>
                    <%= Html.ValidationMessage("vatnumber")%>
                </p>
                <p class="submit submit_right">
                    <input type="submit" value="Modifier" class="corner" />
                </p>
            </fieldset>                 
        </div>
        <%Html.EndForm(); %>
        
        </div>
</div>
</asp:Content>

