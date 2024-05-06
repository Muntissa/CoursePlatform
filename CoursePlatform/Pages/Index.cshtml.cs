using CoursePlatform.Common;
using CoursePlatform.Common.Additional;
using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoursePlatform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IServiceProvider _serviceProvider;

        public IndexModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IList<Profile> Profiles { get; set; }

        public async void OnGet()
        {
            await _serviceProvider.GetRequiredService<IContextHelper>().InContext<CoursePlatformContext>(async (context) =>
            {
                Profiles = context.Set<Profile>().ToList(); 
            });

        }
    }
}
