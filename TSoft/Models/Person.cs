using System;

namespace TSoft.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ?BirthDate { get; set; }
        public bool? Gender { get; set; }
        public int? CityId { get; set; }
        public int? StreetId { get; set; }
        public bool Status { get; set; }
        public string IdCardNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Rank { get; set; }
    }
}
