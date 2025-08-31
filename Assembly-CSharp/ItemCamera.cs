using System;
using System.Collections;
using UnityEngine;

public class ItemCamera : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000918 RID: 2328 RVA: 0x0002B748 File Offset: 0x00029948
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

	// Token: 0x06000919 RID: 2329 RVA: 0x0002B755 File Offset: 0x00029955
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0002B78C File Offset: 0x0002998C
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

	// Token: 0x0600091B RID: 2331 RVA: 0x0002B805 File Offset: 0x00029A05
	public void InputCancel()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0002B80E File Offset: 0x00029A0E
	public void InputHideUI()
	{
		HideUI.Toggle();
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0002B815 File Offset: 0x00029A15
	public void InputCapture()
	{
		if (!this.isTakingScreenshot)
		{
			CoroutineUtil.Start(this.TakeScreenshot());
		}
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0002B82C File Offset: 0x00029A2C
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

	// Token: 0x0600091F RID: 2335 RVA: 0x0002B857 File Offset: 0x00029A57
	public void InputLock()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Static);
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0002B860 File Offset: 0x00029A60
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

	// Token: 0x06000921 RID: 2337 RVA: 0x0002B86F File Offset: 0x00029A6F
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0002B880 File Offset: 0x00029A80
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

	// Token: 0x06000923 RID: 2339 RVA: 0x0002B9F7 File Offset: 0x00029BF7
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0002BA00 File Offset: 0x00029C00
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

	// Token: 0x06000925 RID: 2341 RVA: 0x0002BA8E File Offset: 0x00029C8E
	public virtual void Cancel()
	{
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0002BA90 File Offset: 0x00029C90
	private void OnDestroy()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0002BA9C File Offset: 0x00029C9C
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

	// Token: 0x06000928 RID: 2344 RVA: 0x0002BB31 File Offset: 0x00029D31
	public virtual void OnRemove()
	{
		this.SetCameraMode(ItemCamera.CameraMode.Off);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0002BB3A File Offset: 0x00029D3A
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
