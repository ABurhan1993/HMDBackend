using CrmBackend.Application.DTOs.InquiryDtos;

namespace CrmBackend.Application.Commands.InquiryCommands
{
    public class AddInquiryCommand
    {
        public CreateInquiryRequest Request { get; }

        public AddInquiryCommand(CreateInquiryRequest request)
        {
            Request = request;
        }
    }
}
