using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200049E RID: 1182
	[AddComponentMenu("")]
	public class PlayerMouseSpriteExample : MonoBehaviour
	{
		// Token: 0x06001D5F RID: 7519 RVA: 0x00072E98 File Offset: 0x00071098
		private void Awake()
		{
			this.pointer = Object.Instantiate<GameObject>(this.pointerPrefab);
			this.pointer.transform.localScale = new Vector3(this.spriteScale, this.spriteScale, this.spriteScale);
			if (this.hideHardwarePointer)
			{
				Cursor.visible = false;
			}
			this.mouse = PlayerMouse.Factory.Create();
			this.mouse.playerId = this.playerId;
			this.mouse.xAxis.actionName = this.horizontalAction;
			this.mouse.yAxis.actionName = this.verticalAction;
			this.mouse.wheel.yAxis.actionName = this.wheelAction;
			this.mouse.leftButton.actionName = this.leftButtonAction;
			this.mouse.rightButton.actionName = this.rightButtonAction;
			this.mouse.middleButton.actionName = this.middleButtonAction;
			this.mouse.pointerSpeed = 1f;
			this.mouse.wheel.yAxis.repeatRate = 5f;
			this.mouse.screenPosition = new Vector2((float)Screen.width * 0.5f, (float)Screen.height * 0.5f);
			this.mouse.ScreenPositionChangedEvent += this.OnScreenPositionChanged;
			this.OnScreenPositionChanged(this.mouse.screenPosition);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0007300C File Offset: 0x0007120C
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.pointer.transform.Rotate(Vector3.forward, this.mouse.wheel.yAxis.value * 20f);
			if (this.mouse.leftButton.justPressed)
			{
				this.CreateClickEffect(new Color(0f, 1f, 0f, 1f));
			}
			if (this.mouse.rightButton.justPressed)
			{
				this.CreateClickEffect(new Color(1f, 0f, 0f, 1f));
			}
			if (this.mouse.middleButton.justPressed)
			{
				this.CreateClickEffect(new Color(1f, 1f, 0f, 1f));
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00016798 File Offset: 0x00014998
		private void OnDestroy()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.mouse.ScreenPositionChangedEvent -= this.OnScreenPositionChanged;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000730E4 File Offset: 0x000712E4
		private void CreateClickEffect(Color color)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.clickEffectPrefab);
			gameObject.transform.localScale = new Vector3(this.spriteScale, this.spriteScale, this.spriteScale);
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(this.mouse.screenPosition.x, this.mouse.screenPosition.y, this.distanceFromCamera));
			gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
			Object.Destroy(gameObject, 0.5f);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00073174 File Offset: 0x00071374
		private void OnScreenPositionChanged(Vector2 position)
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, this.distanceFromCamera));
			this.pointer.transform.position = vector;
		}

		// Token: 0x04001EBC RID: 7868
		[Tooltip("The Player that will control the mouse")]
		public int playerId;

		// Token: 0x04001EBD RID: 7869
		[Tooltip("The Rewired Action used for the mouse horizontal axis.")]
		public string horizontalAction = "MouseX";

		// Token: 0x04001EBE RID: 7870
		[Tooltip("The Rewired Action used for the mouse vertical axis.")]
		public string verticalAction = "MouseY";

		// Token: 0x04001EBF RID: 7871
		[Tooltip("The Rewired Action used for the mouse wheel axis.")]
		public string wheelAction = "MouseWheel";

		// Token: 0x04001EC0 RID: 7872
		[Tooltip("The Rewired Action used for the mouse left button.")]
		public string leftButtonAction = "MouseLeftButton";

		// Token: 0x04001EC1 RID: 7873
		[Tooltip("The Rewired Action used for the mouse right button.")]
		public string rightButtonAction = "MouseRightButton";

		// Token: 0x04001EC2 RID: 7874
		[Tooltip("The Rewired Action used for the mouse middle button.")]
		public string middleButtonAction = "MouseMiddleButton";

		// Token: 0x04001EC3 RID: 7875
		[Tooltip("The distance from the camera that the pointer will be drawn.")]
		public float distanceFromCamera = 1f;

		// Token: 0x04001EC4 RID: 7876
		[Tooltip("The scale of the sprite pointer.")]
		public float spriteScale = 0.05f;

		// Token: 0x04001EC5 RID: 7877
		[Tooltip("The pointer prefab.")]
		public GameObject pointerPrefab;

		// Token: 0x04001EC6 RID: 7878
		[Tooltip("The click effect prefab.")]
		public GameObject clickEffectPrefab;

		// Token: 0x04001EC7 RID: 7879
		[Tooltip("Should the hardware pointer be hidden?")]
		public bool hideHardwarePointer = true;

		// Token: 0x04001EC8 RID: 7880
		[NonSerialized]
		private GameObject pointer;

		// Token: 0x04001EC9 RID: 7881
		[NonSerialized]
		private PlayerMouse mouse;
	}
}
