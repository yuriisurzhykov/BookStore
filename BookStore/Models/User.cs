using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public enum UserType
    {
        SIMPLE,
        ADMIN
    }

    [Table("usr", Schema = "public")]
    public class User
    {
        [Key]
        [Required]
        [Column("usr_id")]
        public long Id { get; set; }
        [Required]
        [Column("usr_login")]
        public string Login { get; set; }
        [Required]
        [Column("usr_pass")]
        public string Password { get; set; }
        [Column("usr_type")]
        public UserType UserType { get; set; }
    }
}