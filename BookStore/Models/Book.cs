using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    [Table("book", Schema = "public")]
    public class Book
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("book_id")]
        public long Id { get; set; }
        [Column("image")]
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Required]
        [Column("name")]
        [Display(Name = "Book name")]
        public string Name { get; set; }
        [Required]
        [Column("description")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Column("author")]
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Required]
        [Column("cost")]
        [Display(Name = "Price")]
        [Range(0,100000, ErrorMessage = "The price must be between 0 and 100 000")]
        public decimal Cost { get; set; }
    }
}