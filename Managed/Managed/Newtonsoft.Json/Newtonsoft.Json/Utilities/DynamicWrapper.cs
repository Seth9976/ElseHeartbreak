using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200009D RID: 157
	internal static class DynamicWrapper
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001AC59 File Offset: 0x00018E59
		private static ModuleBuilder ModuleBuilder
		{
			get
			{
				DynamicWrapper.Init();
				return DynamicWrapper._moduleBuilder;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001AC68 File Offset: 0x00018E68
		private static void Init()
		{
			if (DynamicWrapper._moduleBuilder == null)
			{
				lock (DynamicWrapper._lock)
				{
					if (DynamicWrapper._moduleBuilder == null)
					{
						AssemblyName assemblyName = new AssemblyName("Newtonsoft.Json.Dynamic");
						assemblyName.KeyPair = new StrongNameKeyPair(DynamicWrapper.GetStrongKey());
						AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
						DynamicWrapper._moduleBuilder = assemblyBuilder.DefineDynamicModule("Newtonsoft.Json.DynamicModule", false);
					}
				}
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		private static byte[] GetStrongKey()
		{
			string text = "Newtonsoft.Json.Dynamic.snk";
			byte[] array2;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text))
			{
				if (manifestResourceStream == null)
				{
					throw new MissingManifestResourceException("Should have " + text + " as an embedded resource.");
				}
				int num = (int)manifestResourceStream.Length;
				byte[] array = new byte[num];
				manifestResourceStream.Read(array, 0, num);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001AD58 File Offset: 0x00018F58
		public static Type GetWrapper(Type interfaceType, Type realObjectType)
		{
			Type type = DynamicWrapper._wrapperDictionary.GetType(interfaceType, realObjectType);
			if (type == null)
			{
				lock (DynamicWrapper._lock)
				{
					type = DynamicWrapper._wrapperDictionary.GetType(interfaceType, realObjectType);
					if (type == null)
					{
						type = DynamicWrapper.GenerateWrapperType(interfaceType, realObjectType);
						DynamicWrapper._wrapperDictionary.SetType(interfaceType, realObjectType, type);
					}
				}
			}
			return type;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		public static object GetUnderlyingObject(object wrapper)
		{
			DynamicWrapperBase dynamicWrapperBase = wrapper as DynamicWrapperBase;
			if (dynamicWrapperBase == null)
			{
				throw new ArgumentException("Object is not a wrapper.", "wrapper");
			}
			return dynamicWrapperBase.UnderlyingObject;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		private static Type GenerateWrapperType(Type interfaceType, Type underlyingType)
		{
			TypeBuilder typeBuilder = DynamicWrapper.ModuleBuilder.DefineType("{0}_{1}_Wrapper".FormatWith(CultureInfo.InvariantCulture, new object[] { interfaceType.Name, underlyingType.Name }), TypeAttributes.Sealed, typeof(DynamicWrapperBase), new Type[] { interfaceType });
			WrapperMethodBuilder wrapperMethodBuilder = new WrapperMethodBuilder(underlyingType, typeBuilder);
			foreach (MethodInfo methodInfo in interfaceType.AllMethods())
			{
				wrapperMethodBuilder.Generate(methodInfo);
			}
			return typeBuilder.CreateType();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001AEA4 File Offset: 0x000190A4
		public static T CreateWrapper<T>(object realObject) where T : class
		{
			Type wrapper = DynamicWrapper.GetWrapper(typeof(T), realObject.GetType());
			DynamicWrapperBase dynamicWrapperBase = (DynamicWrapperBase)Activator.CreateInstance(wrapper);
			dynamicWrapperBase.UnderlyingObject = realObject;
			return dynamicWrapperBase as T;
		}

		// Token: 0x04000257 RID: 599
		private static readonly object _lock = new object();

		// Token: 0x04000258 RID: 600
		private static readonly WrapperDictionary _wrapperDictionary = new WrapperDictionary();

		// Token: 0x04000259 RID: 601
		private static ModuleBuilder _moduleBuilder;
	}
}
