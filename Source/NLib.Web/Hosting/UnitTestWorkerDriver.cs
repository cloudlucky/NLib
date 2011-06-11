// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestWorkerDriver.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Hosting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Hosting;

    /// <summary>
    /// Provides a simple way to get a response of an ASP.NET request outside an Internet Information Services (IIS) application.
    /// </summary>
    public class UnitTestWorkerDriver : IWorkerDriver
    {
        /// <summary>
        /// The worker request.
        /// </summary>
        private IWorkerRequest workerRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestWorkerDriver"/> class.
        /// </summary>
        public UnitTestWorkerDriver()
            : this(new WorkerRequest())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestWorkerDriver"/> class.
        /// </summary>
        /// <param name="workerRequest">The worker request.</param>
        public UnitTestWorkerDriver(IWorkerRequest workerRequest)
        {
            CheckError.ArgumentNullException(workerRequest, "workerRequest");

            this.InitAppDomain(workerRequest);
        }

        /// <summary>
        /// Gets the worker process.
        /// </summary>
        protected IWorkerRequest WorkerProcess
        {
            get { return this.workerRequest; }
        }

        /// <summary>
        /// Gets the base directory.
        /// </summary>
        protected virtual string BaseDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <summary>
        /// Gets the bin directory.
        /// </summary>
        protected virtual string BinDirectory
        {
            get { return Path.Combine(this.BaseDirectory, "bin"); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            this.WorkerProcess.Dispose();

            if (Directory.Exists(this.BinDirectory))
            {
                Directory.Delete(this.BinDirectory, true);
            }
        }

        /// <summary>
        /// Gets the response of a request.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The response</returns>
        public virtual HttpResponse GetResponse(string page, string queryString)
        {
            return new HttpResponse(this.WorkerProcess.ProcessRequest(page, queryString));
        }

        /// <summary>
        /// Copies the binaries.
        /// </summary>
        protected virtual void CopyBinaries()
        {
            Directory.CreateDirectory(this.BinDirectory);
            var binairies = Directory.GetFiles(this.BaseDirectory, "*.*")
                                     .Where(x => x.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || x.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase) || x.EndsWith(".exe", StringComparison.OrdinalIgnoreCase));

            foreach (var file in binairies)
            {
                var fileName = Path.GetFileName(file);

                if (fileName != null)
                {
                    File.Copy(file, Path.Combine(this.BinDirectory, fileName), true);
                }
            }
        }

        /// <summary>
        /// Inits the app domain.
        /// </summary>
        /// <param name="worker">The worker request.</param>
        private void InitAppDomain(IWorkerRequest worker)
        {
            this.CopyBinaries();

            this.workerRequest = (IWorkerRequest)ApplicationHost.CreateApplicationHost(worker.GetType(), "/MyTestVirtualDir", this.BaseDirectory);
        }
    }
}
