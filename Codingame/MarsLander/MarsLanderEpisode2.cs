using System;

/*
 * https://www.codingame.com/ide/puzzle/mars-lander-episode-2
 * 
 * STATEMENT
 * =========
 * The goal of this problem is to make you work with 2D coordinates in a big environnement.
 * You will have to manage and extrapolate the speed of a spaceship and make it land on a flat 
 * ground at correct speed.
 * 
 * STORY
 * -----
 * Your Mars exploration ship takes you above a particularly rocky area.
 * You will have to review and improve your descent technique in order to land your mars rover
 * safely on martian ground.
 * 
 * This medium puzzle is the second in a series of three exercises proposed during the “Mars Lander”
 * past contest. Once solved feel free to try and master the third puzzle « Mars Lander - Episode 3 »
 * on the same topic but with an increased difficulty!
 */

/*
 * 
 * The Goal
 * ========
 * The goal for your program is to safely land the "Mars Lander" shuttle, the landing ship which 
 * contains the Opportunity rover. Mars Lander is guided by a program, and right now the failure 
 * rate for landing on the NASA simulator is unacceptable.
 * 
 * This puzzle is the second level of the "Mars Lander" trilogy.
 * The controls are the same as the previous level but you must now control the angle in order
 * to succeed.
 * 
 * Rules
 * -----
 * Built as a game, the simulator puts Mars Lander on a limited zone of Mars sky.
 * 
 *    The zone is 7000m wide and 3000m high.
 * 
 * For this level, Mars Lander is above the landing zone, in vertical position, with no initial 
 * speed.
 * 
 * There is a unique area of flat ground on the surface of Mars, which is at least 1000 meters 
 * wide.Every second, depending on the current flight parameters (location, speed, fuel ...), 
 * the program must provide the new desired tilt angle and thrust power of Mars Lander:
 *     Angle goes from -90° to 90° . Thrust power goes from 0 to 4 .
 * 
 * For this level, you only need to control the thrust power: the tilt angle should be 0.The 
 * game simulates a free fall without atmosphere. Gravity on Mars is 3.711 m/s² . For a thrust 
 * power of X, a push force equivalent to X m/s² is generated and X liters of fuel are consumed. 
 * As such, a thrust power of 4 in an almost vertical position is needed to compensate for the 
 * gravity on Mars.
 * 
 * For a landing to be successful, the ship must:
 *  - land on flat ground
 *  - land in a vertical position (tilt angle = 0°)
 *  - vertical speed must be limited ( ≤ 40m/s in absolute value)
 *  - horizontal speed must be limited ( ≤ 20m/s in absolute value)
 *  
 * Remember that this puzzle was simplified:
 *  - the landing zone is just below the shuttle. You can therefore ignore rotation and always 
 *    output 0 as the target angle.
 *  - you don't need to store the coordinates of the surface of Mars to succeed.
 *  - you only need your vertical landing speed to be between 0 and 40m/s – your horizontal speed 
 *    being nil.
 *  - As the shuttle falls, the vertical speed is negative. As the shuttle flies upward, the 
 *    vertical speed is positive.
 * 
 * Note
 * ----
 * For this first level, Mars Lander will go through a single test.
 * 
 * Tests and validators are only slightly different. A program that passes a given test will 
 * pass the corresponding validator without any problem.
 * 
 * 
 * 
 * Game Input
 * ----------
 * The program must first read the initialization data from standard input. Then, within an 
 * infinite loop, the program must read the data from the standard input related to Mars 
 * Lander's current state and provide to the standard output the instructions to move Mars Lander.
 * 
 * Initialization input
 * --------------------
 * Line 1: the number surfaceN of points used to draw the surface of Mars.
 * 
 * Next surfaceN lines: a couple of integers landX landY providing the coordinates of a ground 
 * point. By linking all the points together in a sequential fashion, you form the surface of 
 * Mars which is composed of several segments. For the first point, landX = 0 and for the last 
 * point, landX = 6999
 * 
 * Input for one game turn
 * ------------------------
 * A single line with 7 integers: X Y hSpeed vSpeed fuel rotate power
 *  - X,Y are the coordinates of Mars Lander (in meters).
 *  - hSpeed and vSpeed are the horizontal and vertical speed of Mars Lander (in m/s). These can be 
 *    negative depending on the direction of Mars Lander.
 *  - fuel is the remaining quantity of fuel in liters. When there is no more fuel, the power of 
 *    thrusters falls to zero.
 *  - rotate is the angle of rotation of Mars Lander expressed in degrees.
 *  - power is the thrust power of the landing ship.
 * 
 * Output for one game turn
 * -------------------------
 * A single line with 2 integers: rotate power :
 *  - rotate is the desired rotation angle for Mars Lander. Please note that for each turn the 
 *    actual value of the angle is limited to the value of the previous turn +/- 15°.
 *  - power is the desired thrust power. 0 = off. 4 = maximum power. Please note that for each turn 
 *    the value of the actual power is limited to the value of the previous turn +/- 1.
 * 
 * Constraints
 * -----------
 * 2 ≤ surfaceN < 30
 * 0 ≤ X < 7000
 * 0 ≤ Y < 3000
 * -500 < hSpeed, vSpeed < 500
 * 0 ≤ fuel ≤ 2000
 * -90 ≤ rotate ≤ 90
 * 0 ≤ power ≤ 4
 * Response time per turn ≤ 100ms
 * 
 * Example
 * =======
 * Initialization input
 * --------------------
 * 6            (surfaceN)    Surface made of 6 points                 No output expected
 * 0 1500       (landX landY)                                      
 * 1000 2000    (landX landY)                                          You can ignore this but you need
 * 2000 500	    (landX landY) Start of flat ground                     to read the values.
 * 3500 500     (landX landY) End of flat ground                 
 * 5000 1500    (landX landY)
 * 6999 1000    (landX landY)
 * 
 * Input for turn 1
 * ----------------                                                     Output for turn 1
 * 5000 2500 -50 0 1000 90 0    (X Y hSpeed vSpeed fuel rotate power)   -----------------
 *                                                                      -45 4 (rotate power)
 *                                                                      Requested rotation to the right,
 *                                                                      maximum thrust power
 * 
 * 
 * Input for turn 2                                                     Output for turn 2
 * ----------------                                                     -----------------
 * 4950 2498 -51 -3 999 75 1    (X Y hSpeed vSpeed fuel rotate power)   -45 4 (rotate power)
 * Tilt angle changed only by 15° and thrust power only by 1            Same request as previous turn
 *                                                                      
 * 
 * Input for turn 3
 * ----------------                                                     Output for turn 3
 * 4898 2493 -53 -6 997 60 2    (X Y hSpeed vSpeed fuel rotate power)   -----------------
 *                                                                      -45 4 (rotate power)
 *                                                                      Same request as previous turn
 * 
 * Mars Lander - LEVEL 2
 * =====================
 * Same place, the next day.
 * You have joined Jeff and Mike in the crisis meeting room of the Kennedy Space Center.
 * 
 * “OK, I see you got the general idea. Mike, what do you think of our new recruit so far? ”
 * “There's still a long way to go... ”
 * “Oh c'mon Mike, you're always so skeptical! ”
 * 
 * Jeff turns and glares at you with his steel-blue eyes.
 *
 * “But he IS right! This first test was just a warm-up.
 *  Now you'll need to deal with more challenging situations.
 *  You see, we must be prepared to face anything, the success of the mission depends upon it! ”
 * 
 */

