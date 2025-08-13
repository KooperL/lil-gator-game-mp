using System;

// Token: 0x0200016D RID: 365
public class LogicStateSubmerge : LogicState
{
	// Token: 0x06000789 RID: 1929 RVA: 0x00025346 File Offset: 0x00023546
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

	// Token: 0x040009B6 RID: 2486
	private int swimmingCounter;
}
