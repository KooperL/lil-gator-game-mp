using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class ItemSearch<T> : MonoBehaviour, IItemBehaviour where T : MonoBehaviour
{
	// Token: 0x0600099A RID: 2458 RVA: 0x0002D17A File Offset: 0x0002B37A
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.OnUse();
		}
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0002D185 File Offset: 0x0002B385
	protected IEnumerator RunSearch()
	{
		this.isSearching = true;
		T[] list = this.GetList();
		int index = 0;
		Vector3 position = base.transform.position;
		float closestDistance = float.PositiveInfinity;
		T closest = default(T);
		while (index < list.Length)
		{
			int num = Mathf.CeilToInt((float)list.Length * Time.deltaTime / this.searchTime);
			while (num > 0 && index < list.Length)
			{
				num--;
				if (this.IsValid(list[index]))
				{
					float num2 = Vector3.Distance(list[index].transform.position, position);
					if (num2 < closestDistance)
					{
						closest = list[index];
						closestDistance = num2;
					}
				}
				int num3 = index;
				index = num3 + 1;
			}
			yield return null;
		}
		this.isSearching = false;
		this.SearchResult(closest);
		yield break;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0002D194 File Offset: 0x0002B394
	protected virtual void SearchResult(T result)
	{
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0002D196 File Offset: 0x0002B396
	protected virtual T[] GetList()
	{
		return null;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0002D199 File Offset: 0x0002B399
	protected virtual bool IsValid(T item)
	{
		return false;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0002D19C File Offset: 0x0002B39C
	protected virtual void OnUse()
	{
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0002D19E File Offset: 0x0002B39E
	public virtual void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0002D1A0 File Offset: 0x0002B3A0
	public virtual void Cancel()
	{
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0002D1A2 File Offset: 0x0002B3A2
	public virtual void OnRemove()
	{
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0002D1A4 File Offset: 0x0002B3A4
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000BF6 RID: 3062
	protected bool isSearching;

	// Token: 0x04000BF7 RID: 3063
	public float searchTime = 1f;

	// Token: 0x04000BF8 RID: 3064
	protected bool isOnRight;
}
