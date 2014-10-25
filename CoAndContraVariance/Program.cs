using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance
{
    public class Base { public string Name { get; set; } }
    public class Derived : Base
    {
        public Derived()
        {

        }
        public Derived(string name)
        {
            this.Name = name;
        }
    }

    class Program
    {
        public static Derived MyMethod(Base b, string name)
        {
            b.Name = name;
            return b as Derived ?? new Derived(name);
        }
        
        static void Main(string[] args)
        {
            /*
             * Func<in T, out TResult> f1
             * in T = Base
             * out TResult = Derived
             * */
            Func<Base, string, Derived> f1 = MyMethod;

            // Covariant return type.

            /*
             * Func<in T, out TResult> f2
             * in T = Base
             * out TResult = Base => f1's TResult (Derived) 
             * is more specific than f2's TResult (Base). 
             * 
             * */
            Func<Base, string, Base> f2 = f1; 
            Base b2 = f2(new Derived(), "derived");

            // Contravariant parameter type.

            /*
             * Func<in T, out TResult> f3
             * in T = Derived => f3's T (Derived) is more specific
             * than f1's T (Base)
             * out TResult = Derived 
             * 
             * */
            Func<Derived, string, Derived> f3 = f1;
            Derived d3 = f3(new Derived(), "derived");
        }
    }
}
