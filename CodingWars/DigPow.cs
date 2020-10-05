using System;
using System.Collections.Generic;
using System.Globalization;

using NUnit.Framework;

namespace CodeWarsKatas
{
	// https://www.codewars.com/kata/5552101f47fc5178b1000050/solutions/csharp
	public static class DigPow
	{
		public static long digPow(int n, int p)
		{
			List<int> numbers = new List<int>();
			foreach (char digit in n.ToString())
			{
				numbers.Add(int.Parse(digit.ToString()));
			}

			double nsum = 0;
			for (int i = 0, power = p; i < numbers.Count; i++, power++)
			{
				nsum += Math.Pow(numbers[i], power);
			}

			if (nsum % n == 0)
			{
				return (long)(nsum / n);
			};

			return -1;
		}
	}

	[TestFixture]
	public class DigPowTests
	{

		[Test]
		public void Test1()
		{
			Assert.AreEqual(1, DigPow.digPow(89, 1));
		}
		[Test]
		public void Test2()
		{
			Assert.AreEqual(-1, DigPow.digPow(92, 1));
		}
		[Test]
		public void Test3()
		{
			Assert.AreEqual(51, DigPow.digPow(46288, 3));
		}
	}


}
