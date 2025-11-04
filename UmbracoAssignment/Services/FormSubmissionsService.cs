using Umbraco.Cms.Core.Services;
using UmbracoAssignment.ViewModels;

namespace UmbracoAssignment.Services;

public class FormSubmissionsService(IContentService contentService, ILogger<FormSubmissionsService> logger)
{
    private readonly IContentService _contentService = contentService;
    private readonly ILogger<FormSubmissionsService> _logger = logger; //All logger prats generted with ChatGPT 5
    public bool SaveCallbackRequest(CallbackFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "callbackRequest");

            request.SetValue("callbackRequestName", model.Name);
            request.SetValue("callbackRequestEmail", model.Email);
            request.SetValue("callbackRequestPhone", model.Phone);
            request.SetValue("callbackRequestOption", model.SeclectedOptions);

            var saveResult = _contentService.Save(request);

            if (!saveResult.Success)
                _logger.LogError("SaveCallbackRequest: Save failed for {RequestName}.", requestName);

            return saveResult.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SaveCallbackRequest: exception for Email={Email}, Name={Name}.", model.Email, model.Name);
            return false;
        }
    }

    public bool SaveQuestionRequest(QuestionFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "questionRequest");

            request.SetValue("questionRequestName", model.Name);
            request.SetValue("questionRequestEmail", model.Email);
            request.SetValue("questionRequestQuestion", model.Question);

            var saveResult = _contentService.Save(request);

            if (!saveResult.Success)
                _logger.LogError("SaveCallbackRequest: Save failed for {RequestName}.", requestName);


            return saveResult.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SaveQuestionRequest: exception for Email={Email}, Name={Name}.", model.Email, model.Name);
            return false;
        }
    }

    public bool SaveSupportRequest(SupportFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Email}";
            var request = _contentService.Create(requestName, container, "supportRequest");

            request.SetValue("supportRequestEmail", model.Email);
  

            var saveResult = _contentService.Save(request);
            if (!saveResult.Success)
                _logger.LogError("SaveSupportRequest: Save failed for {RequestName}.", requestName);

            return saveResult.Success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SaveSupportRequest: exception for Email={Email}.", model.Email);
            return false;
        }
    }
}
