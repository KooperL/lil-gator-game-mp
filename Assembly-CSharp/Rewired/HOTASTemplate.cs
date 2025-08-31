using System;

namespace Rewired
{
	public sealed class HOTASTemplate : ControllerTemplate, IHOTASTemplate, IControllerTemplate
	{
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0004E0D2 File Offset: 0x0004C2D2
		IControllerTemplateButton IHOTASTemplate.stickTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(3);
			}
		}

		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0004E0DB File Offset: 0x0004C2DB
		IControllerTemplateButton IHOTASTemplate.stickTriggerStage2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0004E0E4 File Offset: 0x0004C2E4
		IControllerTemplateButton IHOTASTemplate.stickPinkyButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0004E0ED File Offset: 0x0004C2ED
		IControllerTemplateButton IHOTASTemplate.stickPinkyTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(154);
			}
		}

		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0004E0FA File Offset: 0x0004C2FA
		IControllerTemplateButton IHOTASTemplate.stickButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0004E103 File Offset: 0x0004C303
		IControllerTemplateButton IHOTASTemplate.stickButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0004E10C File Offset: 0x0004C30C
		IControllerTemplateButton IHOTASTemplate.stickButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0004E115 File Offset: 0x0004C315
		IControllerTemplateButton IHOTASTemplate.stickButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004E11F File Offset: 0x0004C31F
		IControllerTemplateButton IHOTASTemplate.stickButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0004E129 File Offset: 0x0004C329
		IControllerTemplateButton IHOTASTemplate.stickButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004E133 File Offset: 0x0004C333
		IControllerTemplateButton IHOTASTemplate.stickButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0004E13D File Offset: 0x0004C33D
		IControllerTemplateButton IHOTASTemplate.stickButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004E147 File Offset: 0x0004C347
		IControllerTemplateButton IHOTASTemplate.stickButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0004E151 File Offset: 0x0004C351
		IControllerTemplateButton IHOTASTemplate.stickButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0004E15B File Offset: 0x0004C35B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0004E165 File Offset: 0x0004C365
		IControllerTemplateButton IHOTASTemplate.stickBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0004E16F File Offset: 0x0004C36F
		IControllerTemplateButton IHOTASTemplate.stickBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004E179 File Offset: 0x0004C379
		IControllerTemplateButton IHOTASTemplate.stickBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004E183 File Offset: 0x0004C383
		IControllerTemplateButton IHOTASTemplate.stickBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0004E18D File Offset: 0x0004C38D
		IControllerTemplateButton IHOTASTemplate.stickBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0004E197 File Offset: 0x0004C397
		IControllerTemplateButton IHOTASTemplate.stickBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0004E1A1 File Offset: 0x0004C3A1
		IControllerTemplateButton IHOTASTemplate.stickBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0004E1AB File Offset: 0x0004C3AB
		IControllerTemplateButton IHOTASTemplate.stickBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004E1B5 File Offset: 0x0004C3B5
		IControllerTemplateButton IHOTASTemplate.stickBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004E1BF File Offset: 0x0004C3BF
		IControllerTemplateButton IHOTASTemplate.stickBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(161);
			}
		}

		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		IControllerTemplateButton IHOTASTemplate.stickBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(162);
			}
		}

		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004E1D9 File Offset: 0x0004C3D9
		IControllerTemplateButton IHOTASTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004E1E3 File Offset: 0x0004C3E3
		IControllerTemplateButton IHOTASTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004E1ED File Offset: 0x0004C3ED
		IControllerTemplateButton IHOTASTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004E1F7 File Offset: 0x0004C3F7
		IControllerTemplateButton IHOTASTemplate.throttleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004E201 File Offset: 0x0004C401
		IControllerTemplateButton IHOTASTemplate.throttleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0004E20B File Offset: 0x0004C40B
		IControllerTemplateButton IHOTASTemplate.throttleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0004E215 File Offset: 0x0004C415
		IControllerTemplateButton IHOTASTemplate.throttleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004E21F File Offset: 0x0004C41F
		IControllerTemplateButton IHOTASTemplate.throttleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0004E229 File Offset: 0x0004C429
		IControllerTemplateButton IHOTASTemplate.throttleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0004E233 File Offset: 0x0004C433
		IControllerTemplateButton IHOTASTemplate.throttleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0004E23D File Offset: 0x0004C43D
		IControllerTemplateButton IHOTASTemplate.throttleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004E247 File Offset: 0x0004C447
		IControllerTemplateButton IHOTASTemplate.throttleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0004E251 File Offset: 0x0004C451
		IControllerTemplateButton IHOTASTemplate.throttleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0004E25B File Offset: 0x0004C45B
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0004E265 File Offset: 0x0004C465
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0004E26F File Offset: 0x0004C46F
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0004E279 File Offset: 0x0004C479
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004E283 File Offset: 0x0004C483
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0004E28D File Offset: 0x0004C48D
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0004E297 File Offset: 0x0004C497
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004E2A1 File Offset: 0x0004C4A1
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(67);
			}
		}

		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0004E2AB File Offset: 0x0004C4AB
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(68);
			}
		}

		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0004E2B5 File Offset: 0x0004C4B5
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(69);
			}
		}

		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0004E2BF File Offset: 0x0004C4BF
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(132);
			}
		}

		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0004E2CC File Offset: 0x0004C4CC
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(133);
			}
		}

		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004E2D9 File Offset: 0x0004C4D9
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(134);
			}
		}

		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0004E2E6 File Offset: 0x0004C4E6
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(135);
			}
		}

		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(136);
			}
		}

		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0004E300 File Offset: 0x0004C500
		IControllerTemplateAxis IHOTASTemplate.throttleSlider1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(70);
			}
		}

		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004E30A File Offset: 0x0004C50A
		IControllerTemplateAxis IHOTASTemplate.throttleSlider2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(71);
			}
		}

		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004E314 File Offset: 0x0004C514
		IControllerTemplateAxis IHOTASTemplate.throttleSlider3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(72);
			}
		}

		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004E31E File Offset: 0x0004C51E
		IControllerTemplateAxis IHOTASTemplate.throttleSlider4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(73);
			}
		}

		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004E328 File Offset: 0x0004C528
		IControllerTemplateAxis IHOTASTemplate.throttleDial1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(74);
			}
		}

		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004E332 File Offset: 0x0004C532
		IControllerTemplateAxis IHOTASTemplate.throttleDial2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(142);
			}
		}

		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0004E33F File Offset: 0x0004C53F
		IControllerTemplateAxis IHOTASTemplate.throttleDial3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(143);
			}
		}

		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0004E34C File Offset: 0x0004C54C
		IControllerTemplateAxis IHOTASTemplate.throttleDial4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(144);
			}
		}

		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0004E359 File Offset: 0x0004C559
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(145);
			}
		}

		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004E366 File Offset: 0x0004C566
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(146);
			}
		}

		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0004E373 File Offset: 0x0004C573
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(147);
			}
		}

		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0004E380 File Offset: 0x0004C580
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(148);
			}
		}

		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004E38D File Offset: 0x0004C58D
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(149);
			}
		}

		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004E39A File Offset: 0x0004C59A
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(150);
			}
		}

		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004E3A7 File Offset: 0x0004C5A7
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(151);
			}
		}

		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(152);
			}
		}

		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004E3C1 File Offset: 0x0004C5C1
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(153);
			}
		}

		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004E3CE File Offset: 0x0004C5CE
		IControllerTemplateAxis IHOTASTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(168);
			}
		}

		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004E3DB File Offset: 0x0004C5DB
		IControllerTemplateAxis IHOTASTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(169);
			}
		}

		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		IControllerTemplateAxis IHOTASTemplate.slidePedals
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(170);
			}
		}

		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004E3F5 File Offset: 0x0004C5F5
		IControllerTemplateStick IHOTASTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick>(171);
			}
		}

		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004E402 File Offset: 0x0004C602
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick1
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(172);
			}
		}

		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0004E40F File Offset: 0x0004C60F
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick2
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(173);
			}
		}

		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0004E41C File Offset: 0x0004C61C
		IControllerTemplateHat IHOTASTemplate.stickHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(174);
			}
		}

		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0004E429 File Offset: 0x0004C629
		IControllerTemplateHat IHOTASTemplate.stickHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(175);
			}
		}

		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0004E436 File Offset: 0x0004C636
		IControllerTemplateHat IHOTASTemplate.stickHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(176);
			}
		}

		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004E443 File Offset: 0x0004C643
		IControllerTemplateHat IHOTASTemplate.stickHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(177);
			}
		}

		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004E450 File Offset: 0x0004C650
		IControllerTemplateThrottle IHOTASTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(178);
			}
		}

		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0004E45D File Offset: 0x0004C65D
		IControllerTemplateThrottle IHOTASTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(179);
			}
		}

		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004E46A File Offset: 0x0004C66A
		IControllerTemplateThumbStick IHOTASTemplate.throttleMiniStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(180);
			}
		}

		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004E477 File Offset: 0x0004C677
		IControllerTemplateHat IHOTASTemplate.throttleHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(181);
			}
		}

		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004E484 File Offset: 0x0004C684
		IControllerTemplateHat IHOTASTemplate.throttleHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(182);
			}
		}

		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0004E491 File Offset: 0x0004C691
		IControllerTemplateHat IHOTASTemplate.throttleHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(183);
			}
		}

		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0004E49E File Offset: 0x0004C69E
		IControllerTemplateHat IHOTASTemplate.throttleHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(184);
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0004E4AB File Offset: 0x0004C6AB
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
