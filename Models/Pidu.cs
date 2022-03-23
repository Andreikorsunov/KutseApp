using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KutseApp.Models
{
    public class Pidu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "On vaja sisesta pidu nime!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "On vaja sisesta kuupäev!")]
        public DateTime Date { get; set; }
    }
}