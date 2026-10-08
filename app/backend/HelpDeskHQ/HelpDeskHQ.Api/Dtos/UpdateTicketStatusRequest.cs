using System.ComponentModel.DataAnnotations;
using HelpDeskHQ.Api.Models;

namespace HelpDeskHQ.Api.Dtos;

public class UpdateTicketStatusRequest
{
    [EnumDataType(typeof(TicketStatus))]
    public TicketStatus Status { get; set; }
}