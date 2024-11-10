using lexicon_university.Persistance.Data;
using LexiconUniversity.Persistance;
using Microsoft.EntityFrameworkCore;

namespace lexicon_university.Web.Extentions
{
    public static class WebAppExtentions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<LexiconUniversityContext>();
                // await context.Database.EnsureDeletedAsync();
                // await context.Database.MigrateAsync();
                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

    }
}
