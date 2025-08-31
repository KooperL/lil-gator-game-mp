using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
	// Token: 0x06000F98 RID: 3992 RVA: 0x0004ADE5 File Offset: 0x00048FE5
	public void OnChangeVolume(float value)
	{
		this.volumeMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x0004AE04 File Offset: 0x00049004
	public void OnChangeSensitivity(float value)
	{
		Settings.mouseSensitivity = Mathf.Pow(value, 2f);
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x0004AE16 File Offset: 0x00049016
	public void OnChangeVerticalInvert(bool value)
	{
		Settings.mouseInvertVertical = value;
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x0004AE1E File Offset: 0x0004901E
	public void OnChangeHorizontalInvert(bool value)
	{
		Settings.mouseInvertHorizontal = value;
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0004AE28 File Offset: 0x00049028
	public void ResetPlayerPosition()
	{
		if (Game.WorldState == WorldState.Flashback)
		{
			Player.movement.SetPosition(new Vector3(5123.46f, 20.31f, 16.09f));
			return;
		}
		Player.movement.SetPosition(new Vector3(-79.429115f, 1.9195004f, 164.39221f));
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0004AE7B File Offset: 0x0004907B
	public void GiveCraftingMaterials()
	{
		this.craftingResource.Amount += 100;
		this.populationResource.Amount += 30;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0004AEA8 File Offset: 0x000490A8
	[ContextMenu("Unlock Items")]
	public void UnlockEverything()
	{
		foreach (ItemObject itemObject in ItemManager.i.items)
		{
			if (!itemObject.ignoreUnlockAll)
			{
				itemObject.IsUnlocked = true;
			}
		}
		Player.itemManager.Refresh();
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x0004AEEC File Offset: 0x000490EC
	[ContextMenu("Relock Items")]
	public void RelockEverything()
	{
		foreach (ItemObject itemObject in ItemManager.i.items)
		{
			itemObject.IsUnlocked = false;
			itemObject.IsShopUnlocked = false;
		}
		Player.itemManager.Refresh();
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x0004AF2C File Offset: 0x0004912C
	[ContextMenu("Unlock Recipes")]
	public void GiveAllRecipes()
	{
		foreach (ItemObject itemObject in ItemManager.i.items)
		{
			if (!itemObject.ignoreUnlockAll)
			{
				itemObject.IsShopUnlocked = true;
			}
		}
		Player.itemManager.Refresh();
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0004AF6F File Offset: 0x0004916F
	public void SaveSettings()
	{
		if (Settings.s != null)
		{
			Settings.s.WriteToDisk();
		}
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0004AF88 File Offset: 0x00049188
	public void ExitToDesktop()
	{
		Application.Quit();
	}

	public AudioMixer volumeMixer;

	public Slider volumeSlider;

	public ItemResource craftingResource;

	public ItemResource populationResource;
}
