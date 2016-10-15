using System;
using System.Linq;

namespace zadatak1
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegerList listint = new IntegerList();
            listint.ListExample(          
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

        public class IntegerList : IIntegerList
        {
            // konstanta 
            public readonly int MAX = 4;
            private int[] _internalStorage;

            // Konstruktor bez ulaznih argumenata koji inicijalizira privatan spremnik na veličinu od 4 elementa.
            public IntegerList()
            {
                _internalStorage = new int[MAX];
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
                _internalStorage[_internalStorage.Length] = item;
            }
            public bool removeAt(int item)
            {
                if (item > _internalStorage.Length)
                {
                    return false;
                }
                _internalStorage[item] = 0;
                Array.ConstrainedCopy(_internalStorage, item + 1, _internalStorage, item, _internalStorage.Length - (item + 1));
                return true;
            }
            public bool remove(int item)
            {
                int position = Array.IndexOf(_internalStorage, item);
                return removeAt(position);
            }
            public int getElement(int item)
            {
                if (item > 0 && item < _internalStorage.Length)
                {
                    return _internalStorage[item];
                }
                else
                {
                    throw IndexOutOfRangeException("Index nije u intervalu spremnika !");
                }
            }
            public int IndexOf(int item)
            {
                int index = Array.IndexOf(_internalStorage, item);
                if(index>0 && index<_internalStorage.Length)
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
               for(int x=0;x<_internalStorage.Length;x++)
                {
                    _internalStorage[x] = 0;
                }
            }
            public bool Contains(int item)
            {
              int index=  Array.IndexOf(_internalStorage, item);
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
  

}

