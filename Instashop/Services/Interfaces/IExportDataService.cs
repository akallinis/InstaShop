using Instashop.MVVM.Models;

namespace Instashop.Services.Interfaces;

public interface IExportDataService
{
    void ExportToExcel(List<Sale> data, string filePath);
}
