
using System;

using NUnit.Framework;

namespace CodeWarsKatas
{
	/*
	 * Write a function that accepts two square matrices (N x N two dimensional arrays), and return the sum of the two. 
	 * Both matrices being passed into the function will be of size N x N (square), containing only integers.
	 * 
	 * How to sum two matrices:
	 * 
	 * Take each cell [n][m] from the first matrix, and add it with the same [n][m] cell from the second matrix.
	 * This will be cell [n][m] of the solution matrix.
	 * 
	 * Visualization:
	 * 
	 * |1 2 3|     |2 2 1|     |1+2 2+2 3+1|     |3 4 4|
	 * |3 2 1|  +  |3 2 3|  =  |3+3 2+2 1+3|  =  |6 4 4|
	 * |1 1 1|     |1 1 3|     |1+1 1+1 1+3|     |2 2 4|
	 * 
	 * Example
	 * matrixAddition(
	 *   [ [1, 2, 3],
	 *     [3, 2, 1],
	 *     [1, 1, 1] ],
	 * //      +
	 *   [ [2, 2, 1],
	 *     [3, 2, 3],
	 *     [1, 1, 3] ] )
	 * 
	 * // returns:
	 *   [ [3, 4, 4],
	 *     [6, 4, 4],
	 *     [2, 2, 4] ]	 
	 */

	public static class MatrixAdditionKata
	{
		public static int[][] MatrixAddition(int[][] a, int[][] b)
		{
			int[][] outputMatrix = a;

			for (int i = 0; i < outputMatrix.Length; i++)
			{ 
				for (int j = 0; j < outputMatrix.Length; j++)
				{
					outputMatrix[i][j] = a[i][j] + b[i][j];
				}
			}

			return outputMatrix;
		}

	}

	[TestFixture]
	public class MatrixAddionTests
	{
		[Test]
		public void SampleTest()
		{
			Assert.AreEqual(new int[][] {new int[] {3, 5},
								   new int[] {3, 5}}, MatrixAdditionKata.MatrixAddition(new int[][] {new int[] {1, 2},
																					   new int[] {1, 2}},
																							 new int[][] {new int[] {2, 3},
																									new int[] {2, 3}}));
		}
	}
}
