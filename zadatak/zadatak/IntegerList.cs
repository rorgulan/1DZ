using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak
{
    class IntegerList:IIntegerList
    {
        // konstanta 
        public readonly int MAX = 4;
        private int[] _internalStorage;

        // Konstruktor bez ulaznih argumenata koji inicijalizira privatan spremnik na veličinu od 4 elementa.
        public IntegerList()
        {
            this._internalStorage = new int[MAX];
        }

        // Konstruktor koji prima cijeli broj initialSize i kreira privatan spremnik veličine initialSize
        public IntegerList(int initialSize)
        {
            if (initialSize < 0)
            {
                // Bacam iznimku ako se unese negativna vrijednost 
                throw new ArgumentException("initialSize has to be greather then zero!");
            }
            this._internalStorage = new int[initialSize];
        }
        // ... IIntegerList implementation ...

        public void Add(int item)
        {
            if (_internalStorage.Length == MAX)
            {
                Array.Resize(_internalStorage, MAX * 2);
            }
            this._internalStorage[this._internalStorage.Length] = item;
        }
        public bool RemoveAt(int item)
        {
            if (item > _internalStorage.Length)
            {
                return false;
            }
            _internalStorage[item] = 0;
            Array.ConstrainedCopy(_internalStorage, item + 1, _internalStorage, item, _internalStorage.Length - (item + 1));
            return true;
        }
        public bool Remove(int item)
        {
            int position = Array.IndexOf(_internalStorage, item);
            return RemoveAt(position);
        }
        public int GetElement(int item)
        {
            if (item > 0 && item < _internalStorage.Length)
            {
                return _internalStorage[item];
            }
            return 0;
            /*
            else
            {
                throw IndexOutOfRangeException("Index nije u intervalu spremnika !");
            }
            */
        }
        public int IndexOf(int item)
        {
            int index = Array.IndexOf(_internalStorage, item);
            if (index > 0 && index < _internalStorage.Length)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }
        public int Count { get; }
        public void Clear()
        {
            for (int x = 0; x < _internalStorage.Length; x++)
            {
                _internalStorage[x] = 0;
            }
        }
        public bool Contains(int item)
        {
            int index = Array.IndexOf(_internalStorage, item);
            if (index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ListExample(IIntegerList listOfIntegers)
        {
            listOfIntegers.Add(1); // [1]
            listOfIntegers.Add(2); // [1 ,2]
            listOfIntegers.Add(3); // [1 ,2 ,3]
            listOfIntegers.Add(4); // [1 ,2 ,3 ,4]
            listOfIntegers.Add(5); // [1 ,2 ,3 ,4 ,5]
            listOfIntegers.RemoveAt(0); // [2 ,3 ,4 ,5]
            listOfIntegers.Remove(5); // [2 ,3 ,4]
            Console.WriteLine(listOfIntegers.Count); // 3
            Console.WriteLine(listOfIntegers.Remove(100)); // false
            Console.WriteLine(listOfIntegers.RemoveAt(5)); // false
            listOfIntegers.Clear(); // []
            Console.WriteLine(listOfIntegers.Count); // 0
        }
    }
}
