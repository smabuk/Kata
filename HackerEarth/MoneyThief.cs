using System;

using Xunit;

/*
 * https://www.hackerearth.com/problem/algorithm/money-thief/description/
 *
 * The burglar has found himself a new place for his Thievery again. They can only enter this area through a special house called "root". Besides the root, each house has one and only one parent house. After a tour, the smart thief realized that all houses in this place forms a binary tree. It will automatically contact the police if two directly-linked houses were broken into on the same night.
 * 
 * Determine the maximum amount of money the thief can rob tonight without alerting the police.
 * 
 * Input:
 * A line containing an integer T, the no. of Test Cases.
 * First Line of each test case contain an integer N, the no. of Nodes in the tree.
 * Next line containing N integers, the values on the nodes.where '-1' indicates a NULL node.
 * 
 * 
 * Output:
 * -------
 * Output for each Test Case the maximum amount of money the thief can rob tonight modulo 10^9+7 in one Line.
 * 
 * Constraints:
 * ------------
 * 1<= T <= 100
 * 1<= N <= 5000
 * 1<= A[i] <=10^9
 * 
 * SAMPLE INPUT                                      SAMPLE OUTPUT
 * 2                                                 7
 * 7                                                 9
 * 3 2 3 -1 3 -1 1
 * 7
 * 3 4 5 1 3 -1 1
 *
 * 
 * Explanation
 * -----------
 * In 1st test case:
 *       3*
 *     /   \
 *    2     3
 *    \      \
 *     3*     1*
 * 
 * Maximum amount of money the thief can rob = 3 + 3 + 1 = 7.
 * 
 * 
 * In 2nd test case:
 * 
 * 
 *       3
 *     /   \
 *    4*    5*
 *   / \      \ 
 *  1   3      1 
 * 
 *  Maximum amount of money the thief can rob = 4 + 5 = 9.
 * 
 * Time Limit:	1.0 sec(s) for each input file.
 * Memory Limit:	256 MB
 * Source Limit:	1024 KB
 *
 */
namespace HackerEarth
{
	public class House
	{
		public long Value;
		public House Left = null;
		public House Right = null;
		public bool LeftHouseSet = false;
		public bool RightHouseSet = false;

		public long TotalValue;
		public long LeftTotalValue;
		public long RightTotalValue;

		public House(long value)
		{
			Value = value;
		}
	}

	public class MoneyThief
	{
		public static long[] MoneyThievery(string[] lineInputs)
		{
			// int T = int.Parse(Console.ReadLine());
			int T = int.Parse(lineInputs[0]);
			long[] stolen = new long[T];

			for (int i = 0; i < T; i++)
			{
				// int N = int.Parse(Console.ReadLine());
				// string[] tree = Console.ReadLine().Split(' ');

				int startLine = 1 + (2 * i);
				int N = int.Parse(lineInputs[startLine]);

				string[] tree = lineInputs[startLine + 1].Split(' ');

				House root = new House(long.Parse(tree[0]));
				House current = root;
				// House parent;
				for (int treeI = 0; treeI < tree.Length; treeI++)
				{
					if (!current.LeftHouseSet)
					{
						current.Left = new House(long.Parse(tree[treeI]));
						current.LeftHouseSet = true;
						continue;
					}
					if (!current.RightHouseSet)
					{
						current.Right = new House(long.Parse(tree[treeI]));
						current.RightHouseSet = true;
						current = FindNextFreeHouse(ref root);
						continue;
					}

				}


				stolen[i] = N; // ToDo: replace with result
			}

			for (int i = 0; i < stolen.Length; i++)
			{
				Console.WriteLine(stolen[i]);        // Writing output to STDOUT
			}

			return stolen;
		}

		private static House FindNextFreeHouse(ref House root) {
			House current = root;
			int level = 0;



			return current;
		}

		[Theory]
		[InlineData(new string[] { "1", "7", "3 2 3 -1 3 -1 1" }, new long[] { 7 })]
		[InlineData(new string[] { "1", "7", "3 4 5 1 3 -1 1" }, new long[] { 9 })]
		[InlineData(new string[] { "2", "7", "3 2 3 -1 3 -1 1", "7", "3 4 5 1 3 -1 1" }, new long[] { 7, 9 })]
		public void MoneyThief_StealsExpectedAmount(string[] inputs, long[] expected)
		{
			Assert.Equal(MoneyThievery(inputs), expected);
		}
	}
}
