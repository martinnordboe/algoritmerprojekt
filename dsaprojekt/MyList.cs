using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace dsaprojekt
{
    /// <summary>
    /// Min egen generiske liste, som er et krav i opgaven.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyList<T> : IEnumerable<T>
    {
        private T[] items;
        private int count;
        private IComparer<T> comparer = Comparer<T>.Default;
		public int comparisonCount = 0;
		public double elapsedNanoseconds = 0.0D;
		public double elapsedMilliseconds = 0.0D;


        // MUST HAVE - Er den del af opgaven.
        // Er løst
        public int Count { get { return count; } }


        // Hvis ingen int bliver sat, så sættes størrelsen automatisk som 4.
        public MyList(int initalCapacity = 4)
        {
            if(initalCapacity < 1)
            {
                this.items = new T[1];
            }
            else
            {
                this.items = new T[initalCapacity];
            }
			count = 0;
            //comparer = Comparer<T>.Default;
        }

		// MUST HAVE - Er den del af opgaven.
		// Er løst
		/// <summary>
		/// Tilføjer elementet til kollektion, efter det har sikret at der er plads.
		/// </summary>
		/// <param name="item">Det element der skal tilføjes MyList collection</param>
		public void Add(T item)
        {
            EnsureCapacity(count + 1);
            this.items[count] = item;
            count++;
        }

		/// <summary>
		/// Returnerer en enumerator der kan iterere gennem listen.
		/// Generiske version kaldes når man bruger foreach på MyList.
		/// </summary>
		/// <returns>Returnerer en IEnumerator. Kan bruges til at gennemløbe listen.</returns>
		public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < count; i++)
            {
                yield return this.items[i];
            }
        }

		/// <summary>
		/// Implementering af den ikke-generiske IEnumerable.GetEnumerator(), som det kræves af interfacen.
		/// Denne kaldes kun når listen castes til IEnumerable (uden generics).
		/// Bruger den generiske GetEnumerator().
		/// </summary>
		/// <returns>Returnerer en IEnumerator. Kan bruges til at gennemløbe listen.</returns>
		IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator(); 
        }

		// MUST HAVE - er en del af opgaven.
		// Er løst
		/// <summary>
		/// Indexer property der giver array-lignende adgang til elementerne i listen.
		/// Tillader både læsning (get) og skrivning (set) af elementer på en bestemt position.
		/// </summary>
		/// <param name="index">Positionen af elementet der skal tilgås. Starter i 0.</param>
		/// <returns>Returnerer elementet på den angivne position.</returns>
		/// <exception cref="IndexOutOfRangeException">Kastes hvis index er negativ eller større end eller lig med Count.</exception>
		public T this[int index]
		{
			get 
            {
				if(index < 0 || index >= count)
                {
					throw new IndexOutOfRangeException();
                }
				return items[index];
			}
			set
			{
				if(index < 0 || index >= count)
                {
					throw new IndexOutOfRangeException();
                }
				items[index] = value;
			}
		}

		/// <summary>
		/// Fjerner elementet på den angivne position og flytter alle efterfølgende elementer en plads til venstre.
		/// </summary>
		/// <param name="index">Positionen af elementet der skal fjernes. Starter i 0.</param>
		/// <exception cref="IndexOutOfRangeException">Kastes hvis index er negativ eller større end eller lig med Count.</exception>
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= count)
			{
				throw new IndexOutOfRangeException();
			}

			// Flyt alle elementer efter index en plads til venstre
			int moveCount = count - index - 1;
			if (moveCount > 0)
			{
				Array.Copy(this.items, index + 1, this.items, index, moveCount);
			}

			// Sæt sidste element til default for at undgå memory leak
			this.items[count - 1] = default(T);
			count--;
		}

		/// <summary>
		/// Fjerner det første ting på listen, der matcher det angivne element fra listen.
		/// Bruger standard equality comparer til at sammenligne elementer.
		/// </summary>
		/// <param name="item">Elementet der skal fjernes.</param>
		/// <returns>True hvis elementet blev fundet og fjernet, ellers false.</returns>
		public bool Remove(T item)
		{
			// Skal have loopet alle items i arrayet igennem, finde positionen på itemet og kalde RemoveAt med index positionen
			// Find index for itemet
			int index = -1;
			for (int i = 0; i < count; i++)
			{
				// Sammenligner elementetet i arrayet "items" med argumentet - om det er ens / equal.
				if (EqualityComparer<T>.Default.Equals(this.items[i], item))
				{
					// Hvis det findes, så sæt index til at være det rigtige index
					index = i;
					break;
				}
			}

			// Hvis elementet ikke blev fundet, returner false
			if (index == -1)
			{
				return false;
			}

			// Fjern itemet på den fundne position
			RemoveAt(index);
			return true;
		}

		/// <summary>
		/// Sætter alle pladser i arrayet til at være default værdi, og sætter count til 0 igen.
		/// </summary>
		public void Clear()
		{
			Array.Clear(this.items, 0, count);
			count = 0;
		}

        /// <summary>
        /// Funktionen tager imod et integer argument med pladsen der er brug for.
        /// Hvis der ikke er plads i arrayet "items", så kaldes funktionen "Resize", 
        /// som laver et nyt array og overfører data fra eksisterende til det nye.
        /// </summary>
        /// <param name="capacity"></param>
		private void EnsureCapacity(int capacity)
        {
            if(this.items.Length >= capacity)
            {
                return;
            }
            Resize();
        }

        /// <summary>
        /// Fordobler pladsen i arrayet "items", ved at lave et nyt array og kopiere eksisterende data over i det nye. 
        /// Derefter sættes det nye array som værende "items".
        /// </summary>
        private void Resize()
        {
            // At fordoble kan måske godt være lidt voldsomt - jeg bør overveje om der skal laves en smartere måde.
			T[] newArray = new T[this.items.Length * 2];
			Array.Copy(this.items, newArray, this.items.Length);
			this.items = newArray;
		}


		/// <summary>
		/// Lavet ud fra pseudo kode i undervisningsmaterialet.
		/// </summary>
		public void InsertionSort()
		{
			Stopwatch sw = Stopwatch.StartNew();
			comparisonCount = 0;

			for(int i = 0; i < count; i++)
			{
				T value = this.items[i];
				int pointer = i;

				while (pointer > 0)
				{
					comparisonCount++;
					if(comparer.Compare(value, items[pointer - 1]) < 0)
					{
						items[pointer] = items[pointer - 1];
						pointer--;
					}
					else
					{
						break;
					}
				}
				items[pointer] = value;
			}
			sw.Stop();
			elapsedMilliseconds = sw.Elapsed.TotalMilliseconds;
			elapsedNanoseconds = sw.Elapsed.TotalNanoseconds;
		}

		/// <summary>
		/// Lavet ud fra pseudo kode i undervisningsmaterialet.
		/// </summary>
		public void BubbleSort()
		{
			Stopwatch sw = Stopwatch.StartNew();
			comparisonCount = 0;

			bool swapped = true;
			while(swapped == true) 
			{
				swapped = false;
				for(int i = 1; i < count; i++)
				{
					comparisonCount++;
					if(comparer.Compare(items[i], items[i - 1]) < 0)
					{
						T temporary = items[i];
						items[i] = items[i - 1];
						items[i - 1] = temporary;
						swapped = true;
					}
				}
			}
			sw.Stop();
			elapsedMilliseconds = sw.Elapsed.TotalMilliseconds;
			elapsedNanoseconds = sw.Elapsed.TotalNanoseconds;
		}

		/// <summary>
		/// Lavet ud fra pseudo koden i undervisningsmaterialet. Opdelt i to funktioner, da den ene skal kunne kalde sig selv og pass en MyList som argument, imens denne
		/// Skal fungere ved at metoden bliver kaldt på instansen.
		/// </summary>
		public void QuickSort()
		{
			Stopwatch sw = Stopwatch.StartNew();
			comparisonCount = 0;

			MyList<T> sorted = QuickSort(this);
			for(int i = 0; i < count; i++)
			{
				items[i] = sorted[i];
			}
			sw.Stop();
			elapsedMilliseconds = sw.Elapsed.TotalMilliseconds;
			elapsedNanoseconds = sw.Elapsed.TotalNanoseconds;
		} 

		// Den rekursive Quick Sort funktion. Private så den ikke kaldes ude fra.
		// Bør kigge mere ind i in-place QuickSort i stedet for det her. Det her virker ikke særligt effektivt, da der oprettes utallige nye collections.
		private MyList<T> QuickSort(MyList<T> collection)
		{
			if (collection is null)
			{
				throw new Exception("Fy, det er ikke så godt, collection er null");
			}
			// Vigtigt, ellers så fejler alt xD den bliver aldrig færdig og giver out of range indexes, hvilket må være 0. Derfor aldrig lad den komme ned på 0.
			if(collection.Count <= 1)
			{
				return collection;
			}

			T pivot = collection[0];

			MyList<T> before = new MyList<T>(collection.Count);
			MyList<T> after = new MyList<T>(collection.Count);

			for(int i = 1; i < collection.Count;i++)
			{
				T value = collection[i];
				comparisonCount++;
				if(comparer.Compare(value, pivot) < 0)
				{
					before.Add(value);
				}
				else
				{
					after.Add(value);
				}
			}

			MyList<T> sortedBefore = QuickSort(before);
			MyList<T> sortedAfter = QuickSort(after);

			MyList<T> result = new MyList<T>(collection.Count);
			for(int i = 0; i < sortedBefore.Count; i++)
			{
				result.Add(sortedBefore[i]);
			}

			result.Add(pivot);

			for(int i = 0; i < sortedAfter.Count; i++)
			{
				result.Add(sortedAfter[i]);
			}

			return result;
		}





















		
		public void QuickSortMiddlePivot()
		{
			Stopwatch sw = Stopwatch.StartNew();
			comparisonCount = 0;

			MyList<T> sorted = QuickSortMiddlePivot(this);
			for (int i = 0; i < count; i++)
			{
				items[i] = sorted[i];
			}
			sw.Stop();
			elapsedMilliseconds = sw.Elapsed.TotalMilliseconds;
			elapsedNanoseconds = sw.Elapsed.TotalNanoseconds;
		}

		/// <summary>
		/// Quick Sort metode, hvor pivot er skiftet til midten af MyList.
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		private MyList<T> QuickSortMiddlePivot(MyList<T> collection)
		{
			if (collection is null)
			{
				throw new Exception();
			}
			// Vigtigt, ellers så fejler alt xD den bliver aldrig færdig og giver out of range indexes, hvilket må være 0. Derfor aldrig lad den komme ned på 0.
			if (collection.Count <= 1)
			{
				return collection;
			}

			// Halvdelen af listen
			int half = collection.Count / 2;

			// Pivot sættes til halvdelen
			T pivot = collection[half];

			MyList<T> before = new MyList<T>(collection.Count);
			MyList<T> after = new MyList<T>(collection.Count);

			// Da vi ikke længere sætter pivot til starten, sættes loopet til at starte ved 0
			for (int i = 0; i < collection.Count; i++)
			{
				// Undgå pivot - ellers er resten det samme
				if(i == half)
				{
					continue;
				}

				T value = collection[i];
				comparisonCount++;
				if (comparer.Compare(value, pivot) < 0)
				{
					before.Add(value);
				}
				else
				{
					after.Add(value);
				}
			}

			MyList<T> sortedBefore = QuickSortMiddlePivot(before);
			MyList<T> sortedAfter = QuickSortMiddlePivot(after);

			MyList<T> result = new MyList<T>(collection.Count);
			for (int i = 0; i < sortedBefore.Count; i++)
			{
				result.Add(sortedBefore[i]);
			}

			result.Add(pivot);

			for (int i = 0; i < sortedAfter.Count; i++)
			{
				result.Add(sortedAfter[i]);
			}

			return result;
		}

	}
}
