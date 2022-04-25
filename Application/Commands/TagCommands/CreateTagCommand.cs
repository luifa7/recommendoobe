using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands.TagCommands
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
