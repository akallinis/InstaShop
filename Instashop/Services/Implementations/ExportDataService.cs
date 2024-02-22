using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using OfficeOpenXml;
using System.IO;

namespace Instashop.Services.Implementations;

public class ExportDataService : IExportDataService
{
    public void ExportToExcel(List<Sale> data, string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Instashop sales");

            // Header
            worksheet.Cells[1, 1].Value = "Sold At";
            worksheet.Cells[1, 2].Value = "Total Value";
            worksheet.Cells[1, 3].Value = "Product Id";
            worksheet.Cells[1, 4].Value = "Unit Price";
            worksheet.Cells[1, 5].Value = "Quantity";

            // Data
            int row = 2;
            foreach (var item in data)
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var soldAt = epoch.AddMilliseconds((long)item.SoldAt);
                worksheet.Cells[row, 1].Value = soldAt.ToLongDateString();
                worksheet.Cells[row, 2].Value = item.TotalValue;
                if (item.Products != null)
                {
                    foreach (var productSale in item.Products)
                    {
                        worksheet.Cells[row, 3].Value = productSale.ProductId;
                        worksheet.Cells[row, 4].Value = productSale.UnitPrice;
                        worksheet.Cells[row, 5].Value = productSale.Quantity;
                        row++;
                    }
                }
            }

            FileInfo excelFile = new FileInfo(filePath);
            package.SaveAs(excelFile);
        }
    }
}
