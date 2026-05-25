using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving concrete wrapper classes to implement the application settings feature in Window Forms applications.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C7 RID: 455
	public abstract class ApplicationSettingsBase : SettingsBase, global::System.ComponentModel.INotifyPropertyChanged
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class to its default state.</summary>
		// Token: 0x06000FE6 RID: 4070 RVA: 0x00029AB8 File Offset: 0x00027CB8
		protected ApplicationSettingsBase()
		{
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000FE7 RID: 4071 RVA: 0x00029AE4 File Offset: 0x00027CE4
		protected ApplicationSettingsBase(global::System.ComponentModel.IComponent owner)
			: this(owner, string.Empty)
		{
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied settings key.</summary>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00029AF4 File Offset: 0x00027CF4
		protected ApplicationSettingsBase(string settingsKey)
		{
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ApplicationSettingsBase" /> class using the supplied owner component and settings key.</summary>
		/// <param name="owner">The component that will act as the owner of the application settings object.</param>
		/// <param name="settingsKey">A <see cref="T:System.String" /> that uniquely identifies separate instances of the wrapper class.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x06000FE9 RID: 4073 RVA: 0x00029B28 File Offset: 0x00027D28
		protected ApplicationSettingsBase(global::System.ComponentModel.IComponent owner, string settingsKey)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.providerService = (ISettingsProviderService)owner.Site.GetService(typeof(ISettingsProviderService));
			this.settingsKey = settingsKey;
			base.Initialize(this.Context, this.Properties, this.Providers);
		}

		/// <summary>Occurs after the value of an application settings property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000FEA RID: 4074 RVA: 0x00029B88 File Offset: 0x00027D88
		// (remove) Token: 0x06000FEB RID: 4075 RVA: 0x00029BA4 File Offset: 0x00027DA4
		public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		/// <summary>Occurs before the value of an application settings property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000FEC RID: 4076 RVA: 0x00029BC0 File Offset: 0x00027DC0
		// (remove) Token: 0x06000FED RID: 4077 RVA: 0x00029BDC File Offset: 0x00027DDC
		public event SettingChangingEventHandler SettingChanging;

		/// <summary>Occurs after the application settings are retrieved from storage.</summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000FEE RID: 4078 RVA: 0x00029BF8 File Offset: 0x00027DF8
		// (remove) Token: 0x06000FEF RID: 4079 RVA: 0x00029C14 File Offset: 0x00027E14
		public event SettingsLoadedEventHandler SettingsLoaded;

		/// <summary>Occurs before values are saved to the data store.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000FF0 RID: 4080 RVA: 0x00029C30 File Offset: 0x00027E30
		// (remove) Token: 0x06000FF1 RID: 4081 RVA: 0x00029C4C File Offset: 0x00027E4C
		public event SettingsSavingEventHandler SettingsSaving;

		/// <summary>Returns the value of the named settings property for the previous version of the same application.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the specified <see cref="T:System.Configuration.SettingsProperty" /> if found; otherwise, null.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the settings property whose value is to be returned.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">The property does not exist. The property count is zero or the property cannot be found in the data store.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00029C68 File Offset: 0x00027E68
		public object GetPreviousVersion(string propertyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Refreshes the application settings property values from persistent storage.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00029C70 File Offset: 0x00027E70
		public void Reload()
		{
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				IApplicationSettingsProvider applicationSettingsProvider = settingsProvider as IApplicationSettingsProvider;
				if (applicationSettingsProvider != null)
				{
					applicationSettingsProvider.Reset(this.Context);
				}
			}
		}

		/// <summary>Restores the persisted application settings values to their corresponding default properties.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FF4 RID: 4084 RVA: 0x00029CF4 File Offset: 0x00027EF4
		public void Reset()
		{
			this.Reload();
		}

		/// <summary>Stores the current values of the application settings properties.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00029CFC File Offset: 0x00027EFC
		public override void Save()
		{
			this.Context.CurrentSettings = this;
			foreach (object obj in this.Providers)
			{
				SettingsProvider settingsProvider = (SettingsProvider)obj;
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj2 in this.PropertyValues)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj2;
					if (settingsPropertyValue.Property.Provider == settingsProvider)
					{
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				if (settingsPropertyValueCollection.Count > 0)
				{
					settingsProvider.SetPropertyValues(this.Context, settingsPropertyValueCollection);
				}
			}
			this.Context.CurrentSettings = null;
		}

		/// <summary>Updates application settings to reflect a more recent installation of the application.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x00029E14 File Offset: 0x00028014
		public virtual void Upgrade()
		{
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.PropertyChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00029E18 File Offset: 0x00028018
		protected virtual void OnPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingChangingEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00029E34 File Offset: 0x00028034
		protected virtual void OnSettingChanging(object sender, SettingChangingEventArgs e)
		{
			if (this.SettingChanging != null)
			{
				this.SettingChanging(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsLoaded" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Configuration.SettingsLoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FF9 RID: 4089 RVA: 0x00029E50 File Offset: 0x00028050
		protected virtual void OnSettingsLoaded(object sender, SettingsLoadedEventArgs e)
		{
			if (this.SettingsLoaded != null)
			{
				this.SettingsLoaded(sender, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingsSaving" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06000FFA RID: 4090 RVA: 0x00029E6C File Offset: 0x0002806C
		protected virtual void OnSettingsSaving(object sender, global::System.ComponentModel.CancelEventArgs e)
		{
			if (this.SettingsSaving != null)
			{
				this.SettingsSaving(sender, e);
			}
		}

		/// <summary>Gets the application settings context associated with the settings group.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsContext" /> associated with the settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00029E88 File Offset: 0x00028088
		[global::System.ComponentModel.Browsable(false)]
		public override SettingsContext Context
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsContext settingsContext;
				try
				{
					if (this.context == null)
					{
						this.context = new SettingsContext();
						this.context["SettingsKey"] = string.Empty;
						Type type = base.GetType();
						this.context["GroupName"] = type.FullName;
						this.context["SettingsClassType"] = type;
					}
					settingsContext = this.context;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsContext;
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00029F40 File Offset: 0x00028140
		private void CacheValuesByProvider(SettingsProvider provider)
		{
			SettingsPropertyCollection settingsPropertyCollection = new SettingsPropertyCollection();
			foreach (object obj in this.Properties)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (settingsProperty.Provider == provider)
				{
					settingsPropertyCollection.Add(settingsProperty);
				}
			}
			if (settingsPropertyCollection.Count > 0)
			{
				SettingsPropertyValueCollection settingsPropertyValueCollection = provider.GetPropertyValues(this.Context, settingsPropertyCollection);
				this.PropertyValues.Add(settingsPropertyValueCollection);
			}
			this.OnSettingsLoaded(this, new SettingsLoadedEventArgs(provider));
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00029FF8 File Offset: 0x000281F8
		private void InitializeSettings(SettingsPropertyCollection settings)
		{
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00029FFC File Offset: 0x000281FC
		private object GetPropertyValue(string propertyName)
		{
			SettingsProperty settingsProperty = this.Properties[propertyName];
			if (settingsProperty == null)
			{
				throw new SettingsPropertyNotFoundException(propertyName);
			}
			if (this.propertyValues == null)
			{
				this.InitializeSettings(this.Properties);
			}
			if (this.PropertyValues[propertyName] == null)
			{
				this.CacheValuesByProvider(settingsProperty.Provider);
			}
			return this.PropertyValues[propertyName].PropertyValue;
		}

		/// <summary>Gets or sets the value of the specified application settings property.</summary>
		/// <returns>If found, the value of the named settings property; otherwise, null.</returns>
		/// <param name="propertyName">A <see cref="T:System.String" /> containing the name of the property to access.</param>
		/// <exception cref="T:System.Configuration.SettingsPropertyNotFoundException">There are no properties associated with the current wrapper or the specified property could not be found.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyIsReadOnlyException">An attempt was made to set a read-only property.</exception>
		/// <exception cref="T:System.Configuration.SettingsPropertyWrongTypeException">The value supplied is of a type incompatible with the settings property, during a set operation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038B RID: 907
		[global::System.MonoTODO]
		public override object this[string propertyName]
		{
			get
			{
				if (base.IsSynchronized)
				{
					lock (this)
					{
						return this.GetPropertyValue(propertyName);
					}
				}
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				SettingsProperty settingsProperty = this.Properties[propertyName];
				if (settingsProperty == null)
				{
					throw new SettingsPropertyNotFoundException(propertyName);
				}
				if (settingsProperty.IsReadOnly)
				{
					throw new SettingsPropertyIsReadOnlyException(propertyName);
				}
				if (value != null && !settingsProperty.PropertyType.IsAssignableFrom(value.GetType()))
				{
					throw new SettingsPropertyWrongTypeException(propertyName);
				}
				if (this.PropertyValues[propertyName] == null)
				{
					this.CacheValuesByProvider(settingsProperty.Provider);
				}
				SettingChangingEventArgs settingChangingEventArgs = new SettingChangingEventArgs(propertyName, base.GetType().FullName, this.settingsKey, value, false);
				this.OnSettingChanging(this, settingChangingEventArgs);
				if (!settingChangingEventArgs.Cancel)
				{
					this.PropertyValues[propertyName].PropertyValue = value;
					this.OnPropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
				}
			}
		}

		/// <summary>Gets the collection of settings properties in the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing all the <see cref="T:System.Configuration.SettingsProperty" /> objects used in the current wrapper.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The associated settings provider could not be found or its instantiation failed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0002A190 File Offset: 0x00028390
		[global::System.ComponentModel.Browsable(false)]
		public override SettingsPropertyCollection Properties
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyCollection settingsPropertyCollection;
				try
				{
					if (this.properties == null)
					{
						LocalFileSettingsProvider localFileSettingsProvider = null;
						this.properties = new SettingsPropertyCollection();
						foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
						{
							SettingAttribute[] array2 = (SettingAttribute[])propertyInfo.GetCustomAttributes(typeof(SettingAttribute), false);
							if (array2 != null && array2.Length != 0)
							{
								this.CreateSettingsProperty(propertyInfo, this.properties, ref localFileSettingsProvider);
							}
						}
					}
					settingsPropertyCollection = this.properties;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsPropertyCollection;
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002A268 File Offset: 0x00028468
		private void CreateSettingsProperty(PropertyInfo prop, SettingsPropertyCollection properties, ref LocalFileSettingsProvider local_provider)
		{
			SettingsAttributeDictionary settingsAttributeDictionary = new SettingsAttributeDictionary();
			SettingsProvider settingsProvider = null;
			object obj = null;
			SettingsSerializeAs settingsSerializeAs = SettingsSerializeAs.String;
			bool flag = false;
			foreach (Attribute attribute in prop.GetCustomAttributes(false))
			{
				if (attribute is SettingsProviderAttribute)
				{
					Type type = Type.GetType(((SettingsProviderAttribute)attribute).ProviderTypeName);
					settingsProvider = (SettingsProvider)Activator.CreateInstance(type);
					settingsProvider.Initialize(null, null);
				}
				else if (attribute is DefaultSettingValueAttribute)
				{
					obj = ((DefaultSettingValueAttribute)attribute).Value;
				}
				else if (attribute is SettingsSerializeAsAttribute)
				{
					settingsSerializeAs = ((SettingsSerializeAsAttribute)attribute).SerializeAs;
					flag = true;
				}
				else if (attribute is ApplicationScopedSettingAttribute || attribute is UserScopedSettingAttribute)
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
				else
				{
					settingsAttributeDictionary.Add(attribute.GetType(), attribute);
				}
			}
			if (!flag)
			{
				global::System.ComponentModel.TypeConverter converter = global::System.ComponentModel.TypeDescriptor.GetConverter(prop.PropertyType);
				if (converter != null && (!converter.CanConvertFrom(typeof(string)) || !converter.CanConvertTo(typeof(string))))
				{
					settingsSerializeAs = SettingsSerializeAs.Xml;
				}
			}
			SettingsProperty settingsProperty = new SettingsProperty(prop.Name, prop.PropertyType, settingsProvider, false, obj, settingsSerializeAs, settingsAttributeDictionary, false, false);
			if (this.providerService != null)
			{
				settingsProperty.Provider = this.providerService.GetSettingsProvider(settingsProperty);
			}
			if (settingsProvider == null)
			{
				if (local_provider == null)
				{
					local_provider = new LocalFileSettingsProvider();
					local_provider.Initialize(null, null);
				}
				settingsProperty.Provider = local_provider;
				settingsProvider = local_provider;
			}
			if (settingsProvider != null)
			{
				SettingsProvider settingsProvider2 = this.Providers[settingsProvider.Name];
				if (settingsProvider2 != null)
				{
					settingsProperty.Provider = settingsProvider2;
				}
			}
			properties.Add(settingsProperty);
			if (settingsProperty.Provider != null && this.Providers[settingsProperty.Provider.Name] == null)
			{
				this.Providers.Add(settingsProperty.Provider);
			}
		}

		/// <summary>Gets a collection of property values.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> of property values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0002A47C File Offset: 0x0002867C
		[global::System.ComponentModel.Browsable(false)]
		public override SettingsPropertyValueCollection PropertyValues
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsPropertyValueCollection settingsPropertyValueCollection;
				try
				{
					if (this.propertyValues == null)
					{
						this.propertyValues = new SettingsPropertyValueCollection();
					}
					settingsPropertyValueCollection = this.propertyValues;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsPropertyValueCollection;
			}
		}

		/// <summary>Gets the collection of application settings providers used by the wrapper.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProviderCollection" /> containing all the <see cref="T:System.Configuration.SettingsProvider" /> objects used by the settings properties of the current settings wrapper.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0002A4F0 File Offset: 0x000286F0
		[global::System.ComponentModel.Browsable(false)]
		public override SettingsProviderCollection Providers
		{
			get
			{
				if (base.IsSynchronized)
				{
					Monitor.Enter(this);
				}
				SettingsProviderCollection settingsProviderCollection;
				try
				{
					if (this.providers == null)
					{
						this.providers = new SettingsProviderCollection();
					}
					settingsProviderCollection = this.providers;
				}
				finally
				{
					if (base.IsSynchronized)
					{
						Monitor.Exit(this);
					}
				}
				return settingsProviderCollection;
			}
		}

		/// <summary>Gets or sets the settings key for the application settings group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the settings key for the current settings group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0002A564 File Offset: 0x00028764
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x0002A56C File Offset: 0x0002876C
		[global::System.ComponentModel.Browsable(false)]
		public string SettingsKey
		{
			get
			{
				return this.settingsKey;
			}
			set
			{
				this.settingsKey = value;
			}
		}

		// Token: 0x0400046D RID: 1133
		private string settingsKey;

		// Token: 0x0400046E RID: 1134
		private SettingsContext context;

		// Token: 0x0400046F RID: 1135
		private SettingsPropertyCollection properties;

		// Token: 0x04000470 RID: 1136
		private ISettingsProviderService providerService;

		// Token: 0x04000471 RID: 1137
		private SettingsPropertyValueCollection propertyValues;

		// Token: 0x04000472 RID: 1138
		private SettingsProviderCollection providers;
	}
}
