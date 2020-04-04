using MediatR;

namespace Ascalon.DumperService.Features.Dumpers.PostDumper
{
    public class PostDumperCommand : IRequest
    {
        public string IpAddress { get; set; }

        public string Gfx { get; set; }

        public string Gfy { get; set; }

        public string Gfz { get; set; }

        public string Wx { get; set; }

        public string Wy { get; set; }

        public string Wz { get; set; }

        public string Speed { get; set; }

        public string Label { get; set; }

        public string Time { get; set; }
    }
}
