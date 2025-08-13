using System;
using Rewired;
using Rewired.UI.ControlMapper;
using UnityEngine;

// Token: 0x020003B3 RID: 947
public class UIMenus : MonoBehaviour
{
	// Token: 0x06001202 RID: 4610 RVA: 0x0005A240 File Offset: 0x00058440
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

	// Token: 0x06001203 RID: 4611 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnable()
	{
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0005A2BC File Offset: 0x000584BC
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

	// Token: 0x06001206 RID: 4614 RVA: 0x0005A310 File Offset: 0x00058510
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

	// Token: 0x06001207 RID: 4615 RVA: 0x0005A380 File Offset: 0x00058580
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

	// Token: 0x06001208 RID: 4616 RVA: 0x0000F592 File Offset: 0x0000D792
	public void SetGameplayState(bool isInGameplay, bool showNotifications)
	{
		this.notifications.SetActive(showNotifications);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
	public void SetGameplayState(bool isInGameplay)
	{
		this.notifications.SetActive(isInGameplay);
		this.uiGameplay.SetActive(isInGameplay);
		Game.State = (isInGameplay ? GameState.Play : GameState.Menu);
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0005A3E4 File Offset: 0x000585E4
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

	// Token: 0x04001765 RID: 5989
	public static UIMenus u;

	// Token: 0x04001766 RID: 5990
	public static GameObject gameplay;

	// Token: 0x04001767 RID: 5991
	public GameObject uiGameplay;

	// Token: 0x04001768 RID: 5992
	public static UIShop shop;

	// Token: 0x04001769 RID: 5993
	public UIShop uiShop;

	// Token: 0x0400176A RID: 5994
	public static UIBar buildingUpgradeBar;

	// Token: 0x0400176B RID: 5995
	public UIBar uiBuildingUpgradeBar;

	// Token: 0x0400176C RID: 5996
	public static UICraftNotification craftNotification;

	// Token: 0x0400176D RID: 5997
	public UICraftNotification uiCraftNotification;

	// Token: 0x0400176E RID: 5998
	public static UICharacterNotification characterNotification;

	// Token: 0x0400176F RID: 5999
	public UICharacterNotification uiCharacterNotification;

	// Token: 0x04001770 RID: 6000
	public GameObject notifications;

	// Token: 0x04001771 RID: 6001
	public static UIReticle reticle;

	// Token: 0x04001772 RID: 6002
	public UIReticle uiReticle;

	// Token: 0x04001773 RID: 6003
	public static UICameraOverlay cameraOverlay;

	// Token: 0x04001774 RID: 6004
	public UICameraOverlay uiCameraOverlay;

	// Token: 0x04001775 RID: 6005
	public UISubMenu itemsMenu;

	// Token: 0x04001776 RID: 6006
	public ButtonTutorial itemMenuTutorial;

	// Token: 0x04001777 RID: 6007
	public PlayerInventoryCamera playerInventoryCamera;

	// Token: 0x04001778 RID: 6008
	public UISubMenu pauseMenu;

	// Token: 0x04001779 RID: 6009
	public Blackout blackout;

	// Token: 0x0400177A RID: 6010
	public UIRootMenu rootMenu;

	// Token: 0x0400177B RID: 6011
	private Player rePlayer;
}
