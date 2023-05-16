using System.ComponentModel.DataAnnotations;

namespace WebFrontToBack.Models
{
    public class RecentWork
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImagePath { get; set; }
        

    } 
}
