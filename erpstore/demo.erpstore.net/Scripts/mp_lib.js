/// Windows PNG

var bgsleight	= function() {
	
	function addLoadEvent(func) {
		var oldonload = window.onload;
		if (typeof window.onload != 'function') {
			window.onload = func;
		} else {
			window.onload = function() {
				if (oldonload) {
					oldonload();
				}
				func();
			}
		}
	}
	
	function fnLoadPngs() {
		var rslt = navigator.appVersion.match(/MSIE (\d+\.\d+)/, '');
		var itsAllGood = (rslt != null && Number(rslt[1]) >= 5.5);
		for (var i = document.all.length - 1, obj = null; (obj = document.all[i]); i--) {
			if (itsAllGood && obj.currentStyle.backgroundImage.match(/\.png/i) != null) {
				fnFixPng(obj);
				obj.attachEvent("onpropertychange", fnPropertyChanged);
			}
		}
	}

	function fnPropertyChanged() {
		if (window.event.propertyName == "style.backgroundImage") {
			var el = window.event.srcElement;
			if (!el.currentStyle.backgroundImage.match(/x\.gif/i)) {
				var bg	= el.currentStyle.backgroundImage;
				var src = bg.substring(5,bg.length-2);
				el.filters.item(0).src = src;
				el.style.backgroundImage = "url(/content/images/space.png)";
			}
		}
	}

	function fnFixPng(obj) {
		var bg	= obj.currentStyle.backgroundImage;
		var src = bg.substring(5,bg.length-2);
		obj.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "', sizingMethod='scale')";
		obj.style.backgroundImage = "url(/content/images/space.png)";
	}
	
	
	
	
	return {
		
		init: function() {
			
			if (navigator.platform == "Win32" && navigator.appName == "Microsoft Internet Explorer" && window.attachEvent) {
				addLoadEvent(fnLoadPngs);
			}
			
		}
	}
	
}();

bgsleight.init();


/// slideshow de div ///

var cur_slide = 0; //numero d'ordre du layer a afficher
	var zone_slide = 'content_slide';//nom de la zone de slide
	var balise_slide = 'div'; //balise presente a l'interieur de la zone de slide
	var timer = 10000; // delais en millisecondes

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


/// Pub en fin de chargement ///

function pub() 

{
	document.getElementById("pub").style.display="block";
	document.getElementById("pub2").style.display="block";
	document.getElementById("pub3").style.display="block";
	document.getElementById("pub4").style.display="block";
	document.getElementById("pub5").style.display="block";
	document.getElementById("pub6").style.display="block";
	document.getElementById("pub7").style.display="block";
	document.getElementById("pub8").style.display="block";
}



/// Montrer-cacher les div ///

function montre(id) {
	  if (document.getElementById) {
		  document.getElementById(id).style.display="block";
		} else if (document.all) {
		  document.all[id].style.display="block";
		} else if (document.layers) {
		  document.layers[id].display="block";
		} } 

 function cache(id) {
	  if (document.getElementById) {
		  document.getElementById(id).style.display="none";
		} else if (document.all) {
		  document.all[id].style.display="none";
		} else if (document.layers) {
		  document.layers[id].display="none";
		} } 



/// Form archives spip

function navigate(form) 
{
 var go = (form.menu.options[form.menu.selectedIndex].value);
 document.location=go;
}

