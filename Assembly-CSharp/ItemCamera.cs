using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001B7 RID: 439
public class ItemCamera : MonoBehaviour, IItemBehaviour
{
	// Token: 0x1700007E RID: 126
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

	// Token: 0x04000B72 RID: 2930
	public static bool isActive;

	// Token: 0x04000B73 RID: 2931
	public static ItemCamera instance;

	// Token: 0x04000B74 RID: 2932
	protected PlayerItemManager itemManager;

	// Token: 0x04000B75 RID: 2933
	protected PlayerMovement movement;

	// Token: 0x04000B76 RID: 2934
	protected Transform mainCamera;

	// Token: 0x04000B77 RID: 2935
	protected Animator animator;

	// Token: 0x04000B78 RID: 2936
	protected CharacterReactionController reaction;

	// Token: 0x04000B79 RID: 2937
	public bool hasAmmo;

	// Token: 0x04000B7A RID: 2938
	[ConditionalHide("hasAmmo", true)]
	public int ammo = 6;

	// Token: 0x04000B7B RID: 2939
	private int currentAmmo;

	// Token: 0x04000B7C RID: 2940
	public AudioSourceVariance aimSound;

	// Token: 0x04000B7D RID: 2941
	public AudioSourceVariance fireSound;

	// Token: 0x04000B7E RID: 2942
	public GameObject forwardCamera;

	// Token: 0x04000B7F RID: 2943
	public GameObject backwardsCamera;

	// Token: 0x04000B80 RID: 2944
	public GameObject staticCameraPrefab;

	// Token: 0x04000B81 RID: 2945
	private GameObject staticCamera;

	// Token: 0x04000B82 RID: 2946
	public GameObject visibleCamera;

	// Token: 0x04000B83 RID: 2947
	public MoveToBone[] anchorBones;

	// Token: 0x04000B84 RID: 2948
	public ItemCamera.CameraMode cameraMode = ItemCamera.CameraMode.Off;

	// Token: 0x04000B85 RID: 2949
	private bool isTakingScreenshot;

	// Token: 0x04000B86 RID: 2950
	public bool isOnRight;

	// Token: 0x020003DA RID: 986
	public enum CameraMode
	{
		// Token: 0x04001C38 RID: 7224
		Forward,
		// Token: 0x04001C39 RID: 7225
		Backward,
		// Token: 0x04001C3A RID: 7226
		Static,
		// Token: 0x04001C3B RID: 7227
		Off
	}
}
