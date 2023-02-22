using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FakeItEasy;
using FakeXrmEasy;

using System.Collections.Generic;
using CustomWorkflow;


namespace CustomWorkflowUnitTestProject
{
    [TestClass]
    public class CustomWorkflowUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fakedContext = new XrmFakedContext();

            //Inputs
            var inputs = new Dictionary<string, object> {
            {"InputText", "The quick fox jumped over the lazy brown dog"},
        };

            var result = fakedContext.ExecuteCodeActivity<WordCount>(inputs);

            Assert.IsTrue(result["CountofWords"].Equals(9));
           
        }
    }
}
