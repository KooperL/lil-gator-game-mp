using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class ThemedElement : MonoBehaviour
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x0005ED4A File Offset: 0x0005CF4A
		private void Start()
		{
			ControlMapper.ApplyTheme(this._elements);
		}

		[SerializeField]
		private ThemedElement.ElementInfo[] _elements;

		[Serializable]
		public class ElementInfo
		{
			// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x000787BB File Offset: 0x000769BB
			public string themeClass
			{
				get
				{
					return this._themeClass;
				}
			}

			// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000787C3 File Offset: 0x000769C3
			public Component component
			{
				get
				{
					return this._component;
				}
			}

			[SerializeField]
			private string _themeClass;

			[SerializeField]
			private Component _component;
		}
	}
}
