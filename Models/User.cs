using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Unicorns.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required (ErrorMessage = "Username is required!")]
        [MinLength(3, ErrorMessage = "Username must be 3 characters or more!")]
        public string UserName {get;set;}
        [Required (ErrorMessage="Email is required")]
        [EmailAddress (ErrorMessage="Please enter a valid eamil")]
        public string Email {get;set;}
        [DataType(DataType.Password)]
        [Required (ErrorMessage="Password is required")]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        public string Password {get;set;}
        [NotMapped]
        [Compare("Password", ErrorMessage="Confirm Password must match Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}