using System;
using UnityEngine;

// Token: 0x0200037D RID: 893
[ExecuteInEditMode]
public class ScreenSpace : MonoBehaviour
{
	// Token: 0x06001104 RID: 4356 RVA: 0x0000EA43 File Offset: 0x0000CC43
	public static void ForceReload()
	{
		if (ScreenSpace.instance != null)
		{
			ScreenSpace.instance.Load();
		}
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0000EA5C File Offset: 0x0000CC5C
	private void OnEnable()
	{
		ScreenSpace.instance = this;
		this.Load();
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0000EA6A File Offset: 0x0000CC6A
	private void Update()
	{
		if (Screen.width != this.widthBuffer || Screen.height != this.heightBuffer)
		{
			this.Refresh();
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0000EA8C File Offset: 0x0000CC8C
	protected void Load()
	{
		this.Refresh();
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0000EA94 File Offset: 0x0000CC94
	protected void Refresh()
	{
		this.widthBuffer = Screen.width;
		this.heightBuffer = Screen.height;
		if (this.widthBuffer == 0 || this.heightBuffer == 0)
		{
			return;
		}
		this.RefreshScale();
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x000561B4 File Offset: 0x000543B4
	private void RefreshScale()
	{
		Vector2 vector = Vector2.one;
		switch (this.selectedFitMode)
		{
		case ScreenSpace.FitMode.BEST_FIT:
			vector = this.BestFit();
			break;
		case ScreenSpace.FitMode.MAINTAIN_ASPECT:
			vector = this.MaintainAspectFit();
			break;
		case ScreenSpace.FitMode.SCALE_TO_FIT:
			vector = this.ScaleToFit();
			break;
		}
		vector /= this.toScreen;
		this.relativeScale = Vector2.zero;
		Vector2 vector2;
		vector2..ctor((float)Screen.width / (float)Screen.height, 1f);
		for (int i = 0; i < 2; i++)
		{
			this.relativeScale[i] = vector2[i] / vector[i];
		}
		this.UpdateScreenScaleVariable();
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0000EAC3 File Offset: 0x0000CCC3
	public static void SetZoom(float zoom, Vector2 zoomPan)
	{
		if (ScreenSpace.instance == null)
		{
			return;
		}
		ScreenSpace.instance.zoom = zoom;
		ScreenSpace.instance.zoomPan = zoomPan;
		ScreenSpace.instance.UpdateScreenScaleVariable();
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0005625C File Offset: 0x0005445C
	public void UpdateScreenScaleVariable()
	{
		Shader.SetGlobalVector("SCREEN_SCALE", new Vector4(this.zoomPan.x, this.zoomPan.y, this.zoom / this.relativeScale.x, this.zoom / this.relativeScale.y));
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x000562B4 File Offset: 0x000544B4
	public static Vector2 CameraToScreen(Vector2 cameraPosition)
	{
		cameraPosition += Vector2.Scale(ScreenSpace.instance.zoomPan, new Vector2(8f, 4.5f));
		cameraPosition.x *= ScreenSpace.instance.zoom / ScreenSpace.instance.relativeScale.x;
		cameraPosition.y *= ScreenSpace.instance.zoom / ScreenSpace.instance.relativeScale.y;
		cameraPosition *= 16f;
		return cameraPosition;
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x00056340 File Offset: 0x00054540
	private Vector2 BestFit()
	{
		Vector2 vector = this.MaintainAspectFit();
		float num = vector.y / (float)this.screenRT.height;
		num = Mathf.Floor(num);
		if (num > 0f)
		{
			vector = num * new Vector2((float)this.screenRT.width, (float)this.screenRT.height);
		}
		return vector;
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0005639C File Offset: 0x0005459C
	private Vector2 MaintainAspectFit()
	{
		float num = (float)this.screenRT.width / (float)this.screenRT.height;
		float num2 = Mathf.Min((float)Screen.width / num, (float)Screen.height);
		return new Vector2(num2 * num, num2);
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x0000EAF3 File Offset: 0x0000CCF3
	private Vector2 ScaleToFit()
	{
		return new Vector2((float)Screen.width, (float)Screen.height);
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06001112 RID: 4370 RVA: 0x0000EB06 File Offset: 0x0000CD06
	public float toScreen
	{
		get
		{
			return (float)Screen.height;
		}
	}

	// Token: 0x040015FE RID: 5630
	private static ScreenSpace instance;

	// Token: 0x040015FF RID: 5631
	public RenderTexture screenRT;

	// Token: 0x04001600 RID: 5632
	public ScreenSpace.FitMode selectedFitMode;

	// Token: 0x04001601 RID: 5633
	private int widthBuffer;

	// Token: 0x04001602 RID: 5634
	private int heightBuffer;

	// Token: 0x04001603 RID: 5635
	public float zoom;

	// Token: 0x04001604 RID: 5636
	public Vector2 zoomPan;

	// Token: 0x04001605 RID: 5637
	private Vector2 relativeScale;

	// Token: 0x0200037E RID: 894
	public enum FitMode
	{
		// Token: 0x04001607 RID: 5639
		BEST_FIT,
		// Token: 0x04001608 RID: 5640
		MAINTAIN_ASPECT,
		// Token: 0x04001609 RID: 5641
		SCALE_TO_FIT,
		// Token: 0x0400160A RID: 5642
		INACTIVE
	}
}
