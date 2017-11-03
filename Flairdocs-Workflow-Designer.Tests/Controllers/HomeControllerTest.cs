using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flairdocs_Workflow_Designer;
using Flairdocs_Workflow_Designer.Controllers;
using Flairdocs_Workflow_Designer.Models;

namespace Flairdocs_Workflow_Designer.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TitleExists()
        {
            Console.WriteLine("Workflow test");
            HomeController controller = new HomeController();
            Boolean result = controller.TitleExists("Test Workflow");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Workflow()
        {
            WorkflowContext context = new WorkflowContext();
            var workflows = from w in context.Workflows
                            where w.Title == "Test Workflow"
                            select w;

            Guid id = workflows.First().Id;
            HomeController controller = new HomeController();
            ViewResult result = controller.Workflow(id) as ViewResult;
            Assert.AreEqual("Test Workflow", result.ViewBag.Title);

        }

        [TestMethod]
        public void WorkflowSearchExists()
        {
            String test = "Test Workflow";
            HomeController controller = new HomeController();
            Guid? result = controller.WorkflowSearch(test);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WorkflowSearchDoesNotExist()
        {
            String test = "";
            HomeController controller = new HomeController();
            Guid? result = controller.WorkflowSearch(test);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateRejected()
        {
            String title = "Test Workflow";
            String des = "Test";
            HomeController controller = new HomeController();
            Guid? result = controller.Create(title, des);
            Assert.IsNull(result);
        }
    }
}
