using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	public class ThemedElement : MonoBehaviour
	{
		// Token: 0x06001C80 RID: 7296 RVA: 0x00015CC0 File Offset: 0x00013EC0
		private void Start()
		{
			ControlMapper.ApplyTheme(this._elements);
		}

		[SerializeField]
		private ThemedElement.ElementInfo[] _elements;

		[Serializable]
		public class ElementInfo
		{
			// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00015CCD File Offset: 0x00013ECD
			public string themeClass
			{
				get
				{
					return this._themeClass;
				}
			}

			// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00015CD5 File Offset: 0x00013ED5
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
