using MediatR;
using System.Collections.Generic;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public class PostDumperCommand : IRequest
    {
        public List<float> Array { get; set; }

        public string Label { get; set; }

        public string IpAddress { get; set; }
    }
}
