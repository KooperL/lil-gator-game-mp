using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200032B RID: 811
	[AddComponentMenu("")]
	public class ThemedElement : MonoBehaviour
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x0005ED4A File Offset: 0x0005CF4A
		private void Start()
		{
			ControlMapper.ApplyTheme(this._elements);
		}

		// Token: 0x040018D4 RID: 6356
		[SerializeField]
		private ThemedElement.ElementInfo[] _elements;

		// Token: 0x0200049A RID: 1178
		[Serializable]
		public class ElementInfo
		{
			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x000787BB File Offset: 0x000769BB
			public string themeClass
			{
				get
				{
					return this._themeClass;
				}
			}

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000787C3 File Offset: 0x000769C3
			public Component component
			{
				get
				{
					return this._component;
				}
			}

			// Token: 0x04001F33 RID: 7987
			[SerializeField]
			private string _themeClass;

			// Token: 0x04001F34 RID: 7988
			[SerializeField]
			private Component _component;
		}
	}
}
