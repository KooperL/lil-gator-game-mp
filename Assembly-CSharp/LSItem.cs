using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001CE RID: 462
[AddComponentMenu("Logic/LogicState - Items")]
public class LSItem : LogicState
{
	// Token: 0x0600089B RID: 2203 RVA: 0x00037858 File Offset: 0x00035A58
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

	// Token: 0x0600089C RID: 2204 RVA: 0x0000879B File Offset: 0x0000699B
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

	// Token: 0x04000B36 RID: 2870
	public ItemObject[] items;
}
