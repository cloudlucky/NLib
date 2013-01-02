// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BagBaseDebugView.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// For debugging <see cref="BagBase{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type .</typeparam>
    internal class BagBaseDebugView<T>
    {
        /// <summary>
        /// The bag for debbuging.
        /// </summary>
        private readonly BagBase<T> bag;

        /// <summary>
        /// Initializes a new instance of the <see cref="BagBaseDebugView{T}"/> class.
        /// </summary>
        /// <param name="bag">The bag  .</param>
        public BagBaseDebugView(BagBase<T> bag)
        {
            Check.Current.ArgumentNullException(bag != null, "bag");
            this.bag = bag;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<T, int>[] Items
        {
            get { return this.bag.ToDictionary(x => x, x => this.bag.GetCount(x)).ToArray(); }
        }
    }
}