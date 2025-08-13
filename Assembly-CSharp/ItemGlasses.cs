using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

// Token: 0x0200024C RID: 588
public class ItemGlasses : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B03 RID: 2819 RVA: 0x0003EAC8 File Offset: 0x0003CCC8
	public static void SetPixelFilterEnabled(bool isEnabled)
	{
		ItemGlasses.pixelPerfectCamera = MainCamera.p;
		if (ItemGlasses.pixelPerfectCamera == null)
		{
			ItemGlasses.pixelPerfectCamera = Object.FindObjectOfType<PixelPerfectCamera>();
		}
		ItemGlasses.pixelPerfectCamera.enabled = isEnabled;
		CameraSpaceCanvas c = CameraSpaceCanvas.c;
		if (c != null)
		{
			c.Set(!isEnabled);
		}
		ItemGlasses.ppLayer = ItemGlasses.pixelPerfectCamera.GetComponent<PostProcessLayer>();
		if (isEnabled)
		{
			ItemGlasses.ppLayer.enabled = true;
			if (!ItemGlasses.ppLayer.haveBundlesBeenInited)
			{
				ItemGlasses.ppLayer.InitBundles();
			}
			ItemGlasses.ppLayer.ResetHistory();
		}
		ItemGlasses.ppLayer.enabled = !isEnabled;
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0000A773 File Offset: 0x00008973
	private void Start()
	{
		ItemGlasses.SetPixelFilterEnabled(true);
		this.itemManager = Player.itemManager;
		this.itemManager.bareHead.SetActive(true);
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0000A797 File Offset: 0x00008997
	public void OnRemove()
	{
		ItemGlasses.SetPixelFilterEnabled(false);
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00002229 File Offset: 0x00000429
	public void Cancel()
	{
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x00002229 File Offset: 0x00000429
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00002229 File Offset: 0x00000429
	public void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000E01 RID: 3585
	private static PixelPerfectCamera pixelPerfectCamera;

	// Token: 0x04000E02 RID: 3586
	private static PostProcessLayer ppLayer;

	// Token: 0x04000E03 RID: 3587
	private PlayerItemManager itemManager;
}
