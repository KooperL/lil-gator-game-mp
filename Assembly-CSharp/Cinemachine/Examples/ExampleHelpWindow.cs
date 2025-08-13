using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020002F9 RID: 761
	[AddComponentMenu("")]
	public class ExampleHelpWindow : MonoBehaviour
	{
		// Token: 0x06001032 RID: 4146 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
		private void OnGUI()
		{
			if (this.mShowingHelpWindow)
			{
				Vector2 vector = GUI.skin.label.CalcSize(new GUIContent(this.m_Description));
				Vector2 vector2 = vector * 0.5f;
				float maxWidth = Mathf.Min((float)Screen.width - 40f, vector.x);
				float num = (float)Screen.width * 0.5f - maxWidth * 0.5f;
				float num2 = (float)Screen.height * 0.4f - vector2.y;
				Rect rect = new Rect(num, num2, maxWidth, vector.y);
				GUILayout.Window(400, rect, delegate(int id)
				{
					this.DrawWindow(id, maxWidth);
				}, this.m_Title, Array.Empty<GUILayoutOption>());
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004DC84 File Offset: 0x0004BE84
		private void DrawWindow(int id, float maxWidth)
		{
			GUILayout.BeginVertical(GUI.skin.box, Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.m_Description, Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
			if (GUILayout.Button("Got it!", Array.Empty<GUILayoutOption>()))
			{
				this.mShowingHelpWindow = false;
			}
		}

		// Token: 0x04001546 RID: 5446
		public string m_Title;

		// Token: 0x04001547 RID: 5447
		[Multiline]
		public string m_Description;

		// Token: 0x04001548 RID: 5448
		private bool mShowingHelpWindow = true;

		// Token: 0x04001549 RID: 5449
		private const float kPadding = 40f;
	}
}
