using System;
using System.Collections;
using Rewired;
using UnityEngine;

// Token: 0x02000130 RID: 304
[AddComponentMenu("Dialogue Sequence/Wait For Input")]
public class DSWaitForInput : DialogueSequence
{
	// Token: 0x060005A5 RID: 1445 RVA: 0x0002E6FC File Offset: 0x0002C8FC
	public override void Activate()
	{
		if (this.player == null)
		{
			this.player = ReInput.players.GetPlayer(0);
		}
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMove), 0, 3, ReInput.mapping.GetActionId("Move Horizontal"));
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMove), 0, 3, ReInput.mapping.GetActionId("Move Vertical"));
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnInteract), 0, 3, ReInput.mapping.GetActionId("Interact"));
		this.player.AddInputEventDelegate(new Action<InputActionEventData>(this.Jump), 0, 3, ReInput.mapping.GetActionId("Jump"));
		if (this.waitUntilTriggered == null)
		{
			this.waitUntilTriggered = new WaitUntil(() => this.isTriggered);
		}
		base.Activate();
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
	public override void Deactivate()
	{
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMove));
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnInteract));
		this.player.RemoveInputEventDelegate(new Action<InputActionEventData>(this.Jump));
		base.Deactivate();
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x000060E0 File Offset: 0x000042E0
	public override YieldInstruction Run()
	{
		return base.StartCoroutine(this.WaitUntilTriggered());
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x000060EE File Offset: 0x000042EE
	private IEnumerator WaitUntilTriggered()
	{
		this.isTriggered = false;
		while (!this.isTriggered)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x000060FD File Offset: 0x000042FD
	private void OnInteract(InputActionEventData obj)
	{
		if (this.waitForInteract)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0000610E File Offset: 0x0000430E
	private void Jump(InputActionEventData obj)
	{
		if (this.waitForJump)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0000611F File Offset: 0x0000431F
	private void OnMove(InputActionEventData obj)
	{
		if (this.waitForMove)
		{
			this.isTriggered = true;
		}
	}

	// Token: 0x040007B1 RID: 1969
	private Player player;

	// Token: 0x040007B2 RID: 1970
	public bool waitForMove;

	// Token: 0x040007B3 RID: 1971
	public bool waitForInteract;

	// Token: 0x040007B4 RID: 1972
	public bool waitForJump;

	// Token: 0x040007B5 RID: 1973
	private bool isTriggered;

	// Token: 0x040007B6 RID: 1974
	private WaitUntil waitUntilTriggered;
}
