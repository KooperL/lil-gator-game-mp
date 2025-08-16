using System;
using System.Collections;
using UnityEngine;

public class ItemSearch<T> : MonoBehaviour, IItemBehaviour where T : MonoBehaviour
{
	// Token: 0x06000B7D RID: 2941 RVA: 0x0000ACA5 File Offset: 0x00008EA5
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.OnUse();
		}
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0000ACB0 File Offset: 0x00008EB0
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

	// Token: 0x06000B7F RID: 2943 RVA: 0x00002229 File Offset: 0x00000429
	protected virtual void SearchResult(T result)
	{
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00006415 File Offset: 0x00004615
	protected virtual T[] GetList()
	{
		return null;
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x00003A8E File Offset: 0x00001C8E
	protected virtual bool IsValid(T item)
	{
		return false;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00002229 File Offset: 0x00000429
	protected virtual void OnUse()
	{
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Cancel()
	{
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnRemove()
	{
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0000ACBF File Offset: 0x00008EBF
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	protected bool isSearching;

	public float searchTime = 1f;

	protected bool isOnRight;
}
