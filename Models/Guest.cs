using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KutseApp.Models
{
    public class Guest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sisesta nimi")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sisesta e-maili")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Vale e-mali formati")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Sisesta oma telefoni number")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Vale telefoni numbri formati")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Tee oma vastus")]
        public bool? WillAttend { get; set; }
    }
}