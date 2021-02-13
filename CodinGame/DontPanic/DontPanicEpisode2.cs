using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

/*
 * https://www.codingame.com/ide/puzzle/don't-panic-episode-2
 * 
 * 
 * 
 * 
 * 
 */

namespace CodinGame {
	public static class DontPanicEpisode2Solution {
		public static string DontPanicEpisode2Main(string[] args) {
			string[] inputs;
			inputs = Console.ReadLine().Split(' ');
			int nbFloors = int.Parse(inputs[0]); // number of floors
			int width = int.Parse(inputs[1]); // width of the area
			int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
			int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
			int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
			int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
			int nbAdditionalElevators = int.Parse(inputs[6]); // number of additional elevators that you can build
			int nbElevators = int.Parse(inputs[7]); // number of elevators
			int MAX_WIDTH = width - 1;
			int MIN_WIDTH = 0;
			List<(int floor, int position)> elevators = new List<(int, int)>();
			for (int i = 0; i < nbElevators; i++) {
				inputs = Console.ReadLine().Split(' ');
				int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
				int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
				elevators.Add((elevatorFloor, elevatorPos));
				Console.Error.WriteLine($"Elevator: {elevatorFloor}, {elevatorPos}");
			}

			Console.Error.WriteLine($"Additional Elevators: {nbAdditionalElevators}");

			// game loop
			while (true) {
				inputs = Console.ReadLine().Split(' ');
				int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
				int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
				string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT
				string action = "WAIT";
				if (cloneFloor >= MIN_WIDTH) {
					int targetPos;
					string targetDirection = "";
					if (cloneFloor == exitFloor) {
						targetPos = exitPos;
						targetDirection = targetPos - clonePos > 0 ? "RIGHT" : targetPos - clonePos < 0 ? "LEFT" : direction;
						action = direction == targetDirection ? "WAIT" : "BLOCK";
					} else if (!elevators.Where(x => x.floor == cloneFloor).Any()) {
						action = "ELEVATOR";
						elevators.Add((cloneFloor, clonePos));
					} else {
						//ToDo: Find best elevator instead of first one
						targetPos = GetNearestElevatorPosition(elevators, cloneFloor, clonePos);
						targetDirection = targetPos - clonePos > 0 ? "RIGHT" : targetPos - clonePos < 0 ? "LEFT" : direction;
						action = direction == targetDirection ? "WAIT" : "BLOCK";
					}
				}
				// Write an action using Console.WriteLine()
				// To debug: Console.Error.WriteLine("Debug messages...");

				Console.WriteLine(action); // action: WAIT or BLOCK or ELEVATOR
			}
		}

		private static int GetNearestElevatorPosition(List<(int floor, int position)> elevators, int cloneFloor, int clonePos)
			=> elevators
				.Where(x => x.floor == cloneFloor)
				.OrderBy(x => (Math.Abs(clonePos - x.position)))
				.First()
				.position;
	}
}
