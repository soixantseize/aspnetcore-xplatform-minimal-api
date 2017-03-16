using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreBlogService.Data.Entities;


namespace AspNetCoreBlogService.Data
{
    public class BlogArticleRepository
    {
        public async Task <List<BlogArticle>> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                var dbEntity = await context.BlogArticles.ToListAsync();
                return dbEntity;
            }
        }

        public async Task <BlogArticle> GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var dbEntity = await context.BlogArticles.FirstOrDefaultAsync();
                return dbEntity;
            }
        }

        public async Task <int> AddAsync(BlogArticle value)
        {
            using (var context = new ApplicationDbContext())
            {
                context.BlogArticles.Add(new BlogArticle { ArticleTitle = value.ArticleTitle, ArticleContent = value.ArticleContent});
                return await context.SaveChangesAsync();
            }
        }

        public async Task <int> UpdateAsync(BlogArticle updatedValue)
        {
            using (var context = new ApplicationDbContext())
            {
                var value = await context.BlogArticles.SingleOrDefaultAsync(v => v.Id == updatedValue.Id);
                if (value == null) 
                    return 0;
                value.ArticleTitle = updatedValue.ArticleTitle;
                value.ArticleContent = updatedValue.ArticleContent;

                return await context.SaveChangesAsync();
            }
        }

        public async Task <int> DeleteAsync(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var value = await context.BlogArticles.SingleOrDefaultAsync(v => v.Id == id);

                context.BlogArticles.Remove(value);
                return await context.SaveChangesAsync();
            }
        }
    }
}