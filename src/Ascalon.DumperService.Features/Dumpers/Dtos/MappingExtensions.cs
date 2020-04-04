using Ascalon.DumperService.SreamService.Dtos;
using Ascalon.DumperService.Features.Dumpers.PostDumper;

namespace Ascalon.DumperService.Features.Dumpers.Dtos
{
    public static class MappingExtensions
    {
        public static DumperData ToDumperData(this PostDumperCommand postDumperCommand)
        {
            return new DumperData()
            {
                Gfx = postDumperCommand.Gfx,
                Gfy = postDumperCommand.Gfy,
                Gfz = postDumperCommand.Gfz,
                Wx = postDumperCommand.Wx,
                Wy = postDumperCommand.Wy,
                Wz = postDumperCommand.Wz,
                Speed = postDumperCommand.Speed,
                Label = postDumperCommand.Label,
                Time = postDumperCommand.Time,
                IpAddress = postDumperCommand.IpAddress,
            };
        }
    }
}
