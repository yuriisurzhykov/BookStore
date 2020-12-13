using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Utils.Payment
{
    public class EmailContext
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Receiver { get; set; }
        [Required]
        public string Name { get; set; }
        public Cart Cart { get; set; }
        public Address Address { get; set; }
    }
}