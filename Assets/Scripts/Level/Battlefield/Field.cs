using UnityEngine;

namespace level.battlefield {

	public class Field {

	// TODO use readonly!
	// TODO use struct not class?

		public Vector3 RealPosition { get; set; }
		public int WayCost { get; set; }
		public Position Position { get; set; }
		public bool IsFree { get; set; }
		public int RemainedActionPoint { get; set; }
		public Field Parent { get; set; }

	}

}