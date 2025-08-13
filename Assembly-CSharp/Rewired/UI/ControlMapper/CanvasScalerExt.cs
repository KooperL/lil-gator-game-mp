using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200042E RID: 1070
	[AddComponentMenu("")]
	public class CanvasScalerExt : CanvasScaler
	{
		// Token: 0x060017C8 RID: 6088 RVA: 0x00012572 File Offset: 0x00010772
		public void ForceRefresh()
		{
			this.Handle();
		}
	}
}
