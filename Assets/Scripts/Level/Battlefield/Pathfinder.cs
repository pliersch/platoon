using System.Collections.Generic;

namespace level.battlefield {

	public class Pathfinder {

		public static Position[] getReachableFields(int xPos, int zPos) {
			List<Position> closedFields = new List<Position>();
			List<Position> openFields = new List<Position>();


			Position[] neighbours = getNeighbours(xPos, zPos);

			return neighbours;
		}

		private static Position[] getNeighbours(int xPos, int zPos) {
			Position[] neighbours = new Position[4];

			neighbours[0] = new Position(xPos, zPos + 1);
			neighbours[1] = new Position(xPos + 1, zPos);
			neighbours[2] = new Position(xPos, zPos - 1); 
			neighbours[3] = new Position(xPos - 1, zPos);

			return neighbours;
		}

	}

}