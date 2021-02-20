using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/bulls-and-cows
 * 
 * 
 * 
 * 
 * 
 */

namespace CodinGame {
	public static class BullsAndCowsSolution {
		public static string BullsAndCowsMain(string[] args) {
			List<(string guess, int bulls, int cows)> turns = new List<(string guess, int bulls, int cows)>();
			List<char> impossibleChars = new List<char>();
			string answer = "";

			//int N = int.Parse(Console.ReadLine());
			int N = int.Parse(args[0]);
			for (int i = 0; i < N; i++) {
				//string[] inputs = Console.ReadLine().Split(' ');
				string[] inputs = args[i + 1].Split(' ');
				string guess = inputs[0];
				int bulls = int.Parse(inputs[1]);
				int cows = int.Parse(inputs[2]);
				if (bulls == guess.Length) {
					// Console.WriteLine(guess);
					return guess;
				}
				if (bulls + cows == 0) {
					impossibleChars = impossibleChars.Concat(guess.ToList()).Distinct().ToList();
				}
				turns.Add((guess, bulls, cows));
			}

			for (int i = 0; i <= 9999; i++) {
				answer = $"{i:0000}";

				if (AnswerContainsImpossibleChars(answer, impossibleChars)) {
					continue;
				}

				bool theAnswer = true;
				foreach ((string guess, int bulls, int cows) in turns) {
					(int bulls, int cows) result = CalculateBullsAndCows(guess, answer);
					if (result.bulls != bulls || result.cows != cows) {
						theAnswer = false;
						break;
					}
				}
				if (theAnswer) {
					break;
				}
			}

			// Console.WriteLine(answer);
			return answer;

		}

		static bool AnswerContainsImpossibleChars(string answer, List<char> impossibleChars) 
			=> answer.Any(c =>impossibleChars.Contains(c));

		static (int bulls, int cows) CalculateBullsAndCows(string guess, string answer) {
			int bulls = 0;
			int cows = 0;
			char[] answerChars = answer.ToCharArray();
			char[] guessChars = guess.ToCharArray();

			for (int i = 0; i < guess.Length; i++) {
				if (guess[i] == answer[i]) {
					bulls++;
					answerChars[i] = 'X';
					guessChars[i] = 'X';
				}
			}

			for (int i = 0; i < guess.Length; i++) {
				if (guessChars[i] == 'X') {
					continue;
				}
				if (Array.IndexOf(answerChars, guessChars[i]) >= 0) {
					cows++;
					answerChars[answer.IndexOf(guessChars[i])] = 'X';
					guessChars[i] = 'X';
				}
			}

			return (bulls, cows);
		}

	}
	public class BullsAndCowsTests {
		[Theory]
		[InlineData(new string[] {
			"1",
			"1234 4 0",
		}
		, "1234")]
		[InlineData(new string[] {
			"2",
			"0473 2 2",
			"7403 0 4",
		}
		, "0374")]
		[InlineData(new string[] {
			"3",
			"9073 2 0",
			"1248 2 0",
			"1043 0 0",
		}
		, "9278")]
		[InlineData(new string[] {
			"1",
			"7878 0 4",
		}
		, "8787")]
		[InlineData(new string[] {
			"4",
			"0123 1 0",
			"4567 0 0",
			"8901 1 0",
			"1110 3 0",
		}
		, "1111")]
		[InlineData(new string[] {
			"10",
			"1111 0 0",
			"2222 1 0",
			"3333 0 0",
			"4444 0 0",
			"5555 0 0",
			"6666 0 0",
			"7777 2 0",
			"8888 1 0",
			"2778 0 4",
			"7287 2 2",
		}
		, "7827")]
		public void BullsAndCows_ShouldBe_Correct(string[] inputs, string expected) {
			Assert.Equal(expected, BullsAndCowsSolution.BullsAndCowsMain(inputs));
		}
	}
}
