<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="form_elements">
    <fieldset class="form">
    <legend>modifier le mot de passe</legend> 
	<% if (ViewData["EditPasswordSuccess"] != null)
	{ %>
    <!-- gestion des notes //-->
    <div class="notes errors">
        <strong>
            <img src="/content/images/icon_noter.png" alt="" />
            A noter !</strong>
        <p class="note">
            <span>Votre mot de passe vient d'etre modifié avec succès !</span>
        </p>
    </div>
    <!-- fin gestion des notes //--> 
	<% } %>
	<% using (Html.BeginChangePasswordForm())
	{ %>
	<p>
		<label for="oldpassword">Ancien mot de passe:</label>
		<span class="form_element_input">
		<% =Html.Password("oldpassword") %></span>
		<% =Html.ValidationMessage("oldpassword", "*") %>
	</p>	
	<p>
		<label for="newpassword">Nouveau mot de passe:</label>
		<span class="form_element_input">
		<% =Html.Password("newpassword") %></span>
		<% =Html.ValidationMessage("newpassword", "*") %>
	</p>
	<p>
		<label for="confirmnewpassword">Confirmation du nouveau mot de passe:</label>
		<span class="form_element_input">
			<% =Html.Password("confirmnewpassword")%></span>
		<% =Html.ValidationMessage("confirmnewpassword", "*")%>
	</p>
	<p class="submit submit_right">
		<input type="submit" value="Modifier" class="corner" />
	</p>
	<% } %>
    </fieldset>
</div>
