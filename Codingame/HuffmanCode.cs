using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Diagnostics;

/*
 * https://www.codingame.com/ide/puzzle/huffman-code
 *
 * Huffman Coding is an algorithm for doing data compression and it forms the basic idea behind file compression.
 * Instead of allowing every character to occupy 8 bits in a file, we use variable-length encoding to assign each
 * symbol a unique binary code according to the frequency of the character in the file, without any ambiguities.
 * 
 * To put this into perspective:
 * 	Suppose a file contains a string “aabacdeade”, where frequency of characters 
 * 	a, b, c, d and e is 4, 1, 1, 2 and 2 respectively.
 * 	We assign binary codes to each character as follows:
 * 	a --> 00	  b --> 010	  c --> 011	  d--> 10	  e--> 11 
 * 
 * The process of encoding can be divided into two parts:
 * 
 * Part 1: Building a Huffman tree
 * -------------------------------
 * First, assume all of the characters as individual trees with frequency as their weight.
 * Now, we use a greedy approach to find the two trees with the smallest weights.
 * Then, join them to create a new tree with the sum of those two as its weight and repeat
 * this process until we have a single tree remaining.
 * 
 * For the above example:
 * 
 * Step 1:   [a] [d] [e]    #         --> Here: a = 4, d = 2, e = 2, (bc) = 2
 *                         / \
 *                       [b] [c]
 * 
 * Step 2:   [a]    #          #      --> Here: a = 4, (bc) = 2, (de) = 4
 *                 / \        / \
 *               [b] [c]    [d] [e]
 * 
 * Step 3:       #            #       --> Here: (de) = 4, (a(bc)) = 6
 *              / \          / \
 *            [a]  #       [d] [e]
 *                / \
 *              [b] [c]
 * 
 * Step 4:        #                   --> Here: ((de)(a(bc))) = 10
 *               / \   
 *              /   \  
 *             /     \   
 *            #       #  
 *           / \     / \
 *        [a]   #  [d] [e]
 *             / \
 *           [b] [c]
 * 
 * 
 * Part 2: Assigning binary codes to each symbol by traversing Huffman tree
 * ------------------------------------------------------------------------
 * Generally, bit ‘0’ represents the left child and bit ‘1’
 * represents the right child
 * 
 *                    #                          
 *                 0 / \ 1  
 *                  /   \  
 *                 /     \
 *                /       \
 *               #         #
 *            0 / \ 1   0 / \ 1
 *             /   \     /   \
 *          [a]     #  [d]   [e]
 *               0 / \ 1
 *                /   \
 *              [b]   [c]
 * 
 * Thus by going through the tree, we will come up with
 * a = 00, b = 010, c = 011, d = 10, e = 11
 * 
 * 
 * Test Case 1
 * -----------
 * n = 5
 * frequencies = 4 1 1 2 2
 * bit length for each test case in order = 2 3 3 2 2 [see above for clarification]
 * total bit count = 4 * 2 + 1 * 3 + 1* 3 + 2 * 2 + 2 * 2 = 22
 * 
 * Output : 22
 * 
 * 
 * Here are some links to understand it further:
 * ---------------------------------------------
 * https://www.geeksforgeeks.org/huffman-coding-greedy-algo-3/
 * https://www.studytonight.com/data-structures/huffman-coding
 * 
 * 
 * 
 * Input
 * -----
 * Line 1: A single integer N representing the number of characters
 * Line 2: An ordered sequence of frequency values separated by space, where
 * 1 2 3 ... N represents char1 = 1, char2 = 2, char3 = 3 ... charN = N
 * 
 * Output
 * ------
 * A single value representing the *least number of bits* used to store the complete file.
 * 
 * Constraints
 * -----------
 * 1 ≤ N ≤ 3000
 * 
 * 
 * Example
 * -------
 * 
 * Input                  Output
 * -----                  ------
 * 5                      22
 * 4 1 1 2 2
 * 
 *  
 */

namespace CodinGame
{

