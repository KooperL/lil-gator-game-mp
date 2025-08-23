using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class ThemedElement : MonoBehaviour
	{
		// Token: 0x06001C81 RID: 7297 RVA: 0x00015CCA File Offset: 0x00013ECA
		private void Start()
		{
			ControlMapper.ApplyTheme(this._elements);
		}

		[SerializeField]
		private ThemedElement.ElementInfo[] _elements;

		[Serializable]
		public class ElementInfo
		{
			// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00015CD7 File Offset: 0x00013ED7
			public string themeClass
			{
				get
				{
					return this._themeClass;
				}
			}

			// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00015CDF File Offset: 0x00013EDF
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
