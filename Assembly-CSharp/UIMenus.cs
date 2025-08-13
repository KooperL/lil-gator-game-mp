using System;
using Rewired;
using Rewired.UI.ControlMapper;
using UnityEngine;

// Token: 0x020002CF RID: 719
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

	// Token: 0x040013E1 RID: 5089
	public static UIMenus u;

	// Token: 0x040013E2 RID: 5090
	public static GameObject gameplay;

	// Token: 0x040013E3 RID: 5091
	public GameObject uiGameplay;

	// Token: 0x040013E4 RID: 5092
	public static UIShop shop;

	// Token: 0x040013E5 RID: 5093
	public UIShop uiShop;

	// Token: 0x040013E6 RID: 5094
	public static UIBar buildingUpgradeBar;

	// Token: 0x040013E7 RID: 5095
	public UIBar uiBuildingUpgradeBar;

	// Token: 0x040013E8 RID: 5096
	public static UICraftNotification craftNotification;

	// Token: 0x040013E9 RID: 5097
	public UICraftNotification uiCraftNotification;

	// Token: 0x040013EA RID: 5098
	public static UICharacterNotification characterNotification;

	// Token: 0x040013EB RID: 5099
	public UICharacterNotification uiCharacterNotification;

	// Token: 0x040013EC RID: 5100
	public GameObject notifications;

	// Token: 0x040013ED RID: 5101
	public static UIReticle reticle;

	// Token: 0x040013EE RID: 5102
	public UIReticle uiReticle;

	// Token: 0x040013EF RID: 5103
	public static UICameraOverlay cameraOverlay;

	// Token: 0x040013F0 RID: 5104
	public UICameraOverlay uiCameraOverlay;

	// Token: 0x040013F1 RID: 5105
	public UISubMenu itemsMenu;

	// Token: 0x040013F2 RID: 5106
	public ButtonTutorial itemMenuTutorial;

	// Token: 0x040013F3 RID: 5107
	public PlayerInventoryCamera playerInventoryCamera;

	// Token: 0x040013F4 RID: 5108
	public UISubMenu pauseMenu;

	// Token: 0x040013F5 RID: 5109
	public Blackout blackout;

	// Token: 0x040013F6 RID: 5110
	public UIRootMenu rootMenu;

	// Token: 0x040013F7 RID: 5111
	private global::Rewired.Player rePlayer;
}
