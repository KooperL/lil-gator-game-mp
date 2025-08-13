using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class ItemCamera : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0000A346 File Offset: 0x00008546
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

	// Token: 0x06000AAA RID: 2730 RVA: 0x0000A353 File Offset: 0x00008553
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0003DB10 File Offset: 0x0003BD10
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

	// Token: 0x06000AAC RID: 2732 RVA: 0x0000A387 File Offset: 0x00008587
	public void InputCancel()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0000A390 File Offset: 0x00008590
	public void InputHideUI()
	{
		HideUI.Toggle();
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0000A397 File Offset: 0x00008597
	public void InputCapture()
	{
		if (!this.isTakingScreenshot)
		{
			CoroutineUtil.Start(this.TakeScreenshot());
		}
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0003DB8C File Offset: 0x0003BD8C
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

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0000A3AD File Offset: 0x000085AD
	public void InputLock()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Static);
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0000A3B6 File Offset: 0x000085B6
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

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0000A3C5 File Offset: 0x000085C5
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0003DBB8 File Offset: 0x0003BDB8
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
				this.staticCamera = Object.Instantiate<GameObject>(this.staticCameraPrefab);
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
			Object.Destroy(this.staticCamera);
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

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0000A3D6 File Offset: 0x000085D6
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0003DD30 File Offset: 0x0003BF30
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

	// Token: 0x06000AB6 RID: 2742 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Cancel()
	{
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0000A387 File Offset: 0x00008587
	private void OnDestroy()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0003DDC0 File Offset: 0x0003BFC0
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

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0000A387 File Offset: 0x00008587
	public virtual void OnRemove()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0000A3DD File Offset: 0x000085DD
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000D7E RID: 3454
	public static bool isActive;

	// Token: 0x04000D7F RID: 3455
	public static ItemCamera instance;

	// Token: 0x04000D80 RID: 3456
	protected PlayerItemManager itemManager;

	// Token: 0x04000D81 RID: 3457
	protected PlayerMovement movement;

	// Token: 0x04000D82 RID: 3458
	protected Transform mainCamera;

	// Token: 0x04000D83 RID: 3459
	protected Animator animator;

	// Token: 0x04000D84 RID: 3460
	protected CharacterReactionController reaction;

	// Token: 0x04000D85 RID: 3461
	public bool hasAmmo;

	// Token: 0x04000D86 RID: 3462
	[ConditionalHide("hasAmmo", true)]
	public int ammo = 6;

	// Token: 0x04000D87 RID: 3463
	private int currentAmmo;

	// Token: 0x04000D88 RID: 3464
	public AudioSourceVariance aimSound;

	// Token: 0x04000D89 RID: 3465
	public AudioSourceVariance fireSound;

	// Token: 0x04000D8A RID: 3466
	public GameObject forwardCamera;

	// Token: 0x04000D8B RID: 3467
	public GameObject backwardsCamera;

	// Token: 0x04000D8C RID: 3468
	public GameObject staticCameraPrefab;

	// Token: 0x04000D8D RID: 3469
	private GameObject staticCamera;

	// Token: 0x04000D8E RID: 3470
	public GameObject visibleCamera;

	// Token: 0x04000D8F RID: 3471
	public MoveToBone[] anchorBones;

	// Token: 0x04000D90 RID: 3472
	public ItemCamera.CameraMode cameraMode = ItemCamera.CameraMode.Off;

	// Token: 0x04000D91 RID: 3473
	private bool isTakingScreenshot;

	// Token: 0x04000D92 RID: 3474
	public bool isOnRight;

	// Token: 0x02000235 RID: 565
	public enum CameraMode
	{
		// Token: 0x04000D94 RID: 3476
		Forward,
		// Token: 0x04000D95 RID: 3477
		Backward,
		// Token: 0x04000D96 RID: 3478
		Static,
		// Token: 0x04000D97 RID: 3479
		Off
	}
}
