using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/chuck-norris
 * 
 * 
 * 
 * 
 * 
 */

namespace CodinGame
{
    public static class ChuckNorrisSolution
    {
		public static string ChuckNorrisMain(string[] args) {
			//string MESSAGE = Console.ReadLine();
			string MESSAGE = args[0];

			ASCIIEncoding ascii = new ASCIIEncoding();
			Byte[] encodedBytes = ascii.GetBytes(MESSAGE);
			string binaryString = "";
			foreach (byte b in encodedBytes) {
				binaryString += Convert.ToString(b, 2).PadLeft(7, '0');
			}

			string output = "";
			int bitCount = 0;
			char prevBit = binaryString[0];
			foreach (char bit in binaryString) {
				if (bit != prevBit) {
					output += " " + (prevBit == '1' ? "0" : "00") + " " + new string('0', bitCount);
					prevBit = bit;
					bitCount = 1;
				} else {
					bitCount++;
				}
			}
			output += " " + (prevBit == '1' ? "0" : "00") + " " + new string('0', bitCount);

			output = output.Trim();


			//Console.WriteLine("answer");
			return output;

		}

	}
	public class ChuckNorrisTests {
		[Theory]
		[InlineData(new string[] {
			"C",
		}
		, "0 0 00 0000 0 00")]
		[InlineData(new string[] {
			"CC",
		}
		, "0 0 00 0000 0 000 00 0000 0 00")]
		[InlineData(new string[] {
			"%",
		}
		, "00 0 0 0 00 00 0 0 00 0 0 0")]
		[InlineData(new string[] {
			"Chuck Norris' keyboard has 2 keys: 0 and white space.",
		}
		, "0 0 00 0000 0 0000 00 0 0 0 00 000 0 000 00 0 0 0 00 0 0 000 00 000 0 0000 00 0 0 0 00 0 0 00 00 0 0 0 00 00000 0 0 00 00 0 000 00 0 0 00 00 0 0 0000000 00 00 0 0 00 0 0 000 00 00 0 0 00 0 0 00 00 0 0 0 00 00 0 0000 00 00 0 00 00 0 0 0 00 00 0 000 00 0 0 0 00 00000 0 00 00 0 0 0 00 0 0 0000 00 00 0 0 00 0 0 00000 00 00 0 000 00 000 0 0 00 0 0 00 00 0 0 000000 00 0000 0 0000 00 00 0 0 00 0 0 00 00 00 0 0 00 000 0 0 00 00000 0 00 00 0 0 0 00 000 0 00 00 0000 0 0000 00 00 0 00 00 0 0 0 00 000000 0 00 00 00 0 0 00 00 0 0 00 00000 0 00 00 0 0 0 00 0 0 0000 00 00 0 0 00 0 0 00000 00 00 0 0000 00 00 0 00 00 0 0 000 00 0 0 0 00 00 0 0 00 000000 0 00 00 00000 0 0 00 00000 0 00 00 0000 0 000 00 0 0 000 00 0 0 00 00 00 0 0 00 000 0 0 00 00000 0 000 00 0 0 00000 00 0 0 0 00 000 0 00 00 0 0 0 00 00 0 0000 00 0 0 0 00 00 0 00 00 00 0 0 00 0 0 0 00 0 0 0 00 00000 0 000 00 00 0 00000 00 0000 0 00 00 0000 0 000 00 000 0 0000 00 00 0 0 00 0 0 0 00 0 0 0 00 0 0 000 00 0")]
		public void ChuckNorris_ShouldBe_Correct(string[] inputs, string expected) {
			Assert.Equal(expected, ChuckNorrisSolution.ChuckNorrisMain(inputs));
		}
	}
}
