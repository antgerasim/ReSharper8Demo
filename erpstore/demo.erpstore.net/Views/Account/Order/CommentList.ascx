<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>

<script type="text/javascript">
	function processResult(content) {

		var json = content.get_response().get_object();
		var result = eval(json);

		$("#validationSummary").empty();
		$("#validationSummary").empty();
		$(':input').removeClass('input-validation-error');

		if (result.Successfull) {
			/* $('#validationSummary').append('<span>' + result.Message + '</span>')
                             .removeClass('error')
                             .addClass('success'); */

			$('#commentlist').append('<p><label><small>' + result.CommentDate + '</small></label><span>' + result.CommentMessage + '</span></p>');

			$('#commentform #comment').value = ''; 
		}
		else {

			$('#validationSummary').append('<p><br/><span>' + result.Message + '</span></p>')
                             .removeClass('success')
                             .addClass('error');

			for (var err in result.Errors) {
				var propertyName = result.Errors[err].PropertyName;
				var errorMessage = result.Errors[err].Error;
				var message = errorMessage;

				$('#' + propertyName).addClass('input-validation-error');
				$('#form_elements').append('<p># ' + message + '</p>');
			}
		}
	}

</script>
<div class="ssccompte-list corner">
    <div class="form_elements" id="commentlist">
         <h4 class="corner">commentaires</h4>
		 <% foreach (Comment comment in Model.Comments) { %>
                <p><label><small>le <%=comment.CreationDate.ToString("dddd dd MMMM yyyy")%></small></label>
                <span><%=comment.Message%></span>
                </p> 
                <div class="separateur">&nbsp;</div>  
                <% } %>
                <div id="validationSummary"><p></p></div>
                <div id="commentform">
                    <%Ajax.BeginAddCommentToOrderForm(new AjaxOptions() { HttpMethod = "POST", OnComplete = "processResult" });%>
                    <p><label>Ajouter un commentaire :</label>
                    <%=Html.Hidden("orderCode", Model.Code)%>
                    <%=Html.TextArea("comment", new { cols = 40, rows = 4 })%></p>
                    <p class="submit submit_right">
                    	<input type="submit" value="Ajouter"/>
                    </p>
                    <%Html.EndForm(); %>
                </div> 
       </div>
 </div>
