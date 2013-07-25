using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERPStore.Models;

namespace ERPStore.Services
{
	/// <summary>
	/// Service de gestion des paniers de commande
	/// </summary>
	public interface ICartService
	{
		/// <summary>
		/// Retourne le panier de type commande en cours
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		OrderCart GetCurrentOrderCart(Models.UserPrincipal user);
		/// <summary>
		/// Retourne le panier de type devis en cours
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		QuoteCart GetCurrentQuoteCart(Models.UserPrincipal user);
		/// <summary>
		/// Retourne le panier courant de type commande ou creation d'un nouveau
		/// si celui-ci n'existe pas
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		OrderCart GetOrCreateOrderCart(Models.UserPrincipal user);
		/// <summary>
		/// Retourne le panier courant de type devis ou creation d'un nouveau
		/// si celui-ci n'existe pas
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		QuoteCart GetOrCreateQuoteCart(Models.UserPrincipal user);
		/// <summary>
		/// Creation d'un panier de type commande
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		OrderCart CreateOrderCart(Models.UserPrincipal user);
		/// <summary>
		/// Creation d'un nouveau panier a partir d'un panier existant pour un 
		/// visiteur donné
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="visitorId">The visitor id.</param>
		/// <returns></returns>
		OrderCart CreateOrderCart(Models.OrderCart cart, string visitorId);
		/// <summary>
		/// Creation d'un panier de type devis
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		QuoteCart CreateQuoteCart(Models.UserPrincipal user);
		/// <summary>
		/// Création et sauvegarde un panier de type commande
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		OrderCart CreateAndSaveOrderCart(Models.UserPrincipal user);
		/// <summary>
		/// Création et sauvegarde un panier de type devis
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		QuoteCart CreateAndSaveQuoteCart(Models.UserPrincipal user);
		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		CartBase GetActiveCartById(string cartId);

		/// <summary>
		/// Gets the cart by id.
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		CartBase GetCartById(string cartId);

		/// <summary>
		/// Recupere un panier de type commande via son ID
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		OrderCart GetOrderCartById(string id);

		/// <summary>
		/// Retourne la liste de tous les paniers non completés pour un
		/// utilisateur donné
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		IList<CartBase> GetCurrentList(Models.UserPrincipal user);

		/// <summary>
		/// Retourne la liste de tous les paniers de type commande non completés pour un
		/// utilisateur donné
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		IList<Models.OrderCart> GetCurrentOrderList(Models.UserPrincipal user);

		/// <summary>
		/// Retourne la liste de tous les paniers de type devis non completés pour un
		/// utilisateur donné
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		IList<Models.QuoteCart> GetCurrentQuoteList(Models.UserPrincipal user);

		/// <summary>
		/// Permet d'ajouter un produit dans le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="product">The product.</param>
		/// <param name="quantity">The quantity.</param>
		/// <param name="salePrice">The sale price.</param>
		/// <param name="isCustomerPriceApplied">if set to <c>true</c> [is customer price applied].</param>
		void AddItem(CartBase cart, Product product, int quantity, Price salePrice, bool isCustomerPriceApplied);
		/// <summary>
		/// Retire un item du panier en fonction de sa position dans la liste
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="index">The index.</param>
		void RemoveItem(CartBase cart, int index);
		/// <summary>
		/// Vide le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		void Clear(CartBase cart);
		/// <summary>
		/// Sauvegarde le panier en cours
		/// </summary>
		/// <param name="cart">The cart.</param>
		void Save(CartBase cart);
		/// <summary>
		/// Retire un panier de la liste des paniers en cours (tout utilisateurs confondus)
		/// </summary>
		/// <param name="cart">The cart.</param>
		void RemoveCart(CartBase cart);
		/// <summary>
		/// Supprime un panier de type commande de la liste d'un utilisateur
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <param name="user">The user.</param>
		void DeleteCart(string cartId, Models.UserPrincipal user);
		/// <summary>
		/// Change le panier de type commande courant par celui indiqué
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <param name="user">The user.</param>
		void ChangeCurrentCart(string cartId, Models.UserPrincipal user);

		/// <summary>
		/// Change le panier de type commande courant par celui indiqué
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <param name="user">The user.</param>
		void ChangeCurrentCart(string cartId, string visitorId);

		/// <summary>
		/// Adds the cart.
		/// </summary>
		/// <param name="cart">The cart.</param>
		void AddCart(CartBase cart);

		/// <summary>
		/// Retourne un panier en fonction de son entité créatrice
		/// </summary>
		/// <param name="entityId">The entity id.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
        OrderCart GetOrderCartByConvertedEntityId(int entityId, Models.UserPrincipal user);

		/// <summary>
		/// Retourne un panier de type devis en fonction de son entité créatrice
		/// </summary>
		/// <param name="entityId">The entity id.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		QuoteCart GetQuoteCartByConvertedEntityId(int entityId, Models.UserPrincipal user);

		/// <summary>
		/// Recalcule le prix d'un item du panier en fonction de la quantité
		/// </summary>
		/// <param name="cartItem">The cart item.</param>
		/// <param name="quantity">The quantity.</param>
		void RecalcCartItem(CartItem cartItem, int quantity);

		/// <summary>
		/// Affecte le panier en cours au visiteur
		/// </summary>
		/// <param name="userId">The user id.</param>
		/// <param name="visitorId">The visitor id.</param>
		void SetCurrentCart(int userId, string visitorId);

		/// <summary>
		/// Conversion d'un panier de type commande en panier de type devis
		/// </summary>
		/// <param name="orderCart">The order cart.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		QuoteCart ConvertToQuoteCart(OrderCart orderCart, UserPrincipal user);

		/// <summary>
		/// Retourne la liste des derniers items ajoutés aux paniers de commande
		/// </summary>
		/// <param name="itemCount">The item count.</param>
		/// <returns></returns>
		IEnumerable<CartItem> GetLastCartItem(int itemCount);

		/// <summary>
		/// Creation d'une ligne de panier
		/// </summary>
		/// <returns></returns>
		Models.CartItem CreateCartItem();

		/// <summary>
		/// Retourne la liste des commentaires pour un panier donné
		/// </summary>
		/// <returns></returns>
		IList<Models.Comment> GetCommentListByCart(OrderCart cart);

		/// <summary>
		/// Applique le stock en temps réel au panier 
		/// </summary>
		/// <returns></returns>
		void ApplyProductStockInfoList(Models.OrderCart cart);

	}
}
