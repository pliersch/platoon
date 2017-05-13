using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Battlefield {

	public class BattlefieldView : MonoBehaviour {

		public GameObject fieldPrefab;
		public GameObject pivot;
		private Field[,] _fields;

		public int rows;
		public int columns;

		private Vector2 _fildSize;
		private float _startX;
		private float _startZ;

		public Field[,] Fields {
			get { return _fields; }
		}

		private void Awake() {
			// only for debug
			ComputeAndCheckFieldSize();
		}

		// Use this for initialization
		private void Start() {
		}

		// Update is called once per frame
		private void Update() {
		}

		// TODO generate ground in Maya with pivot on 0,0 and not in center to avoid computing offset
		// but not the field because units are in the center. 
		public Field[,] GenerateFields(int xDimension, int zDimension) {
			_startX = pivot.transform.position.x;
			_startZ = pivot.transform.position.z;
			float size = _fildSize.x;
			float halfSize = size / 2;
			float offsetX = _startX + halfSize;
			float offsetZ = _startZ + halfSize;
			_fields = new Field[xDimension, zDimension];
			for (int x = 0; x < xDimension; x++) {
				for (int y = 0; y < zDimension; y++) {
					float xPos = x * size;
					float zPos = y * size;
					Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
					GameObject go =
						Instantiate(fieldPrefab, new Vector3(xPos + offsetX, 0.2f, zPos + offsetZ), rotation);
					Field field = (Field) go.GetComponent(typeof(Field));
					field.WayCost = 1;
					field.IsFree = true;
					field.SetTilePosition(x, y);
					field.SetRealPosition(xPos + offsetX, zPos + offsetZ);
					//field.SetActive(false);
					_fields[x, y] = field;
				}
			}
			return _fields;
		}

		public void AddUnit(GameObject unit, int XPos, int zPos) {
			Field field = _fields[XPos, zPos];
			Vector3 position = new Vector3(field.GetRealPosition().x, 0, field.GetRealPosition().y);
			GameObject go = Instantiate(unit, position, unit.transform.rotation);
			//			float x = field.
			//			Instantiate(tank, new Vector3(), tank.transform.rotation);
		}

		private void ComputeAndCheckFieldSize() {
			Renderer fieldRenderer = fieldPrefab.GetComponentInChildren<Renderer>();
			float width = fieldRenderer.bounds.size.x;
			float depth = fieldRenderer.bounds.size.z;
			if (Math.Abs(width - depth) > 0.01) {
				Debug.LogError("Computed size of field: " + width + " and " + depth);
			}
			_fildSize = new Vector2(width, depth);
			Renderer groundRenderer = GetComponentInChildren<Renderer>();
			int x = ((int) groundRenderer.bounds.size.x) / (int) width;
			int y = ((int) groundRenderer.bounds.size.z) / (int) depth;
			if (x != rows) {
				Debug.LogError("Computed size of rows: " + x + " not " + rows);
			}
			if (y != columns) {
				Debug.LogError("Computed size of columns: " + y + " not " + columns);
			}
		}

	}

}