using Hl.Core.Application.Interfaces.Contracts;
using Hmis.Tools.Excel.Writer;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Hl.Infrastructure.Persistence.Reports
{
    public class ExcelManager : IExcelManager
    {
        public (string mimeType, MemoryStream stream) GenerateExcel<T>(List<T> collection)
        {
            var columns = new Dictionary<string, ExcelColumnDescription>();


            foreach (var p in typeof(T).GetProperties())
            {
                var attributes = p.GetCustomAttributes(typeof(DescriptionAttribute), true);
                string description = null;
                if (attributes.Length > 0)
                {
                    var descriptionAttribute = attributes[0] as DescriptionAttribute;
                    description = descriptionAttribute.Description;
                }

                columns.Add(p.Name, new ExcelColumnDescription(description ?? p.Name));
            }


            var excelSheet = new ExcelSheetEntity<T>
            {
                Columns = columns,
                Data = collection,
                Name = "მოთხოვნები"
            };

            var excelBytes = excelSheet.CreateExcel();
            var stream = new MemoryStream(excelBytes);
            //var mimeType = "application/vnm.ms-excel";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return (mimeType, stream);
        }
    }
}
