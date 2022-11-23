using UnityEditor;
using UnityEngine;

namespace Project.Utility {
	public static class GuidGenerator {

		[MenuItem("RPG/Generate Guid")]
		public static string GenerateGuid() {
			string guid = GUID.Generate().ToString();
			Debug.Log($"{guid}");
			
			return guid;
		}
	}
}