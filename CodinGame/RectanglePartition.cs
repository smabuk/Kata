using System;
using System.Collections.Generic;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/rectangle-partition
 * 
 * 
 * 
 * 
 * 
 */

namespace CodinGame {
	public class RectanglePartitionSolution {

		public static int RectanglePartitionMain(string[] lineInputs) {
			string[] inputs;

			//inputs = Console.ReadLine().Split(' ');
			inputs = lineInputs[0].Split(' ');
			int w = int.Parse(inputs[0]);
			int h = int.Parse(inputs[1]);
			int countX = int.Parse(inputs[2]);
			int countY = int.Parse(inputs[3]);
			int count = 0;
			//inputs = Console.ReadLine().Split(' ');
			inputs = lineInputs[1].Split(' ');

			int[] xLines = new int[countX + 2];
			xLines[0] = 0;
			xLines[countX + 1] = w;
			for (int i = 0; i < countX; i++) {
				xLines[i + 1] = int.Parse(inputs[i]);
			}
			//inputs = Console.ReadLine().Split(' ');
			inputs = lineInputs[2].Split(' ');
			int[] yLines = new int[countY + 2];
			yLines[0] = 0;
			yLines[countY + 1] = h;
			for (int i = 0; i < countY; i++) {
				yLines[i + 1] = int.Parse(inputs[i]);
			}
			List<int> ySides = new List<int>();
			foreach (int y1 in yLines) {
				foreach (int y2 in yLines) {
					if (y2 >= y1) {
						continue;
					}
					ySides.Add(y1 - y2);
				}
			}

			foreach (int x1 in xLines) {
				foreach (int x2 in xLines) {
					if (x2 >= x1) {
						continue;
					}
					int side1 = x1 - x2;
					foreach (int ySide in ySides) {
						if (side1 == ySide) {
							count++;
						}
					}
				}
			}

			return count;

		}
	}
	public class RectanglePartitionTests {
		[Theory]
		[InlineData(new string[] { 
			"10 5 2 1",
			"2 5",
			"3" }
		, 4)]
		[InlineData(new string[] { 
			"9 9 2 2",
			"3 6",
			"3 6" }
		, 14)]
		[InlineData(new string[] {
			"200 100 20 10",
			"11 25 26 29 30 40 44 56 65 71 87 98 100 108 130 149 153 161 173 179",
			"1 11 16 17 19 37 38 53 65 69" }
		, 123)]
		[InlineData(new string[] {
			"20000 20000 180 180",
			"90 117 306 744 954 1005 1327 1604 2099 2167 2244 2272 2350 2466 2758 2816 2829 2860 2963 3040 3202 3265 3298 3600 4055 4158 4159 4272 4308 4325 4382 4595 4857 4894 4953 4955 5091 5145 5193 5797 5812 5839 5899 5912 5963 6044 6142 6161 6703 6802 6869 6950 7025 7028 7217 7669 7748 7804 8018 8114 8118 8200 8203 8422 8683 8692 8697 8711 8806 8839 8927 9006 9189 9266 9382 9391 9516 9534 9630 9764 9791 9853 9881 10088 10121 10271 10317 10327 10550 10559 10624 10944 11005 11135 11212 11331 11393 11449 11490 11764 11804 11875 11909 12011 12045 12091 12159 12398 12453 12559 12621 12693 13144 13196 13205 13262 13275 13323 13538 13608 14153 14291 14346 14518 14520 14560 14689 14765 14957 15042 15082 15382 15477 15539 15586 15612 15630 15719 15805 16101 16142 16300 16379 16563 16580 16685 16774 16919 17010 17056 17470 17482 17512 17520 17709 17722 17782 17800 17829 17945 18048 18071 18181 18279 18330 18492 18554 18653 18906 18938 19279 19294 19339 19587 19590 19672 19695 19730 19854 19950",
			"90 177 485 652 664 793 797 943 1151 1271 1506 1569 1650 1789 1906 2037 2077 2221 2241 2323 2354 2533 2588 2858 3039 3223 3360 3480 3483 3587 3635 3842 3919 3962 4356 4410 4563 4949 4997 5059 5074 5090 5175 5187 5256 5309 5354 5384 5848 5995 6374 6506 6516 6547 6563 6628 6632 6709 6910 6916 6947 6983 7203 7345 7602 7800 7844 7858 7955 8206 8213 8317 8400 8435 8488 8538 8580 8710 8813 8815 8887 8896 8913 9111 9124 9209 9317 9345 9350 9544 9593 9945 10125 10208 10225 10270 10522 10993 11014 11025 11118 11231 11481 11538 11742 11761 11856 11887 12078 12112 12325 12506 12601 12617 12620 12719 12763 12875 12917 13257 13403 13485 13488 13635 13727 13814 13863 13882 13921 13996 14027 14068 14139 14338 14410 14526 14563 14617 14620 14691 14763 14786 14978 15215 15730 15776 15941 15964 16094 16174 16182 16303 16309 16450 16458 16649 16703 17008 17143 17220 17330 17628 17877 18203 18390 18463 18486 18611 18692 18693 18729 18902 18978 19219 19299 19355 19373 19506 19520 19599" }
		, 18431)]
		public void RectanglePartition_ShouldBe_Correct(string[] inputs, int expected) {
			Assert.Equal(expected, RectanglePartitionSolution.RectanglePartitionMain(inputs));
		}
	}
}
