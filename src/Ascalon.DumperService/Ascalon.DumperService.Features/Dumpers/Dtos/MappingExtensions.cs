using System;
using Ascalon.DumperService.SreamService.Dtos;
using Ascalon.DumperService.Features.Dumpers.PostDumper;
using System.Globalization;

namespace Ascalon.DumperService.Features.Dumpers.Dtos
{
    public static class MappingExtensions
    {
        public static DumperData ToDumperData(this PostDumperCommand postDumperCommand)
        {
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";

            return new DumperData()
            {
                Gfx = Convert.ToSingle(postDumperCommand.Gfx, format),
                Gfy = Convert.ToSingle(postDumperCommand.Gfy, format),
                Gfz = Convert.ToSingle(postDumperCommand.Gfz, format),
                Wx = Convert.ToSingle(postDumperCommand.Wx, format),
                Wy = Convert.ToSingle(postDumperCommand.Wy, format),
                Wz = Convert.ToSingle(postDumperCommand.Wz, format),
                Id = Convert.ToInt32(postDumperCommand.Id),
                Speed = Convert.ToSingle(postDumperCommand.Speed, format),
            };
        }
    }
}
