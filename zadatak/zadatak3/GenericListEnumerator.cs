using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak3
{
    public class GenericListEnumerator<X> :IEnumerator<X>
    {
        private X _current;
        int position = -1;
        private GenericList<X> lista;
        public GenericListEnumerator(GenericList<X> list)
        {
            lista = list;
            _current = lista.GetElement(0);
        }

        public bool MoveNext()
        {
            position++;
            _current = lista.GetElement(position);
            return (position < lista.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            lista.RemoveAt(position);
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public X Current
        {
            get
            {
                try
                {
                    return _current;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
