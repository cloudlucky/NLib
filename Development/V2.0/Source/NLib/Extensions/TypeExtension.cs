// TODO fluent version API Next vesion
//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="TypeExtension.cs" company=".">
////   Copyright (c) Cloudlucky. All rights reserved.
////   http://www.cloudlucky.com
////   This code is licensed under the Microsoft Public License (Ms-PL)
////   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
//// </copyright>
//// --------------------------------------------------------------------------------------------------------------------

//namespace NLib.Extensions
//{
//    using System;
//    using System.Diagnostics.CodeAnalysis;
//    using System.Linq.Expressions;
//    using System.Reflection;

//    /// <summary>
//    /// Defines extensions methods for <see cref="Type"/>.
//    /// </summary>
//    public static class TypeExtension
//    {
//        /// <summary>
//        /// Searches for the public property with the specified name.
//        /// </summary>
//        /// <typeparam name="T">The type of the elements of source.</typeparam>
//        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
//        /// <param name="type">The type  .</param>
//        /// <param name="keySelector">The selector of the public property to get.</param>
//        /// <returns>
//        /// A <see cref="PropertyInfo"/> object representing the public property with the specified name, if found; otherwise, null.
//        /// </returns>
//        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
//        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
//        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
//        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
//        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "CheckError class do the check")]
//        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
//        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Reviewed. It's ok.")]
//        public static PropertyInfo GetProperty<T, TKey>(this Type<T> type, Expression<Func<T, TKey>> keySelector)
//        {
//            CheckError.ArgumentNullException(type, "type");
//            CheckError.ArgumentNullException(keySelector, "keySelector");

//            MemberExpression memberExpression = null;

//            switch (keySelector.Body.NodeType)
//            {
//                case ExpressionType.Convert:
//                    memberExpression = ((UnaryExpression)keySelector.Body).Operand as MemberExpression;
//                    break;
//                case ExpressionType.MemberAccess:
//                    memberExpression = keySelector.Body as MemberExpression;
//                    break;
//            }

//            if (memberExpression == null)
//            {
//                throw new ArgumentException("Not a member access", "keySelector");
//            }

//            return memberExpression.Member as PropertyInfo;
//        } 
//    }
//}
