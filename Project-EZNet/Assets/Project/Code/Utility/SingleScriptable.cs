using System;
using UnityEngine;

namespace Project.Utility {
	
	/// <summary>
	/// Singleton ScriptableObject
	/// REQUIRES: ScriptableObject asset be name the same as the class minus the word Scriptable
	/// EXAMPLE: MyAssetScriptable -> MyAsset
	/// </summary>
	/// <typeparam name="T"></typeparam>
	 public abstract class SingleScriptable<T> : ScriptableObject where T : ScriptableObject {
		
		private static T instance;
		
		public static T Instance {
			get {
				if (instance == null) {
					instance = CreateSingleScriptable();
				}

				return instance;
			}
		}

		private static T CreateSingleScriptable() {
			return Resources.Load<T>(typeof(T).Name.Replace("Scriptable", ""));
		}
	}
}