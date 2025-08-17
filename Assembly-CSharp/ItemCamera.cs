using System;
using System.Collections;
using UnityEngine;

public class ItemCamera : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0000A67A File Offset: 0x0000887A
	private PlayerItemManager.EquippedState EquippedState
	{
		get
		{
			if (!this.isOnRight)
			{
				return PlayerItemManager.EquippedState.Item;
			}
			return PlayerItemManager.EquippedState.ItemR;
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0000A687 File Offset: 0x00008887
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0003F618 File Offset: 0x0003D818
	public virtual void Input(bool isDown, bool isHeld)
	{
		if (Game.HasControl)
		{
			if (this.cameraMode != ItemCamera.CameraMode.Off)
			{
				this.itemManager.SetEquippedState(this.EquippedState, false);
				return;
			}
			if ((this.movement.IsGrounded || this.movement.InAir) && isDown)
			{
				this.itemManager.SetEquippedState(this.EquippedState, false);
				this.SetCameraMode(ItemCamera.CameraMode.Forward);
				return;
			}
		}
		else if (this.cameraMode != ItemCamera.CameraMode.Off)
		{
			this.SetCameraMode(ItemCamera.CameraMode.Off);
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0000A6BB File Offset: 0x000088BB
	public void InputCancel()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0000A6C4 File Offset: 0x000088C4
	public void InputHideUI()
	{
		HideUI.Toggle();
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0000A6CB File Offset: 0x000088CB
	public void InputCapture()
	{
		if (!this.isTakingScreenshot)
		{
			CoroutineUtil.Start(this.TakeScreenshot());
		}
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0003F694 File Offset: 0x0003D894
	public void InputSwitch()
	{
		ItemCamera.CameraMode cameraMode = this.cameraMode;
		if (cameraMode == ItemCamera.CameraMode.Forward)
		{
			this.SetCameraMode(ItemCamera.CameraMode.Backward);
			return;
		}
		if (cameraMode != ItemCamera.CameraMode.Backward)
		{
			return;
		}
		this.SetCameraMode(ItemCamera.CameraMode.Forward);
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0000A6E1 File Offset: 0x000088E1
	public void InputLock()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Static);
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0000A6EA File Offset: 0x000088EA
	private IEnumerator TakeScreenshot()
	{
		this.isTakingScreenshot = true;
		HideUI.SetUIHidden(true);
		yield return null;
		ScreenCapture.CaptureScreenshot("Screenshot.png", 1);
		yield return null;
		HideUI.SetUIHidden(false);
		this.isTakingScreenshot = false;
		yield break;
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0000A6F9 File Offset: 0x000088F9
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0003F6C0 File Offset: 0x0003D8C0
	private void SetCameraMode(ItemCamera.CameraMode cameraMode)
	{
		this.cameraMode = cameraMode;
		if (cameraMode != ItemCamera.CameraMode.Off && this.aimSound != null)
		{
			this.aimSound.Play();
		}
		this.visibleCamera.SetActive(cameraMode == ItemCamera.CameraMode.Off);
		switch (cameraMode)
		{
		case ItemCamera.CameraMode.Forward:
			PlayerOrbitCamera.active.SetCameraMode(PlayerOrbitCamera.CameraMode.Forward);
			break;
		case ItemCamera.CameraMode.Backward:
			PlayerOrbitCamera.active.SetCameraMode(PlayerOrbitCamera.CameraMode.Backward);
			break;
		case ItemCamera.CameraMode.Static:
			if (this.staticCamera == null)
			{
				this.staticCamera = global::UnityEngine.Object.Instantiate<GameObject>(this.staticCameraPrefab);
			}
			this.staticCamera.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
			PlayerOrbitCamera.active.SetCameraMode(PlayerOrbitCamera.CameraMode.Static);
			break;
		case ItemCamera.CameraMode.Off:
			PlayerOrbitCamera.active.SetCameraMode(PlayerOrbitCamera.CameraMode.Off);
			HideUI.SetUIHidden(false);
			break;
		}
		if (this.staticCamera != null && cameraMode != ItemCamera.CameraMode.Static)
		{
			global::UnityEngine.Object.Destroy(this.staticCamera);
		}
		if (UIMenus.cameraOverlay != null)
		{
			UIMenus.cameraOverlay.SetState(cameraMode, this.isOnRight);
		}
		ItemCamera.isActive = cameraMode != ItemCamera.CameraMode.Off;
		if (cameraMode != ItemCamera.CameraMode.Off)
		{
			ItemCamera.instance = this;
		}
		Player.handIK.SetEnabled(true, cameraMode == ItemCamera.CameraMode.Forward || cameraMode == ItemCamera.CameraMode.Backward);
		Player.itemManager.IsAiming = cameraMode == ItemCamera.CameraMode.Forward || cameraMode == ItemCamera.CameraMode.Backward;
		Player.itemManager.SetItemInUse(this, cameraMode == ItemCamera.CameraMode.Forward || cameraMode == ItemCamera.CameraMode.Backward);
		this.animator.SetBool("Aiming", cameraMode == ItemCamera.CameraMode.Forward || cameraMode == ItemCamera.CameraMode.Backward);
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0000A70A File Offset: 0x0000890A
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0003F838 File Offset: 0x0003DA38
	public virtual void LateUpdate()
	{
		if ((this.cameraMode == ItemCamera.CameraMode.Forward || this.cameraMode == ItemCamera.CameraMode.Backward) && (Player.itemManager.equippedState != this.EquippedState || (Player.movement.isModified && Player.movement.modItemRule == PlayerMovement.ModRule.Locked)))
		{
			this.SetCameraMode(ItemCamera.CameraMode.Off);
		}
		if (this.cameraMode == ItemCamera.CameraMode.Static && (!Game.HasControl || Vector3.Distance(MainCamera.t.position, Player.Position) > 60f || Player.itemManager.IsAiming))
		{
			this.SetCameraMode(ItemCamera.CameraMode.Off);
		}
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Cancel()
	{
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0000A6BB File Offset: 0x000088BB
	private void OnDestroy()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0003F8C8 File Offset: 0x0003DAC8
	public virtual void SetEquipped(bool isEquipped)
	{
		Transform transform = this.itemManager.transform;
		if (base.transform.parent != transform)
		{
			base.transform.ApplyParent(transform);
		}
		foreach (MoveToBone moveToBone in this.anchorBones)
		{
			moveToBone.localScale = new Vector3(this.isOnRight ? (-1f) : 1f, 1f, 1f);
			moveToBone.transform.localScale = moveToBone.localScale;
		}
		if (!isEquipped)
		{
			this.Cancel();
		}
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0000A6BB File Offset: 0x000088BB
	public virtual void OnRemove()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0000A711 File Offset: 0x00008911
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	public static bool isActive;

	public static ItemCamera instance;

	protected PlayerItemManager itemManager;

	protected PlayerMovement movement;

	protected Transform mainCamera;

	protected Animator animator;

	protected CharacterReactionController reaction;

	public bool hasAmmo;

	[ConditionalHide("hasAmmo", true)]
	public int ammo = 6;

	private int currentAmmo;

	public AudioSourceVariance aimSound;

	public AudioSourceVariance fireSound;

	public GameObject forwardCamera;

	public GameObject backwardsCamera;

	public GameObject staticCameraPrefab;

	private GameObject staticCamera;

	public GameObject visibleCamera;

	public MoveToBone[] anchorBones;

	public ItemCamera.CameraMode cameraMode = ItemCamera.CameraMode.Off;

	private bool isTakingScreenshot;

	public bool isOnRight;

	public enum CameraMode
	{
		Forward,
		Backward,
		Static,
		Off
	}
}
