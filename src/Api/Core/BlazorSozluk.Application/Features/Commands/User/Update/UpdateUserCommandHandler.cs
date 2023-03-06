using AutoMapper;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.ViewModels.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;
            _mapper.Map(request, dbUser);

            var rows = await _userRepository.UpdateAsync(dbUser);
            //Email changed/created
            if (emailChanged && rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };
                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                                                    obj: @event,
                                                    exchangeType: SozlukConstants.DefaultExchangeType,
                                                    queueName: SozlukConstants.UserEmailChangedQueueName);

                dbUser.EmailConfirmed = false;
                await _userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
