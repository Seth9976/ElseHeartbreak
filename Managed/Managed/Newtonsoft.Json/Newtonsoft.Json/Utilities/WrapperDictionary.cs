using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200009F RID: 159
	internal class WrapperDictionary
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001B0D9 File Offset: 0x000192D9
		private static string GenerateKey(Type interfaceType, Type realObjectType)
		{
			return interfaceType.Name + "_" + realObjectType.Name;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001B0F4 File Offset: 0x000192F4
		public Type GetType(Type interfaceType, Type realObjectType)
		{
			string text = WrapperDictionary.GenerateKey(interfaceType, realObjectType);
			if (this._wrapperTypes.ContainsKey(text))
			{
				return this._wrapperTypes[text];
			}
			return null;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001B128 File Offset: 0x00019328
		public void SetType(Type interfaceType, Type realObjectType, Type wrapperType)
		{
			string text = WrapperDictionary.GenerateKey(interfaceType, realObjectType);
			if (this._wrapperTypes.ContainsKey(text))
			{
				this._wrapperTypes[text] = wrapperType;
				return;
			}
			this._wrapperTypes.Add(text, wrapperType);
		}

		// Token: 0x0400025E RID: 606
		private readonly Dictionary<string, Type> _wrapperTypes = new Dictionary<string, Type>();
	}
}
