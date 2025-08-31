using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class UIImageHelper : MonoBehaviour
	{
		// Token: 0x060016B4 RID: 5812 RVA: 0x0005F07C File Offset: 0x0005D27C
		public void SetEnabledState(bool newState)
		{
			this.currentState = newState;
			UIImageHelper.State state = (newState ? this.enabledState : this.disabledState);
			if (state == null)
			{
				return;
			}
			Image component = base.gameObject.GetComponent<Image>();
			if (component == null)
			{
				Debug.LogError("Image is missing!");
				return;
			}
			state.Set(component);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005F0CD File Offset: 0x0005D2CD
		public void SetEnabledStateColor(Color color)
		{
			this.enabledState.color = color;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0005F0DB File Offset: 0x0005D2DB
		public void SetDisabledStateColor(Color color)
		{
			this.disabledState.color = color;
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0005F0EC File Offset: 0x0005D2EC
		public void Refresh()
		{
			UIImageHelper.State state = (this.currentState ? this.enabledState : this.disabledState);
			Image component = base.gameObject.GetComponent<Image>();
			if (component == null)
			{
				return;
			}
			state.Set(component);
		}

		[SerializeField]
		private UIImageHelper.State enabledState;

		[SerializeField]
		private UIImageHelper.State disabledState;

		private bool currentState;

		[Serializable]
		private class State
		{
			// Token: 0x06001DA6 RID: 7590 RVA: 0x0007880C File Offset: 0x00076A0C
			public void Set(Image image)
			{
				if (image == null)
				{
					return;
				}
				image.color = this.color;
			}

			[SerializeField]
			public Color color;
		}
	}
}
