using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;


namespace Flairdocs_Workflow_Designer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private String url = "http://localhost:52956/";
        /*
         * Below are tests on the GUI for Chrome
         * */

        [TestMethod]
        public void MenuDimensions_Chrome()
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                String actualHeight = driver.FindElement(By.Id("workflow-designer-menu")).GetCssValue("height");
                Console.WriteLine("Actual Height: " + actualHeight);
                String expectedHeight = "380px";
                String actualWidth = driver.FindElement(By.Id("workflow-designer-menu")).GetCssValue("width");
                Console.WriteLine("Actual Width: " + actualWidth);
                String expectedWidth = "220px";
                Assert.AreEqual(expectedHeight, actualHeight);
                Assert.AreEqual(expectedWidth, actualWidth);
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: MenuDimensions_Chrome");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowWindowDimensions_Chrome()
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                String actualHeight = driver.FindElement(By.Id("workflow-designer-window")).GetCssValue("height");
                Console.WriteLine("Actual Height: " + actualHeight);
                String expectedHeight = "380px";
                Assert.AreEqual(expectedHeight, actualHeight);
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowDimensions_Chrome");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void MenuButtons_Chrome()
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("open-workflow-button"));
                driver.FindElement(By.Id("create-workflow-button"));
                driver.FindElement(By.Id("workflow-audit-log-button"));
                driver.FindElement(By.Id("workflow-settings-button"));
                driver.FindElement(By.ClassName("workflow-live-search"));

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: MenuButtons_Chrome");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowButtons_Chrome()
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("add-reviewer-button"));
                driver.FindElement(By.Id("save-workflow-button"));

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowButtons_Chrome");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowWindowContents_Chrome()
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("workflow-designer-window-buttons"));
                driver.FindElement(By.Id("workflow-designer-window-edit"));

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowContents_Chrome");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowWindowEditScrollable_Chrome()
        {
            ChromeDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                String actualOverflow = driver.FindElement(By.Id("workflow-designer-window-edit")).GetCssValue("overflow");
                String expectedOverflow = "scroll";
                Assert.AreEqual(expectedOverflow, actualOverflow);
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowWindowScrollable_Chrome");
                driver.Close();
                driver.Dispose();
            }
        }

        /*
         * Below are tests on the GUI for IE
         * */

        [TestMethod]
        public void MenuDimensions_IE()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                String actualHeight = driver.FindElement(By.Id("workflow-designer-menu")).GetCssValue("height");
                Console.WriteLine("Actual Height: " + actualHeight);
                String expectedHeight = "366px";
                String actualWidth = driver.FindElement(By.Id("workflow-designer-menu")).GetCssValue("width");
                Console.WriteLine("Actual Width: " + actualWidth);
                String expectedWidth = "198px";
                Assert.AreEqual(expectedHeight, actualHeight);
                Assert.AreEqual(expectedWidth, actualWidth);
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: MenuDimensions_IE");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowWindowDimensions_IE()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                String actualHeight = driver.FindElement(By.Id("workflow-designer-window")).GetCssValue("height");
                Console.WriteLine("Actual Height: " + actualHeight);
                String expectedHeight = "378px";
                Assert.AreEqual(expectedHeight, actualHeight);
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowDimensions_IE");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void MenuButtons_IE()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("open-workflow-button"));
                driver.FindElement(By.Id("create-workflow-button"));
                driver.FindElement(By.Id("workflow-audit-log-button"));
                driver.FindElement(By.Id("workflow-settings-button"));
                driver.FindElement(By.ClassName("workflow-live-search"));

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: MenuButtons_IE");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowButtons_IE()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("add-reviewer-button"));
                driver.FindElement(By.Id("save-workflow-button"));

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowButtons_IE");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowWindowContents_IE()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("workflow-designer-window-buttons"));
                driver.FindElement(By.Id("workflow-designer-window-edit"));

                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowContents_IE");
                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void WorkflowWindowEditScrollable_IE()
        {
            InternetExplorerDriver driver = new InternetExplorerDriver();
            try
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                String actualOverflow = driver.FindElement(By.Id("workflow-designer-window-edit")).GetCssValue("overflow");
                String expectedOverflow = "scroll";
                Assert.AreEqual(expectedOverflow, actualOverflow);
                driver.Close();
                driver.Dispose();
            }
            catch
            {
                Assert.Fail();
                Console.WriteLine("Error executing test case: WorkflowWindowScrollable_IE");
                driver.Close();
                driver.Dispose();
            }
        }
    }
}
