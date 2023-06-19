namespace Moola.Models
{
    public sealed record User : EntityWithId
    {
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100,ErrorMessage = "Last Name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public Account Account { get; set; }
        public List<Finance> Finances { get; set; }
    }
}