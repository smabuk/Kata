using System;
using System.Collections.Generic;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/1d-bush-fire
 * 
 * 
 * 
 * 
 * 
 */

namespace CodinGame {
	public static class BushFire1dSolution {

		public static int[] BushFire1dMain(string[] inputs) {

			List<int> counts = new List<int>();

			int N = int.Parse(inputs[0]);
			for (int i = 0; i < N; i++) {
				char[] line = inputs[i + 1].ToCharArray();

				int count = 0;
				for (int j = 0; j < line.Length; j++) {
					if (line[j] == 'f') {
						count++;
						line[j] = '.';
						if ((j + 1) < line.Length) {
							line[j + 1] = '.';
						}
						if ((j + 2) < line.Length) {
							line[j + 2] = '.';
						}
					}
				}

				//Console.WriteLine(count);
				counts.Add(count);
			}

			return counts.ToArray();

		}
	}
	public class BushFire1dTests {
		[Theory]
		[InlineData(new string[] {
			"2",
			"....f....f..",
			".......fff" }
		, new int[] { 2, 1 })]
		[InlineData(new string[] {
			"18",
			"f.f",
			"fff..ffff..",
			"..",
			"ffffff",
			"ff.ff",
			"ffff..ff.f",
			"fffff.ffff",
			"ffff.ffff",
			"ffff.ffff.ff",
			"ff.ff..fff",
			"ff.ff..ffff.f",
			"....",
			"f",
			"..ff",
			"fffff",
			"f..fff",
			"ff..",
			"f.f.f." }
		, new int[] { 1, 3, 0, 2, 2, 4, 4, 3, 4, 3, 4, 0, 1, 1, 2, 2, 1, 2 })]
		public void BushFire1d_ShouldBe_Correct(string[] inputs, int[] expected) {
			Assert.Equal(expected, BushFire1dSolution.BushFire1dMain(inputs));
		}
	}
}
