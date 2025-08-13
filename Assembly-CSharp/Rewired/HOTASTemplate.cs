using System;

namespace Rewired
{
	// Token: 0x0200030A RID: 778
	public sealed class HOTASTemplate : ControllerTemplate, IHOTASTemplate, IControllerTemplate
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0004E0D2 File Offset: 0x0004C2D2
		IControllerTemplateButton IHOTASTemplate.stickTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(3);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0004E0DB File Offset: 0x0004C2DB
		IControllerTemplateButton IHOTASTemplate.stickTriggerStage2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0004E0E4 File Offset: 0x0004C2E4
		IControllerTemplateButton IHOTASTemplate.stickPinkyButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0004E0ED File Offset: 0x0004C2ED
		IControllerTemplateButton IHOTASTemplate.stickPinkyTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(154);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0004E0FA File Offset: 0x0004C2FA
		IControllerTemplateButton IHOTASTemplate.stickButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0004E103 File Offset: 0x0004C303
		IControllerTemplateButton IHOTASTemplate.stickButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0004E10C File Offset: 0x0004C30C
		IControllerTemplateButton IHOTASTemplate.stickButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0004E115 File Offset: 0x0004C315
		IControllerTemplateButton IHOTASTemplate.stickButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004E11F File Offset: 0x0004C31F
		IControllerTemplateButton IHOTASTemplate.stickButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0004E129 File Offset: 0x0004C329
		IControllerTemplateButton IHOTASTemplate.stickButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004E133 File Offset: 0x0004C333
		IControllerTemplateButton IHOTASTemplate.stickButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0004E13D File Offset: 0x0004C33D
		IControllerTemplateButton IHOTASTemplate.stickButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004E147 File Offset: 0x0004C347
		IControllerTemplateButton IHOTASTemplate.stickButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0004E151 File Offset: 0x0004C351
		IControllerTemplateButton IHOTASTemplate.stickButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0004E15B File Offset: 0x0004C35B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0004E165 File Offset: 0x0004C365
		IControllerTemplateButton IHOTASTemplate.stickBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0004E16F File Offset: 0x0004C36F
		IControllerTemplateButton IHOTASTemplate.stickBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0004E179 File Offset: 0x0004C379
		IControllerTemplateButton IHOTASTemplate.stickBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004E183 File Offset: 0x0004C383
		IControllerTemplateButton IHOTASTemplate.stickBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0004E18D File Offset: 0x0004C38D
		IControllerTemplateButton IHOTASTemplate.stickBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0004E197 File Offset: 0x0004C397
		IControllerTemplateButton IHOTASTemplate.stickBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0004E1A1 File Offset: 0x0004C3A1
		IControllerTemplateButton IHOTASTemplate.stickBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0004E1AB File Offset: 0x0004C3AB
		IControllerTemplateButton IHOTASTemplate.stickBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004E1B5 File Offset: 0x0004C3B5
		IControllerTemplateButton IHOTASTemplate.stickBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004E1BF File Offset: 0x0004C3BF
		IControllerTemplateButton IHOTASTemplate.stickBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(161);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		IControllerTemplateButton IHOTASTemplate.stickBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(162);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004E1D9 File Offset: 0x0004C3D9
		IControllerTemplateButton IHOTASTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0004E1E3 File Offset: 0x0004C3E3
		IControllerTemplateButton IHOTASTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004E1ED File Offset: 0x0004C3ED
		IControllerTemplateButton IHOTASTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004E1F7 File Offset: 0x0004C3F7
		IControllerTemplateButton IHOTASTemplate.throttleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004E201 File Offset: 0x0004C401
		IControllerTemplateButton IHOTASTemplate.throttleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0004E20B File Offset: 0x0004C40B
		IControllerTemplateButton IHOTASTemplate.throttleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0004E215 File Offset: 0x0004C415
		IControllerTemplateButton IHOTASTemplate.throttleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0004E21F File Offset: 0x0004C41F
		IControllerTemplateButton IHOTASTemplate.throttleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0004E229 File Offset: 0x0004C429
		IControllerTemplateButton IHOTASTemplate.throttleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0004E233 File Offset: 0x0004C433
		IControllerTemplateButton IHOTASTemplate.throttleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0004E23D File Offset: 0x0004C43D
		IControllerTemplateButton IHOTASTemplate.throttleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004E247 File Offset: 0x0004C447
		IControllerTemplateButton IHOTASTemplate.throttleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0004E251 File Offset: 0x0004C451
		IControllerTemplateButton IHOTASTemplate.throttleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0004E25B File Offset: 0x0004C45B
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0004E265 File Offset: 0x0004C465
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0004E26F File Offset: 0x0004C46F
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0004E279 File Offset: 0x0004C479
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0004E283 File Offset: 0x0004C483
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0004E28D File Offset: 0x0004C48D
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0004E297 File Offset: 0x0004C497
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0004E2A1 File Offset: 0x0004C4A1
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(67);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0004E2AB File Offset: 0x0004C4AB
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(68);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0004E2B5 File Offset: 0x0004C4B5
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(69);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0004E2BF File Offset: 0x0004C4BF
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(132);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0004E2CC File Offset: 0x0004C4CC
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(133);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004E2D9 File Offset: 0x0004C4D9
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(134);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0004E2E6 File Offset: 0x0004C4E6
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(135);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(136);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0004E300 File Offset: 0x0004C500
		IControllerTemplateAxis IHOTASTemplate.throttleSlider1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(70);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004E30A File Offset: 0x0004C50A
		IControllerTemplateAxis IHOTASTemplate.throttleSlider2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(71);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004E314 File Offset: 0x0004C514
		IControllerTemplateAxis IHOTASTemplate.throttleSlider3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(72);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004E31E File Offset: 0x0004C51E
		IControllerTemplateAxis IHOTASTemplate.throttleSlider4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(73);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004E328 File Offset: 0x0004C528
		IControllerTemplateAxis IHOTASTemplate.throttleDial1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(74);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004E332 File Offset: 0x0004C532
		IControllerTemplateAxis IHOTASTemplate.throttleDial2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(142);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0004E33F File Offset: 0x0004C53F
		IControllerTemplateAxis IHOTASTemplate.throttleDial3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(143);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0004E34C File Offset: 0x0004C54C
		IControllerTemplateAxis IHOTASTemplate.throttleDial4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(144);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0004E359 File Offset: 0x0004C559
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(145);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004E366 File Offset: 0x0004C566
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(146);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0004E373 File Offset: 0x0004C573
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(147);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0004E380 File Offset: 0x0004C580
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(148);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004E38D File Offset: 0x0004C58D
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(149);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0004E39A File Offset: 0x0004C59A
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(150);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004E3A7 File Offset: 0x0004C5A7
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(151);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(152);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004E3C1 File Offset: 0x0004C5C1
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(153);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004E3CE File Offset: 0x0004C5CE
		IControllerTemplateAxis IHOTASTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(168);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004E3DB File Offset: 0x0004C5DB
		IControllerTemplateAxis IHOTASTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(169);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		IControllerTemplateAxis IHOTASTemplate.slidePedals
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(170);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004E3F5 File Offset: 0x0004C5F5
		IControllerTemplateStick IHOTASTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick>(171);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004E402 File Offset: 0x0004C602
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick1
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(172);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0004E40F File Offset: 0x0004C60F
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick2
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(173);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0004E41C File Offset: 0x0004C61C
		IControllerTemplateHat IHOTASTemplate.stickHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(174);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x0004E429 File Offset: 0x0004C629
		IControllerTemplateHat IHOTASTemplate.stickHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(175);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x0004E436 File Offset: 0x0004C636
		IControllerTemplateHat IHOTASTemplate.stickHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(176);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x0004E443 File Offset: 0x0004C643
		IControllerTemplateHat IHOTASTemplate.stickHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(177);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0004E450 File Offset: 0x0004C650
		IControllerTemplateThrottle IHOTASTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(178);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0004E45D File Offset: 0x0004C65D
		IControllerTemplateThrottle IHOTASTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(179);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0004E46A File Offset: 0x0004C66A
		IControllerTemplateThumbStick IHOTASTemplate.throttleMiniStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(180);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0004E477 File Offset: 0x0004C677
		IControllerTemplateHat IHOTASTemplate.throttleHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(181);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x0004E484 File Offset: 0x0004C684
		IControllerTemplateHat IHOTASTemplate.throttleHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(182);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0004E491 File Offset: 0x0004C691
		IControllerTemplateHat IHOTASTemplate.throttleHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(183);
			}
		}

		// Token: 0x17000285 RID: 645
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

		// Token: 0x040015D4 RID: 5588
		public static readonly Guid typeGuid = new Guid("061a00cf-d8c2-4f8d-8cb5-a15a010bc53e");

		// Token: 0x040015D5 RID: 5589
		public const int elementId_stickX = 0;

		// Token: 0x040015D6 RID: 5590
		public const int elementId_stickY = 1;

		// Token: 0x040015D7 RID: 5591
		public const int elementId_stickRotate = 2;

		// Token: 0x040015D8 RID: 5592
		public const int elementId_stickMiniStick1X = 78;

		// Token: 0x040015D9 RID: 5593
		public const int elementId_stickMiniStick1Y = 79;

		// Token: 0x040015DA RID: 5594
		public const int elementId_stickMiniStick1Press = 80;

		// Token: 0x040015DB RID: 5595
		public const int elementId_stickMiniStick2X = 81;

		// Token: 0x040015DC RID: 5596
		public const int elementId_stickMiniStick2Y = 82;

		// Token: 0x040015DD RID: 5597
		public const int elementId_stickMiniStick2Press = 83;

		// Token: 0x040015DE RID: 5598
		public const int elementId_stickTrigger = 3;

		// Token: 0x040015DF RID: 5599
		public const int elementId_stickTriggerStage2 = 4;

		// Token: 0x040015E0 RID: 5600
		public const int elementId_stickPinkyButton = 5;

		// Token: 0x040015E1 RID: 5601
		public const int elementId_stickPinkyTrigger = 154;

		// Token: 0x040015E2 RID: 5602
		public const int elementId_stickButton1 = 6;

		// Token: 0x040015E3 RID: 5603
		public const int elementId_stickButton2 = 7;

		// Token: 0x040015E4 RID: 5604
		public const int elementId_stickButton3 = 8;

		// Token: 0x040015E5 RID: 5605
		public const int elementId_stickButton4 = 9;

		// Token: 0x040015E6 RID: 5606
		public const int elementId_stickButton5 = 10;

		// Token: 0x040015E7 RID: 5607
		public const int elementId_stickButton6 = 11;

		// Token: 0x040015E8 RID: 5608
		public const int elementId_stickButton7 = 12;

		// Token: 0x040015E9 RID: 5609
		public const int elementId_stickButton8 = 13;

		// Token: 0x040015EA RID: 5610
		public const int elementId_stickButton9 = 14;

		// Token: 0x040015EB RID: 5611
		public const int elementId_stickButton10 = 15;

		// Token: 0x040015EC RID: 5612
		public const int elementId_stickBaseButton1 = 18;

		// Token: 0x040015ED RID: 5613
		public const int elementId_stickBaseButton2 = 19;

		// Token: 0x040015EE RID: 5614
		public const int elementId_stickBaseButton3 = 20;

		// Token: 0x040015EF RID: 5615
		public const int elementId_stickBaseButton4 = 21;

		// Token: 0x040015F0 RID: 5616
		public const int elementId_stickBaseButton5 = 22;

		// Token: 0x040015F1 RID: 5617
		public const int elementId_stickBaseButton6 = 23;

		// Token: 0x040015F2 RID: 5618
		public const int elementId_stickBaseButton7 = 24;

		// Token: 0x040015F3 RID: 5619
		public const int elementId_stickBaseButton8 = 25;

		// Token: 0x040015F4 RID: 5620
		public const int elementId_stickBaseButton9 = 26;

		// Token: 0x040015F5 RID: 5621
		public const int elementId_stickBaseButton10 = 27;

		// Token: 0x040015F6 RID: 5622
		public const int elementId_stickBaseButton11 = 161;

		// Token: 0x040015F7 RID: 5623
		public const int elementId_stickBaseButton12 = 162;

		// Token: 0x040015F8 RID: 5624
		public const int elementId_stickHat1Up = 28;

		// Token: 0x040015F9 RID: 5625
		public const int elementId_stickHat1UpRight = 29;

		// Token: 0x040015FA RID: 5626
		public const int elementId_stickHat1Right = 30;

		// Token: 0x040015FB RID: 5627
		public const int elementId_stickHat1DownRight = 31;

		// Token: 0x040015FC RID: 5628
		public const int elementId_stickHat1Down = 32;

		// Token: 0x040015FD RID: 5629
		public const int elementId_stickHat1DownLeft = 33;

		// Token: 0x040015FE RID: 5630
		public const int elementId_stickHat1Left = 34;

		// Token: 0x040015FF RID: 5631
		public const int elementId_stickHat1Up_Left = 35;

		// Token: 0x04001600 RID: 5632
		public const int elementId_stickHat2Up = 36;

		// Token: 0x04001601 RID: 5633
		public const int elementId_stickHat2Up_right = 37;

		// Token: 0x04001602 RID: 5634
		public const int elementId_stickHat2Right = 38;

		// Token: 0x04001603 RID: 5635
		public const int elementId_stickHat2Down_Right = 39;

		// Token: 0x04001604 RID: 5636
		public const int elementId_stickHat2Down = 40;

		// Token: 0x04001605 RID: 5637
		public const int elementId_stickHat2Down_Left = 41;

		// Token: 0x04001606 RID: 5638
		public const int elementId_stickHat2Left = 42;

		// Token: 0x04001607 RID: 5639
		public const int elementId_stickHat2Up_Left = 43;

		// Token: 0x04001608 RID: 5640
		public const int elementId_stickHat3Up = 84;

		// Token: 0x04001609 RID: 5641
		public const int elementId_stickHat3Up_Right = 85;

		// Token: 0x0400160A RID: 5642
		public const int elementId_stickHat3Right = 86;

		// Token: 0x0400160B RID: 5643
		public const int elementId_stickHat3Down_Right = 87;

		// Token: 0x0400160C RID: 5644
		public const int elementId_stickHat3Down = 88;

		// Token: 0x0400160D RID: 5645
		public const int elementId_stickHat3Down_Left = 89;

		// Token: 0x0400160E RID: 5646
		public const int elementId_stickHat3Left = 90;

		// Token: 0x0400160F RID: 5647
		public const int elementId_stickHat3Up_Left = 91;

		// Token: 0x04001610 RID: 5648
		public const int elementId_stickHat4Up = 92;

		// Token: 0x04001611 RID: 5649
		public const int elementId_stickHat4Up_Right = 93;

		// Token: 0x04001612 RID: 5650
		public const int elementId_stickHat4Right = 94;

		// Token: 0x04001613 RID: 5651
		public const int elementId_stickHat4Down_Right = 95;

		// Token: 0x04001614 RID: 5652
		public const int elementId_stickHat4Down = 96;

		// Token: 0x04001615 RID: 5653
		public const int elementId_stickHat4Down_Left = 97;

		// Token: 0x04001616 RID: 5654
		public const int elementId_stickHat4Left = 98;

		// Token: 0x04001617 RID: 5655
		public const int elementId_stickHat4Up_Left = 99;

		// Token: 0x04001618 RID: 5656
		public const int elementId_mode1 = 44;

		// Token: 0x04001619 RID: 5657
		public const int elementId_mode2 = 45;

		// Token: 0x0400161A RID: 5658
		public const int elementId_mode3 = 46;

		// Token: 0x0400161B RID: 5659
		public const int elementId_throttle1Axis = 49;

		// Token: 0x0400161C RID: 5660
		public const int elementId_throttle2Axis = 155;

		// Token: 0x0400161D RID: 5661
		public const int elementId_throttle1MinDetent = 166;

		// Token: 0x0400161E RID: 5662
		public const int elementId_throttle2MinDetent = 167;

		// Token: 0x0400161F RID: 5663
		public const int elementId_throttleButton1 = 50;

		// Token: 0x04001620 RID: 5664
		public const int elementId_throttleButton2 = 51;

		// Token: 0x04001621 RID: 5665
		public const int elementId_throttleButton3 = 52;

		// Token: 0x04001622 RID: 5666
		public const int elementId_throttleButton4 = 53;

		// Token: 0x04001623 RID: 5667
		public const int elementId_throttleButton5 = 54;

		// Token: 0x04001624 RID: 5668
		public const int elementId_throttleButton6 = 55;

		// Token: 0x04001625 RID: 5669
		public const int elementId_throttleButton7 = 56;

		// Token: 0x04001626 RID: 5670
		public const int elementId_throttleButton8 = 57;

		// Token: 0x04001627 RID: 5671
		public const int elementId_throttleButton9 = 58;

		// Token: 0x04001628 RID: 5672
		public const int elementId_throttleButton10 = 59;

		// Token: 0x04001629 RID: 5673
		public const int elementId_throttleBaseButton1 = 60;

		// Token: 0x0400162A RID: 5674
		public const int elementId_throttleBaseButton2 = 61;

		// Token: 0x0400162B RID: 5675
		public const int elementId_throttleBaseButton3 = 62;

		// Token: 0x0400162C RID: 5676
		public const int elementId_throttleBaseButton4 = 63;

		// Token: 0x0400162D RID: 5677
		public const int elementId_throttleBaseButton5 = 64;

		// Token: 0x0400162E RID: 5678
		public const int elementId_throttleBaseButton6 = 65;

		// Token: 0x0400162F RID: 5679
		public const int elementId_throttleBaseButton7 = 66;

		// Token: 0x04001630 RID: 5680
		public const int elementId_throttleBaseButton8 = 67;

		// Token: 0x04001631 RID: 5681
		public const int elementId_throttleBaseButton9 = 68;

		// Token: 0x04001632 RID: 5682
		public const int elementId_throttleBaseButton10 = 69;

		// Token: 0x04001633 RID: 5683
		public const int elementId_throttleBaseButton11 = 132;

		// Token: 0x04001634 RID: 5684
		public const int elementId_throttleBaseButton12 = 133;

		// Token: 0x04001635 RID: 5685
		public const int elementId_throttleBaseButton13 = 134;

		// Token: 0x04001636 RID: 5686
		public const int elementId_throttleBaseButton14 = 135;

		// Token: 0x04001637 RID: 5687
		public const int elementId_throttleBaseButton15 = 136;

		// Token: 0x04001638 RID: 5688
		public const int elementId_throttleSlider1 = 70;

		// Token: 0x04001639 RID: 5689
		public const int elementId_throttleSlider2 = 71;

		// Token: 0x0400163A RID: 5690
		public const int elementId_throttleSlider3 = 72;

		// Token: 0x0400163B RID: 5691
		public const int elementId_throttleSlider4 = 73;

		// Token: 0x0400163C RID: 5692
		public const int elementId_throttleDial1 = 74;

		// Token: 0x0400163D RID: 5693
		public const int elementId_throttleDial2 = 142;

		// Token: 0x0400163E RID: 5694
		public const int elementId_throttleDial3 = 143;

		// Token: 0x0400163F RID: 5695
		public const int elementId_throttleDial4 = 144;

		// Token: 0x04001640 RID: 5696
		public const int elementId_throttleMiniStickX = 75;

		// Token: 0x04001641 RID: 5697
		public const int elementId_throttleMiniStickY = 76;

		// Token: 0x04001642 RID: 5698
		public const int elementId_throttleMiniStickPress = 77;

		// Token: 0x04001643 RID: 5699
		public const int elementId_throttleWheel1Forward = 145;

		// Token: 0x04001644 RID: 5700
		public const int elementId_throttleWheel1Back = 146;

		// Token: 0x04001645 RID: 5701
		public const int elementId_throttleWheel1Press = 147;

		// Token: 0x04001646 RID: 5702
		public const int elementId_throttleWheel2Forward = 148;

		// Token: 0x04001647 RID: 5703
		public const int elementId_throttleWheel2Back = 149;

		// Token: 0x04001648 RID: 5704
		public const int elementId_throttleWheel2Press = 150;

		// Token: 0x04001649 RID: 5705
		public const int elementId_throttleWheel3Forward = 151;

		// Token: 0x0400164A RID: 5706
		public const int elementId_throttleWheel3Back = 152;

		// Token: 0x0400164B RID: 5707
		public const int elementId_throttleWheel3Press = 153;

		// Token: 0x0400164C RID: 5708
		public const int elementId_throttleHat1Up = 100;

		// Token: 0x0400164D RID: 5709
		public const int elementId_throttleHat1Up_Right = 101;

		// Token: 0x0400164E RID: 5710
		public const int elementId_throttleHat1Right = 102;

		// Token: 0x0400164F RID: 5711
		public const int elementId_throttleHat1Down_Right = 103;

		// Token: 0x04001650 RID: 5712
		public const int elementId_throttleHat1Down = 104;

		// Token: 0x04001651 RID: 5713
		public const int elementId_throttleHat1Down_Left = 105;

		// Token: 0x04001652 RID: 5714
		public const int elementId_throttleHat1Left = 106;

		// Token: 0x04001653 RID: 5715
		public const int elementId_throttleHat1Up_Left = 107;

		// Token: 0x04001654 RID: 5716
		public const int elementId_throttleHat2Up = 108;

		// Token: 0x04001655 RID: 5717
		public const int elementId_throttleHat2Up_Right = 109;

		// Token: 0x04001656 RID: 5718
		public const int elementId_throttleHat2Right = 110;

		// Token: 0x04001657 RID: 5719
		public const int elementId_throttleHat2Down_Right = 111;

		// Token: 0x04001658 RID: 5720
		public const int elementId_throttleHat2Down = 112;

		// Token: 0x04001659 RID: 5721
		public const int elementId_throttleHat2Down_Left = 113;

		// Token: 0x0400165A RID: 5722
		public const int elementId_throttleHat2Left = 114;

		// Token: 0x0400165B RID: 5723
		public const int elementId_throttleHat2Up_Left = 115;

		// Token: 0x0400165C RID: 5724
		public const int elementId_throttleHat3Up = 116;

		// Token: 0x0400165D RID: 5725
		public const int elementId_throttleHat3Up_Right = 117;

		// Token: 0x0400165E RID: 5726
		public const int elementId_throttleHat3Right = 118;

		// Token: 0x0400165F RID: 5727
		public const int elementId_throttleHat3Down_Right = 119;

		// Token: 0x04001660 RID: 5728
		public const int elementId_throttleHat3Down = 120;

		// Token: 0x04001661 RID: 5729
		public const int elementId_throttleHat3Down_Left = 121;

		// Token: 0x04001662 RID: 5730
		public const int elementId_throttleHat3Left = 122;

		// Token: 0x04001663 RID: 5731
		public const int elementId_throttleHat3Up_Left = 123;

		// Token: 0x04001664 RID: 5732
		public const int elementId_throttleHat4Up = 124;

		// Token: 0x04001665 RID: 5733
		public const int elementId_throttleHat4Up_Right = 125;

		// Token: 0x04001666 RID: 5734
		public const int elementId_throttleHat4Right = 126;

		// Token: 0x04001667 RID: 5735
		public const int elementId_throttleHat4Down_Right = 127;

		// Token: 0x04001668 RID: 5736
		public const int elementId_throttleHat4Down = 128;

		// Token: 0x04001669 RID: 5737
		public const int elementId_throttleHat4Down_Left = 129;

		// Token: 0x0400166A RID: 5738
		public const int elementId_throttleHat4Left = 130;

		// Token: 0x0400166B RID: 5739
		public const int elementId_throttleHat4Up_Left = 131;

		// Token: 0x0400166C RID: 5740
		public const int elementId_leftPedal = 168;

		// Token: 0x0400166D RID: 5741
		public const int elementId_rightPedal = 169;

		// Token: 0x0400166E RID: 5742
		public const int elementId_slidePedals = 170;

		// Token: 0x0400166F RID: 5743
		public const int elementId_stick = 171;

		// Token: 0x04001670 RID: 5744
		public const int elementId_stickMiniStick1 = 172;

		// Token: 0x04001671 RID: 5745
		public const int elementId_stickMiniStick2 = 173;

		// Token: 0x04001672 RID: 5746
		public const int elementId_stickHat1 = 174;

		// Token: 0x04001673 RID: 5747
		public const int elementId_stickHat2 = 175;

		// Token: 0x04001674 RID: 5748
		public const int elementId_stickHat3 = 176;

		// Token: 0x04001675 RID: 5749
		public const int elementId_stickHat4 = 177;

		// Token: 0x04001676 RID: 5750
		public const int elementId_throttle1 = 178;

		// Token: 0x04001677 RID: 5751
		public const int elementId_throttle2 = 179;

		// Token: 0x04001678 RID: 5752
		public const int elementId_throttleMiniStick = 180;

		// Token: 0x04001679 RID: 5753
		public const int elementId_throttleHat1 = 181;

		// Token: 0x0400167A RID: 5754
		public const int elementId_throttleHat2 = 182;

		// Token: 0x0400167B RID: 5755
		public const int elementId_throttleHat3 = 183;

		// Token: 0x0400167C RID: 5756
		public const int elementId_throttleHat4 = 184;
	}
}
