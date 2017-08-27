using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using NLib.Collections.Generic.Extensions;
using NLib.Collections.Generic.Resources;

namespace NLib.Collections.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a collection of objects that is maintained in Base order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the bag.</typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(BagBaseDebugView<>))]
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public abstract class BagBase<T> : IBag<T>
    {
        /// <inheritdoc />
        public virtual int Count { get; protected set; }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Reviewed. It's OK.")]
        bool ICollection<T>.IsReadOnly => false;

        /// <inheritdoc />
        public abstract ISet<T> UniqueSet { get; }

        /// <summary>
        /// Gets the equality comparer.
        /// </summary>
        protected abstract EqualityComparison<T> EqualityComparer { get; }

        /// <summary>
        /// Gets or sets the implementation model.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. It's OK here for sub classes to change the model.")]
        protected IDictionary<T, int> Model { get; set; }

        /// <inheritdoc />
        public virtual void Add(T item)
        {
            this.Add(item, 1);
        }

        /// <inheritdoc />
        public virtual void Add(T item, int numberCopies)
        {
            if (this.Contains(item))
            {
                this.Model[item] += numberCopies;
            }
            else
            {
                this.Model.Add(item, numberCopies);
            }

            this.Count += numberCopies;
        }

        /// <summary>
        /// Adds the elements of the specified collection in the bag.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            collection?.ForEach(this.Add);
        }

        /// <inheritdoc />
        public virtual bool BagEquals(IEnumerable<T> other)
        {
            return other != null && this.Model.Keys.All(key => other.Count(x => this.EqualityComparer(x, key)) == this.GetCount(key));
        }

        /// <inheritdoc />
        public virtual void Clear()
        {
            this.Model.Clear();
            this.Count = 0;
        }

        /// <inheritdoc />
        public virtual bool Contains(T item)
        {
            return this.Model.ContainsKey(item);
        }

        /// <inheritdoc />
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            Check.Current.ArgumentNullException(array, nameof(array))
                         .Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, CollectionResource.CopyTo_ArgumentOutOfRangeException_ArrayIndex, new { paramName = nameof(arrayIndex) })
                         .Requires<ArgumentException>(arrayIndex < array.Length && arrayIndex + this.Count <= array.Length, CollectionResource.CopyTo_ArgumentException_Array, new { paramName = nameof(array) });

            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
        }

        /// <inheritdoc />
        public virtual void ExceptAllWith(IEnumerable<T> other)
        {
            other.ForEach(i => this.RemoveAll(i));
        }

        /// <inheritdoc />
        public virtual void ExceptWith(IEnumerable<T> other)
        {
            other.ForEach(i => this.Remove(i));
        }

        /// <inheritdoc />
        public virtual int GetCount(T item)
        {
            return this.Model.ContainsKey(item) ? this.Model[item] : 0;
        }

        /// <inheritdoc />
        public virtual IEnumerator<T> GetEnumerator()
        {
            foreach (var n in this.Model)
            {
                for (var i = 0; i < n.Value; ++i)
                {
                    yield return n.Key;
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc />
        public virtual void IntersectWith(IEnumerable<T> other)
        {
            Check.Current.ArgumentNullException(other, nameof(other));

            var tmp = other.ToList();
            foreach (var t in tmp)
            {
                if (!this.Contains(t))
                {
                    this.RemoveAll(t);
                }
                else
                {
                    var t1 = t;
                    var count = tmp.Count(x => this.EqualityComparer(x, t1));
                    var total = this.GetCount(t);

                    if (total > count)
                    {
                        this.Model[t] = count;
                        this.Count -= total - count;
                    }
                }
            }

            var i = 0;
            while (i < this.Model.Keys.Count)
            {
                var key = this.Model.Keys.ElementAt(i);
                if (!tmp.Contains(key))
                {
                    this.RemoveAll(key);
                }
                else
                {
                    ++i;
                }
            }
        }

        /// <inheritdoc />
        public virtual bool IsProperSubBagOf(IEnumerable<T> other)
        {
            var tmp = other.ToList();
            return this.IsSubBagOf(tmp)
                   && this.All(tmp.Contains);
        }

        /// <inheritdoc />
        public virtual bool IsProperSuperBagOf(IEnumerable<T> other)
        {
            return this.IsSuperBagOf(other);
        }

        /// <inheritdoc />
        public virtual bool IsSubBagOf(IEnumerable<T> other)
        {
            var tmp = other.ToList();
            return tmp.All(x => this.Contains(x) && this.Model[x] <= tmp.Count(y => this.EqualityComparer(x, y)));
        }

        /// <inheritdoc />
        public virtual bool IsSuperBagOf(IEnumerable<T> other)
        {
            var tmp = other.ToList();
            return tmp.All(x => this.Contains(x) && this.Model[x] >= tmp.Count(y => this.EqualityComparer(x, y)));
        }

        /// <inheritdoc />
        public virtual bool Overlaps(IEnumerable<T> other)
        {
            return other.All(this.Contains);
        }

        /// <inheritdoc />
        public virtual bool Remove(T item)
        {
            return this.Remove(item, 1) == 1;
        }

        /// <inheritdoc />
        public virtual int Remove(T item, int numberCopies)
        {
            if (!this.Contains(item))
            {
                return 0;
            }

            var before = this.Model[item];
            this.Model[item] -= numberCopies;
            var remove = false;

            if (this.Model[item] <= 0)
            {
                this.Model[item] = 0;
                remove = true;
            }

            var result = before - this.Model[item];
            this.Count -= result;

            if (remove)
            {
                this.Model.Remove(item);
            }

            return result;
        }

        /// <inheritdoc />
        public virtual int RemoveAll(T item)
        {
            if (!this.Contains(item))
            {
                return 0;
            }

            var result = this.Model[item];
            this.Model.Remove(item);
            this.Count -= result;

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "0", Justification = "CheckError class do the check")]
        public virtual void SymmetricExceptWith(IEnumerable<T> other)
        {
            Check.Current.ArgumentNullException(other, nameof(other));

            var tmp = new List<T>();

            foreach (var t in other)
            {
                if (this.Contains(t))
                {
                    this.RemoveAll(t);
                }
                else
                {
                    tmp.Add(t);
                }
            }

            this.AddRange(tmp);
        }

        /// <inheritdoc />
        public virtual void UnionWith(IEnumerable<T> other)
        {
            this.AddRange(other);
        }
    }
}
