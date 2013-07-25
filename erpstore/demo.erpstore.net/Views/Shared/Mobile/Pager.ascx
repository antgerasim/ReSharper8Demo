<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IPaginable>" %>
<%=Html.FirstPageLink(Model, "<< Premier")%>
&nbsp;|&nbsp;
<%=Html.PreviousPageLink(Model, "< Précedent")%>
&nbsp;<span>&nbsp;
Page : <b><%=Model.PageIndex%></b> / <%=Model.GetPageCount()%>
&nbsp;</span>&nbsp;
<% =Html.NextPageLink(Model, "Suivant >")%>
&nbsp;|&nbsp;
<% =Html.LastPageLink(Model, "Dernier >>")%>
