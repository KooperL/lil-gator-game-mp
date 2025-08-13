using System;

namespace Rewired
{
	// Token: 0x020003FB RID: 1019
	public interface IGamepadTemplate : IControllerTemplate
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001367 RID: 4967
		IControllerTemplateButton actionBottomRow1 { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001368 RID: 4968
		IControllerTemplateButton a { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001369 RID: 4969
		IControllerTemplateButton actionBottomRow2 { get; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600136A RID: 4970
		IControllerTemplateButton b { get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600136B RID: 4971
		IControllerTemplateButton actionBottomRow3 { get; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600136C RID: 4972
		IControllerTemplateButton c { get; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600136D RID: 4973
		IControllerTemplateButton actionTopRow1 { get; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600136E RID: 4974
		IControllerTemplateButton x { get; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600136F RID: 4975
		IControllerTemplateButton actionTopRow2 { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001370 RID: 4976
		IControllerTemplateButton y { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001371 RID: 4977
		IControllerTemplateButton actionTopRow3 { get; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001372 RID: 4978
		IControllerTemplateButton z { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001373 RID: 4979
		IControllerTemplateButton leftShoulder1 { get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001374 RID: 4980
		IControllerTemplateButton leftBumper { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001375 RID: 4981
		IControllerTemplateAxis leftShoulder2 { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001376 RID: 4982
		IControllerTemplateAxis leftTrigger { get; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001377 RID: 4983
		IControllerTemplateButton rightShoulder1 { get; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001378 RID: 4984
		IControllerTemplateButton rightBumper { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001379 RID: 4985
		IControllerTemplateAxis rightShoulder2 { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600137A RID: 4986
		IControllerTemplateAxis rightTrigger { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600137B RID: 4987
		IControllerTemplateButton center1 { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600137C RID: 4988
		IControllerTemplateButton back { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600137D RID: 4989
		IControllerTemplateButton center2 { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600137E RID: 4990
		IControllerTemplateButton start { get; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600137F RID: 4991
		IControllerTemplateButton center3 { get; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001380 RID: 4992
		IControllerTemplateButton guide { get; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001381 RID: 4993
		IControllerTemplateThumbStick leftStick { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001382 RID: 4994
		IControllerTemplateThumbStick rightStick { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001383 RID: 4995
		IControllerTemplateDPad dPad { get; }
	}
}
