using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002C3 RID: 707
public class UIFindObjectMarker : MonoBehaviour
{
	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00046F61 File Offset: 0x00045161
	public static UIFindObjectMarker Instance
	{
		get
		{
			if (UIFindObjectMarker.instance == null)
			{
				UIFindObjectMarker.instance = Object.FindObjectOfType<UIFindObjectMarker>(true);
			}
			return UIFindObjectMarker.instance;
		}
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x00046F80 File Offset: 0x00045180
	public void SetTarget(Transform target)
	{
		this.uiFollow.followTarget = target;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x00046F9A File Offset: 0x0004519A
	public void ClearTarget()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00046FA8 File Offset: 0x000451A8
	public void OnPin()
	{
		this.onPin.Invoke();
	}

	// Token: 0x0400135E RID: 4958
	public static UIFindObjectMarker instance;

	// Token: 0x0400135F RID: 4959
	public UIFollow uiFollow;

	// Token: 0x04001360 RID: 4960
	public UnityEvent onPin;
}
