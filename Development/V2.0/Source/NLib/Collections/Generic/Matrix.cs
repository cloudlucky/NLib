using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLib.Collections.Generic
{
    public class Matrix2D
    {
        /// <summary>
        /// Number of rows of the matrix
        /// </summary>
        public int NumberOfRows;

        /// <summary>
        /// Number of columns of the matrix
        /// </summary>
        public int NumberOfColumns;


        /// <summary>
        /// Array
        /// </summary>
        private Number[,] matrix2D;

        /// <summary>
        /// Create a new <paramref name="rows"/> by <paramref name="columns"/> matrix
        /// </summary>
        /// <param name="rows">The number of rows</param>
        /// <param name="columns">The number of columns</param>
        public Matrix2D(int rows, int columns)
        {
            NumberOfRows = rows;
            NumberOfColumns = columns;

            matrix2D = new Number[rows, columns];

            for (int j = 0; j < NumberOfColumns; j++)
            {
                for (int i = 0; i < NumberOfRows; i++)
                {
                    this[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Sum of <paramref name="matrix2DLeft"/> and <paramref name="matrix2DRight"/>
        /// </summary>
        /// <param name="matrix2DLeft"></param>
        /// <param name="matrix2DRight"></param>
        /// <returns>The matrix</returns>
        public static Matrix2D operator + ( Matrix2D matrix2DLeft, Matrix2D matrix2DRight )
        {
            Matrix2D matrix = new Matrix2D(matrix2DLeft.NumberOfRows, matrix2DLeft.NumberOfColumns);

            for (int j = 0; j < matrix2DLeft.NumberOfColumns; j++)
            {
               for (int i = 0; i < matrix2DLeft.NumberOfRows; i++)
               {
                   matrix[i, j] = matrix2DLeft[i, j] + matrix2DRight[i, j];
               }
           }
          
           return matrix;
        }
     
        /// <summary>
        /// 
        /// <summary>
        /// Gets the number with the specified item.
        /// </summary>
        /// <param name="rows">rows</param>
        /// <param name="columns">columns</param>
        /// <returns>The Number if found; otherwise null.</returns>
        public Number this[int rows, int columns]
        {
            get
            {
                return this.matrix2D[rows, columns]; 
            }
            set 
            {
                this.matrix2D[rows, columns] = value;
            }
        }
        
    }
}
