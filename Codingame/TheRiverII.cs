using System;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/the-river-ii-
 *
 * Goal
 * ----
 * 
 * A digital river is a sequence of numbers where every number is followed by the same number plus the sum of its digits. In such a 
 * sequence 123 is followed by 129 (since 1 + 2 + 3 = 6), which again is followed by 141.
 * 
 * We call a digital river, "river K", if it starts with the value K.
 * 
 * For example, river 7 is the sequence beginning with {7, 14, 19, 29, 40, 44, 52, ... } and river 471 is the sequence beginning with {471, 
 * 483, 498, 519, ... }.
 * 
 * Digital rivers can meet. This happens when two digital rivers share the same values. River 32 meets river 47 at 47, while river 471 
 * meets river 480 at 519.
 * 
 * Given a number decide, whether it can be a meeting point of two or more digital rivers. For example, it is easy to check that only river 
 * 20 contains the number 20 in its sequence (as a starting point).
 * 
 * (Idea : BIO'99)
 * 
 * Input
 * -----
 * Line 1: An integer r1.
 * 
 * Output
 * ------
 * Line 1 : YES if r1 can be a meeting points of two digital rivers, NO otherwise.
 * 
 * Constraints
 * -----------
 * 1 ≤ r1 < 100000
 * 
 * Example
 * 
 * Input
 * 20
 * Output
 * NO
 *  
 */

namespace Codingame
{

	static class TheRiverIISolution
	{

		public static bool TheRiverII(string[] args)
		{
			//int r1 = int.Parse(Console.ReadLine());
			int r1 = int.Parse(args[0]);

			for (int i = r1 - ((r1.ToString().Length) * 9); i < r1; i++)
			{
				if (i > 0)
				{
					if (r1 == i + SumOf(i))
					{
						Console.WriteLine("YES");
						return true;
					}
				}
			}

			Console.WriteLine("NO");
			return false;
		}


		public static int SumOf(int number)
		{
			int sum = 0;
			string numStr = number.ToString();

			for (int i = 0; i < numStr.Length; i++)
			{
				sum += int.Parse(numStr[i].ToString());
			}

			return sum;
		}

	}


	public class TheRiverIITests
	{
		[Theory]
		[InlineData(new string[] {"20"} , false)]
		[InlineData(new string[] {"13"} , true)]
		[InlineData(new string[] { "91004" } , false)]
		[InlineData(new string[] { "90000" } , true)]
		public void TheRiverII_ShouldBe_Correct(string[] args, bool expected)
		{
			Assert.Equal(expected, TheRiverIISolution.TheRiverII(args));
		}

		[Theory]
		[InlineData(1234, 10)]
		[InlineData(123, 6)]
		public void Valid_SumOf(int number, int expected)
		{
			Assert.Equal(expected, TheRiverIISolution.SumOf(number));
		}
	}
}
