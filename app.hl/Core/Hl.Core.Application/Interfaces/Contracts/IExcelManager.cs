using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hl.Core.Application.Interfaces.Contracts
{
    public interface IExcelManager
    {
        (string mimeType, MemoryStream stream) GenerateExcel<T>(List<T> collection);
    }
}
