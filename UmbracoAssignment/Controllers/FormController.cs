using Azure.Messaging.ServiceBus;
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
using UmbracoAssignment.Dto;

namespace UmbracoAssignment.Controllers
{
    public class FormController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            FormSubmissionsService formSubmissionsService,
            ServiceBusSender sender
        ) 
        : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        

    {

        private readonly FormSubmissionsService _formSubmissionsService = formSubmissionsService;
        private readonly ServiceBusSender _sender = sender;
        private readonly IProfilingLogger _logger = profilingLogger;


        private async Task EnqueueAsync(string recipientEmail, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(recipientEmail))

            return;


            var payload = new FormSubmissionMessage { RecipientEmail = recipientEmail };
            var msg = new ServiceBusMessage(BinaryData.FromObjectAsJson(payload))
            {
                MessageId = Guid.NewGuid().ToString()
            };
            await _sender.SendMessageAsync(msg, ct);

           

        }

        public async Task<IActionResult> HandleCallbackForm(CallbackFormViewModel model, CancellationToken ct)
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

            await EnqueueAsync(model.Email!, ct);

            TempData["CallbackFormSuccess"] = "Thank you for your request! You will hear from us shortly!";
            return RedirectToCurrentUmbracoPage();
        }

        public async Task<IActionResult> HandleQuestionForm(QuestionFormViewModel model, CancellationToken ct)
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

            await EnqueueAsync(model.Email!, ct);

            TempData["QuestionFormSuccess"] = "Thank you for your request! You will hear from us shortly!";
            return RedirectToCurrentUmbracoPage();
        }

        public async Task<IActionResult> HandleSupportForm(SupportFormViewModel model, CancellationToken ct)
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

            await EnqueueAsync(model.Email!, ct);

            TempData["SupportFormSuccess"] = "Thank you! Our support will contact you shortly.";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
