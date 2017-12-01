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
        WorkflowContext db = new WorkflowContext();

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
        public void TitleExistsTrue()
        {
            Console.WriteLine("Workflow test");
            HomeController controller = new HomeController();
            Boolean result = controller.TitleExists("Test Workflow");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TitleExistsFalse()
        {
            Console.WriteLine("Workflow test");
            HomeController controller = new HomeController();
            Boolean result = controller.TitleExists("");
            Assert.IsFalse(result);
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
        public void WorkflowNotValid()
        {

            Guid id = Guid.Empty;
            HomeController controller = new HomeController();
            ViewResult result = controller.Workflow(id) as ViewResult;
            Assert.IsNull(result);

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

        [TestMethod]
        public void AddStep()
        {
            HomeController controller = new HomeController();
            String title = "Test Workflow";
            Guid? wId = controller.WorkflowSearch(title);
            Workflow w = db.Workflows.Find(wId);
            int stepCountBefore = w.Steps.Count();
            Guid? stepAdded = controller.SaveStep(wId.Value, Guid.Parse("00000000-0000-0000-0000-000000000000"), stepCountBefore);

            Assert.IsNotNull(stepAdded);
        }

        [TestMethod]
        public void RemoveStep()
        {
            HomeController controller = new HomeController();
            String title = "Test Workflow";
            Guid? wId = controller.WorkflowSearch(title);
            Workflow w = db.Workflows.Find(wId);
            int stepCountBefore = w.Steps.Count();
            Step stepToRemove = w.Steps.Last();
            controller.RemoveStep(stepToRemove.Id);
        }


    }
}
