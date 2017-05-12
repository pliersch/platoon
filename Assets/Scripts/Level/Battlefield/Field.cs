using System.Collections;
using System.Collections.Generic;
using Level.Battlefield;
using UnityEngine;

namespace Level.Battlefield {

	public class Field : MonoBehaviour {

		private TilePosition _tileTilePosition;
		private Vector2 _realPosition;
		public int WayCost { get; set; }
		public bool IsFree { get; set; }
		public int RemainedActionPoint { get; set; }

		public Field Parent { get; set; }
		// TODO what is it?
		//private bool _isHidden;

		public void SetTilePosition(int xPos, int zPos) {
			TextMesh textMesh = GetComponentInChildren<TextMesh>();
			textMesh.text = xPos + " - " + zPos;
			_tileTilePosition.xPos = xPos;
			_tileTilePosition.zPos = zPos;
		}

		public TilePosition TilePosition {
			get { return _tileTilePosition; }
		}

		public void SetRealPosition(float xPos, float zPos) {
			_realPosition = new Vector2(xPos, zPos);
		}

		public Vector2 GetRealPosition() {
			return _realPosition;
		}

	}

}