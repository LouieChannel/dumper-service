using Ascalon.DumperService.SreamService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ascalon.DumperService.SreamService
{
    public interface IStreamService
    {
        Task SetData(List<float> array, string ipAddress, string label);
    }
}
