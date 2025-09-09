using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumTest
{
    public class TestData
    {
        public string ID { get; set; }
        public string TestTitle { get; set; }
        public string TestName { get; set; }
        public string TestSteps { get; set; }
        public string Description { get; set; }
        public string TestDataInput { get; set; }
        public string ExpectedResults { get; set; }
        public string ActualResults { get; set; }
        public string Status { get; set; }
    }

    public static class ExcelDataReader
    {
        public static List<TestData> ReadTestData(string filePath, string sheetName)
        {
            List<TestData> testDataList = new List<TestData>();
            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                if (worksheet == null)
                {
                    throw new Exception($"Sheet '{sheetName}' không tồn tại trong file Excel.");
                }

                int rowCount = worksheet.Dimension.Rows;
                // Bỏ qua hàng tiêu đề
                for (int row = 2; row <= rowCount; row++)
                {
                    TestData testData = new TestData
                    {
                        ID = worksheet.Cells[row, 1].Text,
                        TestTitle = worksheet.Cells[row, 2].Text,
                        TestName = worksheet.Cells[row, 3].Text,
                        TestSteps = worksheet.Cells[row, 4].Text,
                        Description = worksheet.Cells[row, 5].Text,
                        TestDataInput = worksheet.Cells[row, 6].Text,
                        ExpectedResults = worksheet.Cells[row, 7].Text,
                        ActualResults = worksheet.Cells[row, 8].Text,
                        Status = worksheet.Cells[row, 9].Text
                    };
                    testDataList.Add(testData);
                }
            }
            return testDataList;
        }
    }
}
