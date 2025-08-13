using System;
using UnityEngine;

// Token: 0x020002A3 RID: 675
[ExecuteInEditMode]
public class ScreenSpace : MonoBehaviour
{
	// Token: 0x06000E40 RID: 3648 RVA: 0x0004472D File Offset: 0x0004292D
	public static void ForceReload()
	{
		if (ScreenSpace.instance != null)
		{
			ScreenSpace.instance.Load();
		}
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00044746 File Offset: 0x00042946
	private void Awake()
	{
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00044748 File Offset: 0x00042948
	private void OnEnable()
	{
		ScreenSpace.instance = this;
		this.Load();
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00044756 File Offset: 0x00042956
	private void OnDisable()
	{
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x00044758 File Offset: 0x00042958
	private void Update()
	{
		if (Screen.width != this.widthBuffer || Screen.height != this.heightBuffer)
		{
			this.Refresh();
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0004477A File Offset: 0x0004297A
	protected void Load()
	{
		this.Refresh();
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x00044782 File Offset: 0x00042982
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

	// Token: 0x06000E47 RID: 3655 RVA: 0x000447B4 File Offset: 0x000429B4
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

	// Token: 0x06000E48 RID: 3656 RVA: 0x0004485B File Offset: 0x00042A5B
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

	// Token: 0x06000E49 RID: 3657 RVA: 0x0004488C File Offset: 0x00042A8C
	public void UpdateScreenScaleVariable()
	{
		Shader.SetGlobalVector("SCREEN_SCALE", new Vector4(this.zoomPan.x, this.zoomPan.y, this.zoom / this.relativeScale.x, this.zoom / this.relativeScale.y));
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000448E4 File Offset: 0x00042AE4
	public static Vector2 CameraToScreen(Vector2 cameraPosition)
	{
		cameraPosition += Vector2.Scale(ScreenSpace.instance.zoomPan, new Vector2(8f, 4.5f));
		cameraPosition.x *= ScreenSpace.instance.zoom / ScreenSpace.instance.relativeScale.x;
		cameraPosition.y *= ScreenSpace.instance.zoom / ScreenSpace.instance.relativeScale.y;
		cameraPosition *= 16f;
		return cameraPosition;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x00044970 File Offset: 0x00042B70
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

	// Token: 0x06000E4C RID: 3660 RVA: 0x000449CC File Offset: 0x00042BCC
	private Vector2 MaintainAspectFit()
	{
		float num = (float)this.screenRT.width / (float)this.screenRT.height;
		float num2 = Mathf.Min((float)Screen.width / num, (float)Screen.height);
		return new Vector2(num2 * num, num2);
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x00044A10 File Offset: 0x00042C10
	private Vector2 ScaleToFit()
	{
		return new Vector2((float)Screen.width, (float)Screen.height);
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00044A23 File Offset: 0x00042C23
	public float toScreen
	{
		get
		{
			return (float)Screen.height;
		}
	}

	// Token: 0x040012A1 RID: 4769
	private static ScreenSpace instance;

	// Token: 0x040012A2 RID: 4770
	public RenderTexture screenRT;

	// Token: 0x040012A3 RID: 4771
	public ScreenSpace.FitMode selectedFitMode;

	// Token: 0x040012A4 RID: 4772
	private int widthBuffer;

	// Token: 0x040012A5 RID: 4773
	private int heightBuffer;

	// Token: 0x040012A6 RID: 4774
	public float zoom;

	// Token: 0x040012A7 RID: 4775
	public Vector2 zoomPan;

	// Token: 0x040012A8 RID: 4776
	private Vector2 relativeScale;

	// Token: 0x02000438 RID: 1080
	public enum FitMode
	{
		// Token: 0x04001D98 RID: 7576
		BEST_FIT,
		// Token: 0x04001D99 RID: 7577
		MAINTAIN_ASPECT,
		// Token: 0x04001D9A RID: 7578
		SCALE_TO_FIT,
		// Token: 0x04001D9B RID: 7579
		INACTIVE
	}
}
