using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CanvasScalerExt : CanvasScaler
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0001296F File Offset: 0x00010B6F
		public void ForceRefresh()
		{
			this.Handle();
		}
	}
}
