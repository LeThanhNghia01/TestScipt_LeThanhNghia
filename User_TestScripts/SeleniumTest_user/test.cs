using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
namespace SeleniumTest
{
    [TestFixture]
    public class ChinhSuaHoSoUser
    {
        private IWebDriver driver;
        private const string TEST_DATA_SHEET = "TC_Nghĩa";

        [SetUp]
       public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3006/");
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            wait.Until(d => d.FindElement(By.CssSelector("div.auth-des span")).Displayed);
            IWebElement loginlink = driver.FindElement(By.CssSelector("div.auth-des span"));
            loginlink.Click();

            IWebElement emailInput = driver.FindElement(By.Name("email"));
            emailInput.SendKeys("lethanhnghia17617@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Name("password"));
            passwordInput.SendKeys("nghia123@@");

            IWebElement loginButton = driver.FindElement(By.CssSelector("button.auth-form_btn"));
            loginButton.Click();

        }
         [Test]
          public void Test1_EditNhungKhongThemDuLieu(){
            TestData testData = TestUtils.GetTestDataByName("Test1_EditNhungKhongThemDuLieu", TEST_DATA_SHEET);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            wait.Until(d => d.FindElement(By.ClassName("btn-profile_edit")).Displayed);
            IWebElement editAc = driver.FindElement(By.ClassName("btn-profile_edit"));
            editAc.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 250);");  
            Thread.Sleep(2000);
            IWebElement saveButton = driver.FindElement(By.CssSelector(".profile_form-btn button"));
            saveButton.Click();

