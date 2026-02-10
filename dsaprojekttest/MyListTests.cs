using System.Collections.Generic;
using dsaprojekt;
using static System.Net.Mime.MediaTypeNames;

namespace dsaprojekttest
{
    [TestClass]
    public sealed class MyListTests
    {

		//Tests skal verificere, at sorteringen:
		//• returnerer korrekt sorteret output
		//• håndterer edge cases korrekt
		//• hver implementeret sorteringsalgoritme skal testes separat.

		//Tests skal som minimum inkludere:
		//• tom liste						-->	InsertionSort_Integer_EmptyList_ReturnsEmptyList
		//• liste med ét element			-->	InsertionSort_Integer_OneElementList_ReturnsOneElementList
		//• liste med flere ens elementer   --> InsertionSort_Integer_SameElementsList_ReturnsSortedSameElementsList
		//• allerede sorteret liste			--> InsertionSort_Integer_AlreadySorted_RemainsSorted



		[TestMethod]
        public void InsertionSort_Integer_First()
        {
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			int expected = 5;
            randomMyList.InsertionSort();
            Assert.AreEqual(expected, randomMyList[0]);
        }

		[TestMethod]
		public void InsertionSort_Integer_Last()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			int expected = 123;
			randomMyList.InsertionSort();
			Assert.AreEqual(expected, randomMyList[randomMyList.Count - 1]);
		}

