using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace CodeWarsKatas
{
	public static class CountSmileysKata
	{
		public static int CountSmileys(string[] smileys)
		{
			List<string> validSmileys = new List<string> {
				":)",
				":D",
				";)",
				";D",
				":-)",
				":-D",
				";-)",
				";-D",
				":~)",
				":~D",
				";~)",
				";~D"
			};
			return smileys.Count(s => validSmileys.Contains(s));
		}

	}

	[TestFixture]
	public class CountSmileysSolutionTest
	{
		[Test]
		public void CountSmileysTest()
		{
			Assert.AreEqual(4, CountSmileysKata.CountSmileys(new string[] { ":D", ":~)", ";~D", ":)" }));
			Assert.AreEqual(2, CountSmileysKata.CountSmileys(new string[] { ":)", ":(", ":D", ":O", ":;" }));
			Assert.AreEqual(1, CountSmileysKata.CountSmileys(new string[] { ";]", ":[", ";*", ":$", ";-D" }));
			Assert.AreEqual(0, CountSmileysKata.CountSmileys(new string[] { ";", ")", ";*", ":$", "8-D" }));
		}
	}
}
