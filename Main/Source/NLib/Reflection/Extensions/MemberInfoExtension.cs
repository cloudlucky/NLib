// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberInfoExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Reflection.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines extensions methods for <see cref="MemberInfo"/>.
    /// </summary>
    public static class MemberInfoExtension
    {
        /// <summary>
        /// When overridden in a derived class, returns the first of custom attributes identified by <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of attribute to search for. 
        /// Only attributes that are assignable to this type are returned.
        /// </typeparam>
        /// <param name="memberInfo">The member info.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
        /// <returns>The first attribute or null.</returns>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
        /// <exception cref="ArgumentNullException">The member info is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// This member belongs to a type that is loaded into the reflection-only context. 
        /// See How to: Load Assemblies into the Reflection-Only Context.
        /// </exception>
        public static T GetCustomAttribute<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            return GetCustomAttributes<T>(memberInfo, inherit).FirstOrDefault();
        }

        /// <summary>
        /// When overridden in a derived class, returns an array of custom attributes identified by <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of attribute to search for. 
        /// Only attributes that are assignable to this type are returned.
        /// </typeparam>
        /// <param name="memberInfo">The member info.</param>
        /// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
        /// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
        /// <exception cref="TypeLoadException">A custom attribute type cannot be loaded.</exception>
        /// <exception cref="ArgumentNullException">The member info is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// This member belongs to a type that is loaded into the reflection-only context. 
        /// See How to: Load Assemblies into the Reflection-Only Context.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public static T[] GetCustomAttributes<T>(this MemberInfo memberInfo, bool inherit) where T : Attribute
        {
            CheckError.ArgumentNullException(memberInfo, "memberInfo");

            return memberInfo.GetCustomAttributes(typeof(T), inherit) as T[];
        }

        /// <summary>
        /// Gets the type of the member info.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns>The type of the member info.</returns>
        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            CheckError.ArgumentNullException(memberInfo, "memberInfo");

            var property = memberInfo as PropertyInfo;
            if (property != null)
            {
                return property.PropertyType;
            }

            var field = memberInfo as FieldInfo;
            if (field != null)
            {
                return field.FieldType;
            }

            throw new NotSupportedException("member info is not supported");
        }
    }
}
