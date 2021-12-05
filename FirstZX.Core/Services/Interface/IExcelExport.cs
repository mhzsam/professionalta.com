using System.Collections.Generic;
using FirstZX.Core.DTO;

namespace FirstZX.Core.Services.Interface
{
    public interface IExcelExport
    {
        List<ExportExcelForecast> GetInformationForExcelFromForecaste();
    }
}