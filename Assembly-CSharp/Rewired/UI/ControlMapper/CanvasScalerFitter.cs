using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[RequireComponent(typeof(CanvasScalerExt))]
	public class CanvasScalerFitter : MonoBehaviour
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x0001296A File Offset: 0x00010B6A
		private void OnEnable()
		{
			this.canvasScaler = base.GetComponent<CanvasScalerExt>();
			this.Update();
			this.canvasScaler.ForceRefresh();
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00012989 File Offset: 0x00010B89
		private void Update()
		{
			if (Screen.width != this.screenWidth || Screen.height != this.screenHeight)
			{
				this.screenWidth = Screen.width;
				this.screenHeight = Screen.height;
				this.UpdateSize();
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00066CBC File Offset: 0x00064EBC
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

		[SerializeField]
		private CanvasScalerFitter.BreakPoint[] breakPoints;

		private CanvasScalerExt canvasScaler;

		private int screenWidth;

		private int screenHeight;

		private Action ScreenSizeChanged;

		[Serializable]
		private class BreakPoint
		{
			[SerializeField]
			public string name;

			[SerializeField]
			public float screenAspectRatio;

			[SerializeField]
			public Vector2 referenceResolution;
		}
	}
}
