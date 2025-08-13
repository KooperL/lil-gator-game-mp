using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000475 RID: 1141
	[AddComponentMenu("")]
	public class ThemedElement : MonoBehaviour
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x0001588A File Offset: 0x00013A8A
		private void Start()
		{
			ControlMapper.ApplyTheme(this._elements);
		}

		// Token: 0x04001DF8 RID: 7672
		[SerializeField]
		private ThemedElement.ElementInfo[] _elements;

		// Token: 0x02000476 RID: 1142
		[Serializable]
		public class ElementInfo
		{
			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00015897 File Offset: 0x00013A97
			public string themeClass
			{
				get
				{
					return this._themeClass;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0001589F File Offset: 0x00013A9F
			public Component component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x04001DF9 RID: 7673
			[SerializeField]
			private string _themeClass;

			// Token: 0x04001DFA RID: 7674
			[SerializeField]
			private Component _component;
		}
	}
}
