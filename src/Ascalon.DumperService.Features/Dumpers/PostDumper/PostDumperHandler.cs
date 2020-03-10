using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ascalon.DumperService.SreamService;
using Ascalon.DumperService.Features.Dumpers.Dtos;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public class PostDumperHandler : IRequestHandler<PostDumperCommand, Unit>
    {
        private readonly IStreamService _streamService;

        public PostDumperHandler(IStreamService streamService)
        {
            _streamService = streamService;
        }

        public async Task<Unit> Handle(PostDumperCommand request, CancellationToken cancellationToken)
        {
            _streamService.SetData(request.ToDumperData());

            return Unit.Value;
        }
    }
}
