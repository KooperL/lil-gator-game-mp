using System;
using UnityEngine;

[ExecuteInEditMode]
public class ScreenSpace : MonoBehaviour
{
	// Token: 0x06001164 RID: 4452 RVA: 0x0000EE2C File Offset: 0x0000D02C
	public static void ForceReload()
	{
		if (ScreenSpace.instance != null)
		{
			ScreenSpace.instance.Load();
		}
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x0000EE45 File Offset: 0x0000D045
	private void OnEnable()
	{
		ScreenSpace.instance = this;
		this.Load();
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0000EE53 File Offset: 0x0000D053
	private void Update()
	{
		if (Screen.width != this.widthBuffer || Screen.height != this.heightBuffer)
		{
			this.Refresh();
		}
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0000EE75 File Offset: 0x0000D075
	protected void Load()
	{
		this.Refresh();
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0000EE7D File Offset: 0x0000D07D
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

	// Token: 0x0600116B RID: 4459 RVA: 0x00058174 File Offset: 0x00056374
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
		Vector2 vector2 = new Vector2((float)Screen.width / (float)Screen.height, 1f);
		for (int i = 0; i < 2; i++)
		{
			this.relativeScale[i] = vector2[i] / vector[i];
		}
		this.UpdateScreenScaleVariable();
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0000EEAC File Offset: 0x0000D0AC
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

	// Token: 0x0600116D RID: 4461 RVA: 0x0005821C File Offset: 0x0005641C
	public void UpdateScreenScaleVariable()
	{
		Shader.SetGlobalVector("SCREEN_SCALE", new Vector4(this.zoomPan.x, this.zoomPan.y, this.zoom / this.relativeScale.x, this.zoom / this.relativeScale.y));
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x00058274 File Offset: 0x00056474
	public static Vector2 CameraToScreen(Vector2 cameraPosition)
	{
		cameraPosition += Vector2.Scale(ScreenSpace.instance.zoomPan, new Vector2(8f, 4.5f));
		cameraPosition.x *= ScreenSpace.instance.zoom / ScreenSpace.instance.relativeScale.x;
		cameraPosition.y *= ScreenSpace.instance.zoom / ScreenSpace.instance.relativeScale.y;
		cameraPosition *= 16f;
		return cameraPosition;
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x00058300 File Offset: 0x00056500
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

	// Token: 0x06001170 RID: 4464 RVA: 0x0005835C File Offset: 0x0005655C
	private Vector2 MaintainAspectFit()
	{
		float num = (float)this.screenRT.width / (float)this.screenRT.height;
		float num2 = Mathf.Min((float)Screen.width / num, (float)Screen.height);
		return new Vector2(num2 * num, num2);
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0000EEDC File Offset: 0x0000D0DC
	private Vector2 ScaleToFit()
	{
		return new Vector2((float)Screen.width, (float)Screen.height);
	}

	// (get) Token: 0x06001172 RID: 4466 RVA: 0x0000EEEF File Offset: 0x0000D0EF
	public float toScreen
	{
		get
		{
			return (float)Screen.height;
		}
	}

	private static ScreenSpace instance;

	public RenderTexture screenRT;

	public ScreenSpace.FitMode selectedFitMode;

	private int widthBuffer;

	private int heightBuffer;

	public float zoom;

	public Vector2 zoomPan;

	private Vector2 relativeScale;

	public enum FitMode
	{
		BEST_FIT,
		MAINTAIN_ASPECT,
		SCALE_TO_FIT,
		INACTIVE
	}
}
