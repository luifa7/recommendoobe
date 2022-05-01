using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using Domain.Core.Objects;
using MediatR;

namespace Application.Core.Commands.TagCommands
{
    public class CreateTagCommand : IRequest<Tag>
    {
        public string RecommendationDId { get; }
        public string Word { get; }

        public CreateTagCommand(string recommendationDId, string word)
        {
            RecommendationDId = recommendationDId;
            Word = word;
        }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Tag>
    {
        private readonly TagCrudService _tagService;

        public CreateTagCommandHandler(TagCrudService tagCrudService)
        {
            _tagService = tagCrudService;
        }

        public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = Tag.Create(request.RecommendationDId, request.Word);

            await _tagService.PersistAsync(tag);

            return tag;
        }
    }
}
