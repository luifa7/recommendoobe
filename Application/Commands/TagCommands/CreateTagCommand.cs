using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Domain.Objects;
using MediatR;

namespace Application.Commands
{
    public class CreateTagCommand : IRequest<Tag>
    {
        public string RecommendationDId;
        public string Word;

        public CreateTagCommand(string recommendationDId, string word)
        {
            RecommendationDId = recommendationDId;
            Word = word;
        }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Tag>
    {
        private readonly TagCRUDService _tagService;

        public CreateTagCommandHandler(TagCRUDService tagCRUDService)
        {
            _tagService = tagCRUDService;
        }

        public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = Tag.Create(request.RecommendationDId, request.Word);

            await _tagService.PersistAsync(tag);

            return tag;
        }
    }
}
