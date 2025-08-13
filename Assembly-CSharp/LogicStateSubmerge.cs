using System;

// Token: 0x020001E1 RID: 481
public class LogicStateSubmerge : LogicState
{
	// Token: 0x060008E8 RID: 2280 RVA: 0x00008AC3 File Offset: 0x00006CC3
	private void FixedUpdate()
	{
		if (Player.movement.IsSwimming)
		{
			this.swimmingCounter++;
		}
		else
		{
			this.swimmingCounter = 0;
		}
		if (this.swimmingCounter > 10)
		{
			base.LogicCompleted();
		}
	}

	// Token: 0x04000B84 RID: 2948
	private int swimmingCounter;
}
