﻿using Eshop.Shared.DTOs;
using Eshop.Shared.Models.Cart;

namespace Eshop.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartDto>>> GetProductsInCart(List<CartItem> cartItems);

}