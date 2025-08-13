using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003A5 RID: 933
public class UIFindObjectMarker : MonoBehaviour
{
	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0000F1C7 File Offset: 0x0000D3C7
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

	// Token: 0x060011B2 RID: 4530 RVA: 0x0000F1E6 File Offset: 0x0000D3E6
	public void SetTarget(Transform target)
	{
		this.uiFollow.followTarget = target;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x00009344 File Offset: 0x00007544
	public void ClearTarget()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x0000F200 File Offset: 0x0000D400
	public void OnPin()
	{
		this.onPin.Invoke();
	}

	// Token: 0x040016D4 RID: 5844
	public static UIFindObjectMarker instance;

	// Token: 0x040016D5 RID: 5845
	public UIFollow uiFollow;

	// Token: 0x040016D6 RID: 5846
	public UnityEvent onPin;
}
