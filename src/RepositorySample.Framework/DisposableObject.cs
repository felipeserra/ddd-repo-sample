using System;
using System.Collections.Generic;
using System.Text;

namespace RepositorySample.Framework
{
    public abstract class DisposableObject : IDisposable
    {
        /// <summary>
        /// Occurs when the current object has been disposed.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableObject"/> class.
        /// </summary>
        ~DisposableObject()
        {
            this.Dispose(false);
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
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
