using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>The <see cref="T:Microsoft.SqlServer.Server.TriggerAction" /> enumeration is used by the <see cref="T:Microsoft.SqlServer.Server.SqlTriggerContext" /> class to indicate what action fired the trigger. </summary>
	// Token: 0x0200014F RID: 335
	public enum TriggerAction
	{
		/// <summary>An ALTER APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006A2 RID: 1698
		AlterAppRole = 138,
		/// <summary>An ALTER ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x040006A3 RID: 1699
		AlterAssembly = 102,
		/// <summary>An ALTER_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x040006A4 RID: 1700
		AlterBinding = 175,
		/// <summary>An ALTER FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040006A5 RID: 1701
		AlterFunction = 62,
		/// <summary>An ALTER INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x040006A6 RID: 1702
		AlterIndex = 25,
		/// <summary>An ALTER LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x040006A7 RID: 1703
		AlterLogin = 145,
		/// <summary>An ALTER PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040006A8 RID: 1704
		AlterPartitionFunction = 192,
		/// <summary>An ALTER PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x040006A9 RID: 1705
		AlterPartitionScheme = 195,
		/// <summary>An ALTER PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x040006AA RID: 1706
		AlterProcedure = 52,
		/// <summary>An ALTER QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x040006AB RID: 1707
		AlterQueue = 158,
		/// <summary>An ALTER ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006AC RID: 1708
		AlterRole = 135,
		/// <summary>An ALTER ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x040006AD RID: 1709
		AlterRoute = 165,
		/// <summary>An ALTER SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x040006AE RID: 1710
		AlterSchema = 142,
		/// <summary>An ALTER SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x040006AF RID: 1711
		AlterService = 162,
		/// <summary>An ALTER TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006B0 RID: 1712
		AlterTable = 22,
		/// <summary>An ALTER TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x040006B1 RID: 1713
		AlterTrigger = 72,
		/// <summary>An ALTER USER Transact-SQL statement was executed.</summary>
		// Token: 0x040006B2 RID: 1714
		AlterUser = 132,
		/// <summary>An ALTER VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x040006B3 RID: 1715
		AlterView = 42,
		/// <summary>A CREATE APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006B4 RID: 1716
		CreateAppRole = 137,
		/// <summary>A CREATE ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x040006B5 RID: 1717
		CreateAssembly = 101,
		/// <summary>A CREATE_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x040006B6 RID: 1718
		CreateBinding = 174,
		/// <summary>A CREATE CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x040006B7 RID: 1719
		CreateContract = 154,
		/// <summary>A CREATE EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x040006B8 RID: 1720
		CreateEventNotification = 74,
		/// <summary>A CREATE FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040006B9 RID: 1721
		CreateFunction = 61,
		/// <summary>A CREATE INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x040006BA RID: 1722
		CreateIndex = 24,
		/// <summary>A CREATE LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x040006BB RID: 1723
		CreateLogin = 144,
		/// <summary>A CREATE MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040006BC RID: 1724
		CreateMsgType = 151,
		/// <summary>A CREATE PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040006BD RID: 1725
		CreatePartitionFunction = 191,
		/// <summary>A CREATE PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x040006BE RID: 1726
		CreatePartitionScheme = 194,
		/// <summary>A CREATE PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x040006BF RID: 1727
		CreateProcedure = 51,
		/// <summary>A CREATE QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x040006C0 RID: 1728
		CreateQueue = 157,
		/// <summary>A CREATE ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006C1 RID: 1729
		CreateRole = 134,
		/// <summary>A CREATE ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x040006C2 RID: 1730
		CreateRoute = 164,
		/// <summary>A CREATE SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x040006C3 RID: 1731
		CreateSchema = 141,
		/// <summary>Not available.</summary>
		// Token: 0x040006C4 RID: 1732
		CreateSecurityExpression = 31,
		/// <summary>A CREATE SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x040006C5 RID: 1733
		CreateService = 161,
		/// <summary>A CREATE SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x040006C6 RID: 1734
		CreateSynonym = 34,
		/// <summary>A CREATE TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006C7 RID: 1735
		CreateTable = 21,
		/// <summary>A CREATE TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x040006C8 RID: 1736
		CreateTrigger = 71,
		/// <summary>A CREATE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040006C9 RID: 1737
		CreateType = 91,
		/// <summary>A CREATE USER Transact-SQL statement was executed.</summary>
		// Token: 0x040006CA RID: 1738
		CreateUser = 131,
		/// <summary>A CREATE VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x040006CB RID: 1739
		CreateView = 41,
		/// <summary>A DELETE Transact-SQL statement was executed.</summary>
		// Token: 0x040006CC RID: 1740
		Delete = 3,
		/// <summary>A DENY Object Permissions Transact-SQL statement was executed.</summary>
		// Token: 0x040006CD RID: 1741
		DenyObject = 171,
		/// <summary>A DROP APPLICATION ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006CE RID: 1742
		DropAppRole = 139,
		/// <summary>A DROP ASSEMBLY Transact-SQL statement was executed.</summary>
		// Token: 0x040006CF RID: 1743
		DropAssembly = 103,
		/// <summary>A DROP_REMOTE_SERVICE_BINDING event type was specified when an event notification was created on the database or server instance.</summary>
		// Token: 0x040006D0 RID: 1744
		DropBinding = 176,
		/// <summary>A DROP CONTRACT Transact-SQL statement was executed.</summary>
		// Token: 0x040006D1 RID: 1745
		DropContract = 156,
		/// <summary>A DROP EVENT NOTIFICATION Transact-SQL statement was executed.</summary>
		// Token: 0x040006D2 RID: 1746
		DropEventNotification = 76,
		/// <summary>A DROP FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040006D3 RID: 1747
		DropFunction = 63,
		/// <summary>A DROP INDEX Transact-SQL statement was executed.</summary>
		// Token: 0x040006D4 RID: 1748
		DropIndex = 26,
		/// <summary>A DROP LOGIN Transact-SQL statement was executed.</summary>
		// Token: 0x040006D5 RID: 1749
		DropLogin = 146,
		/// <summary>A DROP MESSAGE TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040006D6 RID: 1750
		DropMsgType = 153,
		/// <summary>A DROP PARTITION FUNCTION Transact-SQL statement was executed.</summary>
		// Token: 0x040006D7 RID: 1751
		DropPartitionFunction = 193,
		/// <summary>A DROP PARTITION SCHEME Transact-SQL statement was executed.</summary>
		// Token: 0x040006D8 RID: 1752
		DropPartitionScheme = 196,
		/// <summary>A DROP PROCEDURE Transact-SQL statement was executed.</summary>
		// Token: 0x040006D9 RID: 1753
		DropProcedure = 53,
		/// <summary>A DROP QUEUE Transact-SQL statement was executed.</summary>
		// Token: 0x040006DA RID: 1754
		DropQueue = 159,
		/// <summary>A DROP ROLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006DB RID: 1755
		DropRole = 136,
		/// <summary>A DROP ROUTE Transact-SQL statement was executed.</summary>
		// Token: 0x040006DC RID: 1756
		DropRoute = 166,
		/// <summary>A DROP SCHEMA Transact-SQL statement was executed.</summary>
		// Token: 0x040006DD RID: 1757
		DropSchema = 143,
		/// <summary>Not available.</summary>
		// Token: 0x040006DE RID: 1758
		DropSecurityExpression = 33,
		/// <summary>A DROP SERVICE Transact-SQL statement was executed.</summary>
		// Token: 0x040006DF RID: 1759
		DropService = 163,
		/// <summary>A DROP SYNONYM Transact-SQL statement was executed.</summary>
		// Token: 0x040006E0 RID: 1760
		DropSynonym = 36,
		/// <summary>A DROP TABLE Transact-SQL statement was executed.</summary>
		// Token: 0x040006E1 RID: 1761
		DropTable = 23,
		/// <summary>A DROP TRIGGER Transact-SQL statement was executed.</summary>
		// Token: 0x040006E2 RID: 1762
		DropTrigger = 73,
		/// <summary>A DROP TYPE Transact-SQL statement was executed.</summary>
		// Token: 0x040006E3 RID: 1763
		DropType = 93,
		/// <summary>A DROP USER Transact-SQL statement was executed.</summary>
		// Token: 0x040006E4 RID: 1764
		DropUser = 133,
		/// <summary>A DROP VIEW Transact-SQL statement was executed.</summary>
		// Token: 0x040006E5 RID: 1765
		DropView = 43,
		/// <summary>A GRANT OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x040006E6 RID: 1766
		GrantObject = 170,
		/// <summary>A GRANT Transact-SQL statement was executed.</summary>
		// Token: 0x040006E7 RID: 1767
		GrantStatement = 167,
		/// <summary>An INSERT Transact-SQL statement was executed.</summary>
		// Token: 0x040006E8 RID: 1768
		Insert = 1,
		/// <summary>An invalid trigger action, one that is not exposed to the user, occurred.</summary>
		// Token: 0x040006E9 RID: 1769
		Invalid = 0,
		/// <summary>A REVOKE OBJECT Transact-SQL statement was executed.</summary>
		// Token: 0x040006EA RID: 1770
		RevokeObject = 172,
		/// <summary>A REVOKE Transact-SQL statement was executed.</summary>
		// Token: 0x040006EB RID: 1771
		RevokeStatement = 169,
		/// <summary>An UPDATE Transact-SQL statement was executed.</summary>
		// Token: 0x040006EC RID: 1772
		Update = 2
	}
}
