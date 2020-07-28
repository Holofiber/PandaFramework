using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FolderWatcherServerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Type t = typeof(Library.IBaseMessage);
            Type z = typeof(FolderWatcherClient.Requests.SubscribeRequest);

            Assembly SampleAssembly;
            SampleAssembly = Assembly.GetAssembly(t);
            //Assembly.Load()
            //Assert.IsNotNull(SampleAssembly);
            List<Type> typeList = new List<Type>();
            var notSystemAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.StartsWith("System"))
                .Where(x => !x.FullName.StartsWith("Microsoft"));

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                var typeArray = a.GetTypes();
                foreach (Type ta in typeArray)
                {

                    if (t.IsAssignableFrom(ta) && ta.IsClass)
                        typeList.Add(ta);
                }
            }




            Console.WriteLine("end");

            //var a = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies

            /* string str = string.Empty;

             AssemblyName[] assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
             foreach (AssemblyName item in assemblies)
             {
                 str += item.Name + Environment.NewLine;
             }

             if (str != null) ;
             Console.WriteLine(str);*/
        }
    }
}
