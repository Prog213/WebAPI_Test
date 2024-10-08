﻿using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Mappers
{
    public static class IncidentMapper
    {
        public static Contact ToContact(this IncidentDto incidentDto)
        {
            return new Contact
            {
                Email = incidentDto.contactEmail,
                FirstName = incidentDto.contactFirstName,
                LastName = incidentDto.contactLastName,
            };
        }

        public static Incident ToIncidentModel(this IncidentDto incidentDto, int accountId)
        {
            return new Incident
            {
                AccountId = accountId,
                Description = incidentDto.incidentDescription,
                IncidentName = "INC - " + Guid.NewGuid().ToString().Substring(0,8),
            };
        }

        public static IncidentResponseDto ToIncidentResponseDto(this Incident incidentModel)
        {
            return new IncidentResponseDto
            {
                Account = incidentModel.Account.ToAccountResponseDto(),
                Description = incidentModel.Description,
                IncidentName = incidentModel.IncidentName
            };
        }
    }
}
