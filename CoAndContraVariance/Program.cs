using CoAndContraVariance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContraVariance
{
    public class Base {
        public int Id { get; set; }
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

    class MyClass
    {
        
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
            

            Func<Base, string, Derived> fpx = 
                (basetype, astring) => {

                    Console.WriteLine(basetype.Name);
                    return new Derived();
                };

            Func<Base, string, Base> fp5 = fpx;

            Base responseDerived  = fp5(new Base() { Name = "I am a base class instance" }, "some text");


            //MyFunc<Base, string, Base> fp4 = fp3; //No way. Uncomment the out in MyFunc and that line compiles too.

            UseMyFunc(F2, new Base(), " base message");
            UseMyFunc(F2, new Derived(), " derived message");

            var derived = new Genericclass<Derived>(new Derived("derived"));
            IMyInterface<Derived> iDerived = derived;

            IMyInterface<Base> iBase = derived;



            /*
                ContraVariance: Assign a less derived typed generic to a more typed generic.
                Think that way: You can pass a more derived type like the Derived to a less
                derived type like the base in form of an in Type parameter.

                   
            **/
            Action<Base> someBasicAction = (basetype) => Console.WriteLine(basetype.Name);
            Action<Derived> someDerivedAction = someBasicAction;

            someDerivedAction(new Derived() { Name = "I am a decendent" });


            /*
                Nope: MyClass is not a more derived type from the type specified in the
                closed generic Action<Base>.

                MyClass is not a subclass of Base. I think you agree.
                Therefore it is not possible with generics , too.
            **/
            //Action<MyClass> myClassAction = someDerivedAction;


            /*
             Covariance: Assign a more derived type to the less the derived one.
            **/

            Dictionary<Type, Func<IReadOnlyRepository<Base>>> factory = new Dictionary<Type, Func<IReadOnlyRepository<Base>>>
            {
                { typeof(Base), () => new BaseRepository() },
                { typeof(Derived), () => new DerivedRepository() }
            };

            // Covariance
            IReadOnlyRepository<Base> repository = factory[typeof(Derived)]();
            Base item = repository.FindById(1);


            // COntravariance
            IWriteOnlyRepository<Derived> derivedInstance = repository as IRepository<Derived>;
            derivedInstance.Add(new Derived { Name = "a new derived instance " });


            Console.ReadKey();
        }
    }
}
