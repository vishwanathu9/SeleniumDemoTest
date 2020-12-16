using OpenQA.Selenium;
using SeleniumDemoTest.src.test.utilities;
using System;
using Xunit;

[assembly:CollectionBehavior(MaxParallelThreads =4)]
namespace SeleniumDemoTest
{
   

    public class UnitTest
    {
        string testUrl = "https://lambdatest.github.io/sample-todo-app/";
        string itemName = "lets add this to list";

        [Theory]
        [InlineData(BrowserType.Chrome)]
        [InlineData(BrowserType.Firefox)]
        public void NavigateToDoApp(BrowserType browserType)
        {
           using(var driver=WebDriverInfra.Create_Browser(browserType))
            {
                driver.Navigate().GoToUrl(testUrl);
                driver.Manage().Window.Maximize();

                Assert.Equal("Sample page - lambdatest.com", driver.Title);

                //click on first checkbox
                IWebElement firstCheckbox = driver.FindElement(By.Name("li1"));
                firstCheckbox.Click();

                //click on second checkbox
                IWebElement secondCheckbox = driver.FindElement(By.Name("li2"));
                secondCheckbox.Click();

                //verified Item name
                IWebElement textfield=driver.FindElement(By.Id("sampletodotext"));
                textfield.SendKeys(itemName);

                //Click on Add button

                IWebElement addButton = driver.FindElement(By.Id("addbutton"));
                addButton.Click();


                //Verified added item
                IWebElement itemtext = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span"));
                string getText = itemtext.Text;
                Assert.Contains(getText, itemName);

                Console.WriteLine("Lt_ToDo_Test Passed");

                driver.Quit();



            }
        }
    }
}
