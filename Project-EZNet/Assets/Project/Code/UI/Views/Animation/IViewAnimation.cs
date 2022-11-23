using System.Collections;

namespace Project.UI.Views.Animation {
	public interface IViewAnimation {
		IEnumerator IntroAnimation();
		IEnumerator OutroAnimation();
	}
}