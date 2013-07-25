<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function(){
                 LoadBanner('banners');
            });
        function enlarge(ctr)
        {
            var gridItem = $('#' + ctr.id);
            var className =gridItem.attr('class');
            
            
            if(gridItem.hasClass('gridItem'))
            {
                //grid
                
                gridItem.toggleClass('gridItem_on');
            }
            else
            {
                //list
                gridItem.toggleClass('gridItem_v_on');
            }
        }
        function displaySwitch(type)
        {
            var grid = $(".topic .grid");
            var list = $(".topic .list");
            if(type == 'grid')
            {
                if(grid.hasClass('grid_on'))
                    grid.removeClass('grid_on');
                else
                    grid.addClass('grid_on');
            }
            else
            {
                if(list.hasClass('list_on'))
                    list.removeClass('list_on');
                else
                    list.addClass('list_on');   
            }
        }
        function changeDisplay(ctr, id)
        {
            var source = $('#' + ctr.id);
            if(source.hasClass('listview'))
            {
                $('#' + id + ' .gridItem').removeClass('gridItem').addClass('gridItem_v');
            }
            else
            {
                $('#' + id + ' .gridItem_v').removeClass('gridItem_v').addClass('gridItem');
            }
        }
        function LoadBanner(ctr)
        {
            $('#' + ctr + ' .banner:gt(0) ').css("display","none");
            $('#' + ctr + ' ul li a').mouseover(function(){
                var index = $('#' + ctr + ' ul li a').index(this);
                $('#' + ctr + ' .banner').css("display","none");
                $('#' + ctr + ' .banner:eq(' + index+ ') ').fadeIn('slow');
            });
        }
    </script>
        <div id="banners" class="banners">
            <div class="banner">
                <img src="../../Content/images/Banner.gif" />
            </div>
            <div class="banner">
                <img src="../../Content/images/Banner_2.png" />
            </div>
            <div class="banner">
                <img src="../../Content/images/Banner_1.png" />
            </div>
            <ul>
                <li><a href="#0" >ASP.NET MVC Design Competition<img src="defaultIcon.jpg"></a></li>
                <li><a href="#1" >SQL Server 2008 Express<img src="defaultIcon.jpg"></a></li>
                <li><a href="#2" >Visual Studio 2008<img src="defaultIcon.jpg"></a></li>
            </ul>
        </div>
        <div id="gridTopic" class="topic">
            <h2>Feature Products</h2>
            <div class="display">
                <div id="gridTopic_grid" class="gridview" onmouseover="displaySwitch('grid')" onmouseout="displaySwitch('grid')" onclick="changeDisplay(this, 'grid')"></div>
                <div id="gridTopic_list" class="listview" onmouseover="displaySwitch('list')" onmouseout="displaySwitch('list')" onclick="changeDisplay(this, 'grid')"></div>
            </div>
        </div>
        <%Html.ShowProductList(ProductListType.New, "HomeProductList.ascx");%>
</asp:Content>
