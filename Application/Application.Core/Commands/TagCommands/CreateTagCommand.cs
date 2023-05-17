using Application.Core.Interfaces;
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
        private readonly ITagCrudService _tagService;

        public CreateTagCommandHandler(ITagCrudService tagCrudService)
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
