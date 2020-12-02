using System;
using System.Collections.Generic;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/prefix-code
 *
 * Goal
 * ----
 * 
 * Given a fixed set of characters, a code is a table that gives the encoding to use for each character.
 * 
 * A prefix code is a code with the prefix property, which is that there is no character with an encoding that
 * is a prefix (initial segment) of the encoding of another character.
 * 
 * 
 * Your goal is to decode an encoded string using the given prefix code, or say that is not possible.
 * 
 * Example of encoding.
 * Given the string "abracadabra" and the prefix code:
 * a -> 1
 * b -> 001
 * c -> 011
 * d -> 010
 * r -> 000
 * The resulting encoding is: 10010001011101010010001
 * 
 * Thus, if your are given the code above and the input 10010001011101010010001, you should output the string "abracadabra".
 * 
 * With the same prefix code, if the input is 0000, then you should tell that there is an error at index 3.
 * Indeed, the first three characters of this input can be decoded to give an 'r', but that leaves 0, which cannot be decoded.
 * 
 * 
 * -> External link:
 * 
 * https://en.wikipedia.org/wiki/Prefix_code
 * 
 * ->️ What's next?
 * 
 * Once you have solved this puzzle, you can continue the challenge by building efficient prefix codes:
 * https://www.codingame.com/training/medium/huffman-code
 * 
 * 
 * Input
 * -----
 * 
 * Line 1:       A single integer N representing the number of association in the prefix-code table.
 * Next N lines: A binary code Bi and an integer Ci, which tells that the character with ASCII code Ci will be encoded by Bi.
 * Next line:    The binary code S of an encoded string.
 * 
 * Output
 * -------
 * 
 * - If it is not possible to decode the encoded string, print DECODE FAIL AT INDEX i with i the first index in the encoded
 *   string where the decoding fails (index starts from 0).
 * - Otherwise print the decoded string.
 * 
 * Constraints
 * ------------
 * 
 * 0 ≤ N, C ≤ 127
 * S and the binary codes Bi have a length less that or equal to 5000.
 * 
 * Example
 * --------
 * 
 * Input                                    Output
 * -----                                    -------
 * 5                                        abracadabra
 * 1 97
 * 001 98
 * 000 114
 * 011 99
 * 010 100
 * 10010001011101010010001
 * 
 * 
 *  
 */

namespace CodinGame
{

	static class PrefixCodeSolution
	{

		public static string PrefixCode(string[] args)
		{
			Dictionary<string, char> prefixCodes = new Dictionary<string, char>();
			//int n = int.Parse(Console.ReadLine());
			int n = int.Parse(args[0]);

			for (int i = 0; i < n; i++)
			{
				//string[] inputs = Console.ReadLine().Split(' ');
				string[] inputs = args[i + 1].Split(' ');
				string b = inputs[0];
				int c = int.Parse(inputs[1]);
				prefixCodes.TryAdd(b, (char)c);
			}
			// string s = Console.ReadLine();
			string s = args[^1];

			string outputString = String.Empty;
			int index = 0;
			bool isValid = true;
			while (isValid && index < s.Length)
			{
				isValid = false;
				foreach (var pc in prefixCodes)
				{
					if (s[index..].StartsWith(pc.Key))
					{
						outputString += pc.Value;
						index += pc.Key.Length;
						isValid = true;
						break;
					}
				}
			}

			if (!isValid)
			{
				outputString = $"DECODE FAIL AT INDEX {index}";
			}
			return outputString;
		}
	}

	public class PrefixCodeTests
	{
		[Theory]
		[InlineData(new string[] {
			"5",
			"1 97",
			"001 98",
			"000 114",
			"011 99",
			"010 100",
			"10010001011101010010001"
			}
			, "abracadabra")]
		[InlineData(new string[] {
			"9",
			"011 32",
			"0011 33",
			"0010 114",
			"0001 100",
			"0000 101",
			"111 87",
			"110 72",
			"10 108",
			"010 111",
			"1100000101001001111101000101000010110011"
			}
			, "Hello World !")]
		[InlineData(new string[] {
			"18",
			"11 32",
			"1001 97",
			"000011 98",
			"000010 99",
			"0011 100",
			"011 101",
			"000001 102",
			"00101 104",
			"000000 73",
			"00100 105",
			"10111 108",
			"1000 110",
			"00011 111",
			"10110 114",
			"010 116",
			"10101 118",
			"00010 120",
			"10100 58",
			"0000001000101011001101110010000111110100110110001001010110100111000011001000101110010101101000101011110111000001111000110000011101000101011110111000000010000110011011001111010011000100101"
			}
			, "DECODE FAIL AT INDEX 186")]
		public void PrefixCode_ShouldBe_Correct(string[] args, string expected)
		{
			Assert.Equal(expected, PrefixCodeSolution.PrefixCode(args));
		}
	}
}
