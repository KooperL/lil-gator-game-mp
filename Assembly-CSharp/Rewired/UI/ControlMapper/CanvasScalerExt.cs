using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CanvasScalerExt : CanvasScaler
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x00012979 File Offset: 0x00010B79
		public void ForceRefresh()
		{
			this.Handle();
		}
	}
}
