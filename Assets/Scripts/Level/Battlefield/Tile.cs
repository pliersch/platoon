using UnityEngine;

namespace level.battlefield {

	public class Tile : MonoBehaviour {

		public int WayCost { get; set; }
		public bool IsFree { get; set; }
		public int RemainedActionPoint { get; set; }
		public Color _colorOver;
		public Color _colorOut;
		private MeshRenderer _meshRenderer;
		private bool _isOver;

		private void Start() {
			_meshRenderer = gameObject.GetComponent<MeshRenderer>();
			_meshRenderer.material.color = _colorOut;
			_isOver = false;
		}

		public void SetText(string msg) {
			//GetComponentInChildren<TextMesh>().text = msg;
		}

		private void OnMouseOver() {
			if (_isOver) {
				return;
			}
			_isOver = true;
			_meshRenderer.material.color = _colorOver;
		}

		private void OnMouseDown() {
			Debug.Log("mouse down");
		}

		private void OnMouseExit() {
			_isOver = false;
			_meshRenderer.material.color = _colorOut;
		}

	}

}