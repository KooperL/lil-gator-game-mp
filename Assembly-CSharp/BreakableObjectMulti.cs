using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000134 RID: 308
public class BreakableObjectMulti : BreakableObject
{
	// Token: 0x06000661 RID: 1633 RVA: 0x00020FA4 File Offset: 0x0001F1A4
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

	// Token: 0x06000662 RID: 1634 RVA: 0x00021018 File Offset: 0x0001F218
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

	// Token: 0x0400089B RID: 2203
	public BreakableObjectMulti.BreakStage[] breakingStages;

	// Token: 0x0400089C RID: 2204
	public int breakingStage;

	// Token: 0x0400089D RID: 2205
	public UnityEvent onBreakPiece;

	// Token: 0x020003B2 RID: 946
	[Serializable]
	public struct BreakStage
	{
		// Token: 0x04001B85 RID: 7045
		public GameObject[] staticPieces;

		// Token: 0x04001B86 RID: 7046
		public GameObject breakingPrefab;
	}
}
