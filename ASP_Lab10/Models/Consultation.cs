using System.ComponentModel.DataAnnotations;

namespace ASP_Lab10.Models
{
    public class Consultation
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Subject is required!")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Date is Required")]
        public DateTime Date { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
