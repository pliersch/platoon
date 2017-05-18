using System.Collections.Generic;
using level.battlefield.util;
using level.gameObjects;
using UnityEngine;

namespace level.battlefield {

	public class BattlefieldModel : MonoBehaviour {

		private Pathfinder _pathfinder;

		private Field[,] _fields;

//		private Vector3 _pivot;
		private float _tileSize;

		private float _tileCenter;
//		private int _rows;
//		private int _columns;

		// TODO do we need param unit?
		public void UpdateAddedUnit(Unit unit, Position position) {
			_fields[position.x, position.z].IsFree = false;
			//_fields[position.x, position.z].
		}

		public Field GetField(Position position) {
			return _fields[position.x, position.z];
		}

		public Field[,] GenerateFields(int rows, int columns, float tileSize, Vector3 pivot) {
//			_rows = rows;
//			_columns = columns;
//			_pivot = pivot;
			_tileSize = tileSize;
			float halfSize = _tileSize / 2;
			_tileCenter = pivot.x + halfSize;
			_fields = new Field[rows, columns];
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < columns; y++) {
					float xPos = x * _tileSize + _tileCenter;
					float zPos = y * _tileSize + _tileCenter;
					Field field = new Field {
						WayCost = 1,
						IsFree = true,
						Position = new Position(x, y),
						RealPosition = new Vector3(xPos, 0.1f, zPos)
					};
					_fields[x, y] = field;
				}
			}
			// is not nice because hard to find, but performant
			// is there a better solution?
			_pathfinder = new Pathfinder(_fields);
			return _fields;
		}

		public Field[] GetReachableFields(Position position) {
			Field[] fields = _pathfinder.GetReachableFields(position, 5);
			return fields;
		}

		public Position ConvertCoordinateToPosition(Vector3 coordinate) {
			int x = (int) ((coordinate.x - _tileSize / 2) / _tileSize);
			int z = (int) ((coordinate.z - _tileSize / 2) / _tileSize);
			return new Position(x, z);
		}


//		public Vector3 ConvertPositionToCoordinate(int x, int z) {
//			float center = _tileSize / 2;
//			float xPos = x * _tileSize + center;
//			float zPos = z * _tileSize + center;
//			return new Vector3(xPos, 0, zPos);
//
//		}

//		private Position ConvertPositionToCoordinate(Position position) {
//
//		}
		public Field[] GetWay(Position targetPosition) {
			List<Field> endToStart = new List<Field> {};
			Field current = GetField(targetPosition);
			endToStart.Add(current);
			Field parent = current.Parent;
			while (parent != null) {
				endToStart.Add(parent);
				parent = parent.Parent;
			}
			endToStart.Reverse();
			return endToStart.ToArray();
		}

	}

}
