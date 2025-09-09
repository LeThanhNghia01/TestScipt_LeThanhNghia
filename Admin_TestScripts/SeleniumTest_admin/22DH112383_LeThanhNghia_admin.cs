using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
namespace SeleniumTest
{
    [TestFixture]
    public class BaiVietTests
    {
        private IWebDriver driver;
        private const string TEST_DATA_SHEET = "TC_Nghĩa";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/login");

            IWebElement account = driver.FindElement(By.Name("email"));
            account.SendKeys("lethanhnghia@gmail.com");
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("nghia123@@");

            IWebElement loginButton = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-user.btn-block"));
            loginButton.Click();
        }

      [Test]
        public void Test1_NoiDungVsDoDaiNhieuChu()
        {
            TestData testData = TestUtils.GetTestDataByName("Test1_NoiDungVsDoDaiNhieuChu", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-book-open")));
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();
            
            // Chọn tạo bài viết
            Thread.Sleep(2000);
            IWebElement TaoBaiVietBtn = driver.FindElement(By.CssSelector("a[href='/Blog/add']"));
            TaoBaiVietBtn.Click();
            
            // Sử dụng dữ liệu từ Excel
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string title = testDataParts[0];
            string imagePath = testDataParts[1];
            string content = File.ReadAllText(testDataParts[2]);
            string tags = testDataParts[3];
            
         // Điền thông tin bài viết
            wait.Until(d => d.FindElement(By.Name("title")));
            IWebElement Title = driver.FindElement(By.Name("title"));
            Title.SendKeys(title);
            
            IWebElement image = driver.FindElement(By.CssSelector("input[type='file']"));
            image.SendKeys(imagePath);

            IWebElement NoiDung = driver.FindElement(By.CssSelector(@"div[role='textbox']"));
            NoiDung.SendKeys(content);
            
            IWebElement tagsElement = driver.FindElement(By.Name("tags"));
            tagsElement.SendKeys(tags);
            IWebElement BtnSave = driver.FindElement(By.CssSelector(".post-form_btn"));
            BtnSave.Click();
            // Kiểm tra kết quả
            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")));
            Thread.Sleep(3000);
            IWebElement SuccessToast = driver.FindElement(By.CssSelector(".Toastify__toast-body"));
            string toastText = SuccessToast.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "BaiViet", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);     
        }

