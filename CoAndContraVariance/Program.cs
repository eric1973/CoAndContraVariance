using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance
{
    public class Base { 
        public string Name { get; set; }
        public override string ToString()
        {
            return "greetings from Base class: " + this.Name;
        }
    }
    public class Derived : Base
    {
        public Derived()
        {

        }
        public Derived(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return "greetings from Derived class: " + this.Name;
        }
    }

    class Program
    {
        public delegate TResult MyFunc<T1, T2, /*out*/ TResult>(T1 t1, T2 t2);

        public static Derived F2(Base b, string name)
        {
            b.Name = name;
            return b as Derived ?? new Derived(name);
        }


        public static void UseMyFunc(MyFunc<Base, string, Base> func, Base printme, string message)
        {
            Console.WriteLine(func(printme, message).ToString());
        }
        interface IMyInterface<out T>
        {
            T Value { get; }
        }

        class Genericclass<T> : IMyInterface<T>
        {

            public T Value {get; private set; }

            public Genericclass(T value)
            {
                this.Value = value;
            }
        }
        static void Main(string[] args)
        {
            /*
             * Notice the formal func parameter's TResult which is Base!
             * You can pass in methods which return a Base or Derived instance
             * 
             * Advantage: a more general purpose use of method UseFunc
             */
            MyFunc<Base, string, Derived> fp1 = F2;
            MyFunc<Base, string, Base> fp2 = F2;


            MyFunc<Base, string, Derived> fp3 = F2;
            
            //MyFunc<Base, string, Base> fp4 = fp3; No way. Uncomment the out in MyFunc and that line compiles too.

            UseMyFunc(F2, new Base(), " base message");
            UseMyFunc(F2, new Derived(), " derived message");

            var derived = new Genericclass<Derived>(new Derived("derived"));
            IMyInterface<Derived> iDerived = derived;

            IMyInterface<Base> iBase = derived;

            Console.ReadKey();
        }
    }
}
