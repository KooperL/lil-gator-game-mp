using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	[AddComponentMenu("")]
	public class ExampleHelpWindow : MonoBehaviour
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x00060B2C File Offset: 0x0005ED2C
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

		// Token: 0x060013C0 RID: 5056 RVA: 0x00060C00 File Offset: 0x0005EE00
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

		public string m_Title;

		[Multiline]
		public string m_Description;

		private bool mShowingHelpWindow = true;

		private const float kPadding = 40f;
	}
}
