using APISolution.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using SampleMVC.Services;
using SampleMVC.ViewModels;

namespace SampleMVC.Controllers;

public class CategoriesController : Controller
{
    //private readonly ICategoryBLL _categoryBLL;
    private readonly ICategoryServices _categoryServices;

    public CategoriesController(/*ICategoryBLL categoryBLL, */ICategoryServices categoryServices)
    {
        //_categoryBLL = categoryBLL;
		_categoryServices = categoryServices;
    }

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string search = "", string act = "")
    {
        //pengecekan session username
        //if (HttpContext.Session.GetString("user") == null)
        //{
        //    TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Anda harus login terlebih dahulu !</div>";
        //    return RedirectToAction("Login", "Users");
        //}

        //if (TempData["message"] != null)
        //{
        //    ViewData["message"] = TempData["message"];
        //}

        //ViewData["search"] = search;
        //var models = _categoryServices.GetByPage(pageNumber, pageSize);
        var maxsizeTask = _categoryServices.GetCount();
        var maxsize = await maxsizeTask;
        //return Content($"{pageNumber} - {pageSize} - {act}");

        if (act == "next")
        {
            if (pageNumber * pageSize < maxsize)
            {
                pageNumber += 1;
            }
            ViewData["pageNumber"] = pageNumber;
        }
        else if (act == "prev")
        {
            if (pageNumber > 1)
            {
                pageNumber -= 1;
            }
            ViewData["pageNumber"] = pageNumber;
        }
        else
        {
            ViewData["pageNumber"] = 2;
        }

        ViewData["pageSize"] = pageSize;
        //ViewData["action"] = action;


        return View(/*models*/);
    }

	public async Task<IActionResult> GetFromService()
	{
		var categories = await _categoryServices.GetAll();
		List<Category> categoriesList = new List<Category>();
		foreach (var category in categories)
		{
			categoriesList.Add(new Category
			{
				CategoryID = category.CategoryID,
				CategoryName = category.CategoryName
			});
		}
		return View(categoriesList);
	}

	public async Task<IActionResult> Detail(int id)
    {
        var model = await _categoryServices.GetById(id);
        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CategoryCreateDTO categoryCreate)
    {
        try
        {
            _categoryServices.Create(categoryCreate);

			//ViewData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Add Data Category Success !</div>";
			TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Add Data Category Success !</div>";
        }
        catch (Exception ex)
        {
            //ViewData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
        }
        return RedirectToAction("GetFromService");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await _categoryServices.GetById(id);
        if (model == null)
        {
            TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CategoryUpdateDTO categoryEdit)
    {
        try
        {
            await _categoryServices.Update(id, categoryEdit);
            TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Edit Data Category Success !</div>";
        }
        catch (Exception ex)
        {
            ViewData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            return View(categoryEdit);
        }
        return RedirectToAction("GetFromService");
    }



    public async Task<IActionResult> Delete(int id)
    {
        var model = await _categoryServices.GetById(id);
        if (model == null)
        {
            TempData["message"] = @"<div class='alert alert-danger'><strong>Error!</strong>Category Not Found !</div>";
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CategoryDTO category)
    {
        try
        {
            await _categoryServices.Delete(id);
            TempData["message"] = @"<div class='alert alert-success'><strong>Success!</strong>Delete Data Category Success !</div>";
        }
        catch (Exception ex)
        {
            TempData["message"] = $"<div class='alert alert-danger'><strong>Error!</strong>{ex.Message}</div>";
            return View(category);
        }
        return RedirectToAction("GetFromService");
    }

    public IActionResult DisplayDropdownList()
    {
        var categories = _categoryServices.GetAll();
        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public IActionResult DisplayDropdownList(string CategoryID)
    {
        ViewBag.CategoryID = CategoryID;
        ViewBag.Message = $"You selected {CategoryID}";

        ViewBag.Categories = _categoryServices.GetAll();

        return View();
    }

}
