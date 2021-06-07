using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HousePlants.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
        public string DoTest()
        {
            return "Index";
        }
    }
}