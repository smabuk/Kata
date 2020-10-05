using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace CodeWarsKatas
{
    public static class BackSpacesInString
    {

		public static string CleanString(string s)
		{

			string output = "";

			foreach (char ch in s)
			{
				if (ch != '#')
				{
					output += ch;
				}
				else if (output.Length > 0)
				{
					output = output.Remove(output.Length - 1, 1);
				}
			}

			return output;
		}
	}

	[TestFixture]
	public class BackSpacesInStringSolutionTest
	{
		[Test]
		public void BackSpacesInStringTests()
		{
			Assert.AreEqual("ac", BackSpacesInString.CleanString("abc#d##c"));
			Assert.AreEqual("", BackSpacesInString.CleanString("abc####d##c#"));
			Assert.AreEqual("", BackSpacesInString.CleanString("#######"));
		}
	}

}
