using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	[AddComponentMenu("")]
	[RequireComponent(typeof(Image))]
	public class UIImageHelper : MonoBehaviour
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x00070BF4 File Offset: 0x0006EDF4
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

		// Token: 0x06001C9F RID: 7327 RVA: 0x00015E2B File Offset: 0x0001402B
		public void SetEnabledStateColor(Color color)
		{
			this.enabledState.color = color;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00015E39 File Offset: 0x00014039
		public void SetDisabledStateColor(Color color)
		{
			this.disabledState.color = color;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00070C48 File Offset: 0x0006EE48
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
			// Token: 0x06001CA3 RID: 7331 RVA: 0x00015E47 File Offset: 0x00014047
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
