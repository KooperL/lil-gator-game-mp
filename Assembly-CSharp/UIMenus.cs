using System;
using Rewired;
using Rewired.UI.ControlMapper;
using UnityEngine;

public class UIMenus : MonoBehaviour
{
	// Token: 0x06001263 RID: 4707 RVA: 0x0005C4CC File Offset: 0x0005A6CC
	public void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		UIMenus.u = this;
		UIMenus.shop = this.uiShop;
		UIMenus.gameplay = this.uiGameplay;
		UIMenus.buildingUpgradeBar = this.uiBuildingUpgradeBar;
		UIMenus.craftNotification = this.uiCraftNotification;
		UIMenus.characterNotification = this.uiCharacterNotification;
		UIMenus.reticle = this.uiReticle;
		UIMenus.cameraOverlay = this.uiCameraOverlay;
		Blackout.b = this.blackout;
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnable()
	{
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x0005C548 File Offset: 0x0005A748
	private void Update()
	{
		if (Game.g == null || DialogueSequencer.IsInSequence)
		{
			return;
		}
		if (this.rePlayer.GetButtonDown("Inventory"))
		{
			this.OnInventory();
		}
		if (this.rePlayer.GetButtonDown("Pause"))
		{
			this.OnPause();
		}
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x0005C59C File Offset: 0x0005A79C
	public void OnInventory()
	{
		if (ControlMapper.isActive)
		{
			return;
		}
		if (Game.IsInDialogue)
		{
			return;
		}
		if (this.itemsMenu == null)
		{
			return;
		}
		if (Game.State == GameState.Menu)
		{
			this.CloseMenus();
		}
		if (this.itemsMenu.isActivated)
		{
			this.itemsMenu.Deactivate();
			return;
		}
		this.SetGameplayState(false);
		this.itemsMenu.Activate();
		this.itemMenuTutorial.Press();
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x0005C60C File Offset: 0x0005A80C
	public void CloseMenus()
	{
		if (ControlMapper.isActive)
		{
			return;
		}
		if (this.itemsMenu.isActivated)
		{
			this.itemsMenu.Deactivate();
		}
		if (this.pauseMenu.isActivated)
		{
			this.pauseMenu.Deactivate();
		}
		this.notifications.SetActive(true);
		this.uiGameplay.SetActive(true);
		Game.State = GameState.Play;
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x0000F985 File Offset: 0x0000DB85
	public void SetGameplayState(bool isInGameplay, bool showNotifications)
	{
		this.notifications.SetActive(showNotifications);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x0000F9AB File Offset: 0x0000DBAB
	public void SetGameplayState(bool isInGameplay)
	{
		this.notifications.SetActive(isInGameplay);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x0005C670 File Offset: 0x0005A870
	private void OnPause()
	{
		if (ControlMapper.isActive)
		{
			return;
		}
		if (Game.IsInDialogue || UIRootMenu.lastInputTime == Time.time)
		{
			return;
		}
		if (Game.State == GameState.ItemMenu)
		{
			this.CloseMenus();
			UIRootMenu.lastInputTime = Time.time;
			this.SetGameplayState(false, true);
			this.pauseMenu.Activate();
			return;
		}
		if (Game.State == GameState.Menu)
		{
			this.rootMenu.OnCancel();
			return;
		}
		UIRootMenu.lastInputTime = Time.time;
		this.SetGameplayState(false, true);
		this.pauseMenu.Activate();
	}

	public static UIMenus u;

	public static GameObject gameplay;

	public GameObject uiGameplay;

	public static UIShop shop;

	public UIShop uiShop;

	public static UIBar buildingUpgradeBar;

	public UIBar uiBuildingUpgradeBar;

	public static UICraftNotification craftNotification;

	public UICraftNotification uiCraftNotification;

	public static UICharacterNotification characterNotification;

	public UICharacterNotification uiCharacterNotification;

	public GameObject notifications;

	public static UIReticle reticle;

	public UIReticle uiReticle;

	public static UICameraOverlay cameraOverlay;

	public UICameraOverlay uiCameraOverlay;

	public UISubMenu itemsMenu;

	public ButtonTutorial itemMenuTutorial;

	public PlayerInventoryCamera playerInventoryCamera;

	public UISubMenu pauseMenu;

	public Blackout blackout;

	public UIRootMenu rootMenu;

	private global::Rewired.Player rePlayer;
}
