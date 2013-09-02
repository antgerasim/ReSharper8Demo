<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<% 
string ico = null;
string description = null;
switch (Model.SelectedPrice)
{
	case PriceType.Normal:
		if (Model.IsTopSell)
		{
			ico = "/content/images/icos/ico_prodtype_best.png";
			description = "Top Ventes !";
		}
		else if (Model.IsFirstPrice)
		{
			ico = "/content/images/icos/ico_prodtype_first.png";
			description = "Prix Plancher !";
		}
		else if (Model.IsNew)
		{
			ico = "/content/images/icos/ico_prodtype_new.png";
			description = "Nouveauté !";
		}
		break;
	case PriceType.Promotional:
		ico = "/content/images/icos/ico_prodtype_promo.png";
		description = "Promotion !";
		break;
	case PriceType.Destock:
		ico = "/content/images/icos/ico_prodtype_destock.png";
		description = "Destockage !";
		break;
	case PriceType.Customer:
		break;
	default:
		break;
}
if (ico != null)  
{
	Response.Write(string.Format("<img src=\"{0}\" alt=\"{1}\" />", ico, description));
}
%>

