using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Mappers
{
    public static class ContactMapper
    {
        public static ContactDto ToContactDto(this Contact contactModel)
        {
            return new ContactDto
            {
                Email = contactModel.Email,
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName
            };
        }

        public static Contact FromContactDto(this ContactDto contactDto)
        {
            return new Contact
            {
                Email = contactDto.Email,
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName
            };
        }
    }
}
