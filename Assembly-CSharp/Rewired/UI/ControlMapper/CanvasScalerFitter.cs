using System;
using Rewired.Utils;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200042F RID: 1071
	[RequireComponent(typeof(CanvasScalerExt))]
	public class CanvasScalerFitter : MonoBehaviour
	{
		// Token: 0x060017CA RID: 6090 RVA: 0x00012582 File Offset: 0x00010782
		private void OnEnable()
		{
			this.canvasScaler = base.GetComponent<CanvasScalerExt>();
			this.Update();
			this.canvasScaler.ForceRefresh();
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000125A1 File Offset: 0x000107A1
		private void Update()
		{
			if (Screen.width != this.screenWidth || Screen.height != this.screenHeight)
			{
				this.screenWidth = Screen.width;
				this.screenHeight = Screen.height;
				this.UpdateSize();
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00064E28 File Offset: 0x00063028
		private void UpdateSize()
		{
			if (this.canvasScaler.uiScaleMode != 1)
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

		// Token: 0x04001BB8 RID: 7096
		[SerializeField]
		private CanvasScalerFitter.BreakPoint[] breakPoints;

		// Token: 0x04001BB9 RID: 7097
		private CanvasScalerExt canvasScaler;

		// Token: 0x04001BBA RID: 7098
		private int screenWidth;

		// Token: 0x04001BBB RID: 7099
		private int screenHeight;

		// Token: 0x04001BBC RID: 7100
		private Action ScreenSizeChanged;

		// Token: 0x02000430 RID: 1072
		[Serializable]
		private class BreakPoint
		{
			// Token: 0x04001BBD RID: 7101
			[SerializeField]
			public string name;

			// Token: 0x04001BBE RID: 7102
			[SerializeField]
			public float screenAspectRatio;

			// Token: 0x04001BBF RID: 7103
			[SerializeField]
			public Vector2 referenceResolution;
		}
	}
}
