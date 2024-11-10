using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lexicon_university.Web.Services
{
    public class GetCoursesService : IGetCoursesService
    {
        private readonly LexiconUniversityContext context;
        public GetCoursesService(LexiconUniversityContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<SelectListItem>> GetCoursesAsync()
        {
            return await context.Course.Select(c => new SelectListItem
            {
                Text = c.Title.ToString(),
                Value = c.Id.ToString()
            }).ToListAsync();
        }
    }
}
