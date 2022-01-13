using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowTask2.Pages;
using System;
using System.Threading;
using TechTalk.SpecFlow;


namespace SpecflowTask2.Steps
{
    [Binding]
    public sealed class Teststeps
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginPage;
        private readonly AddSellerDetailPage addSellerDetailPage;
        private readonly JoinPage joinpage;

        public Teststeps(IWebDriver driver)
        {
            // Assign 'driver' to private field and use it to initialize a page object
            this.driver = driver;

            // Initialize Selenium page object
            this.loginPage = new LoginPage(driver);

            this.addSellerDetailPage = new AddSellerDetailPage(driver);
            
            this.joinpage = new JoinPage(driver);

         }

        [Given(@"I Launched the application")]
        public void GivenILaunchedTheApplication()
        {
            var htmlReporter = new ExtentHtmlReporter(@"E:\Industryconnect Repo\FinalMars_task2\Final_Mars_Task2\SpecflowTask2\ExtentReport\Login.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            var extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            //Feature Login

            var feature = extent.CreateTest<Feature>("Login");
            var scenario = feature.CreateNode<Scenario>("On filling my details on Joining form I should be registered successfully");
            scenario.CreateNode<Given>("I Launched the application");
            extent.Flush();

            driver.Navigate().GoToUrl("http://localhost:5000/Home");
            driver.Manage().Window.Maximize();
        }

        [Given(@"I Click Login link")]
        public void GivenIClickLoginLink()
        {
            loginPage.ClickLogIn();
        }

        [When(@"I enter ""(.*)"" in username and ""(.*)"" in password field")]
        public void WhenIEnterInUsernameAndInPasswordField(string p0, string p1)
        {
            loginPage.Login("sidra_riz@yahoo.com", "sid6638659");
        }

        [When(@"I click on Login Button")]
        public void WhenIClickOnLoginButton()
        {
            loginPage.ClickLoginButton();
           
        }

        [Then(@"I should be directed to profile page")]
        public void ThenIShouldBeDirectedToProfilePage()
        {
            Thread.Sleep(3000);
            loginPage.verifyHomePageTitle();
        }
               
          //Add Seller details Feature Test Steps
                       
              
        [Given(@"I am on Skill Tab of my Profile")]
        public void GivenIAmOnSkillTabOfMyProfile()
        {
            Thread.Sleep(3000);
            addSellerDetailPage.GotoSkilltab();
        }
        
        [When(@"I enter (.*) in txt box")]
        public void WhenIEnterInTxtBox(string SkillName)
        {
            addSellerDetailPage.EnterSkillName(SkillName);
        }

        [When(@"I enter (.*) in dropdown")]
        public void WhenIEnterInDropdown(string SkillLevel)
        {
            Thread.Sleep(2000);
            addSellerDetailPage.EnterSkillLevel(SkillLevel);
        }


        [When(@"I Click Add Button")]
        public void WhenIClickAddButton()
        {
            addSellerDetailPage.AddSClick();
        }

        [Then(@"Verify the (.*) should be available on screen")]
        public void ThenVerifyTheShouldBeAvailableOnScreen(string Message)
        {
            Thread.Sleep(3000);           

             IWebElement popupmessage = driver.FindElement(By.XPath("//a[contains(@href,'#')]"));
            string getText = popupmessage.Text;

            // Check if the newly added record is present or not 
            Assert.That((Message.Contains(getText)), Is.True);
        }

        [When(@"I click Delete Icon of last added Skill")]
        public void WhenIClickDeleteIconOfLastAddedSkill()
        {
            IWebElement DeleteIcon = driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/tbody[2]/tr[last()]/td[3]/span[2]/i"));
            DeleteIcon.Click();
        }

        [Then(@"verify that record has been deleted")]
        public void ThenVerifyThatRecordHasBeenDeleted()
        {
            Thread.Sleep(3000);
            IWebElement popupmessage = driver.FindElement(By.XPath("//div[contains(.,'Digital Marketing has been deleted')]"));
            string actualresult = popupmessage.Text;
            string expectedresult = "Digital Marketing has been deleted";
         
            // Check if the newly added record is present or not 
            Assert.AreEqual(expectedresult,actualresult); 
        }

        // Join User page test steps
        [Given(@"I click on Join Button")]
        public void GivenIClickOnJoinButton()
        {
            joinpage.JoinButton.Click();
        }

        [When(@"I fill the form with (.*) (.*) (.*) (.*) and (.*)")]
        public void WhenIFillTheFormWithAnd(string Firstname, string Lastname, string Emailaddress, string Password, string Confirmpassword)
        {
            Thread.Sleep(3000);
            joinpage.EnterFormDetails(Firstname, Lastname, Emailaddress, Password, Confirmpassword);
        }


        [When(@"Check the Terms and conditions box")]
        public void WhenCheckTheTermsAndConditionsBox()
        {
            Thread.Sleep(3000);
            joinpage.Joincheckbox.Click();
        }

        [When(@"I Click the Join button")]
        public void WhenIClicksTheJoinButton()
        {
            joinpage.JoinpageButton.Click();
        }

        [Then(@"(.*) should be displayed")]
        public void ThenShouldBeDisplayed(string Message)
        {
            Thread.Sleep(3000);
          //  IWebElement popupmessage = driver.FindElement(By.XPath("//a[contains(@href,'#')]"));
          //  string getText = popupmessage.Text;

          //  Check if the newly added record is present or not 
          //  Assert.That((Message.Contains(getText)), Is.True);
        }

        [Then(@"I should be directed to the SignIn page")]
        public void ThenIShouldBeDirectedToTheSignInPage()
        {
           joinpage.verifyJoinPageTitle();
        }


       












    }



}
