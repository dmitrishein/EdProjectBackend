using System.ComponentModel.DataAnnotations;

namespace EdProject.BLL.Models.Author
{
    public class AuthorModel:BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
