using System.Reflection;

namespace Portfolio.Shared.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Get assembly from appdomain or loads it
        /// </summary>
        /// <param name="assemblyPath">the name of </param>
        /// <returns></returns>
        public static Assembly GetOrLoadAssembly(this string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
            {
                throw new ArgumentNullException(nameof(assemblyPath));
            }
            //Get the loaded assembly in the current domain
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            //Parse actually assembly name from path if needed
            var assemblyNameSpace = assemblyPath.Split('\\').LastOrDefault() ?? String.Empty;
            //Remove the .dll extension
            if (assemblyNameSpace.EndsWith(".dll"))
                assemblyNameSpace = assemblyNameSpace[..^".dll".Length];
            //Check the loaded assemblies
            foreach (var item in loadedAssemblies)
            {
                //If found then return the laoded one
                if (string.Compare(item.GetName().Name, assemblyNameSpace, true) == 0)
                    return item;
            }
            //else try to loaded it based on file path
            return Assembly.LoadFrom(assemblyPath);
        }
    }
}
