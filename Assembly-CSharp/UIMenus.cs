using System;
using Rewired;
using Rewired.UI.ControlMapper;
using UnityEngine;

public class UIMenus : MonoBehaviour
{
	// Token: 0x06000F1E RID: 3870 RVA: 0x00049030 File Offset: 0x00047230
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

	// Token: 0x06000F1F RID: 3871 RVA: 0x000490AC File Offset: 0x000472AC
	private void OnEnable()
	{
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000490AE File Offset: 0x000472AE
	private void OnDisable()
	{
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x000490B0 File Offset: 0x000472B0
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

	// Token: 0x06000F22 RID: 3874 RVA: 0x00049104 File Offset: 0x00047304
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

	// Token: 0x06000F23 RID: 3875 RVA: 0x00049174 File Offset: 0x00047374
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

	// Token: 0x06000F24 RID: 3876 RVA: 0x000491D7 File Offset: 0x000473D7
	public void SetGameplayState(bool isInGameplay, bool showNotifications)
	{
		this.notifications.SetActive(showNotifications);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000491FD File Offset: 0x000473FD
	public void SetGameplayState(bool isInGameplay)
	{
		this.notifications.SetActive(isInGameplay);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00049224 File Offset: 0x00047424
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
