using System.ComponentModel.DataAnnotations;

namespace UmbracoAssignment.ViewModels;

public class QuestionFormViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid Email.")]
    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Please enter your question")]
    [Display(Name = "Question")]
    public string Question { get; set; } = null!;
}
