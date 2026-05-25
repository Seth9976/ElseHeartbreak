using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x020000E8 RID: 232
	internal sealed class libgda
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0003166C File Offset: 0x0002F86C
		public static IntPtr GdaClient
		{
			get
			{
				if (libgda.gdaClient == IntPtr.Zero)
				{
					libgda.gdaClient = libgda.gda_client_new();
				}
				return libgda.gdaClient;
			}
		}

		// Token: 0x06000AC0 RID: 2752
		[DllImport("gobject-2.0", EntryPoint = "g_object_unref")]
		public static extern void FreeObject(IntPtr obj);

		// Token: 0x06000AC1 RID: 2753
		[DllImport("gda-2")]
		public static extern void gda_init(string app_id, string version, int nargs, string[] args);

		// Token: 0x06000AC2 RID: 2754
		[DllImport("gda-2")]
		public static extern GdaValueType gda_value_get_type(IntPtr value);

		// Token: 0x06000AC3 RID: 2755
		[DllImport("gda-2")]
		public static extern long gda_value_get_bigint(IntPtr value);

		// Token: 0x06000AC4 RID: 2756
		[DllImport("gda-2")]
		public static extern bool gda_value_get_boolean(IntPtr value);

		// Token: 0x06000AC5 RID: 2757
		[DllImport("gda-2")]
		public static extern IntPtr gda_value_get_date(IntPtr value);

		// Token: 0x06000AC6 RID: 2758
		[DllImport("gda-2")]
		public static extern double gda_value_get_double(IntPtr value);

		// Token: 0x06000AC7 RID: 2759
		[DllImport("gda-2")]
		public static extern int gda_value_get_integer(IntPtr value);

		// Token: 0x06000AC8 RID: 2760
		[DllImport("gda-2")]
		public static extern float gda_value_get_single(IntPtr value);

		// Token: 0x06000AC9 RID: 2761
		[DllImport("gda-2")]
		public static extern int gda_value_get_smallint(IntPtr value);

		// Token: 0x06000ACA RID: 2762
		[DllImport("gda-2")]
		public static extern string gda_value_get_string(IntPtr value);

		// Token: 0x06000ACB RID: 2763
		[DllImport("gda-2")]
		public static extern IntPtr gda_value_get_time(IntPtr value);

		// Token: 0x06000ACC RID: 2764
		[DllImport("gda-2")]
		public static extern IntPtr gda_value_get_timestamp(IntPtr value);

		// Token: 0x06000ACD RID: 2765
		[DllImport("gda-2")]
		public static extern byte gda_value_get_tinyint(IntPtr value);

		// Token: 0x06000ACE RID: 2766
		[DllImport("gda-2")]
		public static extern bool gda_value_is_null(IntPtr value);

		// Token: 0x06000ACF RID: 2767
		[DllImport("gda-2")]
		public static extern string gda_value_stringify(IntPtr value);

		// Token: 0x06000AD0 RID: 2768
		[DllImport("gda-2")]
		public static extern IntPtr gda_parameter_list_new();

		// Token: 0x06000AD1 RID: 2769
		[DllImport("gda-2")]
		public static extern string gda_type_to_string(GdaValueType type);

		// Token: 0x06000AD2 RID: 2770
		[DllImport("gda-2")]
		public static extern int gda_data_model_get_n_rows(IntPtr model);

		// Token: 0x06000AD3 RID: 2771
		[DllImport("gda-2")]
		public static extern int gda_data_model_get_n_columns(IntPtr model);

		// Token: 0x06000AD4 RID: 2772
		[DllImport("gda-2")]
		public static extern IntPtr gda_data_model_get_value_at(IntPtr model, int col, int row);

		// Token: 0x06000AD5 RID: 2773
		[DllImport("gda-2")]
		public static extern string gda_data_model_get_column_title(IntPtr model, int col);

		// Token: 0x06000AD6 RID: 2774
		[DllImport("gda-2")]
		public static extern IntPtr gda_data_model_describe_column(IntPtr model, int col);

		// Token: 0x06000AD7 RID: 2775
		[DllImport("gda-2")]
		public static extern int gda_data_model_get_column_position(IntPtr model, string name);

		// Token: 0x06000AD8 RID: 2776
		[DllImport("gda-2")]
		public static extern void gda_field_attributes_free(IntPtr fa);

		// Token: 0x06000AD9 RID: 2777
		[DllImport("gda-2")]
		public static extern string gda_field_attributes_get_name(IntPtr fa);

		// Token: 0x06000ADA RID: 2778
		[DllImport("gda-2")]
		public static extern GdaValueType gda_field_attributes_get_gdatype(IntPtr fa);

		// Token: 0x06000ADB RID: 2779
		[DllImport("gda-2")]
		public static extern long gda_field_attributes_get_defined_size(IntPtr fa);

		// Token: 0x06000ADC RID: 2780
		[DllImport("gda-2")]
		public static extern long gda_field_attributes_get_scale(IntPtr fa);

		// Token: 0x06000ADD RID: 2781
		[DllImport("gda-2")]
		public static extern bool gda_field_attributes_get_allow_null(IntPtr fa);

		// Token: 0x06000ADE RID: 2782
		[DllImport("gda-2")]
		public static extern bool gda_field_attributes_get_primary_key(IntPtr fa);

		// Token: 0x06000ADF RID: 2783
		[DllImport("gda-2")]
		public static extern bool gda_field_attributes_get_unique_key(IntPtr fa);

		// Token: 0x06000AE0 RID: 2784
		[DllImport("gda-2")]
		public static extern IntPtr gda_client_new();

		// Token: 0x06000AE1 RID: 2785
		[DllImport("gda-2")]
		public static extern IntPtr gda_client_open_connection(IntPtr client, string dsn, string username, string password, GdaConnectionOptions options);

		// Token: 0x06000AE2 RID: 2786
		[DllImport("gda-2")]
		public static extern IntPtr gda_client_open_connection_from_string(IntPtr client, string provider, string cnc_string, GdaConnectionOptions options);

		// Token: 0x06000AE3 RID: 2787
		[DllImport("gda-2")]
		public static extern bool gda_connection_is_open(IntPtr cnc);

		// Token: 0x06000AE4 RID: 2788
		[DllImport("gda-2")]
		public static extern bool gda_connection_close(IntPtr cnc);

		// Token: 0x06000AE5 RID: 2789
		[DllImport("gda-2")]
		public static extern string gda_connection_get_server_version(IntPtr cnc);

		// Token: 0x06000AE6 RID: 2790
		[DllImport("gda-2")]
		public static extern string gda_connection_get_database(IntPtr cnc);

		// Token: 0x06000AE7 RID: 2791
		[DllImport("gda-2")]
		public static extern string gda_connection_get_dsn(IntPtr cnc);

		// Token: 0x06000AE8 RID: 2792
		[DllImport("gda-2")]
		public static extern string gda_connection_get_cnc_string(IntPtr cnc);

		// Token: 0x06000AE9 RID: 2793
		[DllImport("gda-2")]
		public static extern string gda_connection_get_provider(IntPtr cnc);

		// Token: 0x06000AEA RID: 2794
		[DllImport("gda-2")]
		public static extern string gda_connection_get_username(IntPtr cnc);

		// Token: 0x06000AEB RID: 2795
		[DllImport("gda-2")]
		public static extern string gda_connection_get_password(IntPtr cnc);

		// Token: 0x06000AEC RID: 2796
		[DllImport("gda-2")]
		public static extern bool gda_connection_change_database(IntPtr cnc, string name);

		// Token: 0x06000AED RID: 2797
		[DllImport("gda-2")]
		public static extern IntPtr gda_transaction_new(string name);

		// Token: 0x06000AEE RID: 2798
		[DllImport("gda-2")]
		public static extern IntPtr gda_transaction_get_name(IntPtr xaction);

		// Token: 0x06000AEF RID: 2799
		[DllImport("gda-2")]
		public static extern IntPtr gda_transaction_set_name(IntPtr xaction, string name);

		// Token: 0x06000AF0 RID: 2800
		[DllImport("gda-2")]
		public static extern GdaTransactionIsolation gda_transaction_get_isolation_level(IntPtr xaction);

		// Token: 0x06000AF1 RID: 2801
		[DllImport("gda-2")]
		public static extern void gda_transaction_set_isolation_level(IntPtr xaction, GdaTransactionIsolation level);

		// Token: 0x06000AF2 RID: 2802
		[DllImport("gda-2")]
		public static extern bool gda_connection_begin_transaction(IntPtr cnc, IntPtr xaction);

		// Token: 0x06000AF3 RID: 2803
		[DllImport("gda-2")]
		public static extern bool gda_connection_commit_transaction(IntPtr cnc, IntPtr xaction);

		// Token: 0x06000AF4 RID: 2804
		[DllImport("gda-2")]
		public static extern bool gda_connection_rollback_transaction(IntPtr cnc, IntPtr xaction);

		// Token: 0x06000AF5 RID: 2805
		[DllImport("gda-2")]
		public static extern IntPtr gda_connection_execute_command(IntPtr cnc, IntPtr cmd, IntPtr parameterList);

		// Token: 0x06000AF6 RID: 2806
		[DllImport("gda-2")]
		public static extern int gda_connection_execute_non_query(IntPtr cnc, IntPtr command, IntPtr parameterList);

		// Token: 0x06000AF7 RID: 2807
		[DllImport("gda-2")]
		public static extern IntPtr gda_connection_execute_single_command(IntPtr cnc, IntPtr command, IntPtr parameterList);

		// Token: 0x06000AF8 RID: 2808
		[DllImport("gda-2")]
		public static extern IntPtr gda_connection_get_errors(IntPtr cnc);

		// Token: 0x06000AF9 RID: 2809
		[DllImport("gda-2")]
		public static extern IntPtr gda_command_new(string text, GdaCommandType type, GdaCommandOptions options);

		// Token: 0x06000AFA RID: 2810
		[DllImport("gda-2")]
		public static extern void gda_command_set_text(IntPtr cmd, string text);

		// Token: 0x06000AFB RID: 2811
		[DllImport("gda-2")]
		public static extern void gda_command_set_command_type(IntPtr cmd, GdaCommandType type);

		// Token: 0x06000AFC RID: 2812
		[DllImport("gda-2")]
		public static extern string gda_error_get_description(IntPtr error);

		// Token: 0x06000AFD RID: 2813
		[DllImport("gda-2")]
		public static extern long gda_error_get_number(IntPtr error);

		// Token: 0x06000AFE RID: 2814
		[DllImport("gda-2")]
		public static extern string gda_error_get_source(IntPtr error);

		// Token: 0x06000AFF RID: 2815
		[DllImport("gda-2")]
		public static extern string gda_error_get_sqlstate(IntPtr error);

		// Token: 0x04000418 RID: 1048
		private static IntPtr gdaClient = IntPtr.Zero;
	}
}
