using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Token: 0x020003CF RID: 975
public class UISettings : MonoBehaviour
{
	// Token: 0x060012B8 RID: 4792 RVA: 0x0000FD88 File Offset: 0x0000DF88
	public void OnChangeVolume(float value)
	{
		this.volumeMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x0000FDA7 File Offset: 0x0000DFA7
	public void OnChangeSensitivity(float value)
	{
		Settings.mouseSensitivity = Mathf.Pow(value, 2f);
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x0000FDB9 File Offset: 0x0000DFB9
	public void OnChangeVerticalInvert(bool value)
	{
		Settings.mouseInvertVertical = value;
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x0000FDC1 File Offset: 0x0000DFC1
	public void OnChangeHorizontalInvert(bool value)
	{
		Settings.mouseInvertHorizontal = value;
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x0005C238 File Offset: 0x0005A438
	public void ResetPlayerPosition()
	{
		if (Game.WorldState == WorldState.Flashback)
		{
			Player.movement.SetPosition(new Vector3(5123.46f, 20.31f, 16.09f));
			return;
		}
		Player.movement.SetPosition(new Vector3(-79.429115f, 1.9195004f, 164.39221f));
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x0000FDC9 File Offset: 0x0000DFC9
	public void GiveCraftingMaterials()
	{
		this.craftingResource.Amount += 100;
		this.populationResource.Amount += 30;
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x0005C28C File Offset: 0x0005A48C
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

	// Token: 0x060012BF RID: 4799 RVA: 0x0005C2D0 File Offset: 0x0005A4D0
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

	// Token: 0x060012C0 RID: 4800 RVA: 0x0005C310 File Offset: 0x0005A510
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

	// Token: 0x060012C1 RID: 4801 RVA: 0x0000FDF3 File Offset: 0x0000DFF3
	public void SaveSettings()
	{
		if (Settings.s != null)
		{
			Settings.s.WriteToDisk();
		}
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x0000FE0C File Offset: 0x0000E00C
	public void ExitToDesktop()
	{
		Application.Quit();
	}

	// Token: 0x04001827 RID: 6183
	public AudioMixer volumeMixer;

	// Token: 0x04001828 RID: 6184
	public Slider volumeSlider;

	// Token: 0x04001829 RID: 6185
	public ItemResource craftingResource;

	// Token: 0x0400182A RID: 6186
	public ItemResource populationResource;
}
