// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortedBagDebugView.cs" company=".">
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
    /// For debugging <see cref="SortedBag{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type .</typeparam>
    internal class SortedBagDebugView<T>
    {
        /// <summary>
        /// The bag for debbuging.
        /// </summary>
        private readonly SortedBag<T> bag;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBagDebugView{T}"/> class.
        /// </summary>
        /// <param name="bag">The bag  .</param>
        public SortedBagDebugView(SortedBag<T> bag)
        {
            Check.Requires<ArgumentNullException>(bag != null, new { paramName = "bag" });
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
