using System;

namespace Rewired
{
	public sealed class HOTASTemplate : ControllerTemplate, IHOTASTemplate, IControllerTemplate
	{
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x00010DF5 File Offset: 0x0000EFF5
		IControllerTemplateButton IHOTASTemplate.stickTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(3);
			}
		}

		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00010BFD File Offset: 0x0000EDFD
		IControllerTemplateButton IHOTASTemplate.stickTriggerStage2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00010C06 File Offset: 0x0000EE06
		IControllerTemplateButton IHOTASTemplate.stickPinkyButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00010DFE File Offset: 0x0000EFFE
		IControllerTemplateButton IHOTASTemplate.stickPinkyTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(154);
			}
		}

		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00010C0F File Offset: 0x0000EE0F
		IControllerTemplateButton IHOTASTemplate.stickButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00010C18 File Offset: 0x0000EE18
		IControllerTemplateButton IHOTASTemplate.stickButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00010C21 File Offset: 0x0000EE21
		IControllerTemplateButton IHOTASTemplate.stickButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00010C2A File Offset: 0x0000EE2A
		IControllerTemplateButton IHOTASTemplate.stickButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00010C34 File Offset: 0x0000EE34
		IControllerTemplateButton IHOTASTemplate.stickButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00010CD6 File Offset: 0x0000EED6
		IControllerTemplateButton IHOTASTemplate.stickButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00010C48 File Offset: 0x0000EE48
		IControllerTemplateButton IHOTASTemplate.stickButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		IControllerTemplateButton IHOTASTemplate.stickButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00010C5C File Offset: 0x0000EE5C
		IControllerTemplateButton IHOTASTemplate.stickButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00010C66 File Offset: 0x0000EE66
		IControllerTemplateButton IHOTASTemplate.stickButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00010CF4 File Offset: 0x0000EEF4
		IControllerTemplateButton IHOTASTemplate.stickBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00010CFE File Offset: 0x0000EEFE
		IControllerTemplateButton IHOTASTemplate.stickBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00010D08 File Offset: 0x0000EF08
		IControllerTemplateButton IHOTASTemplate.stickBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00010D12 File Offset: 0x0000EF12
		IControllerTemplateButton IHOTASTemplate.stickBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00010D1C File Offset: 0x0000EF1C
		IControllerTemplateButton IHOTASTemplate.stickBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00010D26 File Offset: 0x0000EF26
		IControllerTemplateButton IHOTASTemplate.stickBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00010D30 File Offset: 0x0000EF30
		IControllerTemplateButton IHOTASTemplate.stickBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00010D3A File Offset: 0x0000EF3A
		IControllerTemplateButton IHOTASTemplate.stickBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00010D44 File Offset: 0x0000EF44
		IControllerTemplateButton IHOTASTemplate.stickBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00010D4E File Offset: 0x0000EF4E
		IControllerTemplateButton IHOTASTemplate.stickBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00010E0B File Offset: 0x0000F00B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(161);
			}
		}

		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00010E18 File Offset: 0x0000F018
		IControllerTemplateButton IHOTASTemplate.stickBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(162);
			}
		}

		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		IControllerTemplateButton IHOTASTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00010E25 File Offset: 0x0000F025
		IControllerTemplateButton IHOTASTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00010E2F File Offset: 0x0000F02F
		IControllerTemplateButton IHOTASTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00010E39 File Offset: 0x0000F039
		IControllerTemplateButton IHOTASTemplate.throttleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00010E43 File Offset: 0x0000F043
		IControllerTemplateButton IHOTASTemplate.throttleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00010E4D File Offset: 0x0000F04D
		IControllerTemplateButton IHOTASTemplate.throttleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00010E57 File Offset: 0x0000F057
		IControllerTemplateButton IHOTASTemplate.throttleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00010E61 File Offset: 0x0000F061
		IControllerTemplateButton IHOTASTemplate.throttleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00010E6B File Offset: 0x0000F06B
		IControllerTemplateButton IHOTASTemplate.throttleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00010E75 File Offset: 0x0000F075
		IControllerTemplateButton IHOTASTemplate.throttleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00010E7F File Offset: 0x0000F07F
		IControllerTemplateButton IHOTASTemplate.throttleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00010E89 File Offset: 0x0000F089
		IControllerTemplateButton IHOTASTemplate.throttleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00010E93 File Offset: 0x0000F093
		IControllerTemplateButton IHOTASTemplate.throttleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00010E9D File Offset: 0x0000F09D
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00010EA7 File Offset: 0x0000F0A7
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00010EB1 File Offset: 0x0000F0B1
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00010EBB File Offset: 0x0000F0BB
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00010EC5 File Offset: 0x0000F0C5
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// (get) Token: 0x0600153A RID: 5434 RVA: 0x00010ECF File Offset: 0x0000F0CF
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00010ED9 File Offset: 0x0000F0D9
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00010EE3 File Offset: 0x0000F0E3
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(67);
			}
		}

		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00010EED File Offset: 0x0000F0ED
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(68);
			}
		}

		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00010EF7 File Offset: 0x0000F0F7
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(69);
			}
		}

		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00010F01 File Offset: 0x0000F101
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(132);
			}
		}

		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00010F0E File Offset: 0x0000F10E
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(133);
			}
		}

		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00010F1B File Offset: 0x0000F11B
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(134);
			}
		}

		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00010F28 File Offset: 0x0000F128
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(135);
			}
		}

		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00010F35 File Offset: 0x0000F135
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(136);
			}
		}

		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00010F42 File Offset: 0x0000F142
		IControllerTemplateAxis IHOTASTemplate.throttleSlider1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(70);
			}
		}

		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00010F4C File Offset: 0x0000F14C
		IControllerTemplateAxis IHOTASTemplate.throttleSlider2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(71);
			}
		}

		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00010F56 File Offset: 0x0000F156
		IControllerTemplateAxis IHOTASTemplate.throttleSlider3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(72);
			}
		}

		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00010F60 File Offset: 0x0000F160
		IControllerTemplateAxis IHOTASTemplate.throttleSlider4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(73);
			}
		}

		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00010F6A File Offset: 0x0000F16A
		IControllerTemplateAxis IHOTASTemplate.throttleDial1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(74);
			}
		}

		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00010F74 File Offset: 0x0000F174
		IControllerTemplateAxis IHOTASTemplate.throttleDial2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(142);
			}
		}

		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00010F81 File Offset: 0x0000F181
		IControllerTemplateAxis IHOTASTemplate.throttleDial3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(143);
			}
		}

		// (get) Token: 0x0600154B RID: 5451 RVA: 0x00010F8E File Offset: 0x0000F18E
		IControllerTemplateAxis IHOTASTemplate.throttleDial4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(144);
			}
		}

		// (get) Token: 0x0600154C RID: 5452 RVA: 0x00010F9B File Offset: 0x0000F19B
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(145);
			}
		}

		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00010FA8 File Offset: 0x0000F1A8
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(146);
			}
		}

		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00010FB5 File Offset: 0x0000F1B5
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(147);
			}
		}

		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00010FC2 File Offset: 0x0000F1C2
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(148);
			}
		}

		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00010FCF File Offset: 0x0000F1CF
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(149);
			}
		}

		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00010FDC File Offset: 0x0000F1DC
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(150);
			}
		}

		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00010FE9 File Offset: 0x0000F1E9
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(151);
			}
		}

		// (get) Token: 0x06001553 RID: 5459 RVA: 0x00010FF6 File Offset: 0x0000F1F6
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(152);
			}
		}

		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00011003 File Offset: 0x0000F203
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(153);
			}
		}

		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00011010 File Offset: 0x0000F210
		IControllerTemplateAxis IHOTASTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(168);
			}
		}

		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0001101D File Offset: 0x0000F21D
		IControllerTemplateAxis IHOTASTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(169);
			}
		}

		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0001102A File Offset: 0x0000F22A
		IControllerTemplateAxis IHOTASTemplate.slidePedals
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(170);
			}
		}

		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00011037 File Offset: 0x0000F237
		IControllerTemplateStick IHOTASTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick>(171);
			}
		}

		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00011044 File Offset: 0x0000F244
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick1
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(172);
			}
		}

		// (get) Token: 0x0600155A RID: 5466 RVA: 0x00011051 File Offset: 0x0000F251
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick2
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(173);
			}
		}

		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0001105E File Offset: 0x0000F25E
		IControllerTemplateHat IHOTASTemplate.stickHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(174);
			}
		}

		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0001106B File Offset: 0x0000F26B
		IControllerTemplateHat IHOTASTemplate.stickHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(175);
			}
		}

		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00011078 File Offset: 0x0000F278
		IControllerTemplateHat IHOTASTemplate.stickHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(176);
			}
		}

		// (get) Token: 0x0600155E RID: 5470 RVA: 0x00011085 File Offset: 0x0000F285
		IControllerTemplateHat IHOTASTemplate.stickHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(177);
			}
		}

		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00011092 File Offset: 0x0000F292
		IControllerTemplateThrottle IHOTASTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(178);
			}
		}

		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0001109F File Offset: 0x0000F29F
		IControllerTemplateThrottle IHOTASTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(179);
			}
		}

		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000110AC File Offset: 0x0000F2AC
		IControllerTemplateThumbStick IHOTASTemplate.throttleMiniStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(180);
			}
		}

		// (get) Token: 0x06001562 RID: 5474 RVA: 0x000110B9 File Offset: 0x0000F2B9
		IControllerTemplateHat IHOTASTemplate.throttleHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(181);
			}
		}

		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000110C6 File Offset: 0x0000F2C6
		IControllerTemplateHat IHOTASTemplate.throttleHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(182);
			}
		}

		// (get) Token: 0x06001564 RID: 5476 RVA: 0x000110D3 File Offset: 0x0000F2D3
		IControllerTemplateHat IHOTASTemplate.throttleHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(183);
			}
		}

		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000110E0 File Offset: 0x0000F2E0
		IControllerTemplateHat IHOTASTemplate.throttleHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(184);
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00010C98 File Offset: 0x0000EE98
		public HOTASTemplate(object payload)
			: base(payload)
		{
		}

		public static readonly Guid typeGuid = new Guid("061a00cf-d8c2-4f8d-8cb5-a15a010bc53e");

		public const int elementId_stickX = 0;

		public const int elementId_stickY = 1;

		public const int elementId_stickRotate = 2;

		public const int elementId_stickMiniStick1X = 78;

		public const int elementId_stickMiniStick1Y = 79;

		public const int elementId_stickMiniStick1Press = 80;

		public const int elementId_stickMiniStick2X = 81;

		public const int elementId_stickMiniStick2Y = 82;

		public const int elementId_stickMiniStick2Press = 83;

		public const int elementId_stickTrigger = 3;

		public const int elementId_stickTriggerStage2 = 4;

		public const int elementId_stickPinkyButton = 5;

		public const int elementId_stickPinkyTrigger = 154;

		public const int elementId_stickButton1 = 6;

		public const int elementId_stickButton2 = 7;

		public const int elementId_stickButton3 = 8;

		public const int elementId_stickButton4 = 9;

		public const int elementId_stickButton5 = 10;

		public const int elementId_stickButton6 = 11;

		public const int elementId_stickButton7 = 12;

		public const int elementId_stickButton8 = 13;

		public const int elementId_stickButton9 = 14;

		public const int elementId_stickButton10 = 15;

		public const int elementId_stickBaseButton1 = 18;

		public const int elementId_stickBaseButton2 = 19;

		public const int elementId_stickBaseButton3 = 20;

		public const int elementId_stickBaseButton4 = 21;

		public const int elementId_stickBaseButton5 = 22;

		public const int elementId_stickBaseButton6 = 23;

		public const int elementId_stickBaseButton7 = 24;

		public const int elementId_stickBaseButton8 = 25;

		public const int elementId_stickBaseButton9 = 26;

		public const int elementId_stickBaseButton10 = 27;

		public const int elementId_stickBaseButton11 = 161;

		public const int elementId_stickBaseButton12 = 162;

		public const int elementId_stickHat1Up = 28;

		public const int elementId_stickHat1UpRight = 29;

		public const int elementId_stickHat1Right = 30;

		public const int elementId_stickHat1DownRight = 31;

		public const int elementId_stickHat1Down = 32;

		public const int elementId_stickHat1DownLeft = 33;

		public const int elementId_stickHat1Left = 34;

		public const int elementId_stickHat1Up_Left = 35;

		public const int elementId_stickHat2Up = 36;

		public const int elementId_stickHat2Up_right = 37;

		public const int elementId_stickHat2Right = 38;

		public const int elementId_stickHat2Down_Right = 39;

		public const int elementId_stickHat2Down = 40;

		public const int elementId_stickHat2Down_Left = 41;

		public const int elementId_stickHat2Left = 42;

		public const int elementId_stickHat2Up_Left = 43;

		public const int elementId_stickHat3Up = 84;

		public const int elementId_stickHat3Up_Right = 85;

		public const int elementId_stickHat3Right = 86;

		public const int elementId_stickHat3Down_Right = 87;

		public const int elementId_stickHat3Down = 88;

		public const int elementId_stickHat3Down_Left = 89;

		public const int elementId_stickHat3Left = 90;

		public const int elementId_stickHat3Up_Left = 91;

		public const int elementId_stickHat4Up = 92;

		public const int elementId_stickHat4Up_Right = 93;

		public const int elementId_stickHat4Right = 94;

		public const int elementId_stickHat4Down_Right = 95;

		public const int elementId_stickHat4Down = 96;

		public const int elementId_stickHat4Down_Left = 97;

		public const int elementId_stickHat4Left = 98;

		public const int elementId_stickHat4Up_Left = 99;

		public const int elementId_mode1 = 44;

		public const int elementId_mode2 = 45;

		public const int elementId_mode3 = 46;

		public const int elementId_throttle1Axis = 49;

		public const int elementId_throttle2Axis = 155;

		public const int elementId_throttle1MinDetent = 166;

		public const int elementId_throttle2MinDetent = 167;

		public const int elementId_throttleButton1 = 50;

		public const int elementId_throttleButton2 = 51;

		public const int elementId_throttleButton3 = 52;

		public const int elementId_throttleButton4 = 53;

		public const int elementId_throttleButton5 = 54;

		public const int elementId_throttleButton6 = 55;

		public const int elementId_throttleButton7 = 56;

		public const int elementId_throttleButton8 = 57;

		public const int elementId_throttleButton9 = 58;

		public const int elementId_throttleButton10 = 59;

		public const int elementId_throttleBaseButton1 = 60;

		public const int elementId_throttleBaseButton2 = 61;

		public const int elementId_throttleBaseButton3 = 62;

		public const int elementId_throttleBaseButton4 = 63;

		public const int elementId_throttleBaseButton5 = 64;

		public const int elementId_throttleBaseButton6 = 65;

		public const int elementId_throttleBaseButton7 = 66;

		public const int elementId_throttleBaseButton8 = 67;

		public const int elementId_throttleBaseButton9 = 68;

		public const int elementId_throttleBaseButton10 = 69;

		public const int elementId_throttleBaseButton11 = 132;

		public const int elementId_throttleBaseButton12 = 133;

		public const int elementId_throttleBaseButton13 = 134;

		public const int elementId_throttleBaseButton14 = 135;

		public const int elementId_throttleBaseButton15 = 136;

		public const int elementId_throttleSlider1 = 70;

		public const int elementId_throttleSlider2 = 71;

		public const int elementId_throttleSlider3 = 72;

		public const int elementId_throttleSlider4 = 73;

		public const int elementId_throttleDial1 = 74;

		public const int elementId_throttleDial2 = 142;

		public const int elementId_throttleDial3 = 143;

		public const int elementId_throttleDial4 = 144;

		public const int elementId_throttleMiniStickX = 75;

		public const int elementId_throttleMiniStickY = 76;

		public const int elementId_throttleMiniStickPress = 77;

		public const int elementId_throttleWheel1Forward = 145;

		public const int elementId_throttleWheel1Back = 146;

		public const int elementId_throttleWheel1Press = 147;

		public const int elementId_throttleWheel2Forward = 148;

		public const int elementId_throttleWheel2Back = 149;

		public const int elementId_throttleWheel2Press = 150;

		public const int elementId_throttleWheel3Forward = 151;

		public const int elementId_throttleWheel3Back = 152;

		public const int elementId_throttleWheel3Press = 153;

		public const int elementId_throttleHat1Up = 100;

		public const int elementId_throttleHat1Up_Right = 101;

		public const int elementId_throttleHat1Right = 102;

		public const int elementId_throttleHat1Down_Right = 103;

		public const int elementId_throttleHat1Down = 104;

		public const int elementId_throttleHat1Down_Left = 105;

		public const int elementId_throttleHat1Left = 106;

		public const int elementId_throttleHat1Up_Left = 107;

		public const int elementId_throttleHat2Up = 108;

		public const int elementId_throttleHat2Up_Right = 109;

		public const int elementId_throttleHat2Right = 110;

		public const int elementId_throttleHat2Down_Right = 111;

		public const int elementId_throttleHat2Down = 112;

		public const int elementId_throttleHat2Down_Left = 113;

		public const int elementId_throttleHat2Left = 114;

		public const int elementId_throttleHat2Up_Left = 115;

		public const int elementId_throttleHat3Up = 116;

		public const int elementId_throttleHat3Up_Right = 117;

		public const int elementId_throttleHat3Right = 118;

		public const int elementId_throttleHat3Down_Right = 119;

		public const int elementId_throttleHat3Down = 120;

		public const int elementId_throttleHat3Down_Left = 121;

		public const int elementId_throttleHat3Left = 122;

		public const int elementId_throttleHat3Up_Left = 123;

		public const int elementId_throttleHat4Up = 124;

		public const int elementId_throttleHat4Up_Right = 125;

		public const int elementId_throttleHat4Right = 126;

		public const int elementId_throttleHat4Down_Right = 127;

		public const int elementId_throttleHat4Down = 128;

		public const int elementId_throttleHat4Down_Left = 129;

		public const int elementId_throttleHat4Left = 130;

		public const int elementId_throttleHat4Up_Left = 131;

		public const int elementId_leftPedal = 168;

		public const int elementId_rightPedal = 169;

		public const int elementId_slidePedals = 170;

		public const int elementId_stick = 171;

		public const int elementId_stickMiniStick1 = 172;

		public const int elementId_stickMiniStick2 = 173;

		public const int elementId_stickHat1 = 174;

		public const int elementId_stickHat2 = 175;

		public const int elementId_stickHat3 = 176;

		public const int elementId_stickHat4 = 177;

		public const int elementId_throttle1 = 178;

		public const int elementId_throttle2 = 179;

		public const int elementId_throttleMiniStick = 180;

		public const int elementId_throttleHat1 = 181;

		public const int elementId_throttleHat2 = 182;

		public const int elementId_throttleHat3 = 183;

		public const int elementId_throttleHat4 = 184;
	}
}
