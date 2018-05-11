using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUiTest
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITests
    {
        public CodedUITests()
        {
        }

        [TestMethod]
        public void InitialTest_CodedUI()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            this.UIMap.InitialTest(); // open browser
            this.UIMap.InitialAssert();
        }

        [TestMethod]
        public void Index_CantLoginWithoutAnyData_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.CantLoginWithoutAnyData();
            this.UIMap.CantLoginWithoutAnyDataAssert();
        }

        [TestMethod]
        public void Index_LoginWithProperValues_ShouldLogin_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.LoginWithProperValuesAssert();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Index_LogoutShouldReturnHome_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.Logout();
            this.UIMap.InitialAssert();
        }

        [TestMethod]
        public void User_IsVisibleUserNameAfterLogin_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.IsVisibleUserNameAfterLoginAssert();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Index_Admin_AfterLoginShouldSeeAllTabs_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.ShouldSeeAllTabs(); //Assert
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Index_NoUserLoggedIn_ShouldNotShowTabs()
        {
            this.UIMap.InitialTest();
            this.UIMap.ShouldNotShowTabsAssert();
        }

        [TestMethod]
        public void User_HaveOnlySessionsTab_CodeUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValuesSimpleUser();
            this.UIMap.SimpleUserSeeOnlySessionTabAssert();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Admin_Employees_ShouldOpenPage_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.OpenEmployeesPage();
            this.UIMap.OpenEmployeesPageAssert();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Employees_ClickOnAppName_ShouldNavigateToHome_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.OpenEmployeesPage();
            this.UIMap.ClickOnAppName();
            this.UIMap.IsVisibleUserNameAfterLoginAssert();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Admin_Employee_NavigateToAllTabs_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.NavigateToAllTabs();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Employee_Create_BackButton_ShouldReturnEmployeesPage_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.OpenEmployeesPage();
            this.UIMap.EmployeeCreateBackButton();
            this.UIMap.OpenEmployeesPageAssert();
            this.UIMap.Logout();
        }

        [TestMethod]
        public void Employee_Create_ShouldReturnCreatePage_CodedUI()
        {
            this.UIMap.InitialTest();
            this.UIMap.LoginWithProperValues();
            this.UIMap.OpenEmployeesPage();

            this.UIMap.NavigateToCreateEmployee();
            this.UIMap.NavigateToCreateEmployeeAssert();
            this.UIMap.EmployeeBackToList();

            this.UIMap.NavigateToDeleteEmployee();
            this.UIMap.NavigateToDeleteEmployeeAssert();
            this.UIMap.EmployeeBackToList();

            this.UIMap.NavigateToDetailsEmployee();
            this.UIMap.EmployeeNavigateToEditAssert();
            this.UIMap.EmployeeBackToList();

            this.UIMap.NavigateToEditEmployee();
            this.UIMap.NavigateToEditEmployeeAssert();
            this.UIMap.EmployeeBackToList();

            this.UIMap.Logout();
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
