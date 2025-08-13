using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000CD RID: 205
public class ActorMark : GenericPath
{
	// Token: 0x06000353 RID: 851 RVA: 0x00025128 File Offset: 0x00023328
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
		this.actorMover = this.actor.GetComponent<IActorMover>();
		if (this.actorMover == null)
		{
			this.actorMover = this.actor.gameObject.AddComponent<ActorMover>();
		}
		if (this.setMarkOnEnable)
		{
			this.SetMark();
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00025208 File Offset: 0x00023408
	public void SetMark()
	{
		if (this.actorMover == null)
		{
			return;
		}
		float num = (this.overrideSpeed ? this.speed : 0f);
		this.actorMover.SetMark(this.markPath, base.transform.rotation, num, this.onReachMark, this.skipToStart, this.disableInteractionWhileMoving, this.playFootsteps);
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0002526C File Offset: 0x0002346C
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

	// Token: 0x040004C8 RID: 1224
	public bool actorIsPlayer;

	// Token: 0x040004C9 RID: 1225
	[ConditionalHide("actorIsPlayer", true, Inverse = true)]
	public DialogueActor actor;

	// Token: 0x040004CA RID: 1226
	private IActorMover actorMover;

	// Token: 0x040004CB RID: 1227
	public bool setMarkOnEnable = true;

	// Token: 0x040004CC RID: 1228
	public bool overrideSpeed;

	// Token: 0x040004CD RID: 1229
	[ConditionalHide("overrideSpeed", true)]
	public float speed;

	// Token: 0x040004CE RID: 1230
	private Vector3[] markPath;

	// Token: 0x040004CF RID: 1231
	public UnityEvent onReachMark;

	// Token: 0x040004D0 RID: 1232
	public bool skipToStart;

	// Token: 0x040004D1 RID: 1233
	public bool disableInteractionWhileMoving = true;

	// Token: 0x040004D2 RID: 1234
	public bool playFootsteps = true;

	// Token: 0x040004D3 RID: 1235
	private Collider actorCollider;
}
