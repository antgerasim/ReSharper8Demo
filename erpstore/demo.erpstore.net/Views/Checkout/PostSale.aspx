<%@ Page Title="" Language="C#" MasterPageFile="Order.Master" Inherits="System.Web.Mvc.ViewPage<Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>Validation en cours</span>
          </h2>
        <div class="chemin">
            <span><a href="/" title="accueil">accueil</a></span><b>Votre commande</b>
        </div>
            <div class="corner aide">
                <h3><a name="virement_bancaire">Nous vous remercions pour votre confiance</a></h3>
                <div class="corner texte bloc">
                    <script type="text/javascript">
                        var loop = 0;
                        $(function() {
                            showLastOrder();
                        });
            
                    function showLastOrder() {
                        $('#lastorder').empty();
                        $('#lastorder').load('<%=Url.LastOrderHref() %>', {
								lastorderviewname: 'lastorder.ascx',
								waitingviewname: 'waitingorder.ascx'
							}
							, function(html) {
                            $('#lastorder')[0].value = html;
                            if (loop < 20) {
                                loop++;
                                if (html.indexOf('<!--Waiting-->') != -1) {
                                    setTimeout(showLastOrder, 1000 * 5);
                                }
                            }
                        });
                    }
                            
                    </script>
                    <div id="lastorder">
                    </div>
            
                </div>
            </div>
	</div>
</div>

</asp:Content>