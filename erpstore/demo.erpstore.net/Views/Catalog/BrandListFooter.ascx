<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Brand>>" %>

<div class="menu_brands">
	<table style="border:0; margin:0; padding:0;">
    	<tr>
	<% int count = 0; %>
	<% foreach (var item in Model) { %>
	<% if (item.DefaultImage == null) continue; %>
	<% if (count > 8) break; %>
	<td style="vertical-align:middle; text-align:center; padding:0; margin:0; background-color : White;">
		<a href="<%=Url.Href(item) %>">
			<img src="<%=Url.ImageSrc(item, 90, "")%>" alt="<%=item.Name %>"/>
		</a>
	</td>
	<% count++;	} %>
	</tr>
    </table>
</div>