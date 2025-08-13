using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200018F RID: 399
public class BreakableObjectMulti : BreakableObject
{
	// Token: 0x06000786 RID: 1926 RVA: 0x00034798 File Offset: 0x00032998
	private void BreakStageIndex(int stageIndex)
	{
		BreakableObjectMulti.BreakStage breakStage = this.breakingStages[stageIndex];
		if (breakStage.breakingPrefab != null)
		{
			Object.Instantiate<GameObject>(breakStage.breakingPrefab, base.transform.position, base.transform.rotation);
		}
		if (breakStage.staticPieces.Length != 0)
		{
			GameObject[] staticPieces = breakStage.staticPieces;
			for (int i = 0; i < staticPieces.Length; i++)
			{
				staticPieces[i].SetActive(false);
			}
		}
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x0003480C File Offset: 0x00032A0C
	public override void Break(bool fromAttachment, Vector3 velocity, bool isSturdy = false)
	{
		if (!this.isBroken)
		{
			if (!Player.movement.isSledding)
			{
				if (!Player.movement.isRagdolling)
				{
					goto IL_004F;
				}
			}
			while (this.breakingStage < this.breakingStages.Length)
			{
				this.BreakStageIndex(this.breakingStage);
				this.breakingStage++;
			}
			IL_004F:
			if (this.breakingStage < this.breakingStages.Length)
			{
				this.BreakStageIndex(this.breakingStage);
			}
			this.breakingStage++;
			if (this.breakingStage >= this.breakingStages.Length)
			{
				this.intactObject.SetActive(false);
				this.isBroken = true;
				if (this.onBreak != null)
				{
					this.onBreak.Invoke();
				}
				this.SaveTrue();
				return;
			}
			if (this.onBreakPiece != null)
			{
				this.onBreakPiece.Invoke();
			}
		}
	}

	// Token: 0x04000A05 RID: 2565
	public BreakableObjectMulti.BreakStage[] breakingStages;

	// Token: 0x04000A06 RID: 2566
	public int breakingStage;

	// Token: 0x04000A07 RID: 2567
	public UnityEvent onBreakPiece;

	// Token: 0x02000190 RID: 400
	[Serializable]
	public struct BreakStage
	{
		// Token: 0x04000A08 RID: 2568
		public GameObject[] staticPieces;

		// Token: 0x04000A09 RID: 2569
		public GameObject breakingPrefab;
	}
}
