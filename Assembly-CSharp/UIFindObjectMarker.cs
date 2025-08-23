using System;
using UnityEngine;
using UnityEngine.Events;

public class UIFindObjectMarker : MonoBehaviour
{
	// (get) Token: 0x06001212 RID: 4626 RVA: 0x0000F5BA File Offset: 0x0000D7BA
	public static UIFindObjectMarker Instance
	{
		get
		{
			if (UIFindObjectMarker.instance == null)
			{
				UIFindObjectMarker.instance = global::UnityEngine.Object.FindObjectOfType<UIFindObjectMarker>(true);
			}
			return UIFindObjectMarker.instance;
		}
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x0000F5D9 File Offset: 0x0000D7D9
	public void SetTarget(Transform target)
	{
		this.uiFollow.followTarget = target;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x000096AC File Offset: 0x000078AC
	public void ClearTarget()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x0000F5F3 File Offset: 0x0000D7F3
	public void OnPin()
	{
		this.onPin.Invoke();
	}

	public static UIFindObjectMarker instance;

	public UIFollow uiFollow;

	public UnityEvent onPin;
}
