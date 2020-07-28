﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FolderWatcher.Domain
{
    public static class BaseMessageTypes
    {
        private static List<Type> TypesAssignableFromIBaseMessage = new List<Type>();

        public static List<Type> GetBaseMessageTypes()
        {
            if (TypesAssignableFromIBaseMessage.Any())
                return TypesAssignableFromIBaseMessage;

            Type IBaseMessType = typeof(Library.IBaseMessage);
            Assembly SampleAssembly = Assembly.GetAssembly(IBaseMessType);
           
            var customAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.StartsWith("System"))
                .Where(x => !x.FullName.StartsWith("Microsoft"));

            foreach (Assembly assembly in customAssembly)
            {
                var typeArray = assembly.GetTypes();
                foreach (Type type in typeArray)
                {

                    if (IBaseMessType.IsAssignableFrom(type) && type.IsClass)
                        TypesAssignableFromIBaseMessage.Add(type);
                }
            }

            return TypesAssignableFromIBaseMessage;
        }
    }
}
