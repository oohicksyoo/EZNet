using System;
using System.Collections.Generic;
using System.Linq;
using Project.Gameplay.Board;
using Project.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Project.Code.Scriptable {
	[CreateAssetMenu(fileName = "GemAsset", menuName = "RPG/Scriptables/GemAsset", order = 0)]
	public class GemAssetScriptable : SingleScriptable<GemAssetScriptable> {


		#region Serialized Fields

		[SerializeField]
		private GemAsset[] gemAssets;

		#endregion


		#region Public Methods

		public GemAsset GetAssetByType(GemLocation.GemType type) {
			var asset = gemAssets.FirstOrDefault(x => x.Type == type);

			if (asset == null) {
				Debug.LogError($"Missing gem asset of type ({type})");
			}

			return asset;
		}

		#endregion

	}

	[Serializable]
	public class GemAsset {


		#region Serialized Fields

		[SerializeField]
		private GemLocation.GemType type;
		
		[SerializeField]
		private Sprite normal;

		[SerializeField]
		private Sprite highlighted;

		#endregion


		#region Properties

		public GemLocation.GemType Type => type;

		public Sprite Normal => normal;

		public Sprite Highlighted => highlighted;

		#endregion

	}
}