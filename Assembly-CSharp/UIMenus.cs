using System;
using Rewired;
using Rewired.UI.ControlMapper;
using UnityEngine;

public class UIMenus : MonoBehaviour
{
	// Token: 0x06001262 RID: 4706 RVA: 0x0005C204 File Offset: 0x0005A404
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

	// Token: 0x06001263 RID: 4707 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnable()
	{
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x0005C280 File Offset: 0x0005A480
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

	// Token: 0x06001266 RID: 4710 RVA: 0x0005C2D4 File Offset: 0x0005A4D4
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

	// Token: 0x06001267 RID: 4711 RVA: 0x0005C344 File Offset: 0x0005A544
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

	// Token: 0x06001268 RID: 4712 RVA: 0x0000F97B File Offset: 0x0000DB7B
	public void SetGameplayState(bool isInGameplay, bool showNotifications)
	{
		this.notifications.SetActive(showNotifications);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x0000F9A1 File Offset: 0x0000DBA1
	public void SetGameplayState(bool isInGameplay)
	{
		this.notifications.SetActive(isInGameplay);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x0005C3A8 File Offset: 0x0005A5A8
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

	private Player rePlayer;
}
