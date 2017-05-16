using System.Collections.Generic;
using System.Linq;

namespace level.battlefield {

	public class Pathfinder {

		private Field[,] _fields;
		private List<Field> _openFields;
		private List<Field> _closedFields;
		private int _rows;
		private int _columns;

		public Pathfinder(Field[,] fields) {
			_fields = fields;
			_rows = _fields.GetLength(0);
			_columns = _fields.GetLength(1);
		}

		public Field[] GetReachableFields(Position position, int actionPoints) {
			List<Field> reachableNeighbours;
			_openFields = new List<Field>();
			_closedFields = new List<Field>();
			Field chckField;
			Field currentField;

			currentField = _fields[position.x, position.z];
			currentField.RemainedActionPoint = actionPoints;
			_closedFields.Add(currentField);

			reachableNeighbours = getReachableNeighbours(currentField, actionPoints);
			if (reachableNeighbours.Count > 0) {
				for (int i = 0; i < reachableNeighbours.Count; i++) {
					chckField = reachableNeighbours[i];
					_openFields.Add(chckField);
					chckField.Parent = currentField;
					chckField.RemainedActionPoint = actionPoints - chckField.WayCost;
				}
			}
			while (_openFields.Count > 0) {
				//currentField = _openFields.pop();
				// TODO is the same like pop?
				currentField = _openFields[_openFields.Count - 1];
				_openFields.RemoveAt(_openFields.Count - 1);

				reachableNeighbours = getReachableNeighbours(currentField, currentField.RemainedActionPoint);
				for (int j = 0; j < reachableNeighbours.Count; j++) {
					chckField = reachableNeighbours[j];
					if (isNewField(chckField)) {
						_openFields.Add(chckField);
						chckField.Parent = currentField;
						chckField.RemainedActionPoint = currentField.RemainedActionPoint - chckField.WayCost;
					} else {
						if (currentField.RemainedActionPoint - chckField.WayCost > chckField.RemainedActionPoint) {
							chckField.RemainedActionPoint = currentField.RemainedActionPoint - chckField.WayCost;
							chckField.Parent = currentField;
							if (isInClosedList(chckField)) {
								int index = _closedFields.IndexOf(chckField);
								_openFields.Add(_closedFields[index]);
								//_closedFields.splice(index, 1);
								// TODO is the same like splice?
								_closedFields.RemoveAt(index);
							}
						}
					}
				}
				_closedFields.Add(currentField);
			}
			return _closedFields.ToArray();
		}

		private bool isInOpenList(Field field) {
			return _openFields.Contains(field);
		}

		private bool isInClosedList(Field field) {
			return _closedFields.Contains(field);
		}

		private bool isNewField(Field field) {
			return !isInOpenList(field) && !isInClosedList(field);
		}

		private List<Field> getReachableNeighbours(Field field, int remainingActionPoints) {
			Position[] neighboursPoints = GetNeighbourPositions(field.Position);
			List<Field> neighboursFields = new List<Field>();

			for (int i = 0; i < neighboursPoints.Length; i++) {
				if (ExitsField(neighboursPoints[i])) {
					Field chckField = _fields[neighboursPoints[i].x, neighboursPoints[i].z];
					if (chckField.IsFree && remainingActionPoints >= chckField.WayCost) {
						neighboursFields.Add(chckField);
					}
				}
			}
			return neighboursFields;
		}


		private bool ExitsField(Position position) {
			return position.x >= 0 && position.z >= 0 && position.x < _rows && position.z < _columns;
		}

		private Position[] GetNeighbourPositions(Position position) {
			Position[] neighbours = new Position[4];
			int xPos = position.x;
			int zPos = position.z;
			neighbours[0] = new Position(xPos, zPos + 1);
			neighbours[1] = new Position(xPos + 1, zPos);
			neighbours[2] = new Position(xPos, zPos - 1);
			neighbours[3] = new Position(xPos - 1, zPos);

			return neighbours;
		}

	}

}