using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000A3 RID: 163
public class ActorMark : GenericPath
{
	// Token: 0x0600030E RID: 782 RVA: 0x00011C70 File Offset: 0x0000FE70
	private void OnEnable()
	{
		if (this.actorIsPlayer)
		{
			this.actor = DialogueActor.playerActor;
		}
		if (this.actor == null)
		{
			return;
		}
		if (this.markPath == null)
		{
			this.markPath = new Vector3[this.positions.Length + 1];
			for (int i = 0; i < this.positions.Length; i++)
			{
				this.markPath[i] = base.transform.TransformPoint(this.positions[i]);
			}
			this.markPath[this.positions.Length] = base.transform.position;
		}
		if (this.actorMover == null)
		{
			this.actorMover = this.actor.GetComponent<IActorMover>();
		}
		if (this.actorMover == null)
		{
			this.actorMover = this.actor.gameObject.AddComponent<ActorMover>();
		}
		if (this.setMarkOnEnable)
		{
			this.SetMark();
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00011D58 File Offset: 0x0000FF58
	public void SetMark()
	{
		if (this.actorMover == null)
		{
			return;
		}
		float num = (this.overrideSpeed ? this.speed : 0f);
		this.actorMover.SetMark(this.markPath, base.transform.rotation, num, this.onReachMark, this.skipToStart, this.disableInteractionWhileMoving, this.playFootsteps);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00011DBC File Offset: 0x0000FFBC
	[ContextMenu("Snap root to ground")]
	public void SnapRootToGround()
	{
		Vector3 position = base.transform.position;
		if (LayerUtil.SnapToGround(ref position, 5f))
		{
			base.transform.position = position;
		}
		base.SnapToGround();
	}

	// Token: 0x0400042A RID: 1066
	public bool actorIsPlayer;

	// Token: 0x0400042B RID: 1067
	[ConditionalHide("actorIsPlayer", true, Inverse = true)]
	public DialogueActor actor;

	// Token: 0x0400042C RID: 1068
	private IActorMover actorMover;

	// Token: 0x0400042D RID: 1069
	public bool setMarkOnEnable = true;

	// Token: 0x0400042E RID: 1070
	public bool overrideSpeed;

	// Token: 0x0400042F RID: 1071
	[ConditionalHide("overrideSpeed", true)]
	public float speed;

	// Token: 0x04000430 RID: 1072
	private Vector3[] markPath;

	// Token: 0x04000431 RID: 1073
	public UnityEvent onReachMark;

	// Token: 0x04000432 RID: 1074
	public bool skipToStart;

	// Token: 0x04000433 RID: 1075
	public bool disableInteractionWhileMoving = true;

	// Token: 0x04000434 RID: 1076
	public bool playFootsteps = true;

	// Token: 0x04000435 RID: 1077
	private Collider actorCollider;
}
