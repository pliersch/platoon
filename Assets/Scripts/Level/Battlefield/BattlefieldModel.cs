using UnityEngine;

namespace Level.Battlefield {

	public class BattlefieldModel {

		private int _rows;
		private int _columns;
		private Field[,] _fields;

		public BattlefieldModel(int rows, int columns) {
			_rows = rows;
			_columns = columns;
		}

		public void SetFields(Field[,] fields) {
			_fields = fields;
		}

		public void HandleAddUnit(int xPos, int zPos) {
			_fields[xPos, zPos].IsFree = false;
		}

		public Field[,] GetAllFields() {
			return _fields;
		}

		public Field GetField(int xPos, int zPos) {
			return _fields[xPos, zPos];
		}

	}

}