	static class HuffmanCodeSolution
	{
		[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
		public class HuffmanNode
		{
			public string Character { get; set; } = null;
			public int Frequency { get; set; } = 0;
			public HuffmanNode Left { get; set; } = null;
			public HuffmanNode Right { get; set; } = null;

			public HuffmanNode(string character, int frequency)
			{
				Character = character;
				Frequency = frequency;
			}

			public HuffmanNode(string character, int frequency, HuffmanNode left, HuffmanNode right)
				: this(character, frequency)
			{
				Left = left;
				Right = right;
			}

			public int CalculateValue(int level)
			{
				int newLevel = level + 1;
				if (this.Left == null && this.Right == null)
				{
					return this.Frequency * (level == 0 ? 1 : level);
				}
				int value = 0;
				if (this.Left != null)
				{
					value += this.Left.CalculateValue(newLevel);
				}
				if (this.Right != null)
				{
					value += this.Right.CalculateValue(newLevel);
				}
				return value;
			}

			private string GetDebuggerDisplay()
			{
				return $"{Character} {Frequency} {(Left == null ? "null" : "left")} {(Right == null ? "null" : "right")}";
			}
		}

		public static long HuffmanCode(string[] args)
		{

			List<HuffmanNode> huffmanNodes = new List<HuffmanNode>();

			//int n = int.Parse(Console.ReadLine());
			int n = int.Parse(args[0]);
			//string[] inputs = Console.ReadLine().Split(' ');
			string[] inputs = args[1].Split(' ');

			for (int i = 0; i < n; i++)
			{
				int wi = int.Parse(inputs[i]);
				huffmanNodes.Add(new HuffmanNode($"char{i + 1:D4}", wi));
			}

			while (huffmanNodes.Count > 1)
			{
				List<HuffmanNode> newHuffNodes = huffmanNodes.OrderBy(n => n.Frequency).ThenBy(n => n.Character).Take(2).ToList();
				if (newHuffNodes.Count == 2)
				{
					huffmanNodes.Add(new HuffmanNode("xxxx", newHuffNodes[0].Frequency + newHuffNodes[1].Frequency, newHuffNodes[0],  newHuffNodes[1]));
					huffmanNodes.Remove(newHuffNodes[0]);
					huffmanNodes.Remove(newHuffNodes[1]);
				}
			}
			HuffmanNode root = huffmanNodes.SingleOrDefault();
			return root.CalculateValue(0);
		}

	}

