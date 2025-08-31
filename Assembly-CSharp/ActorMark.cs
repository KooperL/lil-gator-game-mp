using System;
using UnityEngine;
using UnityEngine.Events;

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

	public bool actorIsPlayer;

	[ConditionalHide("actorIsPlayer", true, Inverse = true)]
	public DialogueActor actor;

	private IActorMover actorMover;

	public bool setMarkOnEnable = true;

	public bool overrideSpeed;

	[ConditionalHide("overrideSpeed", true)]
	public float speed;

	private Vector3[] markPath;

	public UnityEvent onReachMark;

	public bool skipToStart;

	public bool disableInteractionWhileMoving = true;

	public bool playFootsteps = true;

	private Collider actorCollider;
}
