using System;
using System.Collections.Generic;
using Rewired.ControllerExtensions;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000499 RID: 1177
	[AddComponentMenu("")]
	public class DualShock4SpecialFeaturesExample : MonoBehaviour
	{
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00016693 File Offset: 0x00014893
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000166A5 File Offset: 0x000148A5
		private void Awake()
		{
			this.InitializeTouchObjects();
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00072538 File Offset: 0x00070738
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			IDualShock4Extension firstDS = this.GetFirstDS4(this.player);
			if (firstDS != null)
			{
				base.transform.rotation = firstDS.GetOrientation();
				this.HandleTouchpad(firstDS);
				Vector3 accelerometerValue = firstDS.GetAccelerometerValue();
				this.accelerometerTransform.LookAt(this.accelerometerTransform.position + accelerometerValue);
			}
			if (this.player.GetButtonDown("CycleLight"))
			{
				this.SetRandomLightColor();
			}
			if (this.player.GetButtonDown("ResetOrientation"))
			{
				this.ResetOrientation();
			}
			if (this.player.GetButtonDown("ToggleLightFlash"))
			{
				if (this.isFlashing)
				{
					this.StopLightFlash();
				}
				else
				{
					this.StartLightFlash();
				}
				this.isFlashing = !this.isFlashing;
			}
			if (this.player.GetButtonDown("VibrateLeft"))
			{
				firstDS.SetVibration(0, 1f, 1f);
			}
			if (this.player.GetButtonDown("VibrateRight"))
			{
				firstDS.SetVibration(1, 1f, 1f);
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00072648 File Offset: 0x00070848
		private void OnGUI()
		{
			if (this.textStyle == null)
			{
				this.textStyle = new GUIStyle(GUI.skin.label);
				this.textStyle.fontSize = 20;
				this.textStyle.wordWrap = true;
			}
			if (this.GetFirstDS4(this.player) == null)
			{
				return;
			}
			GUILayout.BeginArea(new Rect(200f, 100f, (float)Screen.width - 400f, (float)Screen.height - 200f));
			GUILayout.Label("Rotate the Dual Shock 4 to see the model rotate in sync.", this.textStyle, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Touch the touchpad to see them appear on the model.", this.textStyle, Array.Empty<GUILayoutOption>());
			ActionElementMap actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(2, "ResetOrientation", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " to reset the orientation. Hold the gamepad facing the screen with sticks pointing up and press the button.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(2, "CycleLight", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " to change the light color.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(2, "ToggleLightFlash", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " to start or stop the light flashing.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(2, "VibrateLeft", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " vibrate the left motor.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(2, "VibrateRight", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " vibrate the right motor.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00072850 File Offset: 0x00070A50
		private void ResetOrientation()
		{
			IDualShock4Extension firstDS = this.GetFirstDS4(this.player);
			if (firstDS != null)
			{
				firstDS.ResetOrientation();
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00072874 File Offset: 0x00070A74
		private void SetRandomLightColor()
		{
			IDualShock4Extension firstDS = this.GetFirstDS4(this.player);
			if (firstDS != null)
			{
				Color color;
				color..ctor(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
				firstDS.SetLightColor(color);
				this.lightObject.GetComponent<MeshRenderer>().material.color = color;
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000728E8 File Offset: 0x00070AE8
		private void StartLightFlash()
		{
			DualShock4Extension dualShock4Extension = this.GetFirstDS4(this.player) as DualShock4Extension;
			if (dualShock4Extension != null)
			{
				dualShock4Extension.SetLightFlash(0.5f, 0.5f);
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0007291C File Offset: 0x00070B1C
		private void StopLightFlash()
		{
			DualShock4Extension dualShock4Extension = this.GetFirstDS4(this.player) as DualShock4Extension;
			if (dualShock4Extension != null)
			{
				dualShock4Extension.StopLightFlash();
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00072944 File Offset: 0x00070B44
		private IDualShock4Extension GetFirstDS4(Player player)
		{
			foreach (Joystick joystick in player.controllers.Joysticks)
			{
				IDualShock4Extension extension = joystick.GetExtension<IDualShock4Extension>();
				if (extension != null)
				{
					return extension;
				}
			}
			return null;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000729A0 File Offset: 0x00070BA0
		private void InitializeTouchObjects()
		{
			this.touches = new List<DualShock4SpecialFeaturesExample.Touch>(2);
			this.unusedTouches = new Queue<DualShock4SpecialFeaturesExample.Touch>(2);
			for (int i = 0; i < 2; i++)
			{
				DualShock4SpecialFeaturesExample.Touch touch = new DualShock4SpecialFeaturesExample.Touch();
				touch.go = GameObject.CreatePrimitive(0);
				touch.go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				touch.go.transform.SetParent(this.touchpadTransform, true);
				touch.go.GetComponent<MeshRenderer>().material.color = ((i == 0) ? Color.red : Color.green);
				touch.go.SetActive(false);
				this.unusedTouches.Enqueue(touch);
			}
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00072A60 File Offset: 0x00070C60
		private void HandleTouchpad(IDualShock4Extension ds4)
		{
			for (int i = this.touches.Count - 1; i >= 0; i--)
			{
				DualShock4SpecialFeaturesExample.Touch touch = this.touches[i];
				if (!ds4.IsTouchingByTouchId(touch.touchId))
				{
					touch.go.SetActive(false);
					this.unusedTouches.Enqueue(touch);
					this.touches.RemoveAt(i);
				}
			}
			for (int j = 0; j < ds4.maxTouches; j++)
			{
				if (ds4.IsTouching(j))
				{
					int touchId = ds4.GetTouchId(j);
					DualShock4SpecialFeaturesExample.Touch touch2 = this.touches.Find((DualShock4SpecialFeaturesExample.Touch x) => x.touchId == touchId);
					if (touch2 == null)
					{
						touch2 = this.unusedTouches.Dequeue();
						this.touches.Add(touch2);
					}
					touch2.touchId = touchId;
					touch2.go.SetActive(true);
					Vector2 vector;
					ds4.GetTouchPosition(j, ref vector);
					touch2.go.transform.localPosition = new Vector3(vector.x - 0.5f, 0.5f + touch2.go.transform.localScale.y * 0.5f, vector.y - 0.5f);
				}
			}
		}

		// Token: 0x04001EA0 RID: 7840
		private const int maxTouches = 2;

		// Token: 0x04001EA1 RID: 7841
		public int playerId;

		// Token: 0x04001EA2 RID: 7842
		public Transform touchpadTransform;

		// Token: 0x04001EA3 RID: 7843
		public GameObject lightObject;

		// Token: 0x04001EA4 RID: 7844
		public Transform accelerometerTransform;

		// Token: 0x04001EA5 RID: 7845
		private List<DualShock4SpecialFeaturesExample.Touch> touches;

		// Token: 0x04001EA6 RID: 7846
		private Queue<DualShock4SpecialFeaturesExample.Touch> unusedTouches;

		// Token: 0x04001EA7 RID: 7847
		private bool isFlashing;

		// Token: 0x04001EA8 RID: 7848
		private GUIStyle textStyle;

		// Token: 0x0200049A RID: 1178
		private class Touch
		{
			// Token: 0x04001EA9 RID: 7849
			public GameObject go;

			// Token: 0x04001EAA RID: 7850
			public int touchId = -1;
		}
	}
}
