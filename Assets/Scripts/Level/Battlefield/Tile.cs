using UnityEngine;

namespace level.battlefield {

	public class Tile : MonoBehaviour {

		public int WayCost { get; set; }
		public bool IsFree { get; set; }
		public int RemainedActionPoint { get; set; }
		public Color _colorOver;
		public Color _colorOut;

		public void SetText(string msg) {
			TextMesh textMesh = GetComponentInChildren<TextMesh>();
			textMesh.text = msg;
		}

		private void OnMouseOver() {
			var r = gameObject.GetComponent<MeshRenderer>();
			r.material.color = _colorOver;
		}

		private void OnMouseDown() {
			Debug.Log("mouse down");
		}

		private void OnMouseExit() {
			var r = gameObject.GetComponent<MeshRenderer>();
			r.material.color = _colorOut;
		}

	}

}