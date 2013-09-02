<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Media>>" %>
<% if (Model.IsNotNullOrEmpty()) { %>
<div class="picturelist">
	<% foreach (var item in Model) { %>
		<div class="picture">
			<img src="<%=item.IconeSrc %>" alt="" longdesc='<%=item.Url%>' id="imagepicto<%=Model.IndexOf(item)%>"/>
		</div>
	<% } %>
</div>
<% } %>

<script type="text/javascript">
	$(document).ready(function() {
		var pictoList = $("img[id^='imagepicto']");
		$.each(pictoList, function() {
			var picto = '#' + this.id;
			var imgsrc = this.attributes["longdesc"].nodeValue;
			$(picto).mouseover(function() {
				document.body.style.cursor = 'hand';
				$("#productPicture").attr("src", imgsrc);
				$("#productPicture").attr("longdesc", imgsrc);
			});
			$(picto).mouseout(function() {
				document.body.style.cursor = 'default';
			});
		});
	});
</script>