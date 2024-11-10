using Microsoft.AspNetCore.Mvc.Rendering;

namespace lexicon_university.Web.Services
{
    public interface IGetCoursesService
    {
        Task<IEnumerable<SelectListItem>> GetCoursesAsync();
    }
}
