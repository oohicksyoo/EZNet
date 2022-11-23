using System;
using System.Collections.Generic;
using System.Linq;
using Project.Gameplay.Board;
using Project.Utility;
using UnityEngine;

namespace Assets.Project.Code.Scriptable {
	
	[CreateAssetMenu(fileName = "LevelData", menuName = "RPG/Scriptables/LevelData", order = 0)]
	public class LevelDataScriptable : SingleScriptable<LevelDataScriptable> {


		#region Serialized Fields

		[SerializeField]
		private List<LevelData> levels;

		#endregion


		#region Public Methods

		public LevelData GetLevelData(string guid) {
			LevelData levelData = levels.FirstOrDefault(x => x.Guid == guid);
			if (levelData != null) {
				levelData.Initialize();
			}

			return levelData;
		}

		#endregion
		
	}

	[Serializable]
	public class LevelData {


		#region Serialized Fields

		[SerializeField]
		private string guid;
		
		[SerializeField]
		private WeightedGemAssetScriptable weightedGemAsset;

		#endregion


		#region Properties

		public string Guid => guid;

		public int WeightedGemLength => weightedGemAsset?.WeightedGameArrayLength ?? 0;

		public Vector2 BoardOffset => weightedGemAsset?.BoardOffset ?? Vector2.zero;
		
		#endregion


		#region Public Methods

		public void Initialize() {
			weightedGemAsset.Initialize();
		}

		public GemLocation.GemType GetWeightGemTypeAt(int index) {
			return weightedGemAsset.GetWeightGemTypeAt(index);
		}

		#endregion

	}
}