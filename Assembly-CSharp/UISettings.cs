using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
	// Token: 0x06001318 RID: 4888 RVA: 0x00010185 File Offset: 0x0000E385
	public void OnChangeVolume(float value)
	{
		this.volumeMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000101A4 File Offset: 0x0000E3A4
	public void OnChangeSensitivity(float value)
	{
		Settings.mouseSensitivity = Mathf.Pow(value, 2f);
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000101B6 File Offset: 0x0000E3B6
	public void OnChangeVerticalInvert(bool value)
	{
		Settings.mouseInvertVertical = value;
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000101BE File Offset: 0x0000E3BE
	public void OnChangeHorizontalInvert(bool value)
	{
		Settings.mouseInvertHorizontal = value;
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x0005E260 File Offset: 0x0005C460
	public void ResetPlayerPosition()
	{
		if (Game.WorldState == WorldState.Flashback)
		{
			Player.movement.SetPosition(new Vector3(5123.46f, 20.31f, 16.09f));
			return;
		}
		Player.movement.SetPosition(new Vector3(-79.429115f, 1.9195004f, 164.39221f));
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x000101C6 File Offset: 0x0000E3C6
	public void GiveCraftingMaterials()
	{
		this.craftingResource.Amount += 100;
		this.populationResource.Amount += 30;
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x0005E2B4 File Offset: 0x0005C4B4
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

	// Token: 0x0600131F RID: 4895 RVA: 0x0005E2F8 File Offset: 0x0005C4F8
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

	// Token: 0x06001320 RID: 4896 RVA: 0x0005E338 File Offset: 0x0005C538
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

	// Token: 0x06001321 RID: 4897 RVA: 0x000101F0 File Offset: 0x0000E3F0
	public void SaveSettings()
	{
		if (Settings.s != null)
		{
			Settings.s.WriteToDisk();
		}
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x00010209 File Offset: 0x0000E409
	public void ExitToDesktop()
	{
		Application.Quit();
	}

	public AudioMixer volumeMixer;

	public Slider volumeSlider;

	public ItemResource craftingResource;

	public ItemResource populationResource;
}