            IWebElement errorMessage = driver.FindElement(By.CssSelector("p.error-message"));
            string toastText = errorMessage.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", toastText, "ChinhSuaHoSoUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", toastText, TEST_DATA_SHEET);
            Assert.Fail("Lỗi mất dữ liệu khi sửa hồ sơ");
          }
           [Test]
          public void Test2_GioiHanKyTu(){
            TestData testData = TestUtils.GetTestDataByName("Test2_GioiHanKyTu", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string ho = testDataParts[0];
            string ten = testDataParts[1];
            string phone =testDataParts[2];
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            wait.Until(d => d.FindElement(By.ClassName("btn-profile_edit")).Displayed);
            IWebElement editAc = driver.FindElement(By.ClassName("btn-profile_edit"));
            editAc.Click();

            IWebElement firstNameInput = driver.FindElement(By.Name("form.firstName"));
            firstNameInput.Clear();
            firstNameInput.SendKeys(ho);

            IWebElement lastNameInput = driver.FindElement(By.Name("form.lastName"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(ten);//31 từ

            IWebElement phoneInput = driver.FindElement(By.Name("form.phone"));
            phoneInput.Clear();
            phoneInput.SendKeys(phone);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 250);");  

            Thread.Sleep(2000);
            IWebElement saveButton = driver.FindElement(By.CssSelector(".profile_form-btn button"));
            saveButton.Click();
          
            IWebElement errorMessage = driver.FindElement(By.CssSelector("p.error-message"));
            string toastText = errorMessage.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);
          
          }
           [Test]
          public void Test3_KiemTraSDTBang10(){
             TestData testData = TestUtils.GetTestDataByName("Test3_KiemTraSDTBang10", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string ho = testDataParts[0];
            string ten = testDataParts[1];
            string phone =testDataParts[2];
            
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            wait.Until(d => d.FindElement(By.ClassName("btn-profile_edit")).Displayed);
            IWebElement editAc = driver.FindElement(By.ClassName("btn-profile_edit"));
            editAc.Click();

            IWebElement firstNameInput = driver.FindElement(By.Name("form.firstName"));
            firstNameInput.Clear();
            firstNameInput.SendKeys(ho);

            IWebElement lastNameInput = driver.FindElement(By.Name("form.lastName"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(ten);

            IWebElement phoneInput = driver.FindElement(By.Name("form.phone"));
            phoneInput.Clear();
            phoneInput.SendKeys(phone);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 250);");  

            Thread.Sleep(2000);
            IWebElement saveButton = driver.FindElement(By.CssSelector(".profile_form-btn button"));
            saveButton.Click();
          
            IWebElement errorMessage = driver.FindElement(By.CssSelector("p.error-message"));
            string toastText = errorMessage.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);
        }
         [Test]
          public void Test4_NhapKyTuVaoSDT(){
            TestData testData = TestUtils.GetTestDataByName("Test4_NhapKyTuVaoSDT", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string ho = testDataParts[0];
            string ten = testDataParts[1];
            string phone =testDataParts[2];
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            wait.Until(d => d.FindElement(By.ClassName("btn-profile_edit")).Displayed);
            IWebElement editAc = driver.FindElement(By.ClassName("btn-profile_edit"));
            editAc.Click();

            IWebElement firstNameInput = driver.FindElement(By.Name("form.firstName"));
            firstNameInput.Clear();
            firstNameInput.SendKeys(ho);
            IWebElement lastNameInput = driver.FindElement(By.Name("form.lastName"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(ten);

            IWebElement phoneInput = driver.FindElement(By.Name("form.phone"));
            phoneInput.Clear();
            phoneInput.SendKeys(phone);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 250);");  

            Thread.Sleep(2000);
            IWebElement saveButton = driver.FindElement(By.CssSelector(".profile_form-btn button"));
            saveButton.Click();

            IWebElement errorMessage = driver.FindElement(By.CssSelector("p.error-message"));
            string toastText = errorMessage.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);
          
        }
          [Test]
          public void Test5_RePasswordKhac(){
         TestData testData = TestUtils.GetTestDataByName("Test5_RePasswordKhac", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string ho = testDataParts[0];
            string ten = testDataParts[1];
            string phone =testDataParts[2];
            string pass = testDataParts[3];
            string repass =testDataParts[4];
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();
            wait.Until(d => d.FindElement(By.ClassName("btn-profile_edit")).Displayed);
            IWebElement editAc = driver.FindElement(By.ClassName("btn-profile_edit"));
            editAc.Click();
            IWebElement firstNameInput = driver.FindElement(By.Name("form.firstName"));
            firstNameInput.SendKeys(ho);

            IWebElement lastNameInput = driver.FindElement(By.Name("form.lastName"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(ten);

            IWebElement phoneInput = driver.FindElement(By.Name("form.phone"));
          
            phoneInput.SendKeys(phone);
            IWebElement passwordInput = driver.FindElement(By.Name("form.password"));
            passwordInput.SendKeys(pass);

            IWebElement confirmPasswordInput = driver.FindElement(By.Name("form.confirmPassword"));
            confirmPasswordInput.SendKeys(repass);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 450);"); 
            Thread.Sleep(1000);
            IWebElement saveButton = driver.FindElement(By.CssSelector(".profile_form-btn button"));
            saveButton.Click();
            wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")).Displayed);
            IWebElement ErrorPass=driver.FindElement(By.CssSelector(".Toastify__toast-body"));
            string toastText = ErrorPass.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);
        }
            [Test]
          public void Test6_RangBuocPassword(){
            TestData testData = TestUtils.GetTestDataByName("Test6_RangBuocPassword", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string[] testDataParts = testData.TestDataInput.Split('\n');
            string ho = testDataParts[0];
            string ten = testDataParts[1];
            string phone =testDataParts[2];
            string pass = testDataParts[3];
            string repass =testDataParts[4];
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();
            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();
            wait.Until(d => d.FindElement(By.ClassName("btn-profile_edit")).Displayed);
            IWebElement editAc = driver.FindElement(By.ClassName("btn-profile_edit"));
            Thread.Sleep(2000);
            editAc.Click();
            IWebElement firstNameInput = driver.FindElement(By.Name("form.firstName"));
            firstNameInput.SendKeys(ho);

            IWebElement lastNameInput = driver.FindElement(By.Name("form.lastName"));
            lastNameInput.Clear();
            lastNameInput.SendKeys(ten);

            IWebElement phoneInput = driver.FindElement(By.Name("form.phone"));
          
            phoneInput.SendKeys(phone);
            IWebElement passwordInput = driver.FindElement(By.Name("form.password"));
            passwordInput.SendKeys(pass);

            IWebElement confirmPasswordInput = driver.FindElement(By.Name("form.confirmPassword"));
            confirmPasswordInput.SendKeys(repass);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 450);"); 

            Thread.Sleep(1000);
            IWebElement saveButton = driver.FindElement(By.CssSelector(".profile_form-btn button"));
            saveButton.Click();
            IWebElement errorMessage = driver.FindElement(By.CssSelector("p.error-message"));
            string toastText = errorMessage.Text.Trim();
            Assert.That(toastText.Contains(testData.ActualResults));
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", toastText, "ChinhSuaHoSoUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", toastText, TEST_DATA_SHEET);
        }
           [TearDown]
        public void TearDown(){
            Thread.Sleep(2000);
            driver.Quit();
        }
         
  }
  [TestFixture]
    public class OrderUser{
       private const string TEST_DATA_SHEET = "TC_Nghĩa";
       private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:3006/");
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            wait.Until(d => d.FindElement(By.CssSelector("div.auth-des span")).Displayed);
            IWebElement loginlink = driver.FindElement(By.CssSelector("div.auth-des span"));
            loginlink.Click();

            IWebElement emailInput = driver.FindElement(By.Name("email"));
            emailInput.SendKeys("lethanhnghia17617@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Name("password"));
            passwordInput.SendKeys("nghia123@@");

            IWebElement loginButton = driver.FindElement(By.CssSelector("button.auth-form_btn"));
            loginButton.Click();
        }
         [Test]
          public void Test1_StatusBar(){
          
            string message = "Lọc đơn hàng theo tình trạng thành công";
            TestData testData = TestUtils.GetTestDataByName("Test1_StatusBar", TEST_DATA_SHEET);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            IWebElement orderButton = driver.FindElement(By.XPath("//p[contains(text(),'Đơn hàng')]"));
            orderButton.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 250);");  
            Thread.Sleep(2000);
            
            IWebElement deliveredTab = driver.FindElement(By.XPath("//li[contains(text(), 'Đã giao')]"));
            deliveredTab.Click();       

            //Xác nhận có chữ đã giao 
            driver.FindElement(By.XPath("//div[@class='order-status']/p[contains(text(),'ĐÃ GIAO')]"));
      
            TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", message, "OrderUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", message, TEST_DATA_SHEET);
            Assert.Pass(message);
        }
        [Test]
        public void Test2_CancelOrderChoXacNhan(){
            TestData testData = TestUtils.GetTestDataByName("Test2_CancelOrderChoXacNhan", TEST_DATA_SHEET);
            string message = "Thất bại do không huỷ được đơn hàng";

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            IWebElement headerAccount = driver.FindElement(By.CssSelector("div.header-account"));
            headerAccount.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            IWebElement accountlink = driver.FindElement(By.CssSelector("a[href='/My-account']"));
            accountlink.Click();

            IWebElement orderButton = driver.FindElement(By.XPath("//p[contains(text(),'Đơn hàng')]"));
            orderButton.Click();
            IWebElement confirmTab = driver.FindElement(By.XPath("//li[contains(normalize-space(), 'Chờ xác nhận')]"));
            confirmTab.Click();


            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0, 250);");  
            Thread.Sleep(2000);    

           // Tìm nút "Hủy Đơn Hàng" đầu tiên
            IWebElement firstCancelButton = driver.FindElement(By.CssSelector(".order-btn button:first-child"));
            firstCancelButton.Click();

            TestUtils.SaveTestResultToExcel(testData.TestName, "FAIL", message, "OrderUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "FAIL", message, TEST_DATA_SHEET);
         
            Assert.Fail(message);
        }
         [Test]
        public void Test3_Detail_btn()
        {
            TestData testData = TestUtils.GetTestDataByName("Test3_Detail_btn", TEST_DATA_SHEET);
            string message = "Hiện ra bảng thông tin chi tiết về đơn hàng";

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(d => d.FindElement(By.CssSelector("div.header-account")).Displayed);
            driver.FindElement(By.CssSelector("div.header-account")).Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/My-account']")).Displayed);
            driver.FindElement(By.CssSelector("a[href='/My-account']")).Click();

            driver.FindElement(By.XPath("//p[contains(text(),'Đơn hàng')]")).Click();

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, 250);");
            Thread.Sleep(2000);
        
            IWebElement detailButton = driver.FindElement(By.XPath("//div[@class='order-btn']/button[contains(text(),'Chi tiết')]"));
            detailButton.Click();
            bool receiverInfoDisplayed = driver.FindElement(By.XPath("//div[contains(@class,'order-body_receiver')]/h3[contains(text(),'Thông tin người nhận')]")).Displayed;

           TestUtils.SaveTestResultToExcel(testData.TestName, "PASS", message, "OrderUser", testData.TestDataInput);
            TestUtils.UpdateTestCaseResult(testData.TestName, "PASS", message, TEST_DATA_SHEET);
            Assert.Pass(message);
        }
        [TearDown]
        public void TearDown(){
            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}