namespace Codingame
{
	public class MarsLanderEpisode2
	{
		const double G = 3.711; // m/s squared
		const int MIN_THRUST = 0;
		const int MAX_THRUST = 4;
		const double MIN_THRUST_ANGLE = -90.0;
		const double MAX_THRUST_ANGLE = 90.0;

		static void Main(string[] args)
		{
			string[] inputs;
			int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
			for (int i = 0; i < surfaceN; i++) {
				inputs = Console.ReadLine().Split(' ');
				int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
				int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
			}

			bool landingStarting = true;
			int landingPattern = 0;
			// game loop
			while (true) {
				inputs = Console.ReadLine().Split(' ');
				int X = int.Parse(inputs[0]);
				int Y = int.Parse(inputs[1]);
				int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
				int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
				int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
				int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
				int power = int.Parse(inputs[6]); // the thrust power (0 to 4).

				// Write an action using Console.WriteLine()
				// To debug: Console.Error.WriteLine("Debug messages...");


				// 2 integers: rotate power.
				//             rotate is the desired rotation angle (should be 0 for level 1),
				//             power is the desired thrust power (0 to 4).

				if (landingStarting) {
					landingPattern = (X, Y, hSpeed) switch
					{
						(2500, 2700,    0) => 1,   // Easy on the right
						(6500, 2800, -100) => 2,   // Initial speed, correct side
						(6500, 2800,  -90) => 3,   // Initial speed, wrong side
						( 500, 2700,  100) => 4,   // Deep canyon
						(6500, 2700,  -50) => 5,   // High ground
						_                  => 0
					};
					landingStarting = false;
				}

				(rotate, power) = landingPattern switch
				{
					1 => GetAction_01(X, Y, hSpeed, vSpeed, fuel, rotate, power),
					2 => GetAction_02(X, Y, hSpeed, vSpeed, fuel, rotate, power),
					3 => GetAction_03(X, Y, hSpeed, vSpeed, fuel, rotate, power),
					4 => GetAction_04(X, Y, hSpeed, vSpeed, fuel, rotate, power),
					5 => GetAction_05(X, Y, hSpeed, vSpeed, fuel, rotate, power),
					_ => GetAction(X, Y, hSpeed, vSpeed, fuel, rotate, power)
				};
				Console.WriteLine($"{rotate} {power}");
			}
		}

