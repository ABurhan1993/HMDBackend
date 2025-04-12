using CrmBackend.Application.Commands.CustomerCommands;
using CrmBackend.Domain.Entities;
using CrmBackend.Domain.Enums;
using CrmBackend.Domain.Services;

namespace CrmBackend.Application.Handlers
{
    public class AddCustomerCommentHandler
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerCommentRepository _customerCommentRepository;
        private readonly IUserRepository _userRepository;

        public AddCustomerCommentHandler(
            ICustomerRepository customerRepository,
            ICustomerCommentRepository customerCommentRepository,
            IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _customerCommentRepository = customerCommentRepository;
            _userRepository = userRepository;
        }

        public async Task<int> Handle(AddCustomerCommentCommand command)
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            var user = await _userRepository.GetByIdAsync(command.CommentAddedBy);
            if (user == null)
                throw new Exception("User not found");

            // تحديث حالة الاتصال إذا تم تحديدها
            if (command.ContactStatus.HasValue)
            {
                customer.ContactStatus = (ContactStatus)command.ContactStatus;
                await _customerRepository.UpdateAsync(customer);
            }

            var comment = new CustomerComment
            {
                CustomerId = command.CustomerId,
                CustomerCommentDetail = command.CommentDetail,
                CommentAddedBy = command.CommentAddedBy,
                CommentAddedOn = DateTime.UtcNow.ToString("s"),
                CreatedBy = command.CommentAddedBy,
                CreatedDate = DateTime.UtcNow.ToString("s"),
                IsActve = true,
                IsDeleted = false
            };

            await _customerCommentRepository.AddAsync(comment);
            return comment.CustomerCommentId;
        }
    }
}