using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000421 RID: 1057
	[AddComponentMenu("Rewired/Rewired Event System")]
	public class RewiredEventSystem : EventSystem
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x00011BBA File Offset: 0x0000FDBA
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x00011BC2 File Offset: 0x0000FDC2
		public bool alwaysUpdate
		{
			get
			{
				return this._alwaysUpdate;
			}
			set
			{
				this._alwaysUpdate = value;
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000620C8 File Offset: 0x000602C8
		protected override void Update()
		{
			if (this.alwaysUpdate)
			{
				EventSystem current = EventSystem.current;
				if (current != this)
				{
					EventSystem.current = this;
				}
				try
				{
					base.Update();
					return;
				}
				finally
				{
					if (current != this)
					{
						EventSystem.current = current;
					}
				}
			}
			base.Update();
		}

		// Token: 0x04001B4F RID: 6991
		[Tooltip("If enabled, the Event System will be updated every frame even if other Event Systems are enabled. Otherwise, only EventSystem.current will be updated.")]
		[SerializeField]
		private bool _alwaysUpdate;
	}
}
