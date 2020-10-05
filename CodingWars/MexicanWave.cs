using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace CodeWarsKatas
{
	/// <summary>
	/// https://www.codewars.com/kata/58f5c63f1e26ecda7e000029/train/csharp
	/// </summary>
	/*
	* Introduction
	* ------------
	* The wave (known as the Mexican wave in the English-speaking world outside North America) is an example 
	* of metachronal rhythm achieved in a packed stadium when successive groups of spectators briefly stand, 
	* yell, and raise their arms. Immediately upon stretching to full height, the spectator returns to the 
	* usual seated position.
	* 
	* The result is a wave of standing spectators that travels through the crowd, even though individual 
	* spectators never move away from their seats. In many large arenas the crowd is seated in a contiguous 
	* circuit all the way around the sport field, and so the wave is able to travel continuously around the 
	* arena; in discontiguous seating arrangements, the wave can instead reflect back and forth through the 
	* crowd. When the gap in seating is narrow, the wave can sometimes pass through it. Usually only one wave 
	* crest will be present at any given time in an arena, although simultaneous, counter-rotating waves have 
	* been produced. (Source Wikipedia)
	* 
	* Task
	* ----
	* In this simple Kata your task is to create a function that turns a string into a Mexican Wave. You will 
	* be passed a string and you must return that string in an array where an uppercase letter is a person 
	* standing up. 
	* 
	* Rules
	* -----
	*  1.  The input string will always be lower case but maybe empty.
	* 
	*  2.  If the character in the string is whitespace then pass over it as if it was an empty seat
	* 
	* Example
	* -------
	* 
	* wave("hello") => []string{"Hello", "hEllo", "heLlo", "helLo", "hellO"}
	* 
	* Good luck and enjoy!
	 * 
	 */
	public class MexicanWave
	{
		public List<string> Wave(string str)
		{
			List<string> wave = new List<string>();

			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] != ' ')
				{
					wave.Add(str.Substring(0, i) + Char.ToUpper(str[i]) + str.Substring(i + 1));
				}
			}

			return wave;
		}
	}
	[TestFixture]
	class MexicanWaveTests
	{
		[TestCase]
		public void BasicTest1()
		{
			MexicanWave kata = new MexicanWave();
			List<string> result = new List<string> { "Hello", "hEllo", "heLlo", "helLo", "hellO" };
			Assert.AreEqual(result, kata.Wave("hello"), "it should return '" + result + "'");
		}

		[TestCase]
		public void BasicTest2()
		{
			MexicanWave kata = new MexicanWave();
			List<string> result = new List<string> { "Codewars", "cOdewars", "coDewars", "codEwars", "codeWars", "codewArs", "codewaRs", "codewarS" };
			Assert.AreEqual(result, kata.Wave("codewars"), "it should return '" + result + "'");
		}

		[TestCase]
		public void BasicTest3()
		{
			MexicanWave kata = new MexicanWave();
			List<string> result = new List<string> { };
			Assert.AreEqual(result, kata.Wave(""), "it should return '" + result + "'");
		}

		[TestCase]
		public void BasicTest4()
		{
			MexicanWave kata = new MexicanWave();
			List<string> result = new List<string> { "Two words", "tWo words", "twO words", "two Words", "two wOrds", "two woRds", "two worDs", "two wordS" };
			Assert.AreEqual(result, kata.Wave("two words"), "it should return '" + result + "'");
		}

		[TestCase]
		public void BasicTest5()
		{
			MexicanWave kata = new MexicanWave();
			List<string> result = new List<string> { " Gap ", " gAp ", " gaP " };
			Assert.AreEqual(result, kata.Wave(" gap "), "it should return '" + result + "'");
		}

	}
}
