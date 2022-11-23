namespace Project.Utility {
	public static class SceneList {
		//Only give access to these scenes in Editor mode
		#if UNITY_EDITOR
			public const string BOOTSTRAP = "Assets/Project/Scenes/Bootstrap.unity";
		#endif

		public const string DEMO = "Demo";
	}
}