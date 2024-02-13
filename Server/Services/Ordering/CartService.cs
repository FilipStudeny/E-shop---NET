using Ecommerce.Shared.Orders;
using Ecommerce.Shared;
using Ecommerce.Server.Database;
using Ecommerce.Server.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.Ordering
{
	public class CartService : ICartService
	{
		private readonly DataContext dataContext;
		private readonly IUserService authenticationService;

		public CartService(DataContext dataContext, IUserService authenticationService)
        {
			this.dataContext = dataContext;
			this.authenticationService = authenticationService;
		}
        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
		{
			cartItem.UserId = authenticationService.GetUserId();
			var sameItemInCart = await dataContext.CartItems
				.FirstOrDefaultAsync(cart =>
					cart.BookId == cartItem.BookId &&
					cart.BookTypeId == cartItem.BookTypeId &&
					cart.UserId == cartItem.UserId
				);

			if(sameItemInCart == null)
			{
				dataContext.CartItems.Add(cartItem);
			}
			else
			{
				sameItemInCart.Quantity += cartItem.Quantity;
			}

			await dataContext.SaveChangesAsync();
			return new ServiceResponse<bool> { Data = true };
		}

		public async Task<ServiceResponse<int>> GetCartItemsCount()
		{
			var count = (await dataContext.CartItems.Where(c => c.UserId == authenticationService.GetUserId()).ToListAsync()).Count;
			return new ServiceResponse<int> { Data = count };
		}

		public async Task<ServiceResponse<List<CartDTO>>> GetCartProducts(List<CartItem> cartItems)
		{
			var result = new ServiceResponse<List<CartDTO>> { Data = new List<CartDTO>() };

			foreach(var item in cartItems)
			{
				var book = await dataContext.Books.Where(b => b.Id == item.BookId).Include(book => book.Images).FirstOrDefaultAsync();
				if(book == null) { continue; }

				var bookVariant = await dataContext.BookVariants
					.Where(v => v.BookId == item.BookId && v.BookTypeId == item.BookTypeId)
					.Include(t => t.BookType).FirstOrDefaultAsync();
				if(bookVariant == null) { continue; }

				var cartItem = new CartDTO
				{
					BookId = book.Id,
					BookTitle = book.Title,
					BookTypeId = bookVariant.BookTypeId,
					BookType = bookVariant.BookType.Name,
					Image = book.DefaultImageUrl,
					Price = bookVariant.Price,
					Quantity = item.Quantity
				};

				result.Data.Add(cartItem);
			}

			return result;
		}

		public async Task<ServiceResponse<List<CartDTO>>> GetDbCartProducts(int? userId = null)
		{
			userId ??= authenticationService.GetUserId();
			var cart = await dataContext.CartItems.Where(cart => cart.UserId == userId).ToListAsync();
			return await GetCartProducts(cart);
		}

		public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
		{
			var item = await dataContext.CartItems
				.FirstOrDefaultAsync(cart =>
					cart.BookId == productId &&
					cart.BookTypeId == productTypeId &&
					cart.UserId == authenticationService.GetUserId());

			if (item == null)
			{
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = "Cart item does not exist."
				};
			}

			dataContext.CartItems.Remove(item);
			await dataContext.SaveChangesAsync();
			return new ServiceResponse<bool> { Data = true };
		}

		public async Task<ServiceResponse<List<CartDTO>>> StoreCartItems(List<CartItem> cartItems)
		{
			var userId = authenticationService.GetUserId();
			cartItems.ForEach(item => item.UserId = userId);
			dataContext.CartItems.AddRange(cartItems);
			await dataContext.SaveChangesAsync();

			return await GetDbCartProducts();
		}

		public async Task<ServiceResponse<bool>> UpdateQauntity(CartItem cartItem)
		{
			var sameItemInCart = await dataContext.CartItems
			   .FirstOrDefaultAsync(cart =>
				   cart.BookId == cartItem.BookId &&
				   cart.BookTypeId == cartItem.BookTypeId&&
				   cart.UserId == authenticationService.GetUserId());

			if (sameItemInCart == null)
			{
				return new ServiceResponse<bool>
				{
					Data = false,
					Success = false,
					Message = "Cart item does not exist."
				};
			}

			sameItemInCart.Quantity = cartItem.Quantity;
			await dataContext.SaveChangesAsync();
			return new ServiceResponse<bool>
			{
				Data = true
			};
		}
	}
}
