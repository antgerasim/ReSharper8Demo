<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="searchbox">
	<h2>Recherche</h2>
	<% Html.BeginERPStoreRouteForm("ProductSearch", FormMethod.Get, new { name = "searchform" }); %>
    <div class="search_elements">
		<% 
			var searchTerm = Request["s"];
			if (!searchTerm.IsNullOrTrimmedEmpty())
			{
				searchTerm = searchTerm.Replace("\"", "&quot;"); 
			}	
		%>
        <input type="text" name="s" class="textsearch" id="schbox" value="<%=searchTerm%>"/>
        <input type="submit" class="btsearch" id="" value=""/>
    </div>
    <% Html.EndForm(); %>
</div>
<script type="text/javascript">
	$(function() {

		if ($("#schbox").val() == '') {
			$("#schbox").val('exemple : tournevis');
			// $("#schbox").setSelectionRange(0, 20);
			// $("#schbox").setCursorPosition(0);
		}

		$("#schbox").focus(function() {
			if ($(this).val() == 'exemple : tournevis') {
				$(this).val('');
			}
		});

		$("#schbox").click(function() {
			$("#schbox").focus();
			$("#schbox").select();
		});


		$("#schbox").blur(function() {
			if ($(this).val() == '') {
				$(this).val('exemple : tournevis');
			}
		});
	});
</script>



