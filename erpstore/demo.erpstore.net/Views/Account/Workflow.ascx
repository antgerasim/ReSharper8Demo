<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<WorkflowList>" %>
<% var list = from doc in Model
   group doc by new { doc.CreationDate.Year, doc.CreationDate.Month, doc.CreationDate.Day } into g
   orderby g.Key.Year, g.Key.Month , g.Key.Day 
   select new { ShortDate = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day), List = g };
%>
<h2 class="titleh2"><span>Dossier</span></h2>                         
<div class="bloc texte">
     <div class="corner bloc_type_liste bloc_workflow">
		
        <ul>			  
        <% foreach (var day in list) { %>
            <li>
                <span><%=string.Format("{0:dddd dd MMMM yyyy}", day.ShortDate)%></span>
                <ul>
                <% foreach (var item in day.List) { %>
                    <% if (item.IsSelected) { %>
                      <% switch (item.Type) { %>
                        <%	case SaleDocumentType.Order: %>
                        <li class="workflow_order workflow_on">                       
                        <%		break;
                            case SaleDocumentType.Quote: %>
                        <li class="workflow_quote workflow_on">             
                        <%		break;
                            case SaleDocumentType.Delivery: %>
                        <li class="workflow_delivery workflow_on">             
                        <%		break;
                            case SaleDocumentType.Invoice: %>
                        <li class="workflow_invoice workflow_on">
                        <% 		break; %>
                        <% } %>
                       <b><%=item.Title%></b>
                       <a href="<%=item.DownloadUrl %>" target="_blank" class="workflow_download" title="télécharger">
                            <img src="/content/images/ico_pdf.png" alt="télécharger" align="absmiddle"/>
                        </a>
                    </li>
                    <% } else { %>
                        <% switch (item.Type) { %>
                        <%	case SaleDocumentType.Order: %>
                        <li class="workflow_order workflow_off">    
                        <%		break;
                            case SaleDocumentType.Quote: %>
                        <li class="workflow_quote workflow_off">    
                        <%		break;
                            case SaleDocumentType.Delivery: %>
                        <li class="workflow_delivery workflow_off">    
                        <%		break;
                            case SaleDocumentType.Invoice: %>
                        <li class="workflow_invoice workflow_off">    
                        <% 		break; %>
                        <% } %>
                        <% if (!item.Url.IsNullOrTrimmedEmpty()) { %>
                        <a href="<%=item.Url%>" title="<%=item.Title%> : " class="workflow_url">
                            <%=item.Title%>
                        </a>
                        <% } else { %> 
							<span><%=item.Title%></span>
                        <% } %>
                        <a href="<%=item.DownloadUrl %>" target="_blank" class="workflow_download" title="télécharger">
                            <img src="/content/images/ico_pdf_little.png" alt="télécharger le document associé" align="absmiddle"/>
                        </a>
                    </li>    	
                    <% } %>
                <% } %>
                </ul>
            </li>
        <% } %>
        </ul>
    </div>
</div> 