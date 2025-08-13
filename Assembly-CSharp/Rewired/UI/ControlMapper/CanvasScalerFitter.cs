using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200031C RID: 796
	[RequireComponent(typeof(CanvasScalerExt))]
	public class CanvasScalerFitter : MonoBehaviour
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x000554EF File Offset: 0x000536EF
		private void OnEnable()
		{
			this.canvasScaler = base.GetComponent<CanvasScalerExt>();
			this.Update();
			this.canvasScaler.ForceRefresh();
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0005550E File Offset: 0x0005370E
		private void Update()
		{
			if (Screen.width != this.screenWidth || Screen.height != this.screenHeight)
			{
				this.screenWidth = Screen.width;
				this.screenHeight = Screen.height;
				this.UpdateSize();
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00055548 File Offset: 0x00053748
		private void UpdateSize()
		{
			if (this.canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
			{
				return;
			}
			if (this.breakPoints == null)
			{
				return;
			}
			float num = (float)Screen.width / (float)Screen.height;
			float num2 = float.PositiveInfinity;
			int num3 = 0;
			for (int i = 0; i < this.breakPoints.Length; i++)
			{
				float num4 = Mathf.Abs(num - this.breakPoints[i].screenAspectRatio);
				if ((num4 <= this.breakPoints[i].screenAspectRatio || MathTools.IsNear(this.breakPoints[i].screenAspectRatio, 0.01f)) && num4 < num2)
				{
					num2 = num4;
					num3 = i;
				}
			}
			this.canvasScaler.referenceResolution = this.breakPoints[num3].referenceResolution;
		}

		// Token: 0x040017A1 RID: 6049
		[SerializeField]
		private CanvasScalerFitter.BreakPoint[] breakPoints;

		// Token: 0x040017A2 RID: 6050
		private CanvasScalerExt canvasScaler;

		// Token: 0x040017A3 RID: 6051
		private int screenWidth;

		// Token: 0x040017A4 RID: 6052
		private int screenHeight;

		// Token: 0x040017A5 RID: 6053
		private Action ScreenSizeChanged;

		// Token: 0x0200046D RID: 1133
		[Serializable]
		private class BreakPoint
		{
			// Token: 0x04001E46 RID: 7750
			[SerializeField]
			public string name;

			// Token: 0x04001E47 RID: 7751
			[SerializeField]
			public float screenAspectRatio;

			// Token: 0x04001E48 RID: 7752
			[SerializeField]
			public Vector2 referenceResolution;
		}
	}
}
