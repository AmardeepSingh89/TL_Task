using System;
using HackerNews.Interfaces;
using HackerNews.LineCommand;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackerNews.UnitTests
{
    [TestClass]
    public class PostCommandTest
    {

        [TestMethod]
        public void ValidInput()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "--posts", "10" };

            try
            {
                postCommand.ValidCommandLine(args);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void NoArguments()
        {
            ITypeCommand postCommand = new PostCommand();

            try
            {
                postCommand.ValidCommandLine(null);
                Assert.Fail("Expected to get NullReferenceException");
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual(ex.Message, "No command entered");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get NullReferenceException but get: "+ex.Message);
            }
        }

        [TestMethod]
        public void MoreThanTwoArguments()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "test", "hello", "post" };

            try
            {
                postCommand.ValidCommandLine(args);
                Assert.Fail("Expected to get ArgumentException");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Only accept two arguments e.g. --posts 100");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get NullReferenceException but get: " + ex.Message);
            }
        }

        public void EmptyPostArgument()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "", "10" };

            try
            {
                postCommand.ValidCommandLine(args);
                Assert.Fail("Expected to get NullReferenceException");
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual(ex.Message, "Please enter '--posts'");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get NullReferenceException but get: " + ex.Message);
            }
        }

        public void EmptyCountArgument()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "--posts", "" };

            try
            {
                postCommand.ValidCommandLine(args);
                Assert.Fail("Expected to get NullReferenceException");
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual(ex.Message, "Please enter postive number");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get NullReferenceException but get: " + ex.Message);
            }
        }

        public void IncorrectPostArgument()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "post", "10" };

            try
            {
                postCommand.ValidCommandLine(args);
                Assert.Fail("Expected to get ArgumentException");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Please enter '--posts'");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get ArgumentException but get: " + ex.Message);
            }
        }

        [TestMethod]
        public void InvalidInputNumber()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "--posts", "aaa" };

            try
            {
                postCommand.ValidCommandLine(args);
                Assert.Fail("Expected to get ArgumentException");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Please enter a positive integer up to 100");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get ArgumentException but get: " + ex.Message);
            }
        }

        [TestMethod]
        public void Over100InputNumber()
        {
            ITypeCommand postCommand = new PostCommand();

            var args = new[] { "--posts", "123" };

            try
            {
                postCommand.ValidCommandLine(args);
                Assert.Fail("Expected to get ArgumentException");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(ex.Message, "Number must be up to 100");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected to get ArgumentException but get: " + ex.Message);
            }
        }
    }
}
