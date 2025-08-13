using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class ItemSearch<T> : MonoBehaviour, IItemBehaviour where T : MonoBehaviour
{
	// Token: 0x06000B31 RID: 2865 RVA: 0x0000A986 File Offset: 0x00008B86
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.OnUse();
		}
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0000A991 File Offset: 0x00008B91
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

	// Token: 0x06000B33 RID: 2867 RVA: 0x00002229 File Offset: 0x00000429
	protected virtual void SearchResult(T result)
	{
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x0000614F File Offset: 0x0000434F
	protected virtual T[] GetList()
	{
		return null;
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x000039A2 File Offset: 0x00001BA2
	protected virtual bool IsValid(T item)
	{
		return false;
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00002229 File Offset: 0x00000429
	protected virtual void OnUse()
	{
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Cancel()
	{
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnRemove()
	{
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0000A9A0 File Offset: 0x00008BA0
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000E1C RID: 3612
	protected bool isSearching;

	// Token: 0x04000E1D RID: 3613
	public float searchTime = 1f;

	// Token: 0x04000E1E RID: 3614
	protected bool isOnRight;
}
