using System;
using System.Collections;
using System.Collections.Generic;

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
        private IComparer<T> comparer;

        // MUST HAVE - Er den del af opgaven.
        // Er løst
        public int Count { get { return count; } }

        
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
            comparer = Comparer<T>.Default;
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
	}
}
