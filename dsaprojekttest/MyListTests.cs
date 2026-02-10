using dsaprojekt;

namespace dsaprojekttest
{
    [TestClass]
    public sealed class MyListTests
    {
        MyList<int> myList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };

        [TestMethod]
        public void InsertionSort_Integer_First()
        {
            int expected = 5;
            myList.InsertionSort();
            Assert.AreEqual(expected, myList[0]);
        }

		[TestMethod]
		public void InsertionSort_Integer_Last()
		{
			int expected = 123;
			myList.InsertionSort();
			Assert.AreEqual(expected, myList[myList.Count - 1]);
		}







		[TestMethod]
		public void BubbleSort_Integer_First()
		{
			int expected = 5;
			myList.BubbleSort();
			Assert.AreEqual(expected, myList[0]);
		}

		[TestMethod]
		public void BubbleSort_Integer_Last()
		{
			int expected = 123;
			myList.BubbleSort();
			Assert.AreEqual(expected, myList[myList.Count - 1]);
		}







		[TestMethod]
		public void QuickSort_Integer_First()
		{
			int expected = 5;
			myList.QuickSort();
			Assert.AreEqual(expected, myList[0]);
		}

		[TestMethod]
		public void QuickSort_Integer_Last()
		{
			int expected = 123;
			myList.QuickSort();
			Assert.AreEqual(expected, myList[myList.Count - 1]);
		}
	}
}
