using Ascalon.DumperService.SreamService.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ascalon.DumperService.SreamService
{
    public interface IStreamService
    {
        void SetData(DumperData dumperData);

        void SendDataToNeuralNetwork();
    }
}
