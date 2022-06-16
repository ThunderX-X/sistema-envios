using System.ComponentModel.DataAnnotations;

namespace Clients.Service.DTOs
{
    public class UpdateClientDTO
    {
        [MinLength(2)]
        public string FirstName { get; set; }

        [MinLength(2)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

    }
}
