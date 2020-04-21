using Ascalon.DumperService.SreamService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            await _streamService.SetData(request.Array, request.IpAddress, request.Label);

            return Unit.Value;
        }
    }
}
