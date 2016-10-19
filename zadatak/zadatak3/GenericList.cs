using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak3
{
   public class GenericList<X> : IGenericList<X>
    {
        public readonly int MAX = 4;
        private X[] _internalStorage;
        // Konstruktor bez ulaznih argumenata koji inicijalizira privatan spremnik na veličinu od 4 elementa.
        public GenericList()
        {
            this._internalStorage = new X[MAX];
        }
        // Konstruktor koji prima cijeli broj initialSize i kreira privatan spremnik veličine initialSize
        public GenericList(int initialSize)
        {
            if (initialSize < 0)
            {
                // Bacam iznimku ako se unese negativna vrijednost 
                throw new ArgumentException("initialSize has to be greather then zero!");
            }
            else if (initialSize > 0)
            {
                this._internalStorage = new X[initialSize];
            }
            else
            {
                this._internalStorage = new X[4];
            }
        }
        // ... IIntegerList implementation ...
        public void Add(X item)
        {
            if (_internalStorage[_internalStorage.Length - 1] != null)
            {
                List<X> list = new List<X>();
                for (int i = 0; i < _internalStorage.Length; i++)
                {
                    list.Add(_internalStorage[i]);
                }
                int l = list.Count;
                _internalStorage = new X[_internalStorage.Length * 2];
                for (int j = 0; j < l; j++)
                {
                    _internalStorage[j] = list.ElementAt(j);
                }
                _internalStorage[l] = item;
            }
            else
            {
                for (int i = 0; i < _internalStorage.Length; i++)
                {
                    if (_internalStorage[i] == null)
                    {
                        _internalStorage[i] = item;
                        break;
                    }
                }
            }
        }

        public bool RemoveAt(int item)
        {
            if (item < 0 || item >= _internalStorage.Length || _internalStorage[item] == null)
            {
                return false;
            }
            else
            {
                _internalStorage[item] = default(X);
                Array.ConstrainedCopy(_internalStorage, item + 1, _internalStorage, item, _internalStorage.Length - (item + 1));
                return true;
            }
        }
        public bool Remove(X item)
        {
            int position = Array.IndexOf(_internalStorage, item);
            if (position > 0 && position < _internalStorage.Length)
            {
                return RemoveAt(position);
            }
            return false;
        }
        public X GetElement(int item)
        {
            if (item >= 0 && item < _internalStorage.Length)
            {
                return _internalStorage[item];
            }
            else
            {
                throw new IndexOutOfRangeException("Index nije u intervalu spremnika !");
            }
        }
        public int IndexOf(X item)
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
                    if (_internalStorage[i] != null)
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
                _internalStorage[x] = default(X);
            }
        }
        public bool Contains(X item)
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
        // IEnumerable <X> implementation
        public IEnumerator <X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        // ...


    }
}
