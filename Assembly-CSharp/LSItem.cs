using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Items")]
public class LSItem : LogicState
{
	// Token: 0x060008DB RID: 2267 RVA: 0x00038FE8 File Offset: 0x000371E8
	public override void CheckLogic()
	{
		bool flag = true;
		foreach (ItemObject itemObject in this.items)
		{
			if (!itemObject.IsUnlocked)
			{
				flag = false;
				itemObject.onItemUnlocked.AddListener(new UnityAction(this.CheckLogic));
			}
		}
		if (flag)
		{
			if (Game.HasControl)
			{
				this.LogicCompleted();
				return;
			}
			base.StartCoroutine(this.CompleteWhenInGameplay());
		}
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00008AAF File Offset: 0x00006CAF
	private IEnumerator CompleteWhenInGameplay()
	{
		yield return null;
		while (!Game.HasControl)
		{
			yield return null;
		}
		this.LogicCompleted();
		yield break;
	}

	public ItemObject[] items;
}
