using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadatak3
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<string> stringList = new GenericList<string>();
            stringList.Add(" Hello ");
            stringList.Add(" World ");
            stringList.Add("!");

            
              foreach ( string value in stringList )
                {
                    Console . WriteLine ( value ) ;
                }
            
            /*
            // foreach without the syntax sugar
            IEnumerator<string> enumerator = stringList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string value =(string)enumerator.Current;
                Console.WriteLine(value);
            }
            */
            
        }
    }
}
