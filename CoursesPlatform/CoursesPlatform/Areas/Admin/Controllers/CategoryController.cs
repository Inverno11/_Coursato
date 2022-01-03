using AutoMapper;
using CoursesPlatform.App_Start;
using CoursesPlatform.Database.CoursesPlatform;
using CoursesPlatform.Models;
using CoursesPlatform.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoursesPlatform.Areas.Admin.Controllers
{   
    [Authorize(Roles ="Admins")]
    public class CategoryController : Controller
    {
        private readonly IMapper mapper;
        // GET: Admin/Category

        public CategoryController()
        {
            mapper = AutoMapperConfig.Mapper;


        }
        public ActionResult Index()
        {

            CategoryService categoryService = new CategoryService();
            List<Category> categories = categoryService.ReadAllCategory();
            List<CategoryModel> categoryModelsList = mapper.Map< List<CategoryModel>>(categories);
            return View(categoryModelsList);


            /*    foreach (var cat in categories)
                {
                    if (cat.ParentID != null)
                    {
                        categoryModelsList.Add

                        (
                        new CategoryModel
                        {
                            ID = cat.ID,
                            Name = cat.Name,
                            ParentID = cat.ParentID,
                            ParentName = cat.Category2?.Name

                        }
                        );
                    }
                    else
                    {
                        categoryModelsList.Add

                        (
                        new CategoryModel
                        {
                            ID = cat.ID,
                            Name = cat.Name,
                            

                        }
                        );
                    }
                }
            }
            */




        }

        public ActionResult Create()
        {
            
                CategoryModel categoryModel = new CategoryModel();
                InitMainList(null,ref categoryModel);

                return View(categoryModel);
           

        }
        [HttpPost]
        public ActionResult Create(CategoryModel categoryModel)
        {

            if (ModelState.IsValid)
            {

                CategoryService categoryService = new CategoryService();
                Category category = mapper.Map<Category>(categoryModel);
                category.Category2 = null;

                var isExist = categoryService.Add(category);

                if (isExist == -2)
                {
                    InitMainList(null,ref categoryModel);
                    ViewBag.Message = "The Category is Exist";
                    return View(categoryModel);
                }
                else
                {
                    return RedirectToAction("Index", "Category");
                }
            }
            else
            {
                InitMainList(null,ref categoryModel);
                return View(categoryModel);
            }
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {

            CategoryService categoryService = new CategoryService();
            Category category = categoryService.GetCurrentCategory(ID);


            CategoryModel CurrentCategoryModel = new CategoryModel
            {
                Name = category.Name,
                ID = category.ID,
                ParentID = category.ParentID
            };
            InitMainList(ID,ref CurrentCategoryModel);


            return View(CurrentCategoryModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModel categoryModel)
        {
            InitMainList(categoryModel.ID, ref categoryModel);

            if (ModelState.IsValid)
            {
                //InitMainList(ref categoryModel);
                Category category = new Category
                {
                    Name = categoryModel.Name,
                    ID = categoryModel.ID,
                    ParentID = categoryModel.ParentID.Value
                };



                CategoryService categoryService = new CategoryService();
                if (categoryService.Edit(category) == -3)

                {
                    InitMainList(categoryModel.ID, ref categoryModel);
                    ViewBag.Message = "Category Already Exist";
                    return View(categoryModel);
                }
                else
                {
                    InitMainList(categoryModel.ID, ref categoryModel);
                    return RedirectToAction("Index", "Category");
                }
            }
            InitMainList(categoryModel.ID, ref categoryModel);
            return View(categoryModel);
        }



        public ActionResult Delete(int ID)
        {
            CategoryService categoryService = new CategoryService();
            var DeletedCategory = categoryService.GetCurrentCategory(ID);
            if (DeletedCategory != null)
            {
                CategoryModel CurrentCategoryModel = new CategoryModel
                {
                    Name = DeletedCategory.Name,
                    ID = DeletedCategory.ID,
                    ParentID = DeletedCategory.ParentID,
                    ParentName = DeletedCategory.Category2?.Name

                };

                return View(CurrentCategoryModel);

            }

            else { return View(); } 
        }
        [HttpPost]
        public ActionResult DeleteConfirm(int ID)
        {
            if(ID!=null)
            {
                CategoryService categoryService = new CategoryService();
                categoryService.Delete(ID);
                return RedirectToAction("Index");

            }
            else 
            {
                return View();
            }




        }





        private void InitMainList(int? ID,ref CategoryModel categoryModel)
        {
            CategoryService categoryService = new CategoryService();
            Category ExcludeCategory = new Category();
            if (ID!=null)
            {  ExcludeCategory = categoryService.GetCurrentCategory(ID.Value);
            }
            List <Category> catogries = categoryService.ReadAllCategory();
            catogries.Remove(ExcludeCategory);
           categoryModel.MainCategory = new SelectList(catogries, "ID", "Name");
        }
        
    }
}