		[TestMethod]
		public void InsertionSort_Integer_ReturnsSortedList()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			randomMyList.InsertionSort();
			CollectionAssert.AreEqual(expectedSortedMyList.ToArray(), randomMyList.ToArray());
		}

		[TestMethod]
		public void InsertionSort_Integer_AlreadySorted_RemainsSorted()
		{
			MyList<int> alreadySortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			alreadySortedMyList.InsertionSort();
			CollectionAssert.AreEqual(expectedSortedMyList.ToArray(), alreadySortedMyList.ToArray());
		}

		[TestMethod]
		public void InsertionSort_Integer_EmptyList_ReturnsEmptyList()
		{
			MyList<int> emptyMyList = new MyList<int>();

			emptyMyList.InsertionSort();

			Assert.AreEqual(0, emptyMyList.Count);
		}

		[TestMethod]
		public void InsertionSort_Integer_OneElementList_ReturnsOneElementList()
		{
			MyList<int> oneElementMyList = new MyList<int>() { 27 };
			MyList<int> expectedOneElementMyList = new MyList<int>() { 27 };

			oneElementMyList.InsertionSort();

			CollectionAssert.AreEqual(expectedOneElementMyList.ToArray(), oneElementMyList.ToArray());
		}

		[TestMethod]
		public void InsertionSort_Integer_SameElementsList_ReturnsSortedSameElementsList()
		{
			MyList<int> sameElementsMyList = new MyList<int>() { 10, 64, 29, 10, 30, 57 };
			MyList<int> expectedSameElementsMyList = new MyList<int>() { 10, 10, 29, 30, 57, 64 };

			sameElementsMyList.InsertionSort();

			CollectionAssert.AreEqual(sameElementsMyList.ToArray(), expectedSameElementsMyList.ToArray());
		}




















		[TestMethod]
		public void BubbleSort_Integer_First()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			int expected = 5;
			randomMyList.BubbleSort();
			Assert.AreEqual(expected, randomMyList[0]);
		}

		[TestMethod]
		public void BubbleSort_Integer_Last()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			int expected = 123;
			randomMyList.BubbleSort();
			Assert.AreEqual(expected, randomMyList[randomMyList.Count - 1]);
		}

		[TestMethod]
		public void BubbleSort_Integer_ReturnsSortedList()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			randomMyList.BubbleSort();
			CollectionAssert.AreEqual(expectedSortedMyList.ToArray(), randomMyList.ToArray());
		}

		[TestMethod]
		public void BubbleSort_Integer_AlreadySorted_RemainsSorted()
		{
			MyList<int> alreadySortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			alreadySortedMyList.BubbleSort();
			CollectionAssert.AreEqual(expectedSortedMyList.ToArray(), alreadySortedMyList.ToArray());
		}

		[TestMethod]
		public void BubbleSort_Integer_EmptyList_ReturnsEmptyList()
		{
			MyList<int> emptyMyList = new MyList<int>();

			emptyMyList.BubbleSort();

			Assert.AreEqual(0, emptyMyList.Count);
		}

		[TestMethod]
		public void BubbleSort_Integer_OneElementList_ReturnsOneElementList()
		{
			MyList<int> oneElementMyList = new MyList<int>() { 27 };
			MyList<int> expectedOneElementMyList = new MyList<int>() { 27 };

			oneElementMyList.BubbleSort();

			CollectionAssert.AreEqual(expectedOneElementMyList.ToArray(), oneElementMyList.ToArray());
		}

		[TestMethod]
		public void BubbleSort_Integer_SameElementsList_ReturnsSortedSameElementsList()
		{
			MyList<int> sameElementsMyList = new MyList<int>() { 10, 64, 29, 10, 30, 57 };
			MyList<int> expectedSameElementsMyList = new MyList<int>() { 10, 10, 29, 30, 57, 64 };

			sameElementsMyList.BubbleSort();

			CollectionAssert.AreEqual(sameElementsMyList.ToArray(), expectedSameElementsMyList.ToArray());
		}


























		[TestMethod]
		public void QuickSort_Integer_First()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			int expected = 5;
			randomMyList.QuickSort();
			Assert.AreEqual(expected, randomMyList[0]);
		}

		[TestMethod]
		public void QuickSort_Integer_Last()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			int expected = 123;
			randomMyList.QuickSort();
			Assert.AreEqual(expected, randomMyList[randomMyList.Count - 1]);
		}

		[TestMethod]
		public void QuickSort_Integer_ReturnsSortedList()
		{
			MyList<int> randomMyList = new MyList<int>() { 123, 8, 7, 13, 5, 28, 100 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			randomMyList.QuickSort();
			CollectionAssert.AreEqual(expectedSortedMyList.ToArray(), randomMyList.ToArray());
		}

		[TestMethod]
		public void QuickSort_Integer_AlreadySorted_RemainsSorted()
		{
			MyList<int> alreadySortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };
			MyList<int> expectedSortedMyList = new MyList<int>() { 5, 7, 8, 13, 28, 100, 123 };

			alreadySortedMyList.QuickSort();
			CollectionAssert.AreEqual(expectedSortedMyList.ToArray(), alreadySortedMyList.ToArray());
		}

		[TestMethod]
		public void QuickSort_Integer_EmptyList_ReturnsEmptyList()
		{
			MyList<int> emptyMyList = new MyList<int>();

			emptyMyList.QuickSort();

			Assert.AreEqual(0, emptyMyList.Count);
		}

		[TestMethod]
		public void QuickSort_Integer_OneElementList_ReturnsOneElementList()
		{
			MyList<int> oneElementMyList = new MyList<int>() { 27 };
			MyList<int> expectedOneElementMyList = new MyList<int>() { 27 };

			oneElementMyList.QuickSort();

			CollectionAssert.AreEqual(expectedOneElementMyList.ToArray(), oneElementMyList.ToArray());
		}

		[TestMethod]
		public void QucikSort_Integer_SameElementsList_ReturnsSortedSameElementsList()
		{
			MyList<int> sameElementsMyList = new MyList<int>() { 10, 64, 29, 10, 30, 57 };
			MyList<int> expectedSameElementsMyList = new MyList<int>() { 10, 10, 29, 30, 57, 64 };

			sameElementsMyList.QuickSort();

			CollectionAssert.AreEqual(sameElementsMyList.ToArray(), expectedSameElementsMyList.ToArray());
		}
	}
}
