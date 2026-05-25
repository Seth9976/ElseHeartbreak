using System;

namespace System.Data.OleDb
{
	/// <summary>Returns information about literals used in text commands, data values, and database objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F5 RID: 245
	public enum OleDbLiteral
	{
		/// <summary>A binary literal in a text command. Maps to DBLITERAL_BINARY_LITERAL.</summary>
		// Token: 0x0400043F RID: 1087
		Binary_Literal = 1,
		/// <summary>A catalog name in a text command. Maps to DBLITERAL_CATALOG_NAME.</summary>
		// Token: 0x04000440 RID: 1088
		Catalog_Name,
		/// <summary>The character that separates the catalog name from the rest of the identifier in a text command. Maps to DBLITERAL_CATALOG_SEPARATOR.</summary>
		// Token: 0x04000441 RID: 1089
		Catalog_Separator,
		/// <summary>A character literal in a text command. Maps to DBLITERAL_CHAR_LITERAL.</summary>
		// Token: 0x04000442 RID: 1090
		Char_Literal,
		/// <summary>A column alias in a text command. Maps to DBLITERAL_COLUMN_ALIAS.</summary>
		// Token: 0x04000443 RID: 1091
		Column_Alias,
		/// <summary>A column name used in a text command or in a data-definition interface. Maps to DBLITERAL_COLUMN_NAME.</summary>
		// Token: 0x04000444 RID: 1092
		Column_Name,
		/// <summary>A correlation name (table alias) in a text command. Maps to DBLITERAL_CORRELATION_NAME.</summary>
		// Token: 0x04000445 RID: 1093
		Correlation_Name,
		/// <summary>The name of a cube in a schema (or the catalog if the provider does not support schemas).</summary>
		// Token: 0x04000446 RID: 1094
		Cube_Name = 21,
		/// <summary>A cursor name in a text command. Maps to DBLITERAL_CURSOR_NAME.</summary>
		// Token: 0x04000447 RID: 1095
		Cursor_Name = 8,
		/// <summary>The name of the dimension. If a dimension is part of more than one cube, there is one row for each cube/dimension combination.</summary>
		// Token: 0x04000448 RID: 1096
		Dimension_Name = 22,
		/// <summary>The character used in a LIKE clause to escape the character returned for the DBLITERAL_LIKE_PERCENT literal. For example, if a percent sign (%) is used to match zero or more characters and this is a backslash (\), the characters "abc\%%" match all character values that start with "abc%". Some SQL dialects support a clause (the ESCAPE clause) that can be used to override this value. Maps to DBLITERAL_ESCAPE_PERCENT_PREFIX.</summary>
		// Token: 0x04000449 RID: 1097
		Escape_Percent_Prefix = 9,
		/// <summary>The escape character, if any, used to suffix the character returned for the DBLITERAL_LIKE_PERCENT literal. For example, if a percent sign (%) is used to match zero or more characters and percent signs are escaped by enclosing in open and close square brackets, DBLITERAL_ESCAPE_PERCENT_PREFIX is "[", DBLITERAL_ESCAPE_PERCENT_SUFFIX is "]", and the characters "abc[%]%" match all character values that start with "abc%". Providers that do not use a suffix character to escape the DBLITERAL_ESCAPE_PERCENT character do not return this literal value and can set the lt member of the DBLITERAL structure to DBLITERAL_INVALID if requested. Maps to DBLITERAL_ESCAPE_PERCENT_SUFFIX.</summary>
		// Token: 0x0400044A RID: 1098
		Escape_Percent_Suffix = 29,
		/// <summary>The character used in a LIKE clause to escape the character returned for the DBLITERAL_LIKE_UNDERSCORE literal. For example, if an underscore (_) is used to match exactly one character and this is a backslash (\), the characters "abc\_ _" match all character values that are five characters long and start with "abc_". Some SQL dialects support a clause (the ESCAPE clause) that can be used to override this value. Maps to DBLITERAL_ESCAPE_UNDERSCORE_PREFIX.</summary>
		// Token: 0x0400044B RID: 1099
		Escape_Underscore_Prefix = 10,
		/// <summary>The character used in a LIKE clause to escape the character returned for the DBLITERAL_LIKE_UNDERSCORE literal. For example, if an underscore (_) is used to match exactly one character and this is a backslash (\), the characters "abc\_ _" match all character values that are five characters long and start with "abc_". Some SQL dialects support a clause (the ESCAPE clause) that can be used to override this value. Maps to DBLITERAL_ESCAPE_UNDERSCORE_SUFFIX.</summary>
		// Token: 0x0400044C RID: 1100
		Escape_Underscore_Suffix = 30,
		/// <summary>The name of the hierarchy. If the dimension does not contain a hierarchy or has only one hierarchy, the current column contains a null value.</summary>
		// Token: 0x0400044D RID: 1101
		Hierarchy_Name = 23,
		/// <summary>An index name used in a text command or in a data-definition interface. Maps to DBLITERAL_INDEX_NAME.</summary>
		// Token: 0x0400044E RID: 1102
		Index_Name = 11,
		/// <summary>An invalid value. Maps to DBLITERAL_INVALID.</summary>
		// Token: 0x0400044F RID: 1103
		Invalid = 0,
		/// <summary>Name of the cube to which the current level belongs.</summary>
		// Token: 0x04000450 RID: 1104
		Level_Name = 24,
		/// <summary>The character used in a LIKE clause to match zero or more characters. For example, if this is a percent sign (%), the characters "abc%" match all character values that start with "abc". Maps to DBLITERAL_LIKE_PERCENT.</summary>
		// Token: 0x04000451 RID: 1105
		Like_Percent = 12,
		/// <summary>The character used in a LIKE clause to match exactly one character. For example, if this is an underscore (_), the characters "abc_" match all character values that are four characters long and start with "abc". Maps to DBLITERAL_LIKE_UNDERSCORE.</summary>
		// Token: 0x04000452 RID: 1106
		Like_Underscore,
		/// <summary>The name of the member.</summary>
		// Token: 0x04000453 RID: 1107
		Member_Name = 25,
		/// <summary>A procedure name in a text command. Maps to DBLITERAL_PROCEDURE_NAME.</summary>
		// Token: 0x04000454 RID: 1108
		Procedure_Name = 14,
		/// <summary>The name of the property.</summary>
		// Token: 0x04000455 RID: 1109
		Property_Name = 26,
		/// <summary>The character used in a text command as the opening quote for quoting identifiers that contain special characters. Maps to DBLITERAL_QUOTE_PREFIX.</summary>
		// Token: 0x04000456 RID: 1110
		Quote_Prefix = 15,
		/// <summary>The character used in a text command as the closing quote for quoting identifiers that contain special characters. 1.x providers that use the same character as the prefix and suffix may not return this literal value and can set the member of the DBLITERAL structure to DBLITERAL_INVALID if requested. Maps to DBLITERAL_QUOTE_SUFFIX.</summary>
		// Token: 0x04000457 RID: 1111
		Quote_Suffix = 28,
		/// <summary>A schema name in a text command. Maps to DBLITERAL_SCHEMA_NAME.</summary>
		// Token: 0x04000458 RID: 1112
		Schema_Name = 16,
		/// <summary>The character that separates the schema name from the rest of the identifier in a text command. Maps to DBLITERAL_SCHEMA_SEPARATOR.</summary>
		// Token: 0x04000459 RID: 1113
		Schema_Separator = 27,
		/// <summary>A table name used in a text command or in a data-definition interface. Maps to DBLITERAL_TABLE_NAME.</summary>
		// Token: 0x0400045A RID: 1114
		Table_Name = 17,
		/// <summary>A text command, such as an SQL statement. Maps to DBLITERAL_TEXT_COMMAND.</summary>
		// Token: 0x0400045B RID: 1115
		Text_Command,
		/// <summary>A user name in a text command. Maps to DBLITERAL_USER_NAME.</summary>
		// Token: 0x0400045C RID: 1116
		User_Name,
		/// <summary>A view name in a text command. Maps to DBLITERAL_VIEW_NAME.</summary>
		// Token: 0x0400045D RID: 1117
		View_Name
	}
}
