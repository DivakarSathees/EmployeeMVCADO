using NUnit.Framework;
using dotnetapp.Controllers;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using Moq;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeApp.Tests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Type controllerType;
        private Type employeeType;
        private PropertyInfo[] properties;

        [SetUp]
        public void Setup()
        {
            // Get the type of the EmployeeController
             employeeType = typeof(dotnetapp.Models.Employee);
            properties = employeeType.GetProperties();
            controllerType = typeof(EmployeeController);
        }

        [Test]
        public void TestIndexMethodExists()
        {
            // Arrange
            MethodInfo indexMethod = controllerType.GetMethod("Index");

            // Assert
            Assert.IsNotNull(indexMethod, "Index method should exist in EmployeeController.");
        }

        [Test]
        public void TestCreateGetMethodExists()
        {
            // Arrange
            MethodInfo createGetMethod = controllerType.GetMethod("Create", new Type[0]);

            // Assert
            Assert.IsNotNull(createGetMethod, "Create method should exist in EmployeeController.");
        }

        [Test]
        public void TestCreatePostMethodExists()
        {
            // Arrange
            MethodInfo createPostMethod = controllerType.GetMethod("Create", new Type[] { typeof(Employee) });

            // Assert
            Assert.IsNotNull(createPostMethod, "Create POST method should exist in EmployeeController.");
        }

        [Test]
        public void TestSearchMethodExists()
        {
            // Arrange
            MethodInfo createPostMethod = controllerType.GetMethod("Search", new Type[] { typeof(string) });

            // Assert
            Assert.IsNotNull(createPostMethod, "Search method should exist in EmployeeController.");
        }


        [Test]
        public void TestCreatePostMethodReturnsActionResult()
        {
            // Arrange
            MethodInfo createPostMethod = controllerType.GetMethod("Create", new Type[] { typeof(Employee) });
            var controller = new EmployeeController();

            // Act
            var employee = new Employee(); // Create a sample Furniture object
            var result = createPostMethod.Invoke(controller, new object[] { employee });

            // Assert
            Assert.IsInstanceOf<IActionResult>(result, "Create (POST) method should return an IActionResult.");
        }

        [Test]
        public void TestEmployeeClassExists()
        {
            // Arrange
            Type furnitureType = typeof(dotnetapp.Models.Employee);

            // Assert
            Assert.IsNotNull(furnitureType, "Employee class should exist.");
        }

        [Test]
        public void TestEmployeePropertiesExist()
        {
            // Assert
            Assert.IsNotNull(properties, "Employee class should have properties.");
            Assert.IsTrue(properties.Length > 0, "Employee class should have at least one property.");
        }

         [Test]
        public void TestidProperty()
        {
            // Arrange
            PropertyInfo idProperty = properties.FirstOrDefault(p => p.Name == "id");

            // Assert
            Assert.IsNotNull(idProperty, "id property should exist.");
            Assert.AreEqual(typeof(int), idProperty.PropertyType, "id property should have data type 'int'.");
        }

        [Test]
        public void TestNameProperty()
        {
            // Arrange
            PropertyInfo productProperty = properties.FirstOrDefault(p => p.Name == "Name");

            // Assert
            Assert.IsNotNull(productProperty, "Name property should exist.");
            Assert.AreEqual(typeof(string), productProperty.PropertyType, "Name property should have data type 'string'.");
        }

        [Test]
        public void TestDOBProperty()
        {
            // Arrange
            PropertyInfo descriptionProperty = properties.FirstOrDefault(p => p.Name == "DOB");

            // Assert
            Assert.IsNotNull(descriptionProperty, "DOB property should exist.");
            Assert.AreEqual(typeof(DateTime), descriptionProperty.PropertyType, "DOB property should have data type 'string'.");
        }

        [Test]
        public void TestsalaryProperty()
        {
            // Arrange
            PropertyInfo descriptionProperty = properties.FirstOrDefault(p => p.Name == "salary");

            // Assert
            Assert.IsNotNull(descriptionProperty, "salary property should exist.");
            Assert.AreEqual(typeof(decimal), descriptionProperty.PropertyType, "salary property should have data type 'string'.");
        }

        // [Test]
        // public void TestDimensionsProperty()
        // {
        //     // Arrange
        //     PropertyInfo descriptionProperty = properties.FirstOrDefault(p => p.Name == "Dimensions");

        //     // Assert
        //     Assert.IsNotNull(descriptionProperty, "Dimensions property should exist.");
        //     Assert.AreEqual(typeof(string), descriptionProperty.PropertyType, "Dimensions property should have data type 'string'.");
        // }

        // [Test]
        // public void TestPriceProperty()
        // {
        //     // Arrange
        //     PropertyInfo descriptionProperty = properties.FirstOrDefault(p => p.Name == "Price");

        //     // Assert
        //     Assert.IsNotNull(descriptionProperty, "Price property should exist.");
        //     Assert.AreEqual(typeof(decimal), descriptionProperty.PropertyType, "Price property should have data type 'string'.");
        // }

        [Test]
        public void TestEmployeeDBContext_ClassExists_in_Models()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.EmployeeDBContext");
            Assert.NotNull(postType, "EmployeeDBContext class does not exist.");
        }

        // [Test]
        // public void TestEmployee_Folder_Exists()
        // {
        //     bool viewsFolderExists = Directory.Exists(@"/home/coder/project/workspace/dotnetapp/Views/Employee");

        //     Assert.IsTrue(viewsFolderExists, "Employee does not exist.");
        // }

        [Test]
        public void Test_CreateViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Employee", "Create.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }

        [Test]
        public void Test_IndexViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Employee", "Index.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Index.cshtml view file does not exist.");
        }

        [Test]
        public void TestSearchViewResult()
        {
            // Arrange
            var controller = new EmployeeController();

            // Act
            var result = controller.Search("query") as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Search method should return a ViewResult.");
        }
    }
}
