using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalcTest
{
    internal class Program
    {
        static void Result(int a, int b, Type type, object obj, string method)
        {
            object res = type.GetMethod(method).Invoke(obj, new object[] { a, b });
            Console.WriteLine($"Result: {res}");
        }
        static void Main(string[] args)
        {
            Assembly libAsm = Assembly.Load(AssemblyName.GetAssemblyName("CalcLibrary.dll"));
            Console.WriteLine($"Assesmbly loaded:{libAsm.FullName}");

            Module libModule = libAsm.GetModule("CalcLibrary.dll");
            Console.WriteLine($"Module loaded:{libModule.FullyQualifiedName}");

            Type arithmeticType = libModule.GetType("CalcLibrary.ArithmeticOperations");
            Type extendedType = libModule.GetType("CalcLibrary.ExtendedOperations");
            Console.WriteLine($"Types get:{arithmeticType.FullName} - {extendedType.FullName}");


            object aritOperations = Activator.CreateInstance(arithmeticType);
            object extOperations = Activator.CreateInstance(extendedType);

            Result(100, 40, arithmeticType, aritOperations, "Multi");
            Result(200, 40, extendedType, extOperations, "Percent");
        }
    }
}
