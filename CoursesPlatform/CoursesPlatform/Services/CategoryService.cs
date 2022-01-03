using CoursesPlatform.Database.CoursesPlatform;
using System.Collections.Generic;
using System.Linq;

namespace CoursesPlatform.Services
{
    public interface ICategoryService
    {
         List <Category> ReadAllCategory();
          int Add(Category category);
        int Edit(Category category);

        Category GetCurrentCategory(int ID);

        bool Delete(int ID);

    }
    public class CategoryService : ICategoryService
    {
        public CoursesPlatformEntities context { set; get; }

        public CategoryService()
        {
            context = new CoursesPlatformEntities();
        }
        public List<Category> ReadAllCategory() 
        {
            
          var returned=  context.Categories.ToList();

            return returned;


        }

        public int Add(Category category)
        {
            var CategoryName = category.Name.ToLower();
           var CategoryIsExist= context.Categories.Where(x => x.Name == CategoryName).Any();

            if(CategoryIsExist)
            {
                return -2; 
            }

            else
            {
                context.Categories.Add(category);
                return context.SaveChanges();

            }
        }

        public int Edit(Category category)

        {

            var CategoryName = category.Name.ToLower();
            var CategoryIsExist = context.Categories.Where(x => x.Name == CategoryName).Any();
            if (CategoryIsExist)
            {
                return -3;

            }
            else
            {
                var EditedRecorde = context.Categories.Where(x => x.ID == category.ID).FirstOrDefault();
                EditedRecorde.Name = category.Name;
                EditedRecorde.ParentID = category.ParentID;
             return  context.SaveChanges();
            }
        }

        public Category GetCurrentCategory(int ID)
        { 
            return context.Categories.Find(ID);
        }

        public bool Delete(int ID)
        {
            var Category = GetCurrentCategory(ID);
            if (Category != null)
            {
                context.Categories.Remove(Category);
                return context.SaveChanges() > 0 ? true : false;
            }
            else
            {
                return false;
            }



        }

        
    }
}