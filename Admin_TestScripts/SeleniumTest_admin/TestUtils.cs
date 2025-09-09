using OfficeOpenXml;

namespace SeleniumTest
{
    public static class TestUtils
    {
        private static string excelFilePath = @"D:\HK2_NĂM 3\DBCLPM_LT\22DH112383_LeThanhNghia_TestResults.xlsx";
        private static string testDataFilePath = @"D:\HK2_NĂM 3\DBCLPM_LT\22DH112383_LeThanhNghia_TestCase.xlsx";

       public static TestData GetTestDataByName(string testName, string sheetName)
        {
            var allTestData = ExcelDataReader.ReadTestData(testDataFilePath, sheetName);
            var testData = allTestData.Find(t => t.TestName == testName);

            if (testData != null && string.IsNullOrWhiteSpace(testData.TestDataInput))
            {
                testData.TestDataInput = "Không có dữ liệu truyền vào";
            }

            return testData;
        }


        public static void SaveTestResultToExcel(string testName, string result, string message, string sheetName, string testDataInput)
        {
            FileInfo fileInfo = new FileInfo(excelFilePath);
            using (ExcelPackage package = fileInfo.Exists ? new ExcelPackage(fileInfo) : new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == sheetName);
                if (worksheet == null)
                {
                    worksheet = package.Workbook.Worksheets.Add(sheetName);
                }

                int row = 1;
                if (worksheet.Dimension != null)
                {
                    row = worksheet.Dimension.Rows + 1;
                }
                else
                {
                    worksheet.Cells[1, 1].Value = "Test Name";
                    worksheet.Cells[1, 2].Value = "Result";
                    worksheet.Cells[1, 3].Value = "Message";
                    worksheet.Cells[1, 4].Value = "Timestamp";
                    worksheet.Cells[1, 5].Value = "Test Data Input"; // Cột mới
                    row = 2;
                }

                worksheet.Cells[row, 1].Value = testName;
                worksheet.Cells[row, 2].Value = result;
                worksheet.Cells[row, 3].Value = message;
                worksheet.Cells[row, 4].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cells[row, 5].Value = testDataInput; // Lưu dữ liệu đầu vào của test case

                // Định dạng tiêu đề nếu là lần đầu ghi file
                if (row == 2)
                {
                    using (var range = worksheet.Cells[1, 1, 1, 5])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }
                }

                var cellResult = worksheet.Cells[row, 2];
                if (result == "PASS")
                {
                    cellResult.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cellResult.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                }
                else if (result == "FAIL")
                {
                    cellResult.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cellResult.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightPink);
                }

                worksheet.Cells.AutoFitColumns();
                package.Save();
            }
        }


        public static void UpdateTestCaseResult(string testName, string result, string actualResult, string sheetName)
        {
            FileInfo fileInfo = new FileInfo(testDataFilePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                if (worksheet == null)
                {
                    throw new Exception($"Sheet '{sheetName}' không tồn tại trong file Excel.");
                }

                int rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    if (worksheet.Cells[row, 3].Text == testName)
                    {
                        worksheet.Cells[row, 8].Value = actualResult;
                        worksheet.Cells[row, 9].Value = result;

                        if (result == "PASS")
                        {
                            worksheet.Cells[row, 9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                        }
                        else
                        {
                            worksheet.Cells[row, 9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[row, 9].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightPink);
                        }
                        break;
                    }
                }
                package.Save();
            }
        }
    }
}
