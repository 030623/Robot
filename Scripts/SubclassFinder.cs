using System;
using System.Collections.Generic;
using System.Reflection;

public static class SubclassFinder
{
	public static List<Type> GetSubclasses<T>()
	{
		List<Type> list = new List<Type>();
		Type typeFromHandle = typeof(T);
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type[] types = assemblies[i].GetTypes();
			foreach (Type type in types)
			{
				if (type.IsSubclassOf(typeFromHandle) || type == typeFromHandle)
				{
					list.Add(type);
				}
			}
		}
		return list;
	}
}