	public class HuffmanCodeTests
	{
		[Theory]
		[InlineData("Test 1", new string[] {
			"5",
			"4 1 1 2 2"
			}
			, 22)]
		[InlineData("Test 2", new string[] {
			"1",
			"54"
			}
			, 54)]
		[InlineData("Test 3", new string[] {
			"50",
			"171 57 128 30 23 151 77 58 198 218 97 91 79 87 82 103 158 28 208 182 145 200 17 166 149 17 45 42 170 51 99 151 144 222 98 194 27 112 75 36 16 134 105 64 103 157 223 211 132 95"
			}
			, 30519)]
		[InlineData("Test 4", new string[] {
			"100",
			"94 257 263 127 283 183 17 34 224 86 175 105 217 74 172 200 211 241 291 154 123 168 31 129 16 125 68 135 236 267 182 43 265 255 160 23 205 188 202 116 116 12 94 219 257 284 138 34 177 238 116 221 249 167 300 130 45 102 91 80 33 215 278 41 239 175 72 81 198 232 175 101 47 200 271 136 201 154 237 266 139 105 235 65 48 103 248 76 157 128 294 249 157 261 53 109 74 111 30 66"
			}
			, 99737)]
		[InlineData("Test 5", new string[] {
			"1001",
			"1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100 101 102 103 104 105 106 107 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 126 127 128 129 130 131 132 133 134 135 136 137 138 139 140 141 142 143 144 145 146 147 148 149 150 151 152 153 154 155 156 157 158 159 160 161 162 163 164 165 166 167 168 169 170 171 172 173 174 175 176 177 178 179 180 181 182 183 184 185 186 187 188 189 190 191 192 193 194 195 196 197 198 199 200 201 202 203 204 205 206 207 208 209 210 211 212 213 214 215 216 217 218 219 220 221 222 223 224 225 226 227 228 229 230 231 232 233 234 235 236 237 238 239 240 241 242 243 244 245 246 247 248 249 250 251 252 253 254 255 256 257 258 259 260 261 262 263 264 265 266 267 268 269 270 271 272 273 274 275 276 277 278 279 280 281 282 283 284 285 286 287 288 289 290 291 292 293 294 295 296 297 298 299 300 301 302 303 304 305 306 307 308 309 310 311 312 313 314 315 316 317 318 319 320 321 322 323 324 325 326 327 328 329 330 331 332 333 334 335 336 337 338 339 340 341 342 343 344 345 346 347 348 349 350 351 352 353 354 355 356 357 358 359 360 361 362 363 364 365 366 367 368 369 370 371 372 373 374 375 376 377 378 379 380 381 382 383 384 385 386 387 388 389 390 391 392 393 394 395 396 397 398 399 400 401 402 403 404 405 406 407 408 409 410 411 412 413 414 415 416 417 418 419 420 421 422 423 424 425 426 427 428 429 430 431 432 433 434 435 436 437 438 439 440 441 442 443 444 445 446 447 448 449 450 451 452 453 454 455 456 457 458 459 460 461 462 463 464 465 466 467 468 469 470 471 472 473 474 475 476 477 478 479 480 481 482 483 484 485 486 487 488 489 490 491 492 493 494 495 496 497 498 499 500 500 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100 101 102 103 104 105 106 107 108 109 110 111 112 113 114 115 116 117 118 119 120 121 122 123 124 125 126 127 128 129 130 131 132 133 134 135 136 137 138 139 140 141 142 143 144 145 146 147 148 149 150 151 152 153 154 155 156 157 158 159 160 161 162 163 164 165 166 167 168 169 170 171 172 173 174 175 176 177 178 179 180 181 182 183 184 185 186 187 188 189 190 191 192 193 194 195 196 197 198 199 200 201 202 203 204 205 206 207 208 209 210 211 212 213 214 215 216 217 218 219 220 221 222 223 224 225 226 227 228 229 230 231 232 233 234 235 236 237 238 239 240 241 242 243 244 245 246 247 248 249 250 251 252 253 254 255 256 257 258 259 260 261 262 263 264 265 266 267 268 269 270 271 272 273 274 275 276 277 278 279 280 281 282 283 284 285 286 287 288 289 290 291 292 293 294 295 296 297 298 299 300 301 302 303 304 305 306 307 308 309 310 311 312 313 314 315 316 317 318 319 320 321 322 323 324 325 326 327 328 329 330 331 332 333 334 335 336 337 338 339 340 341 342 343 344 345 346 347 348 349 350 351 352 353 354 355 356 357 358 359 360 361 362 363 364 365 366 367 368 369 370 371 372 373 374 375 376 377 378 379 380 381 382 383 384 385 386 387 388 389 390 391 392 393 394 395 396 397 398 399 400 401 402 403 404 405 406 407 408 409 410 411 412 413 414 415 416 417 418 419 420 421 422 423 424 425 426 427 428 429 430 431 432 433 434 435 436 437 438 439 440 441 442 443 444 445 446 447 448 449 450 451 452 453 454 455 456 457 458 459 460 461 462 463 464 465 466 467 468 469 470 471 472 473 474 475 476 477 478 479 480 481 482 483 484 485 486 487 488 489 490 491 492 493 494 495 496 497 498 499 500"
			}
			, 2439074)]
		[InlineData("Test 6", new string[] {
			"2800",
			"1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1"
			}
			, 32304)]
		public void HuffmanCode_ShouldBe_Correct(string testName, string[] args, long expected)
		{
			Console.WriteLine($"Name: {testName}");
			Assert.Equal(expected, HuffmanCodeSolution.HuffmanCode(args));
		}
	}
}
