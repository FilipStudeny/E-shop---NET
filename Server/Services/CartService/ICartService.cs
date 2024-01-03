﻿using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;

namespace Eshop.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartDto>>> GetCartProducts(List<CartItem> cartItems);
    Task<ServiceResponse<List<CartDto>>> StoreCartItems(List<CartItem> cartItems);
    Task<ServiceResponse<int>> GetCartItemsCount();
    Task<ServiceResponse<List<CartDto>>> GetDbCartProducts();
    Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
    Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
    Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId);
}