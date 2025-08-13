using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class SetPlayerActorState : MonoBehaviour
{
	// Token: 0x0600040F RID: 1039 RVA: 0x00017C20 File Offset: 0x00015E20
	private void OnEnable()
	{
		Player.actor.SetStateAndPosition((int)this.state, (int)this.position, this.skipTransition, false);
		if (this.applyTransform)
		{
			IActorMover component = Player.animator.GetComponent<IActorMover>();
			if (component != null)
			{
				component.CancelMove();
			}
			MountedActor component2 = Player.animator.GetComponent<MountedActor>();
			if (component2 != null)
			{
				component2.CancelMount();
			}
			RaycastHit raycastHit;
			if (this.snapToFloor && Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, out raycastHit, 3f))
			{
				base.transform.position = raycastHit.point;
			}
			Player.movement.ApplyTransform(base.transform);
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00017CD8 File Offset: 0x00015ED8
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, out raycastHit, 3f, LayerMask.GetMask(new string[] { "default", "Terrain" })))
		{
			base.transform.position = raycastHit.point;
		}
	}

	// Token: 0x040005B0 RID: 1456
	public ActorPosition position;

	// Token: 0x040005B1 RID: 1457
	public ActorState state = ActorState.S_Happy;

	// Token: 0x040005B2 RID: 1458
	public bool skipTransition;

	// Token: 0x040005B3 RID: 1459
	public bool applyTransform;

	// Token: 0x040005B4 RID: 1460
	[ConditionalHide("applyTransform")]
	public bool snapToFloor;

	// Token: 0x040005B5 RID: 1461
	private int stateID = Animator.StringToHash("State");

	// Token: 0x040005B6 RID: 1462
	private int positionID = Animator.StringToHash("Position");
}
