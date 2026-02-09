using System;
using System.Collections;
using System.Collections.Generic;

namespace dsaprojekt
{
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
		public void Add(T item)
        {
            EnsureCapacity(count + 1);
            this.items[count] = item;
            count++;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < count; i++)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator(); 
        }




        // TODO: Index.
        // MUST HAVE - er en del af opgaven.
        // indexer property med int. Ligesom med List, så skal jeg nok have lavet en "out of range"
        // Skal både kunne sættes og hentes (get / set).




        private void EnsureCapacity(int capacity)
        {
            if(this.items.Length >= capacity)
            {
                return;
            }
            Resize();
        }

        private void Resize()
        {
			T[] newArray = new T[this.items.Length * 2];
			Array.Copy(this.items, newArray, this.items.Length);
			this.items = newArray;
		}
	}
}
