using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyProject.Controllers;
using System.Web.Mvc;
using System.Data.Entity;

namespace MyProject.Tests.Controllers
{
    /// <summary>
    /// Descripción resumida de ProfileControllerTest
    /// </summary>
    [TestClass]
    public class ProfileControllerTest
    {

              #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CreateInvalidProfile()
        {
            Mock<DataAccess.MyProjectEntities> repo = new Mock<DataAccess.MyProjectEntities>();
            repo.Verify(r => r.SaveChanges(), Times.Never);
            var controller = new MyProject.Controllers.ProfileController(repo.Object);
            var profile = new Mock<DataAccess.Profile>();
            var user = new Mock<DataAccess.User>();
            var countries = new Mock<DbSet<DataAccess.Country>>();
            var users = new Mock<DbSet<DataAccess.User>>();
            var userTypes = new Mock<DbSet<DataAccess.UserType>>();
            var profiles = new Mock<DbSet<DataAccess.Profile>>();
            var country = new Mock<DataAccess.Country>();
            repo.Setup(r => r.Country).Returns(countries.Object);
            repo.Setup(r => r.User).Returns(users.Object);
            repo.Setup(r => r.UserType).Returns(userTypes.Object);
            repo.Setup(r => r.Profile).Returns(profiles.Object);
            controller.ModelState.AddModelError("email", "empty");
            user.Setup(p => p.Email).Returns("");
            profile.Setup(p => p.User1).Returns(user.Object);
            profile.Setup(p => p.Country1).Returns(country.Object);
            ViewResult result = controller.Create(profile.Object) as ViewResult;
            Assert.IsNotNull(result.ViewBag.Country);
            Assert.IsNotNull(result.ViewBag.User);
            Assert.IsNotNull(result.ViewBag.UserType);
        }

        [TestMethod]
        public void CreateValidProfile()
        {
            Mock<DataAccess.MyProjectEntities> repo = new Mock<DataAccess.MyProjectEntities>();
            var controller = new MyProject.Controllers.ProfileController(repo.Object);
            var profile = new Mock<DataAccess.Profile>();
            var user = new Mock<DataAccess.User>();

            user.Setup(n => n.Name).Returns("Nombre");
            user.Setup(e => e.Email).Returns("Email");
            user.Setup(u => u.UserType.UserType1).Returns("Freelancer");
            profile.Setup(n => n.PhoneNumber).Returns("88888888");
            profile.Setup(c => c.Country1.Name).Returns("Costa Rica");
            profile.Setup(p => p.User1).Returns(user.Object);
            RedirectToRouteResult result = controller.Create(profile.Object) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsNull(result.RouteValues["controller"]);
        }
    }
}
