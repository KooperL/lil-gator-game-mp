using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200031B RID: 795
	[AddComponentMenu("")]
	public class CanvasScalerExt : CanvasScaler
	{
		// Token: 0x060013FD RID: 5117 RVA: 0x000554DF File Offset: 0x000536DF
		public void ForceRefresh()
		{
			this.Handle();
		}
	}
}
