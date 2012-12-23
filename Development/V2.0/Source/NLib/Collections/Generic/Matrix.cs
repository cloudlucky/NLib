// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Matrix.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System;

    // http://en.wikipedia.org/wiki/Matrix_(mathematics)
    public class Matrix<T>
    {
        private readonly int columns;
        private readonly T[,] matrix;
        private readonly int rows;

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.matrix = new T[rows, columns];
        }

        public int Columns
        {
            get { return this.columns; }
        }

        public int Rows
        {
            get { return this.rows; }
        }

        public T this[int i, int j]
        {
            get { return this.matrix[i, j]; }
            set { this.matrix[i, j] = value; }
        }

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                // TODO throw better exception
                throw new Exception();
            }

            var m = new Matrix<T>()
        }
    }
}
