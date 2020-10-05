using System;

using NUnit.Framework;

namespace CodeWarsKatas
{
	public class smile67Kata
	{
		public string CalculateString(string calcIt)
		{
			string n1 = "";
			string n2 = "";
			string op = "";

			string validNumberChars = "1234567890.";
			string validOps = "+-/*";

			foreach (char ch in calcIt)
			{
				if (validNumberChars.Contains(ch))
				{
					if (string.IsNullOrEmpty(op))
					{
						n1 += ch;
					}
					else
					{
						n2 += ch;
					}
				}
				if (validOps.Contains(ch))
				{
					op += ch;
				}
			}


			double doubleResult = op switch
			{
				"+" => double.Parse(n1) + double.Parse(n2),
				"-" => double.Parse(n1) - double.Parse(n2),
				"/" => double.Parse(n1) / double.Parse(n2),
				"*" => double.Parse(n1) * double.Parse(n2),
				_ => throw new NotImplementedException()
			};
			string result = ((long)Math.Round(doubleResult)).ToString();
			return result;
		}

	}
	[TestFixture]

	public class smile67KataTest
	{
		[Test]
		public void smile67KataTest_withoutRandom1()
		{
			Assert.AreEqual("47", new smile67Kata().CalculateString(";$%§fsdfsd235??df/sdfgf5gh.000kk0000"));
		}
		[Test]
		public void smile67KataTest_withoutRandom2()
		{
			Assert.AreEqual("54929268", new smile67Kata().CalculateString("sdfsd23454sdf*2342"));
		}
		[Test]
		public void smile67KataTest_withoutRandom3()
		{
			Assert.AreEqual("-210908", new smile67Kata().CalculateString("fsdfsd235???34.4554s4234df-sdfgf2g3h4j442"));
		}
		[Test]
		public void smile67KataTest_withoutRandom4()
		{
			Assert.AreEqual("234676", new smile67Kata().CalculateString("fsdfsd234.4554s4234df+sf234442"));
		}
	}
}
