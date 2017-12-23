using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LINQ;
using LINQ.Tasks;
using NUnit.Framework;

namespace LINQTests
{
    [TestFixture]
    public class Tests
    {
        private Tasks _tasks;

        [SetUp]
        public void SetUp()
        {
            _tasks = new Tasks();
        }

        [Test]
        public void Task1Test()
        {
            Assert.AreEqual(
                "ЭТА СОБАКА ЕСТ \n" +
                "СЛИШКОМ МНОГО ОВОЩЕЙ \n" +
                "ПОСЛЕ ОБЕДА ", _tasks.Task1("This dog eats too much vegetables after lunch"));
        }

        [Test]
        public void Task2Test()
        {
            int k = 4;
            int[] inputArray = { 0, 1, 22, 11, 15, 17, 16, 1238, 33, 365, 37 };
            int[] checkingArray = { 15, 17, 33, 37 };
            Assert.AreEqual(checkingArray.Reverse(), _tasks.Task2(k, inputArray));
        }

        [Test]
        public void Task3Test()
        {
            int k = 2;
            string[] inputArray =
            {
                "aaa",
                "m",
                "ccccc",
                "acacac",
                "zm",
                "zm",
                "vt",
                "tv",
                "r",
                "y"
            };
            string checkingString = "vttvmzmzcacacacccccaaa";
            Assert.AreEqual(checkingString, _tasks.Task3(k, inputArray));
        }

        [Test]
        public void Task4Test()
        {
            string[] checkingArray =
            {
                "Mounts when duration >15:5 | Year:2008",
                "Mounts when duration >15:2 | Year:2005",
                "Mounts when duration >15:2 | Year:2006",
                "Mounts when duration >15:0 | Year:2009"
            };
            Assert.AreEqual(checkingArray, _tasks.Task4(1, DbFitnesCenter.Db));
        }

        [Test]
        public void Task5Test()
        {
            XDocument rightDcument = XDocument.Load(@"E:\Veeam\Workspaces\Yury.Aksenov.Workspace\Labs\Lab 2.LINQ\LINQ\LINQTests\XMLCheckingFile.xml");
            XDocument resultDocument =
                _tasks.Task5(@"E:\Veeam\Workspaces\Yury.Aksenov.Workspace\Labs\Lab 2.LINQ\LINQ\LINQTests\XMLSource.xml");


            Assert.AreEqual(rightDcument.ToString(),resultDocument.ToString());
        }

        public static class DbFitnesCenter
        {
            public static readonly List<Client> Db = new List<Client>
            {
                new Client(1, 2005, 2, 14),
                new Client(1, 2005, 3, 15),
                new Client(1, 2005, 5, 10),
                new Client(1, 2005, 7, 19),
                new Client(1, 2005, 8, 20),
                new Client(1, 2006, 1, 90),
                new Client(1, 2006, 2, 10),
                new Client(1, 2006, 4, 25),
                new Client(1, 2008, 2, 37),
                new Client(1, 2008, 3, 9),
                new Client(1, 2008, 4, 14),
                new Client(1, 2008, 5, 23),
                new Client(1, 2008, 6, 27),
                new Client(1, 2008, 9, 90),
                new Client(1, 2008, 10, 32),
                new Client(1, 2009, 11, 10),
                new Client(1, 2009, 12, 10),
                new Client(1, 2009, 10, 10),
                new Client(1, 2009, 11, 10),
                new Client(1, 2008, 12, 10),
                new Client(2, 2007, 1, 1),
                new Client(2, 2007, 2, 3),
                new Client(2, 2007, 3, 1),
                new Client(2, 2007, 4, 2),
                new Client(2, 2007, 5, 5),
                new Client(2, 2007, 9, 1),
                new Client(2, 2007, 10, 0),
            };
        }
    }
}
