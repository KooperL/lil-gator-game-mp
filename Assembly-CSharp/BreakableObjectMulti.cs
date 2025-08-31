using System;
using UnityEngine;
using UnityEngine.Events;

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

	public BreakableObjectMulti.BreakStage[] breakingStages;

	public int breakingStage;

	public UnityEvent onBreakPiece;

	[Serializable]
	public struct BreakStage
	{
		public GameObject[] staticPieces;

		public GameObject breakingPrefab;
	}
}
