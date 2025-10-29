using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть опис завдання")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Будь ласка введіть дату")]
        public DateTime? DueDate { get; set; }
        [Required(ErrorMessage = "Будь ласка виберіть категорію")]
        public string CategoryId { get; set; } = string.Empty;
        [ValidateNever]
        public Category Category { get; set; } = null!;
        [Required(ErrorMessage = "Будь ласка виберіть статус")]
        public string StatusId { get; set; } = string.Empty;
        [ValidateNever]
        public Status Status { get; set; } = null!;

        public bool OverDue => StatusId == "open" && DueDate < DateTime.Today;

    }
}
