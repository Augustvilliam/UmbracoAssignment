using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoAssignment.Services;
using UmbracoAssignment.ViewModels;

namespace UmbracoAssignment.Controllers
{
    public class FormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, FormSubmissionsService formSubmissionsService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        private readonly FormSubmissionsService _formSubmissionsService = formSubmissionsService;

        public IActionResult HandleCallbackForm(CallbackFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var result = _formSubmissionsService.SaveCallbackRequest(model);
            if (!result)
            {
                TempData["FormError"] = "There was an error submitting the form. Please try again.";
                return RedirectToCurrentUmbracoPage();
            }

            TempData["FormSuccess"] = "Thank you for your request! You will hear from us shortly!";

            return RedirectToCurrentUmbracoPage();
        }
    }
}
