using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UmbracoAssignment.ViewModels;

public class CallbackFormViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",ErrorMessage = "Invalid Email.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone Number is required")]
    [Display(Name = "Phone")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Pleace Select an option")]
    public string SeclectedOptions { get; set; } = null!;

    [BindNever]
    public IEnumerable<string> Options { get; set; } = [];
}
