using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text.RegularExpressions.Syntax;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents an immutable regular expression.</summary>
	// Token: 0x0200048A RID: 1162
	[Serializable]
	public class Regex : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class.</summary>
		// Token: 0x0600299E RID: 10654 RVA: 0x0008B22C File Offset: 0x0008942C
		protected Regex()
		{
		}

		/// <summary>Initializes and compiles a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression.</summary>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		// Token: 0x0600299F RID: 10655 RVA: 0x0008B234 File Offset: 0x00089434
		public Regex(string pattern)
			: this(pattern, RegexOptions.None)
		{
		}

		/// <summary>Initializes and compiles a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class for the specified regular expression, with options that modify the pattern.</summary>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise OR combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> contains an invalid flag.</exception>
		// Token: 0x060029A0 RID: 10656 RVA: 0x0008B240 File Offset: 0x00089440
		public Regex(string pattern, RegexOptions options)
		{
			if (pattern == null)
			{
				throw new ArgumentNullException("pattern");
			}
			Regex.validate_options(options);
			this.pattern = pattern;
			this.roptions = options;
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.Regex" /> class using serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains a serialized pattern and <see cref="T:System.Text.RegularExpressions.RegexOptions" />  information.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">The pattern that <paramref name="info" /> contains is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="info" /> contains an invalid <see cref="T:System.Text.RegularExpressions.RegexOptions" />  flag.</exception>
		// Token: 0x060029A1 RID: 10657 RVA: 0x0008B274 File Offset: 0x00089474
		protected Regex(SerializationInfo info, StreamingContext context)
			: this(info.GetString("pattern"), (RegexOptions)((int)info.GetValue("options", typeof(RegexOptions))))
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data necessary to deserialize the current <see cref="T:System.Text.RegularExpressions.Regex" /> object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with serialization information.</param>
		/// <param name="context">The place to store and retrieve serialized data. Reserved for future use.</param>
		// Token: 0x060029A3 RID: 10659 RVA: 0x0008B2CC File Offset: 0x000894CC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("pattern", this.ToString(), typeof(string));
			info.AddValue("options", this.Options, typeof(RegexOptions));
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named file.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile. </param>
		/// <param name="assemblyname">The file name of the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.-or-The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029A4 RID: 10660 RVA: 0x0008B314 File Offset: 0x00089514
		[global::System.MonoTODO]
		public static void CompileToAssembly(RegexCompilationInfo[] regexes, AssemblyName aname)
		{
			Regex.CompileToAssembly(regexes, aname, new CustomAttributeBuilder[0], null);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects to a named file with specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile. </param>
		/// <param name="assemblyname">The file name of the assembly. </param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.-or-The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029A5 RID: 10661 RVA: 0x0008B324 File Offset: 0x00089524
		[global::System.MonoTODO]
		public static void CompileToAssembly(RegexCompilationInfo[] regexes, AssemblyName aname, CustomAttributeBuilder[] attribs)
		{
			Regex.CompileToAssembly(regexes, aname, attribs, null);
		}

		/// <summary>Compiles one or more specified <see cref="T:System.Text.RegularExpressions.Regex" /> objects and a specified resource file to a named assembly with specified attributes.</summary>
		/// <param name="regexinfos">An array that describes the regular expressions to compile. </param>
		/// <param name="assemblyname">The file name of the assembly. </param>
		/// <param name="attributes">An array that defines the attributes to apply to the assembly. </param>
		/// <param name="resourceFile">The name of the Win32 resource file to include in the assembly. </param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="assemblyname" /> parameter's <see cref="P:System.Reflection.AssemblyName.Name" /> property is an empty or null string.-or-The regular expression pattern of one or more objects in <paramref name="regexinfos" /> contains invalid syntax.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyname" /> or <paramref name="regexinfos" /> is null. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The <paramref name="resourceFile" /> parameter designates an invalid Win32 resource file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file designated by the <paramref name="resourceFile" /> parameter cannot be found.  </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029A6 RID: 10662 RVA: 0x0008B330 File Offset: 0x00089530
		[global::System.MonoTODO]
		public static void CompileToAssembly(RegexCompilationInfo[] regexes, AssemblyName aname, CustomAttributeBuilder[] attribs, string resourceFile)
		{
			throw new NotImplementedException();
		}

		/// <summary>Escapes a minimal set of characters (\, *, +, ?, |, {, [, (,), ^, $,., #, and white space) by replacing them with their escape codes. This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.</summary>
		/// <returns>A string of characters with metacharacters converted to their escaped form.</returns>
		/// <param name="str">The input string containing the text to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x060029A7 RID: 10663 RVA: 0x0008B338 File Offset: 0x00089538
		public static string Escape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return global::System.Text.RegularExpressions.Syntax.Parser.Escape(str);
		}

		/// <summary>Converts any escaped characters in the input string.</summary>
		/// <returns>A string of characters with any escaped characters converted to their unescaped form.</returns>
		/// <param name="str">The input string containing the text to convert. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="str" /> includes an unrecognized escape sequence.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x060029A8 RID: 10664 RVA: 0x0008B354 File Offset: 0x00089554
		public static string Unescape(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return global::System.Text.RegularExpressions.Syntax.Parser.Unescape(str);
		}

		/// <summary>Indicates whether the regular expression finds a match in the input string using the regular expression specified in the <paramref name="pattern" /> parameter.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029A9 RID: 10665 RVA: 0x0008B370 File Offset: 0x00089570
		public static bool IsMatch(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern, RegexOptions.None);
		}

		/// <summary>Indicates whether the regular expression finds a match in the input string, using the regular expression specified in the <paramref name="pattern" /> parameter and the matching options supplied in the <paramref name="options" /> parameter.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise OR combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Text.RegularExpressions.RegexOptions" />  value.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029AA RID: 10666 RVA: 0x0008B37C File Offset: 0x0008957C
		public static bool IsMatch(string input, string pattern, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.IsMatch(input);
		}

		/// <summary>Searches the specified input string for the first occurrence of the regular expression supplied in the <paramref name="pattern" /> parameter.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null. -or-<paramref name="pattern" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029AB RID: 10667 RVA: 0x0008B398 File Offset: 0x00089598
		public static Match Match(string input, string pattern)
		{
			return Regex.Match(input, pattern, RegexOptions.None);
		}

		/// <summary>Searches the input string for the first occurrence of the regular expression supplied in a <paramref name="pattern" /> parameter, using the matching options supplied in the <paramref name="options" /> parameter.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to be tested for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise OR combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029AC RID: 10668 RVA: 0x0008B3A4 File Offset: 0x000895A4
		public static Match Match(string input, string pattern, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Match(input);
		}

		/// <summary>Searches the specified input string for all occurrences of the regular expression specified in the <paramref name="pattern" /> parameter.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029AD RID: 10669 RVA: 0x0008B3C0 File Offset: 0x000895C0
		public static MatchCollection Matches(string input, string pattern)
		{
			return Regex.Matches(input, pattern, RegexOptions.None);
		}

		/// <summary>Searches the specified input string for all occurrences of a specified regular expression, using the specified matching options.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise combination of the enumeration values that specify options for matching.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029AE RID: 10670 RVA: 0x0008B3CC File Offset: 0x000895CC
		public static MatchCollection Matches(string input, string pattern, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Matches(input);
		}

		/// <summary>Within a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.-or-<paramref name="evaluator" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029AF RID: 10671 RVA: 0x0008B3E8 File Offset: 0x000895E8
		public static string Replace(string input, string pattern, MatchEvaluator evaluator)
		{
			return Regex.Replace(input, pattern, evaluator, RegexOptions.None);
		}

		/// <summary>Within a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. Specified options modify the matching operation.</summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string. </param>
		/// <param name="options">A bitwise OR combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.-or-<paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029B0 RID: 10672 RVA: 0x0008B3F4 File Offset: 0x000895F4
		public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Replace(input, evaluator);
		}

		/// <summary>Within a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.-or-<paramref name="replacement" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029B1 RID: 10673 RVA: 0x0008B414 File Offset: 0x00089614
		public static string Replace(string input, string pattern, string replacement)
		{
			return Regex.Replace(input, pattern, replacement, RegexOptions.None);
		}

		/// <summary>Within a specified input string, replaces all strings that match a specified regular expression with a specified replacement string. Specified options modify the matching operation. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="options">A bitwise OR combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.-or-<paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029B2 RID: 10674 RVA: 0x0008B420 File Offset: 0x00089620
		public static string Replace(string input, string pattern, string replacement, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Replace(input, replacement);
		}

		/// <summary>Splits the input string at the positions defined by a regular expression pattern.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029B3 RID: 10675 RVA: 0x0008B440 File Offset: 0x00089640
		public static string[] Split(string input, string pattern)
		{
			return Regex.Split(input, pattern, RegexOptions.None);
		}

		/// <summary>Splits the input string at the positions defined by a specified regular expression pattern. Specified options modify the matching operation.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <param name="pattern">The regular expression pattern to match. </param>
		/// <param name="options">A bitwise OR combination of the enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">A regular expression parsing error has occurred.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="pattern" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="options" /> is not a valid bitwise combination of <see cref="T:System.Text.RegularExpressions.RegexOptions" /> values.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029B4 RID: 10676 RVA: 0x0008B44C File Offset: 0x0008964C
		public static string[] Split(string input, string pattern, RegexOptions options)
		{
			Regex regex = new Regex(pattern, options);
			return regex.Split(input);
		}

		/// <summary>Gets or sets the maximum number of entries in the current static cache of compiled regular expressions.</summary>
		/// <returns>The maximum number of entries in the static cache.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.</exception>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x0008B468 File Offset: 0x00089668
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x0008B474 File Offset: 0x00089674
		public static int CacheSize
		{
			get
			{
				return Regex.cache.Capacity;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("CacheSize");
				}
				Regex.cache.Capacity = value;
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x0008B494 File Offset: 0x00089694
		private static void validate_options(RegexOptions options)
		{
			if ((options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.RightToLeft | RegexOptions.ECMAScript | RegexOptions.CultureInvariant)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & RegexOptions.ECMAScript) != RegexOptions.None && (options & ~(RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ECMAScript)) != RegexOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0008B4DC File Offset: 0x000896DC
		private void Init()
		{
			this.machineFactory = Regex.cache.Lookup(this.pattern, this.roptions);
			if (this.machineFactory == null)
			{
				this.InitNewRegex();
			}
			else
			{
				this.group_count = this.machineFactory.GroupCount;
				this.gap = this.machineFactory.Gap;
				this.mapping = this.machineFactory.Mapping;
				this.group_names = this.machineFactory.NamesMapping;
			}
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x0008B560 File Offset: 0x00089760
		private void InitNewRegex()
		{
			this.machineFactory = Regex.CreateMachineFactory(this.pattern, this.roptions);
			Regex.cache.Add(this.pattern, this.roptions, this.machineFactory);
			this.group_count = this.machineFactory.GroupCount;
			this.gap = this.machineFactory.Gap;
			this.mapping = this.machineFactory.Mapping;
			this.group_names = this.machineFactory.NamesMapping;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0008B5E4 File Offset: 0x000897E4
		private static IMachineFactory CreateMachineFactory(string pattern, RegexOptions options)
		{
			global::System.Text.RegularExpressions.Syntax.Parser parser = new global::System.Text.RegularExpressions.Syntax.Parser();
			global::System.Text.RegularExpressions.Syntax.RegularExpression regularExpression = parser.ParseRegularExpression(pattern, options);
			ICompiler compiler;
			if (!Regex.old_rx)
			{
				if ((options & RegexOptions.Compiled) != RegexOptions.None)
				{
					compiler = new CILCompiler();
				}
				else
				{
					compiler = new RxCompiler();
				}
			}
			else
			{
				compiler = new PatternCompiler();
			}
			regularExpression.Compile(compiler, (options & RegexOptions.RightToLeft) != RegexOptions.None);
			IMachineFactory machineFactory = compiler.GetMachineFactory();
			Hashtable hashtable = new Hashtable();
			machineFactory.Gap = parser.GetMapping(hashtable);
			machineFactory.Mapping = hashtable;
			machineFactory.NamesMapping = Regex.GetGroupNamesArray(machineFactory.GroupCount, machineFactory.Mapping);
			return machineFactory;
		}

		/// <summary>Returns the options passed into the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>The <paramref name="options" /> parameter that was passed into the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</returns>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x0008B67C File Offset: 0x0008987C
		public RegexOptions Options
		{
			get
			{
				return this.roptions;
			}
		}

		/// <summary>Gets a value indicating whether the regular expression searches from right to left.</summary>
		/// <returns>true if the regular expression searches from right to left; otherwise false.</returns>
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x0008B684 File Offset: 0x00089884
		public bool RightToLeft
		{
			get
			{
				return (this.roptions & RegexOptions.RightToLeft) != RegexOptions.None;
			}
		}

		/// <summary>Returns an array of capturing group names for the regular expression.</summary>
		/// <returns>A string array of group names.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060029BD RID: 10685 RVA: 0x0008B698 File Offset: 0x00089898
		public string[] GetGroupNames()
		{
			string[] array = new string[1 + this.group_count];
			Array.Copy(this.group_names, array, 1 + this.group_count);
			return array;
		}

		/// <summary>Returns an array of capturing group numbers that correspond to group names in an array.</summary>
		/// <returns>An integer array of group numbers.</returns>
		// Token: 0x060029BE RID: 10686 RVA: 0x0008B6C8 File Offset: 0x000898C8
		public int[] GetGroupNumbers()
		{
			int[] array = new int[1 + this.group_count];
			Array.Copy(this.GroupNumbers, array, 1 + this.group_count);
			return array;
		}

		/// <summary>Returns the group name that corresponds to the specified group number.</summary>
		/// <returns>A string that contains the group name associated with the specified group number. If there is no group name that corresponds to <paramref name="i" />, the method returns <see cref="F:System.String.Empty" />.</returns>
		/// <param name="i">The group number to convert to the corresponding group name. </param>
		// Token: 0x060029BF RID: 10687 RVA: 0x0008B6F8 File Offset: 0x000898F8
		public string GroupNameFromNumber(int i)
		{
			i = this.GetGroupIndex(i);
			if (i < 0)
			{
				return string.Empty;
			}
			return this.group_names[i];
		}

		/// <summary>Returns the group number that corresponds to the specified group name.</summary>
		/// <returns>The group number that corresponds to the specified group name.</returns>
		/// <param name="name">The group name to convert to the corresponding group number. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x060029C0 RID: 10688 RVA: 0x0008B718 File Offset: 0x00089918
		public int GroupNumberFromName(string name)
		{
			if (!this.mapping.Contains(name))
			{
				return -1;
			}
			int num = (int)this.mapping[name];
			if (num >= this.gap)
			{
				num = int.Parse(name);
			}
			return num;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x0008B760 File Offset: 0x00089960
		internal int GetGroupIndex(int number)
		{
			if (number < this.gap)
			{
				return number;
			}
			if (this.gap > this.group_count)
			{
				return -1;
			}
			return Array.BinarySearch<int>(this.GroupNumbers, this.gap, this.group_count - this.gap + 1, number);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0008B7B0 File Offset: 0x000899B0
		private int default_startat(string input)
		{
			return (!this.RightToLeft || input == null) ? 0 : input.Length;
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in the input string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C3 RID: 10691 RVA: 0x0008B7D0 File Offset: 0x000899D0
		public bool IsMatch(string input)
		{
			return this.IsMatch(input, this.default_startat(input));
		}

		/// <summary>Indicates whether the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor finds a match in the input string beginning at the specified starting position in the string.</summary>
		/// <returns>true if the regular expression finds a match; otherwise, false.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The character position at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> cannot be less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C4 RID: 10692 RVA: 0x0008B7E0 File Offset: 0x000899E0
		public bool IsMatch(string input, int startat)
		{
			return this.Match(input, startat).Success;
		}

		/// <summary>Searches the specified input string for the first occurrence of the regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C5 RID: 10693 RVA: 0x0008B7F0 File Offset: 0x000899F0
		public Match Match(string input)
		{
			return this.Match(input, this.default_startat(input));
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The zero-based character position at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C6 RID: 10694 RVA: 0x0008B800 File Offset: 0x00089A00
		public Match Match(string input, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			return this.CreateMachine().Scan(this, input, startat, input.Length);
		}

		/// <summary>Searches the input string for the first occurrence of a regular expression, beginning at the specified starting position and searching only the specified number of characters.</summary>
		/// <returns>An object that contains information about the match.</returns>
		/// <param name="input">The string to be tested for a match. </param>
		/// <param name="beginning">The zero-based character position in the input string at which to begin the search. </param>
		/// <param name="length">The number of characters in the substring to include in the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="beginning" /> is less than zero or greater than the length of <paramref name="input" />.-or-<paramref name="length" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C7 RID: 10695 RVA: 0x0008B850 File Offset: 0x00089A50
		public Match Match(string input, int startat, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			if (length < 0 || length > input.Length - startat)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			return this.CreateMachine().Scan(this, input, startat, startat + length);
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C8 RID: 10696 RVA: 0x0008B8C0 File Offset: 0x00089AC0
		public MatchCollection Matches(string input)
		{
			return this.Matches(input, this.default_startat(input));
		}

		/// <summary>Searches the specified input string for all occurrences of a regular expression, beginning at the specified starting position in the string.</summary>
		/// <returns>A collection of the <see cref="T:System.Text.RegularExpressions.Match" /> objects found by the search. If no matches are found, the method returns an empty collection object.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="startat">The character position in the input string at which to start the search. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029C9 RID: 10697 RVA: 0x0008B8D0 File Offset: 0x00089AD0
		public MatchCollection Matches(string input, int startat)
		{
			Match match = this.Match(input, startat);
			return new MatchCollection(match);
		}

		/// <summary>Within a specified input string, replaces all strings that match a specified regular expression with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="evaluator" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029CA RID: 10698 RVA: 0x0008B8EC File Offset: 0x00089AEC
		public string Replace(string input, MatchEvaluator evaluator)
		{
			return this.Replace(input, evaluator, int.MaxValue, this.default_startat(input));
		}

		/// <summary>Within a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="evaluator" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029CB RID: 10699 RVA: 0x0008B904 File Offset: 0x00089B04
		public string Replace(string input, MatchEvaluator evaluator, int count)
		{
			return this.Replace(input, evaluator, count, this.default_startat(input));
		}

		/// <summary>Within a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a string returned by a <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="evaluator">A custom method that examines each match and returns either the original matched string or a replacement string.</param>
		/// <param name="count">The maximum number of times the replacement will occur. </param>
		/// <param name="startat">The character position in the input string where the search begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="evaluator" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029CC RID: 10700 RVA: 0x0008B918 File Offset: 0x00089B18
		public string Replace(string input, MatchEvaluator evaluator, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (evaluator == null)
			{
				throw new ArgumentNullException("evaluator");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			BaseMachine baseMachine = (BaseMachine)this.CreateMachine();
			if (this.RightToLeft)
			{
				return baseMachine.RTLReplace(this, input, evaluator, count, startat);
			}
			Regex.Adapter adapter = new Regex.Adapter(evaluator);
			return baseMachine.LTRReplace(this, input, new BaseMachine.MatchAppendEvaluator(adapter.Evaluate), count, startat);
		}

		/// <summary>Within a specified input string, replaces all strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="replacement" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029CD RID: 10701 RVA: 0x0008B9BC File Offset: 0x00089BBC
		public string Replace(string input, string replacement)
		{
			return this.Replace(input, replacement, int.MaxValue, this.default_startat(input));
		}

		/// <summary>Within a specified input string, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="count">The maximum number of times the replacement can occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="replacement" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029CE RID: 10702 RVA: 0x0008B9D4 File Offset: 0x00089BD4
		public string Replace(string input, string replacement, int count)
		{
			return this.Replace(input, replacement, count, this.default_startat(input));
		}

		/// <summary>Within a specified input substring, replaces a specified maximum number of strings that match a regular expression pattern with a specified replacement string. </summary>
		/// <returns>A new string that is identical to the input string, except that a replacement string takes the place of each matched string.</returns>
		/// <param name="input">The string to search for a match. </param>
		/// <param name="replacement">The replacement string. </param>
		/// <param name="count">Maximum number of times the replacement can occur. </param>
		/// <param name="startat">The character position in the input string where the search begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.-or-<paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029CF RID: 10703 RVA: 0x0008B9E8 File Offset: 0x00089BE8
		public string Replace(string input, string replacement, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			return this.CreateMachine().Replace(this, input, replacement, count, startat);
		}

		/// <summary>Splits the specified input string at the positions defined by a regular expression pattern specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to split. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029D0 RID: 10704 RVA: 0x0008BA5C File Offset: 0x00089C5C
		public string[] Split(string input)
		{
			return this.Split(input, int.MaxValue, this.default_startat(input));
		}

		/// <summary>Splits the specified input string a specified maximum number of times at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to be split. </param>
		/// <param name="count">The maximum number of times the split can occur. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029D1 RID: 10705 RVA: 0x0008BA74 File Offset: 0x00089C74
		public string[] Split(string input, int count)
		{
			return this.Split(input, count, this.default_startat(input));
		}

		/// <summary>Splits the specified input string a specified maximum number of times at the positions defined by a regular expression specified in the <see cref="T:System.Text.RegularExpressions.Regex" /> constructor. The search for the regular expression pattern starts at a specified character position in the input string.</summary>
		/// <returns>An array of strings.</returns>
		/// <param name="input">The string to be split. </param>
		/// <param name="count">The maximum number of times the split can occur. </param>
		/// <param name="startat">The character position in the input string where the search will begin. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startat" /> is less than zero or greater than the length of <paramref name="input" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060029D2 RID: 10706 RVA: 0x0008BA88 File Offset: 0x00089C88
		public string[] Split(string input, int count, int startat)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startat < 0 || startat > input.Length)
			{
				throw new ArgumentOutOfRangeException("startat");
			}
			return this.CreateMachine().Split(this, input, count, startat);
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		/// <exception cref="T:System.NotSupportedException">References have already been initialized. </exception>
		// Token: 0x060029D3 RID: 10707 RVA: 0x0008BAE8 File Offset: 0x00089CE8
		protected void InitializeReferences()
		{
			if (this.refsInitialized)
			{
				throw new NotSupportedException("This operation is only allowed once per object.");
			}
			this.refsInitialized = true;
			this.Init();
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>true if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.Compiled" /> option; otherwise, false.</returns>
		// Token: 0x060029D4 RID: 10708 RVA: 0x0008BB10 File Offset: 0x00089D10
		protected bool UseOptionC()
		{
			return (this.roptions & RegexOptions.Compiled) != RegexOptions.None;
		}

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method.</summary>
		/// <returns>true if the <see cref="P:System.Text.RegularExpressions.Regex.Options" /> property contains the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option; otherwise, false.</returns>
		// Token: 0x060029D5 RID: 10709 RVA: 0x0008BB20 File Offset: 0x00089D20
		protected bool UseOptionR()
		{
			return (this.roptions & RegexOptions.RightToLeft) != RegexOptions.None;
		}

		/// <summary>Returns the regular expression pattern that was passed into the Regex constructor.</summary>
		/// <returns>The <paramref name="pattern" /> parameter that was passed into the Regex constructor.</returns>
		// Token: 0x060029D6 RID: 10710 RVA: 0x0008BB34 File Offset: 0x00089D34
		public override string ToString()
		{
			return this.pattern;
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x0008BB3C File Offset: 0x00089D3C
		internal int GroupCount
		{
			get
			{
				return this.group_count;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x0008BB44 File Offset: 0x00089D44
		internal int Gap
		{
			get
			{
				return this.gap;
			}
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0008BB4C File Offset: 0x00089D4C
		private IMachine CreateMachine()
		{
			return this.machineFactory.NewInstance();
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0008BB5C File Offset: 0x00089D5C
		private static string[] GetGroupNamesArray(int groupCount, IDictionary mapping)
		{
			string[] array = new string[groupCount + 1];
			IDictionaryEnumerator enumerator = mapping.GetEnumerator();
			while (enumerator.MoveNext())
			{
				array[(int)enumerator.Value] = (string)enumerator.Key;
			}
			return array;
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x0008BBA4 File Offset: 0x00089DA4
		private int[] GroupNumbers
		{
			get
			{
				if (this.group_numbers == null)
				{
					this.group_numbers = new int[1 + this.group_count];
					for (int i = 0; i < this.gap; i++)
					{
						this.group_numbers[i] = i;
					}
					for (int j = this.gap; j <= this.group_count; j++)
					{
						this.group_numbers[j] = int.Parse(this.group_names[j]);
					}
					return this.group_numbers;
				}
				return this.group_numbers;
			}
		}

		// Token: 0x04001A0E RID: 6670
		private static FactoryCache cache = new FactoryCache(15);

		// Token: 0x04001A0F RID: 6671
		private static readonly bool old_rx = Environment.GetEnvironmentVariable("MONO_NEW_RX") == null;

		// Token: 0x04001A10 RID: 6672
		private IMachineFactory machineFactory;

		// Token: 0x04001A11 RID: 6673
		private IDictionary mapping;

		// Token: 0x04001A12 RID: 6674
		private int group_count;

		// Token: 0x04001A13 RID: 6675
		private int gap;

		// Token: 0x04001A14 RID: 6676
		private bool refsInitialized;

		// Token: 0x04001A15 RID: 6677
		private string[] group_names;

		// Token: 0x04001A16 RID: 6678
		private int[] group_numbers;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A17 RID: 6679
		protected internal string pattern;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A18 RID: 6680
		protected internal RegexOptions roptions;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A19 RID: 6681
		[global::System.MonoTODO]
		protected internal Hashtable capnames;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A1A RID: 6682
		[global::System.MonoTODO]
		protected internal Hashtable caps;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A1B RID: 6683
		[global::System.MonoTODO]
		protected internal RegexRunnerFactory factory;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A1C RID: 6684
		[global::System.MonoTODO]
		protected internal int capsize;

		/// <summary>Used by a <see cref="T:System.Text.RegularExpressions.Regex" /> object generated by the <see cref="Overload:System.Text.RegularExpressions.Regex.CompileToAssembly" /> method. </summary>
		// Token: 0x04001A1D RID: 6685
		[global::System.MonoTODO]
		protected internal string[] capslist;

		// Token: 0x0200048B RID: 1163
		private class Adapter
		{
			// Token: 0x060029DC RID: 10716 RVA: 0x0008BC30 File Offset: 0x00089E30
			public Adapter(MatchEvaluator ev)
			{
				this.ev = ev;
			}

			// Token: 0x060029DD RID: 10717 RVA: 0x0008BC40 File Offset: 0x00089E40
			public void Evaluate(Match m, StringBuilder sb)
			{
				sb.Append(this.ev(m));
			}

			// Token: 0x04001A1E RID: 6686
			private MatchEvaluator ev;
		}
	}
}
