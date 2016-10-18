using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak
{
        public class Program
    { 
        public static void Main(string[] args)
        {
            IntegerList list = new IntegerList();
            ListExample(list);
        }
        public static void ListExample(IIntegerList listOfIntegers)
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
        public interface IIntegerList
    {
        /// <summary >
        /// Adds an item to the collection .
        /// </ summary >
        void Add(int item);
        /// <summary >
        /// Removes the first occurrence of an item from the collection .
        /// If the item was not found , method does nothing .
        /// </ summary >
        bool Remove(int item);
        /// <summary >
        /// Removes the item at the given index in the collection .
        /// </ summary >
        bool RemoveAt(int index);
        /// <summary >
        /// Returns the item at the given index in the collection .
        /// </ summary >
        int GetElement(int index);
        /// <summary >
        /// Returns the index of the item in the collection .
        /// If item is not found in the collection , method returns -1.
        /// </ summary >
        int IndexOf(int item);
        /// <summary >
        /// Readonly property . Gets the number of items contained in the collection.
        /// </ summary >
        int Count { get; }
        /// <summary >
        /// Removes all items from the collection .
        /// </ summary >
        void Clear();
        /// <summary >
        /// Determines whether the collection contains a specific value .
        /// </ summary >
        bool Contains(int item);
    }
        class IntegerList : IIntegerList
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
        else if (initialSize > 0)
        {
            this._internalStorage = new int[initialSize];
        }
        else
        {
            this._internalStorage = new int[4];
        }
    }
        // ... IIntegerList implementation ...
        public void Add(int item)
        {
            if (_internalStorage[_internalStorage.Length - 1]!= 0)
                {
                List<int> list = new List<int>();
                for(int i=0;i<_internalStorage.Length;i++)
                {
                    list.Add(_internalStorage[i]);
                }
                int l = list.Count;
                _internalStorage = new int[_internalStorage.Length * 2];
                for (int j = 0; j < l; j++)
                {
                    _internalStorage[j] = list.ElementAt(j);
                }
                _internalStorage[l] = item;
            }
            else 
            {
                foreach(int i in _internalStorage)
                {
                    if (_internalStorage[i] == 0)
                    {
                        _internalStorage[i] = item;
                        break;
                    }
                }
            }
        }
            
    public bool RemoveAt(int item)
    {
            if (item<0 || item >= _internalStorage.Length || _internalStorage[item]==0)
            {
                return false;
            }
            else
            {
                _internalStorage[item] = 0;
                Array.ConstrainedCopy(_internalStorage, item + 1, _internalStorage, item,_internalStorage.Length-(item+1));
                return true;
            }
    }
    public bool Remove(int item)
    {
        int position = Array.IndexOf(_internalStorage, item);
            if (position > 0 && position < _internalStorage.Length)
            {
                return RemoveAt(position);
            }
            return false;
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
    public int Count
        {
            get
            {
                int i = 0;
                while (i != _internalStorage.Length)
                {
                    if (_internalStorage[i] != 0)
                    {
                        i++;
                    }
                    else
                    {
                        return i;
                    }
                }
                return _internalStorage.Length;
            }
        }   
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

}
}
