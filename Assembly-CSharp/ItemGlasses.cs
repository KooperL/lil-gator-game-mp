using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

public class ItemGlasses : MonoBehaviour, IItemBehaviour
{
	// Token: 0x0600096C RID: 2412 RVA: 0x0002CB08 File Offset: 0x0002AD08
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

	// Token: 0x0600096D RID: 2413 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
	private void Start()
	{
		ItemGlasses.SetPixelFilterEnabled(true);
		this.itemManager = Player.itemManager;
		this.itemManager.bareHead.SetActive(true);
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
	public void OnRemove()
	{
		ItemGlasses.SetPixelFilterEnabled(false);
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0002CBDD File Offset: 0x0002ADDD
	public void Cancel()
	{
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0002CBDF File Offset: 0x0002ADDF
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0002CBE1 File Offset: 0x0002ADE1
	public void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0002CBE3 File Offset: 0x0002ADE3
	public void SetIndex(int index)
	{
	}

	private static PixelPerfectCamera pixelPerfectCamera;

	private static PostProcessLayer ppLayer;

	private PlayerItemManager itemManager;
}
