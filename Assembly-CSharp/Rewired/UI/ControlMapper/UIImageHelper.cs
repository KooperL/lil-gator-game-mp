using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class UIImageHelper : MonoBehaviour
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x00070A84 File Offset: 0x0006EC84
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

		// Token: 0x06001C9F RID: 7327 RVA: 0x00015E0C File Offset: 0x0001400C
		public void SetEnabledStateColor(Color color)
		{
			this.enabledState.color = color;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00015E1A File Offset: 0x0001401A
		public void SetDisabledStateColor(Color color)
		{
			this.disabledState.color = color;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00070AD8 File Offset: 0x0006ECD8
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
			// Token: 0x06001CA3 RID: 7331 RVA: 0x00015E28 File Offset: 0x00014028
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
