using System;

using NUnit.Framework;

namespace CodeWarsKatas
{
	/*
     * https://www.codewars.com/kata/financing-a-purchase
     * 
	 * The description is rather long but it tries to explain what a financing plan is.
	 * 
	 * The fixed monthly payment for a fixed rate mortgage is the amount paid by the borrower every month that ensures that the loan is paid off in full with interest at the end of its term.
	 * 
	 * The monthly payment formula is based on the annuity formula. The monthly payment c depends upon:
	 * 
	 * rate - the monthly interest rate is expressed as a decimal, not a percentage. The monthly rate is simply the given yearly percentage
	 * rate divided by 100 and then by 12.
	 * 
	 * term - the number of monthly payments, called the loan's term.
	 * 
	 * principal - the amount borrowed, known as the loan's principal (or balance).
	 * 
	 * First we have to determine c.
	 * 
	 * We have: c = n /d with n = r * balance and d = 1 - (1 + r)**(-term) where ** is the power function (you can look at the reference below).
	 * 
	 * The payment c is composed of two parts. The first part pays the interest (let us call it int) due for the balance of the given
	 * month, the second part repays the balance (let us call this part princ) hence for the following month we get a new balance = old
	 * balance - princ with c = int + princ.
	 * 
	 * Loans are structured so that the amount of principal returned to the borrower starts out small and increases with each mortgage
	 * payment. While the mortgage payments in the first years consist primarily of interest payments, the payments in the final years consist
	 * primarily of principal repayment.
	 * 
	 * A mortgage's amortization schedule provides a detailed look at precisely what portion of each mortgage payment is dedicated to each
	 * component.
	 * 
	 * In an example of a $100,000, 30-year mortgage with a rate of 6 percents the amortization schedule consists of 360 monthly payments. The
	 * partial amortization schedule below shows with 2 decimal floats the balance between principal and interest payments.
	 * 
	 * --	num_payment	c	princ	int	Balance
	 * --	1	599.55	99.55	500.00	99900.45
	 * --	...	599.55	...	...	...
	 * --	12	599.55	105.16	494.39	98,771.99
	 * --	...	599.55	...	...	...
	 * --	360	599.55	596.57	2.98	0.00
	 *
	 * Task:
	 * Given parameters
	 * 
	 * rate: annual rate as percent (don't forgent to divide by 100*12)
	 * bal: original balance (borrowed amount) 
	 * term: number of monthly payments
	 * num_payment: rank of considered month (from 1 to term)
	 * the function amort will return a formatted string:
	 * 
	 * "num_payment %d c %.0f princ %.0f int %.0f balance %.0f" (with arguments num_payment, c, princ, int, balance)
	 * 
	 * In Common Lisp: return a list with num-payment, c, princ, int, balance each rounded.
	 * 
	 * Examples:
	 * amort(6, 100000, 360, 1) ->
	 * "num_payment 1 c 600 princ 100 int 500 balance 99900"
	 * 
	 * amort(6, 100000, 360, 12) ->
	 * "num_payment 12 c 600 princ 105 int 494 balance 98772"
	 * 
	 */

	public static class FinancingAPurchase
	{
		public static string Amort(double rate, int bal, int term, int num_payments)
		{
			double princ = 0;
			
			double balance = bal;
			double r = rate / (100 * 12);

			double n = r * balance;
			double d = 1 - Math.Pow((1 + r), -term);
			double c = n / d;

			for (int i = 1; i <= num_payments; i++)
			{
				n = r * balance;
				princ = c - n;
				balance -= princ;
			}

			return $"num_payment {num_payments} c {c:0} princ {princ:0} int {n:0} balance {balance:0}";
		}

	}

	[TestFixture]
	public class FinancingAPurchaseTests
	{
		private static void Testing(double rate, int bal, int term, int num_payments, string expected)
		{
			string ans = FinancingAPurchase.Amort(rate, bal, term, num_payments);
			Console.WriteLine("rate: {0}, bal: {1}, term: {2}, num_payments: {3} expected: {4}",
				rate, bal, term, num_payments, expected);
			Assert.AreEqual(expected, ans);
		}
		[Test]
		public static void Test1()
		{
			Testing(7.4, 10215, 24, 20, "num_payment 20 c 459 princ 445 int 14 balance 1809");
			Testing(7.9, 107090, 48, 41, "num_payment 41 c 2609 princ 2476 int 133 balance 17794");
			Testing(6.8, 105097, 36, 4, "num_payment 4 c 3235 princ 2685 int 550 balance 94447");
			Testing(3.8, 48603, 24, 10, "num_payment 10 c 2106 princ 2009 int 98 balance 28799");
			Testing(1.9, 182840, 48, 18, "num_payment 18 c 3959 princ 3769 int 189 balance 115897");
			Testing(1.9, 19121, 48, 2, "num_payment 2 c 414 princ 384 int 30 balance 18353");
			Testing(2.2, 112630, 60, 11, "num_payment 11 c 1984 princ 1810 int 174 balance 92897");
			Testing(5.6, 133555, 60, 53, "num_payment 53 c 2557 princ 2464 int 93 balance 17571");
			Testing(9.8, 67932, 60, 34, "num_payment 34 c 1437 princ 1153 int 283 balance 33532");
			Testing(3.7, 64760, 36, 24, "num_payment 24 c 1903 princ 1829 int 75 balance 22389");
		}
	}
}
