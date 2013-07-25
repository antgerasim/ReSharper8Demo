using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	/// <summary>
	/// Stockage des données liées aux paniers
	/// </summary>
	public interface ICartRepository
	{
		/// <summary>
		/// Recupère l'id du panier en cours, en fonction du contexte et du type de paner
		/// </summary>
		/// <returns></returns>
		string GetCurrentCartId(Models.CartType cartType, Models.UserPrincipal user);
		/// <summary>
		/// Recupère l'id du paner en cours en fonction du visiteur
		/// </summary>
		/// <param name="cartType">Type of the cart.</param>
		/// <param name="visitorId">The visitor id.</param>
		/// <returns></returns>
		string GetCurrentCartIdByVisitorId(Models.CartType cartType, string visitorId);
		/// <summary>
		/// Gets the cart with the specified cart id.
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		/// <value></value>
		Models.CartBase GetActiveCartById(string cartId);
		/// <summary>
		/// Retrouve la liste des paniers en cours via l'indentifiant de visiteur
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <returns></returns>
		List<Models.CartBase> GetActiveCartListByVisitorId(string visitorId);
		/// <summary>
		/// Gets the cart by id.
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		Models.CartBase GetCartById(string cartId);
		/// <summary>
		/// Retrouve un panier de type commande via son ID
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		Models.OrderCart GetOrderCartById(string cartId);
		/// <summary>
		/// Sauvegarde le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		void Save(Models.CartBase cart);
		/// <summary>
		/// Supprime le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		void Remove(Models.CartBase cart);
		/// <summary>
		/// Supprime un panier avec son Id
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		void Remove(string cartId);
		/// <summary>
		/// Recupere la liste des paniers d'un utilisateur en cours
		/// </summary>
		/// <returns></returns>
		IQueryable<Models.CartBase> GetList(Models.UserPrincipal user);
		/// <summary>
		/// Recupere la liste des paniers de type Order d'un utilisateur en cours
		/// </summary>
		/// <returns></returns>
		IList<Models.OrderCart> GetOrderList(Models.UserPrincipal user);
		/// <summary>
		/// Recupere la liste des panier de type Devis d'un utilisateur en cours
		/// </summary>
		/// <returns></returns>
		IList<Models.QuoteCart> GetQuoteList(Models.UserPrincipal user);
		/// <summary>
		/// Change le panier courant
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <param name="cartType">Type of the cart.</param>
		/// <param name="user">The user.</param>
		void ChangeCurrent(string cartId, Models.CartType cartType, Models.UserPrincipal user);
		/// <summary>
		/// Change le panier courant pour un visiteur donné
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <param name="visitorId">The visitor id.</param>
		void ChangeCurrent(string cartId, string visitorId);
		/// <summary>
        /// Conversion d'un panier de type devis en panier de type commande
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns></returns>
        Models.CartBase GetQuoteCartByConvertedEntityId(int entityId);
        /// <summary>
        /// Change le type d'un panier
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="cartType">Type of the cart.</param>
        void ChangeType(Models.CartBase cart, Models.CartType cartType);

		/// <summary>
		/// Retourne la liste des derniers items aujoutés aux paniers de commande
		/// </summary>
		/// <param name="itemCount">The item count.</param>
		/// <returns></returns>
		IList<Models.CartItem> GetLastCartItem(int itemCount);

		/// <summary>
		/// Retourne la liste interrogeable des paniers de type commande
		/// </summary>
		/// <returns></returns>
		IList<ERPStore.Models.OrderCart> GetAllOrderCartList(int index, int pageSize);
	}
}
