using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Utils.Payment
{
    public class Address
    {
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+(\ )?[a-zA-Zа-яА-Я]+$")]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9а-яА-Я]+\/?[0-9]{0,2})")]
        public string Home { get; set; }
        [RegularExpression(@"^[0-9]{1,4}")]
        public string Flat { get; set; }
    }
}