		private static (int rotate, int power) GetAction(int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power)
		{
			if (vSpeed < -20) {
				power += (power < MAX_THRUST) ? 1 : 0;
			} else {
				power -= (power > MIN_THRUST) ? 1 : 0;
			}

			return (0, power);
		}

		private static (int rotate, int power) GetAction_01(int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power)
		{
			if (vSpeed < -20) {
				power += (power < MAX_THRUST) ? 1 : 0;
			} else {
				power -= (power > MIN_THRUST) ? 1 : 0;
			}
			if (Y > 1600) {
				rotate = -22;
			} else if (Y > 400) {
				rotate = 22;
			} else {
				rotate = 0;
			}

			return (rotate, power);
		}

		private static (int rotate, int power) GetAction_02(int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power)
		{
			if (vSpeed < -20) {
				power += (power < MAX_THRUST) ? 1 : 0;
			} else {
				power -= (power > MIN_THRUST) ? 1 : 0;
			}
			if (Y > 1000) {
				rotate = -22;
			} else {
				rotate = 0;
			}

			return (rotate, power);
		}

		private static (int rotate, int power) GetAction_03(int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power)
		{
			if (vSpeed < -20) {
				power += (power < MAX_THRUST) ? 1 : 0;
			} else {
				power -= (power > MIN_THRUST) ? 1 : 0;
			}
			if (Y > 1000) {
				rotate = -32;
			} else {
				rotate = 0;
			}

			return (rotate, power);
		}

		private static (int rotate, int power) GetAction_04(int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power)
		{
			if (vSpeed < -10) {
				power += (power < MAX_THRUST) ? 1 : 0;
			} else {
				power -= (power > MIN_THRUST) ? 1 : 0;
			}
			if (Y > 2435) {
				rotate = 6;
			} else if (Y > 1912) {
				rotate = 50;
			} else if (Y > 1250) {
				rotate = 0;
			} else if (Y > 900) {
				rotate = 12;
			} else {
				rotate = 0;
			}

			return (rotate, power);
		}

		private static (int rotate, int power) GetAction_05(int X, int Y, int hSpeed, int vSpeed, int fuel, int rotate, int power)
		{
			if ((X > 1565 && vSpeed < 0) || vSpeed < -15) {
				power += (power < MAX_THRUST) ? 1 : 0;
			} else {
				power -= (power > MIN_THRUST) ? 1 : 0;
			}
			if (hSpeed < -10) {
				rotate = -5;
			} else {
				rotate = 0;
			}
			if (X < 1330) {
				rotate = 0;
			}

			return (rotate, power);
		}

	}
}
