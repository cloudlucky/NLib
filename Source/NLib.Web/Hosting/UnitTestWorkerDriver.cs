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
            Check.Current.ArgumentNullException(workerRequest, "workerRequest");

            this.InitAppDomain(workerRequest);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitTestWorkerDriver" /> class.
        /// </summary>
        ~UnitTestWorkerDriver()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the base directory.
        /// </summary>
        protected static string BaseDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }

        /// <summary>
        /// Gets the worker request.
        /// </summary>
        protected IWorkerRequest WorkerRequest { get; private set; }

        /// <summary>
        /// Gets the bin directory.
        /// </summary>
        protected string BinDirectory
        {
            get { return Path.Combine(BaseDirectory, "bin"); }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the response of a request.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The response</returns>
        public virtual HttpResponse GetResponse(string page, string queryString)
        {
            return new HttpResponse(this.WorkerRequest.ProcessRequest(page, queryString));
        }

        /// <summary>
        /// Copies the binaries.
        /// </summary>
        protected void CopyBinaries()
        {
            Directory.CreateDirectory(this.BinDirectory);
            var binairies = Directory.GetFiles(BaseDirectory, "*.*")
                                     .Where(x => x.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) || x.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase) || x.EndsWith(".exe", StringComparison.OrdinalIgnoreCase));

            foreach (var file in binairies)
            {
                var fileName = Path.GetFileName(file);

                if (fileName != null)
                {
                    var destination = Path.Combine(this.BinDirectory, fileName);

                    if (!File.Exists(destination))
                    {
                        File.Copy(file, destination);
                    }
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.WorkerRequest != null)
                {
                    this.WorkerRequest.Dispose();
                    this.WorkerRequest = null;
                }
            }
        }

        /// <summary>
        /// Initialize the application domain.
        /// </summary>
        /// <param name="worker">The worker request.</param>
        private void InitAppDomain(IWorkerRequest worker)
        {
            this.CopyBinaries();

            this.WorkerRequest = (IWorkerRequest)ApplicationHost.CreateApplicationHost(worker.GetType(), "/", BaseDirectory);
        }
    }
}
