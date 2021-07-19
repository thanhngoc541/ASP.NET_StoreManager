using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class Member
    {
        [Key] public int MemberId { set; get; }

        [StringLength(100)]
        [Required]
        [RegularExpression(
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { set; get; }

        [StringLength(30)] [Required] public string Password { set; get; }
        [StringLength(40)] [Required] public string CompanyName { set; get; }
        [StringLength(15)] [Required] public string City { set; get; }
        [StringLength(15)] [Required] public string Country { set; get; }
        public bool Login(string password)
        {
            return Password.Equals(password);
        }
    }
}