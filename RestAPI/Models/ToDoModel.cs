using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
    }
}
