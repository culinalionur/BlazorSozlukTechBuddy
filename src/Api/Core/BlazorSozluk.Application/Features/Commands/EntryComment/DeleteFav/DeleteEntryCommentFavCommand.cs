using BlazorSozluk.Application.Features.Commands.EntryComment.DeleteFav;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public DeleteEntryCommentFavCommand(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }

   
}
public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
                                           exchangeType: SozlukConstants.DefaultExchangeType,
                                           queueName: SozlukConstants.DeleteEntryCommentFavQueueName,
                                           obj: new DeleteEntryCommentFavEvent()
                                           {
                                               EntryCommentId = request.EntryCommentId,
                                               CreatedBy = request.UserId
                                           });

        return await Task.FromResult(true);
    }
}
