using UnityEngine;

namespace Level.Battlefield {

	public class Battlefield : MonoBehaviour {

		private BattlefieldModel _model;
		public BattlefieldView view;
		public PrefabFactory factory;
		public int _rows;
		public int _columns;


		void Start() {
			_model = new BattlefieldModel(_rows, _columns);
			var generateFields = view.GenerateFields(_rows, _columns);
			_model.SetFields(generateFields);
			AddUnits();
		}

		private void AddUnits() {
			view.AddUnit(factory.tank, 3, 3);
		}


		void Update() {

		}

	}

}