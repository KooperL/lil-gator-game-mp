using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Items")]
public class LSItem : LogicState
{
	// Token: 0x060008DB RID: 2267 RVA: 0x000391A4 File Offset: 0x000373A4
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

	// Token: 0x060008DC RID: 2268 RVA: 0x00008ACE File Offset: 0x00006CCE
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
