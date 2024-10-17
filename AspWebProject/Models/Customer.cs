using System.ComponentModel.DataAnnotations;

namespace AspWebProject.Models
{
    public class Customer
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="Name alanı boş bırakılamaz")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
