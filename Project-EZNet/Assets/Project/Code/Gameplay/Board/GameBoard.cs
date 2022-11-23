using Assets.Project.Code.Scriptable;
using Project.Managers;
using Project.Utility.Helpers;
using UnityEngine;

namespace Project.Gameplay.Board {
	public class GameBoard : MonoBehaviour {


		#region Constants

		private const int GRID_WIDTH = 6;

		#endregion


		#region Serialized Fields

		[SerializeField]
		private GemLocation[] gemLocations;

		#endregion


		#region Public Methods

		
		//TODO: Introduce a vertical offset to keep chains coming in some how from the top most of the time
		public void FillBoard() {
			LevelData levelData = ApplicationManager.Instance.CurrentLevelController.LevelData;
			int count = 0;
			
			foreach (var gemLocation in gemLocations) {
				float xPosition = Mathf.Floor(count % GRID_WIDTH) + (1 * levelData.BoardOffset.x);
				float yPosition = Mathf.Floor(count / (float)GRID_WIDTH) + (1 * levelData.BoardOffset.y);
				float value = GetNoise(xPosition, yPosition);
				
				value = MathHelper.Remap(value, 0f, 1f, 0f, levelData.WeightedGemLength);
				GemLocation.GemType type = levelData.GetWeightGemTypeAt(Mathf.RoundToInt(value));
				
				gemLocation.TurnOff();
				gemLocation.SetState(type);
				count++;
			}
		}

		#endregion


		#region Private Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Returns 0 - 1</returns>
		private float GetNoise(float x, float y) {
			return Mathf.PerlinNoise(x, y);
		}

		#endregion

	}
}