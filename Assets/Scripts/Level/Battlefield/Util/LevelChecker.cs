using System;
using UnityEngine;

namespace level.battlefield.Util {

	public class LevelChecker {

		public float CheckTileSize(GameObject tile) {
			Renderer fieldRenderer = tile.GetComponentInChildren<Renderer>();
			float width = fieldRenderer.bounds.size.x;
			float depth = fieldRenderer.bounds.size.z;
			if (Math.Abs(width - depth) > 0.01) {
				Debug.LogError("Computed size of tile: " + width + " and " + depth);
			}
			return width;
		}

//		public void CheckGround(GameObject tile) {
//			_fildSize = new Vector2(width, depth);
//			Renderer groundRenderer = GetComponentInChildren<Renderer>();
//			int x = ((int)groundRenderer.bounds.size.x) / (int)width;
//			int y = ((int)groundRenderer.bounds.size.z) / (int)depth;
//			if (x != _rows) {
//				Debug.LogError("Computed size of rows: " + x + " not " + _rows);
//			}
//			if (y != _columns) {
//				Debug.LogError("Computed size of columns: " + y + " not " + _columns);
//			}

	}

}