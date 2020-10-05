using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/a-coin-guessing-game
 *
 * Goal
 * 
 * Yulia has annotated both sides of N identical coins with the numbers from 1 to 2×N. Each number has 
 * been used exactly once and each coin has received an odd number on one side and an even number on the 
 * other side. She asks Zack, who is aware of these rules but does not know the chosen distribution of 
 * numbers, to guess all the even/odd combinations by playing a little game.
 * 
 * Yulia shakes and throws the coins on the table and reveals the resulting (seemingly random) 
 * configuration to Zack, letting him see the numbers on the visible side of each coin. No other 
 * information can allow Zack to identify or distinguish the coins.
 * 
 * Yulia repeats that operation several times and, after T throws in total, stops and informs Zack he 
 * has seen enough to guess all the pairs of numbers on the coins.
 * 
 * Can you help Zack guess the numbers that were written on all of the coins?
 * 
 * Example: N = 3 coins, the numbers from 1 to 6 are used.
 * - First throw: 3 1 6.
 * Zack learns that the even number 6 is not associated with the odd numbers 1 or 3, hence it has to be 
 * paired with 5.
 * - Second throw: 4 1 6.
 * Zack learns that 1 is not paired with 4. He also sees, for the second time, that it is not paired 
 * with 6. So 1 it has to be paired with 2, and consequently 3 is paired with 4.
 * Solution: 1/2, 3/4, 5/6. Expected output: 2 4 6.
 * ========
 * 
 * That said, Yulia has a secret criterion. She calls "coins ring" a sequence of numbers i1, i2, i3, 
 * ..., ik, such that all the coins i1/i2, i2/i3, ..., ik/i1 are still acceptable assuming all possible 
 * "deductions" are made from the configurations that have been seen until now. She never stops before 
 * making sure that Zack can deductively get rid of all the coins rings.
 * 
 * 
 * Input
 * 
 * Line 1: Two space-separated integers N and T corresponding to the number of coins and the number of 
 * configurations to follow.
 * Next T lines: N space-separated integers, in no particular order, corresponding to the coin sides 
 * that Zack sees after each throw.
 * 
 * Output
 * 
 * One line of N space-separated even integers corresponding to the even numbers written on the other 
 * side of the coin sides carrying the odd numbers 1, 3, 5, ..., 2×N-1 in order.
 * 
 * Constraints
 * 2 ≤ N ≤ 150
 * 1 ≤ T ≤ 15
 * 
 * The given data guarantees a unique solution.
 * 
 * 
 * Example
 * Input			Output
 * 2 3              4 2
 * 4 2
 * 2 4
 * 4 3

 */

namespace Codingame
{

	public static class IntExtensions
	{
		public static bool IsEven(this int number)
		{
			return number % 2 == 0;
		}
		public static bool IsOdd(this int number)
		{
			return number % 2 == 1;
		}
	}

	 static class ACoinGuessingGameSolution
	{

		public static string ACoinGuessingGame(string[] lineInputs)
		{
			string[] inputs;
			inputs = lineInputs[0].Split(' ');
			int N = int.Parse(inputs[0]);
			int T = int.Parse(inputs[1]);
			// a dictionary of coins indexed by "oddnumber side"
			//     a dictionary of possible even sides (default is true and is whittled away until 1 remains
			Dictionary<int, Dictionary<int, bool>> coinMatrix = new Dictionary<int, Dictionary<int, bool>>();
			for (int x = 1; x < N * 2; x += 2)
			{
				coinMatrix.Add(x, new Dictionary<int, bool>());
				for (int y = 2; y <= N * 2; y += 2)
				{
					coinMatrix[x].Add(y, true);
				}
			}

			for (int i = 1; i <= T; i++)
			{
				inputs = lineInputs[i].Split(' ');
				int[] visibleSides = inputs.Select(c => int.Parse(c)).ToArray();

				// Ignore line inputs if they are all odd or all even
				if (!(visibleSides.All(c => c.IsOdd()) || visibleSides.All(c => c.IsEven())))
				{
					for (int side = 1; side <= N * 2; side++)
					{
						// Process for sides of coin that are visible
						if (visibleSides.Contains(side))
						{
							var possibleObverses = visibleSides.Where(c => c.IsOdd() == side.IsEven());
							foreach (int possibleObverse in possibleObverses)
							{
								if (side.IsOdd())
								{
									coinMatrix[side][possibleObverse] = false;
								}
								else
								{
									coinMatrix[possibleObverse][side] = false;
								}
							}
						}
						else
						{
							// Process for sides of coin that are face-down
							for (int notItem = 1; notItem <= N * 2; notItem++)
							{
								if (side != notItem && notItem.IsOdd() == side.IsEven() && !visibleSides.Contains(notItem))
								{
									int possibleObverse = notItem;
									if (side.IsOdd())
									{
										coinMatrix[side][possibleObverse] = false;
									}
									else
									{
										coinMatrix[possibleObverse][side] = false;
									}
								}
							}

						}
					}
				}
			}

			string result = "";
			for (int x = 1; x < N * 2; x += 2)
			{
				result += $" {coinMatrix[x].Where(c => c.Value).Single().Key}";
			}
			return result.Trim();
		}
	}


	public class ACoinGuessingGameTests
	{
		[Theory]
		[InlineData(new string[] {"2 3", "4 2", "2 4", "4 3"}, "4 2")]
		[InlineData(new string[] {"3 2", "3 1 6", "4 1 6"}, "2 4 6")]
		[InlineData(new string[] {"5 4", "2 10 6 3 5", "5 1 9 3 2", "9 8 10 4 7", "4 1 9 5 2" }, "10 4 8 2 6")]
		public void CoinGuess_ShouldBe_Correct(string[] inputs, string expected)
		{
			Assert.Equal(ACoinGuessingGameSolution.ACoinGuessingGame(inputs), expected);
		}
	}
}
