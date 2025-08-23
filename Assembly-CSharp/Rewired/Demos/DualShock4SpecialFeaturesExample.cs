using System;
using System.Collections.Generic;
using Rewired.ControllerExtensions;
using UnityEngine;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public class DualShock4SpecialFeaturesExample : MonoBehaviour
	{
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x00016AD3 File Offset: 0x00014CD3
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00016AE5 File Offset: 0x00014CE5
		private void Awake()
		{
			this.InitializeTouchObjects();
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000747AC File Offset: 0x000729AC
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

		// Token: 0x06001DA5 RID: 7589 RVA: 0x000748BC File Offset: 0x00072ABC
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
			ActionElementMap actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, "ResetOrientation", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " to reset the orientation. Hold the gamepad facing the screen with sticks pointing up and press the button.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, "CycleLight", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " to change the light color.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, "ToggleLightFlash", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " to start or stop the light flashing.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, "VibrateLeft", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " vibrate the left motor.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			actionElementMap = this.player.controllers.maps.GetFirstElementMapWithAction(ControllerType.Joystick, "VibrateRight", true);
			if (actionElementMap != null)
			{
				GUILayout.Label("Press " + actionElementMap.elementIdentifierName + " vibrate the right motor.", this.textStyle, Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndArea();
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00074AC4 File Offset: 0x00072CC4
		private void ResetOrientation()
		{
			IDualShock4Extension firstDS = this.GetFirstDS4(this.player);
			if (firstDS != null)
			{
				firstDS.ResetOrientation();
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00074AE8 File Offset: 0x00072CE8
		private void SetRandomLightColor()
		{
			IDualShock4Extension firstDS = this.GetFirstDS4(this.player);
			if (firstDS != null)
			{
				Color color = new Color(global::UnityEngine.Random.Range(0f, 1f), global::UnityEngine.Random.Range(0f, 1f), global::UnityEngine.Random.Range(0f, 1f), 1f);
				firstDS.SetLightColor(color);
				this.lightObject.GetComponent<MeshRenderer>().material.color = color;
			}
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00074B5C File Offset: 0x00072D5C
		private void StartLightFlash()
		{
			DualShock4Extension dualShock4Extension = this.GetFirstDS4(this.player) as DualShock4Extension;
			if (dualShock4Extension != null)
			{
				dualShock4Extension.SetLightFlash(0.5f, 0.5f);
			}
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00074B90 File Offset: 0x00072D90
		private void StopLightFlash()
		{
			DualShock4Extension dualShock4Extension = this.GetFirstDS4(this.player) as DualShock4Extension;
			if (dualShock4Extension != null)
			{
				dualShock4Extension.StopLightFlash();
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00074BB8 File Offset: 0x00072DB8
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

		// Token: 0x06001DAB RID: 7595 RVA: 0x00074C14 File Offset: 0x00072E14
		private void InitializeTouchObjects()
		{
			this.touches = new List<DualShock4SpecialFeaturesExample.Touch>(2);
			this.unusedTouches = new Queue<DualShock4SpecialFeaturesExample.Touch>(2);
			for (int i = 0; i < 2; i++)
			{
				DualShock4SpecialFeaturesExample.Touch touch = new DualShock4SpecialFeaturesExample.Touch();
				touch.go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				touch.go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				touch.go.transform.SetParent(this.touchpadTransform, true);
				touch.go.GetComponent<MeshRenderer>().material.color = ((i == 0) ? Color.red : Color.green);
				touch.go.SetActive(false);
				this.unusedTouches.Enqueue(touch);
			}
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00074CD4 File Offset: 0x00072ED4
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
					ds4.GetTouchPosition(j, out vector);
					touch2.go.transform.localPosition = new Vector3(vector.x - 0.5f, 0.5f + touch2.go.transform.localScale.y * 0.5f, vector.y - 0.5f);
				}
			}
		}

		private const int maxTouches = 2;

		public int playerId;

		public Transform touchpadTransform;

		public GameObject lightObject;

		public Transform accelerometerTransform;

		private List<DualShock4SpecialFeaturesExample.Touch> touches;

		private Queue<DualShock4SpecialFeaturesExample.Touch> unusedTouches;

		private bool isFlashing;

		private GUIStyle textStyle;

		private class Touch
		{
			public GameObject go;

			public int touchId = -1;
		}
	}
}