        [Test]
        public void Test2_BoTrongTieuDe()
        {
            TestData testData = TestUtils.GetTestDataByName("Test2_BoTrongTieuDe", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // Chọn mục bài viết
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-book-open")));
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();
            
            // Chọn tạo bài viết
            Thread.Sleep(2000);
            IWebElement TaoBaiVietBtn = driver.FindElement(By.CssSelector("a[href='/Blog/add']"));
            TaoBaiVietBtn.Click();
            // Điền thông tin bài viết
            wait.Until(d => d.FindElement(By.Name("title")));
            // Sử dụng dữ liệu từ Excel
            string[] testDataParts = testData.TestDataInput.Split('\n');
           string title = testDataParts[0];
            string imagePath = testDataParts[1];
            string content = testDataParts[2];
            string tags = testDataParts[3];

            IWebElement Title = driver.FindElement(By.Name("title"));
            Title.SendKeys(title);
            
            IWebElement image = driver.FindElement(By.CssSelector("input[type='file']"));
            image.SendKeys(imagePath);
            
            IWebElement NoiDung = driver.FindElement(By.CssSelector("div[role='textbox']"));
            NoiDung.SendKeys(content);
            
            IWebElement tagsElement = driver.FindElement(By.Name("tags"));
            tagsElement.SendKeys(tags);
            IWebElement BtnSave = driver.FindElement(By.CssSelector(".post-form_btn"));
            BtnSave.Click();
            // Kiểm tra kết quả
            wait.Until(d => d.FindElement(By.CssSelector("span[style*='color: red']")));
            Thread.Sleep(3000);
            IWebElement errorTitle = driver.FindElement(By.CssSelector("span[style*='color: red']"));
            string errorText = errorTitle.Text.Trim();
            Assert.That(errorText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", errorText, "BaiViet", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", errorText, TEST_DATA_SHEET);
        }
       [Test]
        public void Test3_EditBlog()
        {
            TestData testData = TestUtils.GetTestDataByName("Test3_EditBlog", TEST_DATA_SHEET);
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            // Chọn mục bài viết
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-book-open")));
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();
            
             wait.Until(d => d.FindElement(By.CssSelector("a[href='/Blog']")).Displayed);
            IWebElement ListBlog = driver.FindElement(By.CssSelector("a[href='/Blog']"));
             ListBlog.Click();
            
            // Chọn bài viết cần sửa
            IWebElement editbtn = wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")));
            editbtn.FindElement(By.CssSelector("a.btn-primary")).Click();
            // Điền thông tin bài viết
            wait.Until(d => d.FindElement(By.Name("title")));
            // Sử dụng dữ liệu từ Excel
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string title = testDataParts[0];
            string imagePath = testDataParts[1];
            
            IWebElement Title = driver.FindElement(By.Name("title"));
            Title.Clear();
            Title.SendKeys(title);
            
            IWebElement image = driver.FindElement(By.CssSelector("input[type='file']"));
            image.SendKeys(imagePath);
            // Lưu bài viết
            wait.Until(d => d.FindElement(By.CssSelector(".post-form_btn")));
            IWebElement BtnSave = driver.FindElement(By.CssSelector(".post-form_btn"));
            BtnSave.Click();
            // Kiểm tra kết quả
            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")));
            IWebElement successedit = driver.FindElement(By.CssSelector(".Toastify__toast-body"));
            string successText = successedit.Text.Trim();
            
            Assert.That(successText, Is.EqualTo(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", successText, "BaiViet", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", successText, TEST_DATA_SHEET);
        }
         [Test]
        public void Test4_StyleBlog()
        {
            TestData testData = TestUtils.GetTestDataByName("Test4_StyleBlog", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-book-open")).Displayed);
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();
            
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/Blog']")).Displayed);
            IWebElement ListBlog = driver.FindElement(By.CssSelector("a[href='/Blog']"));
            ListBlog.Click();
           
            IWebElement editbtn =  wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")));
            editbtn.FindElement(By.CssSelector("a.btn-primary")).Click();

            wait.Until(d=>d.FindElement(By.CssSelector(@"div[role='textbox']")));
            IWebElement NoiDung=driver.FindElement(By.CssSelector(@"div[role='textbox']"));
            NoiDung.Click();
            NoiDung.SendKeys(Keys.Control+"a");

            IWebElement BoldBtn =driver.FindElement(By.CssSelector("[title='Bold']"));
            BoldBtn.Click();
            
            IWebElement Underline =driver.FindElement(By.CssSelector("[title='Underline']"));
            Underline.Click();
            
            wait.Until(d => d.FindElement(By.CssSelector(".post-form_btn")).Displayed);
            IWebElement BtnSave = driver.FindElement(By.CssSelector(".post-form_btn"));
            BtnSave.Click();
            
            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")).Displayed);
            IWebElement successedit = driver.FindElement(By.CssSelector(".Toastify__toast-body"));
            string successText = successedit.Text.Trim();
           
            Assert.That(successText, Is.EqualTo(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", successText, "BaiViet", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", successText, TEST_DATA_SHEET);
          
        }
         [Test]
        public void Test5_TitleWithKyTu()
        {
            List<string> testSteps = new List<string>();
            TestData testData = TestUtils.GetTestDataByName("Test5_TitleWithKyTu", TEST_DATA_SHEET);
            var wait =new WebDriverWait(driver,TimeSpan.FromSeconds(10));

            testSteps.Add("Click vào nút Bài Viết");
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();

            Thread.Sleep(2000);
            testSteps.Add("Click vào nút Tạo Bài Viết");
            IWebElement TaoBaiVietBtn = driver.FindElement(By.CssSelector("a[href='/Blog/add']"));
            TaoBaiVietBtn.Click();

            string[] testDataParts = testData.TestDataInput.Split('\n');
            string title = testDataParts[0];
            string imagePath = testDataParts[1];
            string content = testDataParts[2];
            string tags = testDataParts[3];
            wait.Until(d => d.FindElement(By.Name("title")));
            IWebElement Title = driver.FindElement(By.Name("title"));
            Title.SendKeys(title);
            
            IWebElement image = driver.FindElement(By.CssSelector("input[type='file']"));
            image.SendKeys(imagePath);

            IWebElement NoiDung = driver.FindElement(By.CssSelector(@"div[role='textbox']"));
            NoiDung.SendKeys(content);
            
            IWebElement tagsElement = driver.FindElement(By.Name("tags"));
            tagsElement.SendKeys(tags);
            
            IWebElement BtnSave=driver.FindElement(By.CssSelector(".post-form_btn"));
            BtnSave.Click();
            
             wait.Until(d => d.FindElement(By.CssSelector("span[style*='color: red']")));
            IWebElement errorTitle = driver.FindElement(By.CssSelector("span[style*='color: red']"));
            string errorText = errorTitle.Text.Trim();
            
            Assert.That(errorText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", errorText, "BaiViet", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", errorText, TEST_DATA_SHEET);
           
        }
        [Test]
        public void Test6_XoaBlog()
        {
            TestData testData = TestUtils.GetTestDataByName("Test6_XoaBlog", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            // Chọn mục bài viết
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-book-open")));
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();
            Thread.Sleep(1000);
            // Chọn danh sách bài viết
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/Blog']")));
            IWebElement ListBlog = driver.FindElement(By.CssSelector("a[href='/Blog']"));
            ListBlog.Click();
            Thread.Sleep(1000);

            IWebElement row =   wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")));
            IWebElement deleteButton = row.FindElement(By.CssSelector("td.text-center button.btn-warning"));
            deleteButton.Click();
            
            // Xác nhận hộp thoại cảnh báo
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            string alertText = alert.Text;
            Console.WriteLine("Alert Message: " + alertText);
            alert.Accept();
            
            // Kiểm tra thông báo thành công
            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")));
            IWebElement successedit = driver.FindElement(By.CssSelector(".Toastify__toast-body"));
            string successText = successedit.Text.Trim();
            
            Assert.That(successText, Is.EqualTo(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", successText, "BaiViet",testData.TestDataInput ?? "Không có dữ liệu truyền vào");
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", successText, TEST_DATA_SHEET);
        }
        [Test]
        public void Test7_TimKiemBlogByName()
        {
            // Retrieve test data from Excel
            TestData testData = TestUtils.GetTestDataByName("Test7_TimKiemBlogByName", TEST_DATA_SHEET);
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string searchKeyword = testDataParts[0];
            
            // Chọn mục bài viết
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-book-open")));
            IWebElement BaiVietBtn = driver.FindElement(By.CssSelector(".fas.fa-book-open"));
            BaiVietBtn.Click();
             Thread.Sleep(1000);
            // Chọn danh sách bài viết
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/Blog']")));
            IWebElement ListBlog = driver.FindElement(By.CssSelector("a[href='/Blog']"));
            
            ListBlog.Click();
            
            // Nhập từ khóa tìm kiếm từ Excel
            wait.Until(d => d.FindElement(By.CssSelector(".wrapper-search input")));
            IWebElement searchBox = driver.FindElement(By.CssSelector(".wrapper-search input"));
            searchBox.SendKeys(searchKeyword);
            Thread.Sleep(1000);
            searchBox.SendKeys(Keys.Enter);
      
            // Kiểm tra kết quả tìm kiếm
            wait.Until(d => d.FindElement(By.XPath("//td[@class='text-center']/span[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '" 
            + searchKeyword.ToLower() + "')]")));
             Thread.Sleep(1000);
            IWebElement searchResult = driver.FindElement(By.XPath("//td[@class='text-center']/span[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '" 
            + searchKeyword.ToLower() + "')]"));
            string resultText = searchResult.Text.Trim();
             Thread.Sleep(1000);
            Assert.That(resultText.ToLower().Contains(searchKeyword.ToLower()), Is.True, "Không tìm thấy bài viết chứa từ khóa: " + searchKeyword);
            
            // Lưu kết quả test
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", "Tìm kiếm thành công", "BaiViet", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", "Tìm kiếm thành công", TEST_DATA_SHEET);
        }

        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }
    }

     [TestFixture]
     public class ChinhSuaHoSoAdmin{
        private IWebDriver driver;
        private const string TEST_DATA_SHEET = "TC_Nghĩa";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/login");

            IWebElement account = driver.FindElement(By.Name("email"));
            account.SendKeys("lethanhnghia@gmail.com");
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("nghia123@@");

            IWebElement loginButton = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-user.btn-block"));
            loginButton.Click();
        } 
       [Test]
        public void Test1_ThemKyTuVaoHo()
        {
            TestData testData = TestUtils.GetTestDataByName("Test1_ThemKyTuVaoHo", TEST_DATA_SHEET);
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            // Điều hướng đến trang chỉnh sửa hồ sơ
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-user"))).Click();
            Thread.Sleep(1000);
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile']"))).Click();
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile/edit']"))).Click();
            
            Thread.Sleep(2000);
            
            // Sử dụng dữ liệu từ Excel
            string hoValue = testData.TestDataInput.Trim();
            
            IWebElement TenEdit = driver.FindElement(By.Name("firstName"));
            TenEdit.Clear();
            TenEdit.SendKeys(hoValue);

            // Lưu thay đổi
            wait.Until(d => d.FindElement(By.CssSelector("button#btn-save-admin"))).Click();
            
            // Quay lại trang hồ sơ để kiểm tra giá trị đã thay đổi
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile']"))).Click();
            
            // Xác nhận giá trị đã lưu hiển thị đúng trên trang hồ sơ
            wait.Until(d => d.FindElement(By.CssSelector(".col-sm-9.text-secondary")));
            IWebElement TenHienThi = driver.FindElement(By.CssSelector(".col-sm-9.text-secondary"));
            string tenSauCapNhat = TenHienThi.Text.Trim();

            // Kiểm tra dữ liệu đã được cập nhật đúng (chỉ kiểm tra xem có chứa hoValue không)
            Assert.That(tenSauCapNhat.Contains(hoValue), "Họ không chứa ký tự vừa thêm!");

            // Lưu kết quả test vào Excel
            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", "Sửa họ có ký tự đặc biệt được", "ChinhSuaHoSoAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL",  "Sửa họ có ký tự đặc biệt được", TEST_DATA_SHEET);
            Assert.Fail("Sửa họ có ký tự đặc biệt được");
            driver.Quit();
        }
        [Test]
        public void Test2_ThemKyTuVaoPhone()
        {
            TestData testData = TestUtils.GetTestDataByName("Test2_ThemKyTuVaoPhone", TEST_DATA_SHEET);
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Điều hướng đến trang chỉnh sửa hồ sơ
            wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-user"))).Click();
            Thread.Sleep(1000);
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile']"))).Click();
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile/edit']"))).Click();

            Thread.Sleep(1000);

            // Lấy dữ liệu số điện thoại không hợp lệ từ Excel
            string invalidPhone = testData.TestDataInput.Trim();
            
            IWebElement PhoneInput = driver.FindElement(By.Name("phone"));
            PhoneInput.Click();
            PhoneInput.Clear();
   
            PhoneInput.SendKeys(invalidPhone);
            
            Thread.Sleep(2000);

            // Lưu thay đổi
            driver.FindElement(By.CssSelector("button#btn-save-admin")).Click();

            // Kiểm tra thông báo lỗi
            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast--error")).Displayed);
            IWebElement ErrorToast = driver.FindElement(By.CssSelector(".Toastify__toast--error"));
            string toastText = ErrorToast.Text.Trim();

            // Kiểm tra xem thông báo lỗi có chứa nội dung mong đợi không
            Assert.That(toastText.Contains("Số điện thoại không hợp lệ"), "Không có thông báo lỗi hợp lệ!");

            // Lưu kết quả vào Excel
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);

            driver.Quit();
        }
        [Test]
        public void Test3_NullChangePassword()
        {
           string message="Lỗi: Trang bị treo khi đổi mật khẩu với giá trị null.";
            TestData testData = TestUtils.GetTestDataByName("Test3_NullChangePassword", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Chờ biểu tượng tài khoản hiển thị rồi nhấn
            IWebElement TaikhoanBtn = wait.Until(d => d.FindElement(By.CssSelector(".fas.fa-user")));
            TaikhoanBtn.Click();
            Thread.Sleep(2000);
            // Chờ nút "Hồ Sơ" hiển thị rồi nhấn
            IWebElement HoSoBtn = wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile']")));
            HoSoBtn.Click();

            // Chờ nút "Sửa" hiển thị rồi nhấn
            IWebElement SuaBtn = wait.Until(d => d.FindElement(By.CssSelector("a[href='/profile/edit']")));
            SuaBtn.Click();

            // Chờ nút "Đổi mật khẩu" hiển thị rồi nhấn
            driver.FindElement(By.CssSelector("button#btn-change-password")).Click();

            // Chờ nút "Lưu" hiển thị rồi nhấn
            IWebElement SaveBtn = wait.Until(d => d.FindElement(By.CssSelector("button#btn-save-admin")));
            SaveBtn.Click();

            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", message, "ChinhSuaHoSoAdmin", testData.TestDataInput ?? "Không có dữ liệu truyền vào");
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", message, TEST_DATA_SHEET);
            Assert.Fail(message);
            driver.Quit();
        }
         [Test]
        public void Test4_GioiHanKyTuTen()
        { 
            string message="Lỗi: Không có thông báo và ràng buộc giới hạn tên";
            TestData testData = TestUtils.GetTestDataByName("Test4_GioiHanKyTuTen", TEST_DATA_SHEET);
            var wait =new WebDriverWait(driver,TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string Ten = testDataParts[0];
           

            IWebElement TaikhoanBtn = wait.Until(d=>d.FindElement(By.CssSelector(".fas.fa-user")));
            TaikhoanBtn.Click();
            Thread.Sleep(2000);
            IWebElement HoSoBtn = wait.Until(d=>d.FindElement(By.CssSelector("a[href='/profile']")));
            HoSoBtn.Click();

            IWebElement SuaBtn = wait.Until(d=>d.FindElement(By.CssSelector("a[href='/profile/edit']")));
            SuaBtn.Click();

            IWebElement TenEdit = driver.FindElement(By.Name("lastName"));
            TenEdit.Clear();

            TenEdit.SendKeys(Ten); 

            driver.FindElement(By.CssSelector("button#btn-save-admin")).Click();
            
            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", message, "ChinhSuaHoSoAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", message, TEST_DATA_SHEET);
            Assert.Fail(message);
            
        }
          [Test]
        public void Test5_KhacPassword()
        {
                TestData testData = TestUtils.GetTestDataByName("Test5_KhacPassword", TEST_DATA_SHEET);
                string[] testDataParts = testData.TestDataInput.Split('\n');
                string Pass = testDataParts[0];
                string RePass = testDataParts[1];
                Thread.Sleep(2000);
                IWebElement TaikhoanBtn = driver.FindElement(By.CssSelector(".fas.fa-user"));
                TaikhoanBtn.Click();

                Thread.Sleep(1000);
                IWebElement HoSoBtn = driver.FindElement(By.CssSelector("a[href='/profile']"));
                HoSoBtn.Click();

                Thread.Sleep(1000);
                IWebElement SuaBtn = driver.FindElement(By.CssSelector("a[href='/profile/edit']"));
                SuaBtn.Click();

            
                driver.FindElement(By.CssSelector("button#btn-change-password")).Click();

                IWebElement PassNew = driver.FindElement(By.Name("password"));
                PassNew.SendKeys(Pass);

                IWebElement RePassNew = driver.FindElement(By.Name("confirmPassword"));
                RePassNew.SendKeys(RePass);

                 driver.FindElement(By.CssSelector("button#btn-save-admin")).Click();

                Thread.Sleep(2000); 
                IWebElement ErrorPass=driver.FindElement(By.CssSelector(".Toastify__toast-body"));
                Assert.That(ErrorPass.Text.Contains("Mật khẩu nhập lại không trùng khớp"));
                string toastText = ErrorPass.Text.Trim();
      
                TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoAdmin", testData.TestDataInput);
                TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);
                driver.Quit();
           
        }
        [TearDown]
        public void TearDown()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }        
     }
     [TestFixture]
     public class QuanLyDonHangAdmin{
        private IWebDriver driver;
        private const string TEST_DATA_SHEET = "TC_Nghĩa";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3000/login");

            IWebElement account = driver.FindElement(By.Name("email"));
            account.SendKeys("lethanhnghia@gmail.com");
            IWebElement password = driver.FindElement(By.Name("password"));
            password.SendKeys("nghia123@@");

            IWebElement loginButton = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-user.btn-block"));
            loginButton.Click();
        } 
        [Test]
        public void Test1_Delete_PD_ChoXacNhan()
        {
            TestData testData = TestUtils.GetTestDataByName("Test1_Delete_PD_ChoXacNhan", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
             string[] testDataParts = testData.TestDataInput.Split('\n');
            string ChoXacNhan = testDataParts[0];

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/orders']")).Displayed);
            IWebElement orderbtn = driver.FindElement(By.CssSelector("a[href='/orders']"));
            orderbtn.Click();

            wait.Until(d => d.FindElement(By.ClassName("result-product")).Displayed);
            IWebElement selectlist = driver.FindElement(By.ClassName("result-product"));
            selectlist.Click();

            wait.Until(d => d.FindElement(By.ClassName("custom-se")).Displayed);
            SelectElement selectvalue = new SelectElement(driver.FindElement(By.ClassName("custom-se")));
            selectvalue.SelectByValue(ChoXacNhan);

            wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")).Displayed);
            IWebElement row = driver.FindElement(By.XPath("//tr[td[contains(text(),'1')]]"));
            IWebElement deleteButton = row.FindElement(By.ClassName("order-delete"));
            deleteButton.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")).Displayed);
            IWebElement successToast = driver.FindElement(By.CssSelector(".Toastify__toast-body"));

            // Lấy nội dung text từ thông báo
            string toastText = successToast.Text.Trim();
            
            // Kiểm tra kết quả chính xác
            Assert.That(toastText, Is.EqualTo("Xóa đơn hàng thành công!"));

            // Ghi kết quả vào file Excel
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "QuanLyDonHangAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);

            driver.Quit();
        }
         [Test]
        public void Test2_Delete_PD_DaXacNhan()
        {
            TestData testData = TestUtils.GetTestDataByName("Test2_Delete_PD_DaXacNhan", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
             string[] testDataParts = testData.TestDataInput.Split('\n');
            string DaXacNhan = testDataParts[0];

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/orders']")).Displayed);
            IWebElement orderbtn = driver.FindElement(By.CssSelector("a[href='/orders']"));
            orderbtn.Click();

            wait.Until(d => d.FindElement(By.ClassName("result-product")).Displayed);
            IWebElement selectlist = driver.FindElement(By.ClassName("result-product"));
            selectlist.Click();

            wait.Until(d => d.FindElement(By.ClassName("custom-se")).Displayed);
            SelectElement selectvalue = new SelectElement(driver.FindElement(By.ClassName("custom-se")));
            selectvalue.SelectByValue(DaXacNhan);

            wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")).Displayed);
            IWebElement row = driver.FindElement(By.XPath("//tr[td[contains(text(),'1')]]"));
            IWebElement deleteButton = row.FindElement(By.ClassName("order-delete"));
            deleteButton.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")).Displayed);
            IWebElement successToast = driver.FindElement(By.CssSelector(".Toastify__toast-body"));

            // Lấy nội dung text từ thông báo
            string toastText = successToast.Text.Trim();
            
            // Kiểm tra kết quả chính xác
            Assert.That(toastText, Is.EqualTo("Xóa đơn hàng thành công!"));

            // Ghi kết quả vào file Excel
            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", toastText, "QuanLyDonHangAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", toastText, TEST_DATA_SHEET);
            Assert.Fail("Xóa đơn hàng thành công!");
            driver.Quit();
        }
        [Test]
        public void Test3_Delete_PD_DangGiao()
        {
            TestData testData = TestUtils.GetTestDataByName("Test3_Delete_PD_DangGiao", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string DangGiao = testDataParts[0];

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/orders']")).Displayed);
            IWebElement orderbtn = driver.FindElement(By.CssSelector("a[href='/orders']"));
            orderbtn.Click();

            wait.Until(d => d.FindElement(By.ClassName("result-product")).Displayed);
            IWebElement selectlist = driver.FindElement(By.ClassName("result-product"));
            selectlist.Click();

            wait.Until(d => d.FindElement(By.ClassName("custom-se")).Displayed);
            SelectElement selectvalue = new SelectElement(driver.FindElement(By.ClassName("custom-se")));
            selectvalue.SelectByValue(DangGiao);

            wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")).Displayed);
            IWebElement row = driver.FindElement(By.XPath("//tr[td[contains(text(),'1')]]"));
            IWebElement deleteButton = row.FindElement(By.ClassName("order-delete"));
            deleteButton.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")).Displayed);
            IWebElement successToast = driver.FindElement(By.CssSelector(".Toastify__toast-body"));

            // Lấy nội dung text từ thông báo
            string toastText = successToast.Text.Trim();
            
            // Kiểm tra kết quả chính xác
            Assert.That(toastText, Is.EqualTo("Xóa đơn hàng thành công!"));

            // Ghi kết quả vào file Excel
            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", toastText, "QuanLyDonHangAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", toastText, TEST_DATA_SHEET);
            Assert.Fail("Xóa đơn hàng thành công!");
            driver.Quit();
        }
         [Test]
        public void Test4_Delete_PD_DaGiao()
        {
            TestData testData = TestUtils.GetTestDataByName("Test4_Delete_PD_DaGiao", TEST_DATA_SHEET);
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string DaGiao = testDataParts[0];

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/orders']")).Displayed);
            IWebElement orderbtn = driver.FindElement(By.CssSelector("a[href='/orders']"));
            orderbtn.Click();

            wait.Until(d => d.FindElement(By.ClassName("result-product")).Displayed);
            IWebElement selectlist = driver.FindElement(By.ClassName("result-product"));
            selectlist.Click();

            wait.Until(d => d.FindElement(By.ClassName("custom-se")).Displayed);
            SelectElement selectvalue = new SelectElement(driver.FindElement(By.ClassName("custom-se")));
            selectvalue.SelectByValue(DaGiao);

            wait.Until(d => d.FindElement(By.XPath("//tr[td[contains(text(),'1')]]")).Displayed);
            IWebElement row = driver.FindElement(By.XPath("//tr[td[contains(text(),'1')]]"));
            IWebElement deleteButton = row.FindElement(By.ClassName("order-delete"));
            deleteButton.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")).Displayed);
            IWebElement successToast = driver.FindElement(By.CssSelector(".Toastify__toast-body"));

            // Lấy nội dung text từ thông báo
            string toastText = successToast.Text.Trim();
            
            // Kiểm tra kết quả chính xác
            Assert.That(toastText, Is.EqualTo("Xóa đơn hàng thành công!"));

            // Ghi kết quả vào file Excel
            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", toastText, "QuanLyDonHangAdmin", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", toastText, TEST_DATA_SHEET);
            Assert.Fail("Xóa đơn hàng thành công!");
            driver.Quit();
        }
         [Test]
        public void Test5_ChangeStatusPrev(){
                string message="Sai vì: Thay đổi được đơn hàng đã giao về chưa chờ xác nhận";
                TestData testData = TestUtils.GetTestDataByName("Test5_ChangeStatusPrev", TEST_DATA_SHEET);
                var wait=new WebDriverWait(driver,TimeSpan.FromSeconds(10));
                string[] testDataParts = testData.TestDataInput.Split('\n');
                string DaGiao = testDataParts[0];
                string ChoXacNhan = testDataParts[0];

                wait.Until(d=>d.FindElement(By.CssSelector("a[href='/orders']")).Displayed);
                IWebElement orderbtn=driver.FindElement(By.CssSelector("a[href='/orders']"));
                orderbtn.Click();
                
                wait.Until(d=>d.FindElement(By.ClassName("result-product")).Displayed);
                IWebElement selectlist=driver.FindElement(By.ClassName("result-product"));
                selectlist.Click();
                
                wait.Until(d=>d.FindElement(By.ClassName("custom-se")).Displayed);
                SelectElement selectvalue=new SelectElement(driver.FindElement(By.ClassName("custom-se")));
                selectvalue.SelectByValue(DaGiao);

                wait.Until(d=>d.FindElement(By.ClassName("select-cus")).Displayed);
                SelectElement ChangeStatus=new SelectElement(driver.FindElement(By.ClassName("select-cus")));
                ChangeStatus.SelectByValue(ChoXacNhan);
                
                TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", message, "QuanLyDonHangAdmin", testData.TestDataInput);
                TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", message, TEST_DATA_SHEET);
               
                Assert.Fail(message);
        }
          [Test]
            public void Test6_ChangeStatusBoQuaGiaiDoan(){
                string message="Sai vì: Thứ tự thay đổi tình trạng không theo trình tự";
                TestData testData = TestUtils.GetTestDataByName("Test6_ChangeStatusBoQuaGiaiDoan", TEST_DATA_SHEET);
                var wait=new WebDriverWait(driver,TimeSpan.FromSeconds(10));
                string[] testDataParts = testData.TestDataInput.Split('\n');
                string DaGiao = testDataParts[0];
                string ChoXacNhan = testDataParts[0];

                wait.Until(d=>d.FindElement(By.CssSelector("a[href='/orders']")).Displayed);
                IWebElement orderbtn=driver.FindElement(By.CssSelector("a[href='/orders']"));
                orderbtn.Click();
                
                wait.Until(d=>d.FindElement(By.ClassName("result-product")).Displayed);
                IWebElement selectlist=driver.FindElement(By.ClassName("result-product"));
                selectlist.Click();
                
                wait.Until(d=>d.FindElement(By.ClassName("custom-se")).Displayed);
                SelectElement selectvalue=new SelectElement(driver.FindElement(By.ClassName("custom-se")));
                selectvalue.SelectByValue(DaGiao);

                wait.Until(d=>d.FindElement(By.ClassName("select-cus")).Displayed);
                SelectElement ChangeStatus=new SelectElement(driver.FindElement(By.ClassName("select-cus")));
                ChangeStatus.SelectByValue(ChoXacNhan);
                
                TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", message, "QuanLyDonHangAdmin", testData.TestDataInput);
                TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", message, TEST_DATA_SHEET);
               
                    Assert.Fail(message);
        }
          [TearDown]
        public void TearDown()
        {
            Thread.Sleep(3000);
            driver.Quit();
        }        
     }
}

