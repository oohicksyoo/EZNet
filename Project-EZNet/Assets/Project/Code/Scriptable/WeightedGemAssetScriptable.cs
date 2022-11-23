using System;
using System.Collections.Generic;
using System.Linq;
using Project.Gameplay.Board;
using UnityEngine;
using Random = System.Random;

namespace Assets.Project.Code.Scriptable {
	[CreateAssetMenu(fileName = "WeightedGemAsset", menuName = "RPG/Scriptables/WeightedGemAsset", order = 0)]
	public class WeightedGemAssetScriptable : ScriptableObject {


		#region Serialized Fields

		[SerializeField]
		[Tooltip("Seeds with -1 will be set using Time")]
		private int seed = -1;

		[SerializeField]
		private WeightedGemAsset[] weightedGemAssets;

		#endregion
		
		
		#region Non Serialized Fields

		[NonSerialized]
		private List<GemLocation.GemType> weightedGemArray;

		[NonSerialized]
		private Random random;

		#endregion


		#region Properties

		private List<GemLocation.GemType> WeightedGemArray {
			get {
				return weightedGemArray ??= new List<GemLocation.GemType>();
			}
			set {
				weightedGemArray = value;
			}
		}

		private Random Random {
			get {
				return random ??= new Random(seed == -1 ? Mathf.RoundToInt(Time.time) : seed);
			}
		}

		public int WeightedGameArrayLength => this.WeightedGemArray.Count;

		public Vector2 BoardOffset {
			get;
			set;
		}

		#endregion
		
		#region Public Methods

		public void Initialize() {
			if (this.WeightedGameArrayLength != 0) {
				return;
			}

			this.BoardOffset = new Vector2((float)this.Random.NextDouble(), (float)this.Random.NextDouble());

			//Create weight array of gem assets so it can be query whenever
			foreach (var weightedGemAsset in weightedGemAssets) {
				for (int i = 0; i < weightedGemAsset.Percentage; i++) {
					this.WeightedGemArray.Add(weightedGemAsset.Type);
				}
			}

			this.WeightedGemArray = this.WeightedGemArray.OrderBy(x => this.Random.Next()).ToList();
		}

		public GemLocation.GemType GetWeightGemTypeAt(int index) {
			if (index >= 0 && index < this.WeightedGameArrayLength) {
				return this.WeightedGemArray[index];
			}
			
			return this.WeightedGameArrayLength > 0 ? this.WeightedGemArray[0] : GemLocation.GemType.None;
		}

		#endregion
		
	}
	
	[Serializable]
	public class WeightedGemAsset {


		#region Serialized Fields

		[SerializeField]
		private GemLocation.GemType type;

		[SerializeField]
		[Range(0, 10)]
		private int percentage = 1;

		#endregion


		#region Properties

		public GemLocation.GemType Type => type;
		
		public int Percentage => percentage;

		#endregion

	}
}