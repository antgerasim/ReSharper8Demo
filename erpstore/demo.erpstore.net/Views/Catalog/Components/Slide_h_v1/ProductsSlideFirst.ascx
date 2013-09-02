<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>
<div class="bloc" id="content_slide">
	<span class="numSlide"> 
	<%   foreach(var item in ViewData.Model) {  %> 
    	<% if (item.SaleUnitValue == 0) { %>   
        <a class="slideOn" href="javascript:gotoSlide(<%=Model.IndexOf(item)%>)"><%=item.Title.EllipsisAt(20)%></a>
        <% } else { %>
        <a class="slideOff" href="javascript:gotoSlide(<%=Model.IndexOf(item)%>)"><%=item.Title.EllipsisAt(20)%></a>
        <% } %>
   <% } %>
   </span>

    <%   foreach(var item in ViewData.Model) {  
	var visible = (Model.IndexOf(item) == 0) ? "visible" : "hidden";
	%>
    <!-- 1 -->
     <div class="<%=visible%>" id="Slide<%=Model.IndexOf(item)%>">
        <a class="lien_une" title="PLus de détails : <%=Html.Encode(item.Title)%>" href="<%=Url.Href(item)%>">
            <img src="<%=Url.ImageSrc(item, 550, 260, "/content/images/prodvignettebig.gif")%>" alt="plus de détails : <%=item.Title.EllipsisAt(50)%> <%=item.Id%>" />
        </a>
    </div>
   <!-- end 1 -->                    

<% } %>
</div>

<script type="text/javascript">

/// slideshow de div ///

	var cur_slide = 0; //numero d'ordre du layer a afficher
	var zone_slide = 'content_slide';//nom de la zone de slide
	var balise_slide = 'div'; //balise presente a l'interieur de la zone de slide
	var timer = 8000; // delais en millisecondes

	/** ajoute le blur sur le focus **/
	function blurAnchors(){
		if(document.getElementsByTagName) {
		    var divZS = document.getElementById(zone_slide);
		    if (divZS) {
		        var anchors = divZS.getElementsByTagName("div");

		        //collect all anchors A
		        for (var i = 0; i < anchors.length; i++) {
		            // mouse onfocus, blur anchors
		            anchors[i].onfocus = function() { this.blur(); };
		        }
		    }
		}
	}

	/** fin du blur **/


	function arrayToSlide(container,balise)
	{
		if(document.getElementById(container).getElementsByTagName(balise))return document.getElementById(container).getElementsByTagName(balise);
	}

	function slideshow()
	{
		var sliding =  arrayToSlide(zone_slide,balise_slide);
		sliding[cur_slide].className='hidden';
		document.getElementById(zone_slide).getElementsByTagName('span')[0].getElementsByTagName('a')[cur_slide].className='slideOff';

		if((cur_slide+1)<sliding.length)
		{
			sliding[cur_slide+1].className='visible';
			document.getElementById(zone_slide).getElementsByTagName('span')[0].getElementsByTagName('a')[cur_slide+1].className='slideOn';
		}
		if(cur_slide+1>=sliding.length)
		{
		sliding[0].className='visible';
		document.getElementById(zone_slide).getElementsByTagName('span')[0].getElementsByTagName('a')[0].className='slideOn';
		cur_slide=0;
		}
		else
		{
		cur_slide++;
		}
		setTimeout(slideshow,timer);
	}

	function initslide()
	{
		setTimeout(slideshow,timer);
		blurAnchors();
	}

	function gotoSlide(num)
	{
		var sliding =  arrayToSlide(zone_slide,balise_slide);
		sliding[cur_slide].className='hidden';
		document.getElementById(zone_slide).getElementsByTagName('span')[0].getElementsByTagName('a')[cur_slide].className='slideOff';

		if((num)<sliding.length)
		{
			sliding[num].className='visible';
			document.getElementById(zone_slide).getElementsByTagName('span')[0].getElementsByTagName('a')[num].className='slideOn';
		}
		cur_slide=num;
		//Defaire time out precedent et relancer le nouveau
	}

	setTimeout(slideshow,timer);
	blurAnchors();
</script>

