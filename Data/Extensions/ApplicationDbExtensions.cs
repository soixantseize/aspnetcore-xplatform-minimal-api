using System.Linq;
using Microsoft.EntityFrameworkCore;
using AspNetCoreBlogService.Data.Entities;
using AspNetCoreBlogService.Data;

namespace AspNetCoreBlogService.Data.Extensions
{
    public static class ApplicationDbExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.BlogArticles.Any())
                {
                    context.BlogArticles.AddRange(
                        new BlogArticle { ArticleTitle = "Google", ArticleContent = "Google" },
                        new BlogArticle { ArticleTitle = "Microsoft", ArticleContent = "Google"  },
                        new BlogArticle { ArticleTitle = "Facebook", ArticleContent = "Google"  },
                        new BlogArticle { ArticleTitle = "Salesforce", ArticleContent = "Google"  },
                        new BlogArticle { ArticleTitle = "Oracle", ArticleContent = "Google"  });

                    context.SaveChanges();
                }
            }
        }
    }
}