using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IoC.Attributes;
using IoC.Exceptions;
using IoC.Interfaces;

namespace IoC
{
   public class Container
	{
		private readonly Dictionary<Type, Type> _typesDictionary = new Dictionary<Type, Type>();
		private readonly IActivator _activator = new SimpleActivator();
		public void AddAssembly(Assembly assembly)
		{
			IEnumerable<Type> types = assembly.ExportedTypes;
			foreach (Type type in types)
			{
				ImportConstructorAttribute constructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();                
				if (constructorImportAttribute != null || HasImportProperties(type))
				{
					_typesDictionary.Add(type, type);
				}
				else
				{
					IEnumerable<ExportAttribute> exportAttributes = type.GetCustomAttributes<ExportAttribute>();
					foreach (ExportAttribute exportAttribute in exportAttributes)
					{
						Type key = exportAttribute.Contract;
						if (key == null)
						{
							key = type;
						}

						_typesDictionary.Add(key, type);
					}
				}				
			}
		}

		public void AddType(Type type)
		{
			_typesDictionary.Add(type, type);
		}

		public void AddType(Type type, Type baseType)
		{
			_typesDictionary.Add(baseType, type);
		}

		public object CreateInstance(Type type)
		{
			return ConstructInstanceOfType(type);
		}

		public T CreateInstance<T>()
		{
			Type type = typeof(T);
			return (T)ConstructInstanceOfType(type);
		}

		private object ConstructInstanceOfType(Type type)
		{
			if (!_typesDictionary.ContainsKey(type))
			{
				throw new DependencyException("Dependency is not provided");
			}

			Type dependedType = _typesDictionary[type];
			ConstructorInfo constructorInfo = GetConstructor(dependedType);
			object instance = CreateFromConstructor(dependedType, constructorInfo);

			ResolveProperties(dependedType, instance);
			return instance;
		}

		private ConstructorInfo GetConstructor(Type type)
		{
			ConstructorInfo[] constructors = type.GetConstructors();
			if (constructors.Length == 0)
			{
				throw new DependencyException("There are no public constructors!");
			}

			return constructors.First(); 
		}

		private object CreateFromConstructor(Type type, ConstructorInfo constructorInfo)
		{
			ParameterInfo[] parameters = constructorInfo.GetParameters();
			List<object> parametersInstances = new List<object>(parameters.Length);
			foreach (ParameterInfo parameterInfo in parameters)
			{
				parametersInstances.Add(ConstructInstanceOfType(parameterInfo.ParameterType));
			}

			return _activator.CreateInstance(type, parametersInstances.ToArray());;
		}

		private void ResolveProperties(Type type, object instance)
		{
			IEnumerable<PropertyInfo> propertiesRequiredImport = GetPropertiesRequiredImport(type);                

			foreach (PropertyInfo property in propertiesRequiredImport)
			{
				object resolvedProperty = ConstructInstanceOfType(property.PropertyType);

				property.SetValue(instance, resolvedProperty);
			}
		}

		private bool HasImportProperties(Type type)
		{
			IEnumerable<PropertyInfo> propertiesInfo = GetPropertiesRequiredImport(type);
			return propertiesInfo.Any();
		}

		private IEnumerable<PropertyInfo> GetPropertiesRequiredImport(Type type)
		{
			PropertyInfo[] property = type.GetProperties();
			return property.Where(propertyInfo => propertyInfo.GetCustomAttribute<ImportAttribute>() != null).ToList();
		}
	}
}
