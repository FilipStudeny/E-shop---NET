﻿using Eshop.Shared.Models;
using Eshop.Shared.Models.ProductModels;

namespace Eshop.Client.Services.CategoryService;

public interface ICategoryService
{
    event Action OnChange;
    List<Category> Categories { get; set; }
    List<Category> AdminCategories { get; set; }

    Task GetCategories();
    Task GetAdminCategories();
    Task AddCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(int id);
    Category CreateNewCategory();
}