using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Items")]
public class LSItem : LogicState
{
	// Token: 0x0600074F RID: 1871 RVA: 0x00024688 File Offset: 0x00022888
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

	// Token: 0x06000750 RID: 1872 RVA: 0x000246F0 File Offset: 0x000228F0
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
