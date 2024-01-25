using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebPinGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        [BindProperty]
		[Url(ErrorMessage = "Incorrect URL."), Required(ErrorMessage = "URL is required.")]
        public string PageUrl { get; set; }

        public string Pin { get; set; }

        public IActionResult OnPost()
        {
			if (ModelState.IsValid)
            {
                try
                {
					var url = new Uri(PageUrl);

					var domain = url.GetLeftPart(UriPartial.Authority).Split('.');

					var root = domain.Length > 2 ? domain[1] : domain[0];

					var tab = root.Substring(0, 4).ToCharArray();

					foreach (var c in tab)
					{
						Pin += (int)c % 10;
					}
				}
                catch (Exception e) 
                {
					return Redirect("/Error");
				}
			}

            return Page();
        }
    }
}
