using level.gameObjects;
using UnityEngine;

namespace level.battlefield {

	public class BattlefieldModel : MonoBehaviour {

		private Pathfinder _pathfinder;
		private Field[,] _fields;

		// TODO do we need param unit?
		public void UpdateAddedUnit(Unit unit, Position position) {
			_fields[position.x, position.z].IsFree = false;
			//_fields[position.x, position.z].
		}

		public Field GetField(Position position) {
			return _fields[position.x, position.z];
		}

		public Field[,] GenerateFields(int rows, int columns, float tileSize, Vector3 position) {
			float startX = position.x;
			float startZ = position.z;
			float size = tileSize;
			float halfSize = size / 2;
			float offsetX = startX + halfSize;
			float offsetZ = startZ + halfSize;
			_fields = new Field[rows, columns];
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < columns; y++) {
					float xPos = x * size;
					float zPos = y * size;
					Field field = new Field {
						WayCost = 1,
						IsFree = true,
						Position = new Position(x, y),
						RealPosition = new Vector3(xPos + offsetX, 0.1f, zPos + offsetZ)
					};
					//tile.gameObject.SetActive(false);
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

	}

}