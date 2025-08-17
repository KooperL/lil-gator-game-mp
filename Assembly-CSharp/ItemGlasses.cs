using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;

public class ItemGlasses : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B4F RID: 2895 RVA: 0x000405D0 File Offset: 0x0003E7D0
	public static void SetPixelFilterEnabled(bool isEnabled)
	{
		ItemGlasses.pixelPerfectCamera = MainCamera.p;
		if (ItemGlasses.pixelPerfectCamera == null)
		{
			ItemGlasses.pixelPerfectCamera = global::UnityEngine.Object.FindObjectOfType<PixelPerfectCamera>();
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

	// Token: 0x06000B50 RID: 2896 RVA: 0x0000AAA7 File Offset: 0x00008CA7
	private void Start()
	{
		ItemGlasses.SetPixelFilterEnabled(true);
		this.itemManager = Player.itemManager;
		this.itemManager.bareHead.SetActive(true);
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x0000AACB File Offset: 0x00008CCB
	public void OnRemove()
	{
		ItemGlasses.SetPixelFilterEnabled(false);
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x00002229 File Offset: 0x00000429
	public void Cancel()
	{
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x00002229 File Offset: 0x00000429
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00002229 File Offset: 0x00000429
	public void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	private static PixelPerfectCamera pixelPerfectCamera;

	private static PostProcessLayer ppLayer;

	private PlayerItemManager itemManager;
}
