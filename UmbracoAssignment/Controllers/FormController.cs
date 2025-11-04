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
                TempData["CallbackFormError"] = "Please Submit a valid form.";
                return CurrentUmbracoPage();
            }

            var ok = _formSubmissionsService.SaveCallbackRequest(model);

            if (!ok)
            {
                TempData["CallbackFormError"] = "There was an error submitting the form. Please try again.";
                return RedirectToCurrentUmbracoPage();
            }

            TempData["CallbackFormSuccess"] = "Thank you for your request! You will hear from us shortly!";
            return RedirectToCurrentUmbracoPage();
        }

        public IActionResult HandleQuestionForm(QuestionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["QuestionFormError"] = "Please Submit a valid form.";
                return CurrentUmbracoPage();
            }

            var ok = _formSubmissionsService.SaveQuestionRequest(model);

            if (!ok)
            {
                TempData["QuestionFormError"] = "There was an error submitting the form. Please try again.";
                return RedirectToCurrentUmbracoPage();
            }

            TempData["QuestionFormSuccess"] = "Thank you for your request! You will hear from us shortly!";
            return RedirectToCurrentUmbracoPage();
        }

        public IActionResult HandleSupportForm(SupportFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["SupportFormError"] = "Please Submit a valid form.";
                return CurrentUmbracoPage();
            }

            var ok = _formSubmissionsService.SaveSupportRequest(model);

            if (!ok)
            {
                TempData["SupportFormError"] = "There was an error submitting the form. Please try again.";
                return RedirectToCurrentUmbracoPage();
            }

            TempData["SupportFormSuccess"] = "Thank you! Our support will contact you shortly.";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
