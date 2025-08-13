using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200047D RID: 1149
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class UIImageHelper : MonoBehaviour
	{
		// Token: 0x06001C3E RID: 7230 RVA: 0x0006EC6C File Offset: 0x0006CE6C
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

		// Token: 0x06001C3F RID: 7231 RVA: 0x000159EB File Offset: 0x00013BEB
		public void SetEnabledStateColor(Color color)
		{
			this.enabledState.color = color;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000159F9 File Offset: 0x00013BF9
		public void SetDisabledStateColor(Color color)
		{
			this.disabledState.color = color;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
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

		// Token: 0x04001E0A RID: 7690
		[SerializeField]
		private UIImageHelper.State enabledState;

		// Token: 0x04001E0B RID: 7691
		[SerializeField]
		private UIImageHelper.State disabledState;

		// Token: 0x04001E0C RID: 7692
		private bool currentState;

		// Token: 0x0200047E RID: 1150
		[Serializable]
		private class State
		{
			// Token: 0x06001C43 RID: 7235 RVA: 0x00015A07 File Offset: 0x00013C07
			public void Set(Image image)
			{
				if (image == null)
				{
					return;
				}
				image.color = this.color;
			}

			// Token: 0x04001E0D RID: 7693
			[SerializeField]
			public Color color;
		}
	}
}
