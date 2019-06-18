using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Course.ITnews.Web.Hubs.Models
{
    public class InputModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Full name")]
        public string Name { get; set; }
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Your specialization")]
        [DataType(DataType.Text)]
        public virtual string Specialization { get; set; }
        [Display(Name = "Who are you?")]
        [DataType(DataType.Text)]
        public virtual string Gender { get; set; }
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        public virtual string Country { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "User Photo")]
        public IFormFile UserPhoto { get; set; }
        public string Photo { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
