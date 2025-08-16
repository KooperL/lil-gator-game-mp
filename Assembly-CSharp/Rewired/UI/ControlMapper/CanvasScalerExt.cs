using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class CanvasScalerExt : CanvasScaler
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x0001295A File Offset: 0x00010B5A
		public void ForceRefresh()
		{
			this.Handle();
		}
	}
}
