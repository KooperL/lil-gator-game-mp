using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200049D RID: 1181
	[AddComponentMenu("")]
	public class FallbackJoystickIdentificationDemo : MonoBehaviour
	{
		// Token: 0x06001D56 RID: 7510 RVA: 0x0001673B File Offset: 0x0001493B
		private void Awake()
		{
			if (!ReInput.unityJoystickIdentificationRequired)
			{
				return;
			}
			ReInput.ControllerConnectedEvent += this.JoystickConnected;
			ReInput.ControllerDisconnectedEvent += this.JoystickDisconnected;
			this.IdentifyAllJoysticks();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0001676D File Offset: 0x0001496D
		private void JoystickConnected(ControllerStatusChangedEventArgs args)
		{
			this.IdentifyAllJoysticks();
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0001676D File Offset: 0x0001496D
		private void JoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			this.IdentifyAllJoysticks();
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00072CB8 File Offset: 0x00070EB8
		public void IdentifyAllJoysticks()
		{
			this.Reset();
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			Joystick[] joysticks = ReInput.controllers.GetJoysticks();
			if (joysticks == null)
			{
				return;
			}
			this.identifyRequired = true;
			this.joysticksToIdentify = new Queue<Joystick>(joysticks);
			this.SetInputDelay();
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00016775 File Offset: 0x00014975
		private void SetInputDelay()
		{
			this.nextInputAllowedTime = Time.time + 1f;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00072D00 File Offset: 0x00070F00
		private void OnGUI()
		{
			if (!this.identifyRequired)
			{
				return;
			}
			if (this.joysticksToIdentify == null || this.joysticksToIdentify.Count == 0)
			{
				this.Reset();
				return;
			}
			Rect rect;
			rect..ctor((float)Screen.width * 0.5f - 125f, (float)Screen.height * 0.5f - 125f, 250f, 250f);
			GUILayout.Window(0, rect, new GUI.WindowFunction(this.DrawDialogWindow), "Joystick Identification Required", Array.Empty<GUILayoutOption>());
			GUI.FocusWindow(0);
			if (Time.time < this.nextInputAllowedTime)
			{
				return;
			}
			if (!ReInput.controllers.SetUnityJoystickIdFromAnyButtonOrAxisPress(this.joysticksToIdentify.Peek().id, 0.8f, false))
			{
				return;
			}
			this.joysticksToIdentify.Dequeue();
			this.SetInputDelay();
			if (this.joysticksToIdentify.Count == 0)
			{
				this.Reset();
			}
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00072DE4 File Offset: 0x00070FE4
		private void DrawDialogWindow(int windowId)
		{
			if (!this.identifyRequired)
			{
				return;
			}
			if (this.style == null)
			{
				this.style = new GUIStyle(GUI.skin.label);
				this.style.wordWrap = true;
			}
			GUILayout.Space(15f);
			GUILayout.Label("A joystick has been attached or removed. You will need to identify each joystick by pressing a button on the controller listed below:", this.style, Array.Empty<GUILayoutOption>());
			Joystick joystick = this.joysticksToIdentify.Peek();
			GUILayout.Label("Press any button on \"" + joystick.name + "\" now.", this.style, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Skip", Array.Empty<GUILayoutOption>()))
			{
				this.joysticksToIdentify.Dequeue();
				return;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00016788 File Offset: 0x00014988
		private void Reset()
		{
			this.joysticksToIdentify = null;
			this.identifyRequired = false;
		}

		// Token: 0x04001EB5 RID: 7861
		private const float windowWidth = 250f;

		// Token: 0x04001EB6 RID: 7862
		private const float windowHeight = 250f;

		// Token: 0x04001EB7 RID: 7863
		private const float inputDelay = 1f;

		// Token: 0x04001EB8 RID: 7864
		private bool identifyRequired;

		// Token: 0x04001EB9 RID: 7865
		private Queue<Joystick> joysticksToIdentify;

		// Token: 0x04001EBA RID: 7866
		private float nextInputAllowedTime;

		// Token: 0x04001EBB RID: 7867
		private GUIStyle style;
	}
}
