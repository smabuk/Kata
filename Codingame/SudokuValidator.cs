using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/sudoku-validator
 *
 * Goal
 * -----
 * 
 * You get a sudoku grid from a player and you have to check if it has been correctly filled.
 * 
 * A sudoku grid consists of 9×9 = 81 cells split in 9 sub-grids of 3×3 = 9 cells.
 * For the grid to be correct, each row must contain one occurrence of each digit (1 to 9), each column must contain one occurrence of each digit (1 to 9) and each sub-grid must contain one occurrence of each digit (1 to 9).
 * 
 * You shall answer true if the grid is correct or false if it is not.
 * 
 * Input
 * -----
 * 
 * 9 rows of 9 space-separated digits representing the sudoku grid.
 * 
 * Output
 * ------
 * true or false
 * 
 * Constraints
 * -----------
 * 
 * For each digit n in the grid: 1 ≤ n ≤ 9.
 * 
 * Example
 * -------
 * 
 * Input                                 Output
 * -----                                 ------
 * 1 2 3 4 5 6 7 8 9                     true
 * 4 5 6 7 8 9 1 2 3
 * 7 8 9 1 2 3 4 5 6
 * 9 1 2 3 4 5 6 7 8
 * 3 4 5 6 7 8 9 1 2
 * 6 7 8 9 1 2 3 4 5
 * 8 9 1 2 3 4 5 6 7
 * 2 3 4 5 6 7 8 9 1
 * 5 6 7 8 9 1 2 3 4
 * 
 */

namespace Codingame
{

	static class SudokuValidatorSolution
	{

		public static bool SudokuValidator(string[] args)
		{
			int[,] array = new int[9,9];

			for (int i = 0; i < 9; i++)
			{
				// string[] inputs = Console.ReadLine().Split(' ');
				string[] inputs = args[i].Split(' ');
				for (int j = 0; j < 9; j++)
				{
					int n = int.Parse(inputs[j]);
					array[i, j] = n;
				}
			}

			for (int i = 0; i < 9; i++)
			{
				int[] row = new int[9];
				int[] col = new int[9];
				for (int j = 0; j < 9; j++)
				{
					row[j] = array[i, j];
					col[j] = array[j, i];
				}
				if (!ValidArray(row))
				{
					Console.WriteLine("false");
					return false;
				}
				if (!ValidArray(col))
				{
					Console.WriteLine("false");
					return false;
				}
			}
			for (int i = 0; i < 9; i++)
			{
				if (!ValidArray(GetBlock(array, i)))
				{
					Console.WriteLine("false");
					return false;
				}
			}
			// Write an answer using Console.WriteLine()
			// To debug: Console.Error.WriteLine("Debug messages...");

			Console.WriteLine("true");
			return true;
		}


		public static int[] GetBlock(int[,] array, int blockNo)
		{
			int[] block = new int[9];
			int startRow = (blockNo / 3) * 3;
			int startCol = (blockNo % 3) * 3;

			int x = 0;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					block[x++] = array[startRow+i, startCol + j];
				}
			}

			return block;
		}

		public static bool ValidArray(IEnumerable<int> array) 
			=> array.Distinct().Count() == array.Count();


	}


	public class SudokuValidatorTests
	{
		[Theory]
		[InlineData(new string[] { 
			"1 2 3 4 5 6 7 8 9",
			"4 5 6 7 8 9 1 2 3",
			"7 8 9 1 2 3 4 5 6",
			"9 1 2 3 4 5 6 7 8",
			"3 4 5 6 7 8 9 1 2",
			"6 7 8 9 1 2 3 4 5",
			"8 9 1 2 3 4 5 6 7",
			"2 3 4 5 6 7 8 9 1",
			"5 6 7 8 9 1 2 3 4"}
			, true)]
		public void ValidatorSudoku_ShouldBe_Correct(string[] args, bool expected)
		{
			Assert.Equal(expected, SudokuValidatorSolution.SudokuValidator(args));
		}

		[Theory]
		[InlineData(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9}, true)]
		[InlineData(new int[] {7, 2, 3, 4, 5, 6, 1, 8, 9}, true)]
		[InlineData(new int[] {7, 2, 3, 7, 5, 6, 1, 8, 9}, false)]
		public void ValidArray_ShouldBe_9_Unique_Numbers(int[] args, bool expected)
		{
			Assert.Equal(expected, SudokuValidatorSolution.ValidArray(args));
		}
	}
}
