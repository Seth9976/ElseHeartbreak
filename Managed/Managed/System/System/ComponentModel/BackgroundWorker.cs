using System;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x020000CC RID: 204
	[DefaultEvent("DoWork")]
	public class BackgroundWorker : Component
	{
		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060008C3 RID: 2243 RVA: 0x00019E88 File Offset: 0x00018088
		// (remove) Token: 0x060008C4 RID: 2244 RVA: 0x00019EA4 File Offset: 0x000180A4
		public event DoWorkEventHandler DoWork;

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060008C5 RID: 2245 RVA: 0x00019EC0 File Offset: 0x000180C0
		// (remove) Token: 0x060008C6 RID: 2246 RVA: 0x00019EDC File Offset: 0x000180DC
		public event ProgressChangedEventHandler ProgressChanged;

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060008C7 RID: 2247 RVA: 0x00019EF8 File Offset: 0x000180F8
		// (remove) Token: 0x060008C8 RID: 2248 RVA: 0x00019F14 File Offset: 0x00018114
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		/// <summary>Gets a value indicating whether the application has requested cancellation of a background operation.</summary>
		/// <returns>true if the application has requested cancellation of a background operation; otherwise, false. The default is false.</returns>
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00019F30 File Offset: 0x00018130
		[Browsable(false)]
		public bool CancellationPending
		{
			get
			{
				return this.cancel_pending;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>true, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, false.</returns>
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00019F38 File Offset: 0x00018138
		[Browsable(false)]
		public bool IsBusy
		{
			get
			{
				return this.async != null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise false. The default is false.</returns>
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00019F48 File Offset: 0x00018148
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00019F50 File Offset: 0x00018150
		[DefaultValue(false)]
		public bool WorkerReportsProgress
		{
			get
			{
				return this.report_progress;
			}
			set
			{
				this.report_progress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise false. The default is false.</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00019F5C File Offset: 0x0001815C
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00019F64 File Offset: 0x00018164
		[DefaultValue(false)]
		public bool WorkerSupportsCancellation
		{
			get
			{
				return this.support_cancel;
			}
			set
			{
				this.support_cancel = value;
			}
		}

		/// <summary>Requests cancellation of a pending background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.WorkerSupportsCancellation" /> is false. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008CF RID: 2255 RVA: 0x00019F70 File Offset: 0x00018170
		public void CancelAsync()
		{
			if (!this.support_cancel)
			{
				throw new InvalidOperationException("This background worker does not support cancellation.");
			}
			if (!this.IsBusy)
			{
				return;
			}
			this.cancel_pending = true;
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x060008D0 RID: 2256 RVA: 0x00019F9C File Offset: 0x0001819C
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x060008D1 RID: 2257 RVA: 0x00019FA8 File Offset: 0x000181A8
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException("This background worker does not report progress.");
			}
			if (!this.IsBusy)
			{
				return;
			}
			this.async.Post(delegate(object o)
			{
				ProgressChangedEventArgs progressChangedEventArgs = o as ProgressChangedEventArgs;
				this.OnProgressChanged(progressChangedEventArgs);
			}, new ProgressChangedEventArgs(percentProgress, userState));
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008D2 RID: 2258 RVA: 0x00019FF8 File Offset: 0x000181F8
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001A004 File Offset: 0x00018204
		private void ProcessWorker(object argument, AsyncOperation async, SendOrPostCallback callback)
		{
			Exception ex = null;
			DoWorkEventArgs doWorkEventArgs = new DoWorkEventArgs(argument);
			try
			{
				this.OnDoWork(doWorkEventArgs);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				doWorkEventArgs.Cancel = false;
			}
			callback(new object[]
			{
				new RunWorkerCompletedEventArgs(doWorkEventArgs.Result, ex, doWorkEventArgs.Cancel),
				async
			});
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001A078 File Offset: 0x00018278
		private void CompleteWorker(object state)
		{
			object[] array = (object[])state;
			RunWorkerCompletedEventArgs runWorkerCompletedEventArgs = array[0] as RunWorkerCompletedEventArgs;
			AsyncOperation asyncOperation = array[1] as AsyncOperation;
			SendOrPostCallback sendOrPostCallback = delegate(object darg)
			{
				this.async = null;
				this.OnRunWorkerCompleted(darg as RunWorkerCompletedEventArgs);
			};
			asyncOperation.PostOperationCompleted(sendOrPostCallback, runWorkerCompletedEventArgs);
			this.cancel_pending = false;
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true. </exception>
		// Token: 0x060008D5 RID: 2261 RVA: 0x0001A0BC File Offset: 0x000182BC
		public void RunWorkerAsync(object argument)
		{
			if (this.IsBusy)
			{
				throw new InvalidOperationException("The background worker is busy.");
			}
			this.async = AsyncOperationManager.CreateOperation(this);
			BackgroundWorker.ProcessWorkerEventHandler processWorkerEventHandler = new BackgroundWorker.ProcessWorkerEventHandler(this.ProcessWorker);
			processWorkerEventHandler.BeginInvoke(argument, this.async, new SendOrPostCallback(this.CompleteWorker), null, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060008D6 RID: 2262 RVA: 0x0001A114 File Offset: 0x00018314
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			if (this.DoWork != null)
			{
				this.DoWork(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001A130 File Offset: 0x00018330
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			if (this.ProgressChanged != null)
			{
				this.ProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001A14C File Offset: 0x0001834C
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			if (this.RunWorkerCompleted != null)
			{
				this.RunWorkerCompleted(this, e);
			}
		}

		// Token: 0x04000244 RID: 580
		private AsyncOperation async;

		// Token: 0x04000245 RID: 581
		private bool cancel_pending;

		// Token: 0x04000246 RID: 582
		private bool report_progress;

		// Token: 0x04000247 RID: 583
		private bool support_cancel;

		// Token: 0x020004D9 RID: 1241
		// (Invoke) Token: 0x06002C10 RID: 11280
		private delegate void ProcessWorkerEventHandler(object argument, AsyncOperation async, SendOrPostCallback callback);
	}
}
