using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UmbracoAssignment.ViewModels;

public class SupportFormViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid Email.")]
    public string Email { get; set; } = null!;

}
