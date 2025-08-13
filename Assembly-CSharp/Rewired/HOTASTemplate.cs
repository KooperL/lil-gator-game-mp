using System;

namespace Rewired
{
	// Token: 0x02000403 RID: 1027
	public sealed class HOTASTemplate : ControllerTemplate, IHOTASTemplate, IControllerTemplate
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x000109F8 File Offset: 0x0000EBF8
		IControllerTemplateButton IHOTASTemplate.stickTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(3);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x00010800 File Offset: 0x0000EA00
		IControllerTemplateButton IHOTASTemplate.stickTriggerStage2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(4);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00010809 File Offset: 0x0000EA09
		IControllerTemplateButton IHOTASTemplate.stickPinkyButton
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(5);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00010A01 File Offset: 0x0000EC01
		IControllerTemplateButton IHOTASTemplate.stickPinkyTrigger
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(154);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x00010812 File Offset: 0x0000EA12
		IControllerTemplateButton IHOTASTemplate.stickButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(6);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0001081B File Offset: 0x0000EA1B
		IControllerTemplateButton IHOTASTemplate.stickButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(7);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00010824 File Offset: 0x0000EA24
		IControllerTemplateButton IHOTASTemplate.stickButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(8);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0001082D File Offset: 0x0000EA2D
		IControllerTemplateButton IHOTASTemplate.stickButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(9);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00010837 File Offset: 0x0000EA37
		IControllerTemplateButton IHOTASTemplate.stickButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(10);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000108D9 File Offset: 0x0000EAD9
		IControllerTemplateButton IHOTASTemplate.stickButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(11);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0001084B File Offset: 0x0000EA4B
		IControllerTemplateButton IHOTASTemplate.stickButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(12);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x000108E3 File Offset: 0x0000EAE3
		IControllerTemplateButton IHOTASTemplate.stickButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(13);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0001085F File Offset: 0x0000EA5F
		IControllerTemplateButton IHOTASTemplate.stickButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(14);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00010869 File Offset: 0x0000EA69
		IControllerTemplateButton IHOTASTemplate.stickButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(15);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x000108F7 File Offset: 0x0000EAF7
		IControllerTemplateButton IHOTASTemplate.stickBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(18);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00010901 File Offset: 0x0000EB01
		IControllerTemplateButton IHOTASTemplate.stickBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(19);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0001090B File Offset: 0x0000EB0B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(20);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00010915 File Offset: 0x0000EB15
		IControllerTemplateButton IHOTASTemplate.stickBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(21);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0001091F File Offset: 0x0000EB1F
		IControllerTemplateButton IHOTASTemplate.stickBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(22);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00010929 File Offset: 0x0000EB29
		IControllerTemplateButton IHOTASTemplate.stickBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(23);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00010933 File Offset: 0x0000EB33
		IControllerTemplateButton IHOTASTemplate.stickBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(24);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x0001093D File Offset: 0x0000EB3D
		IControllerTemplateButton IHOTASTemplate.stickBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(25);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00010947 File Offset: 0x0000EB47
		IControllerTemplateButton IHOTASTemplate.stickBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(26);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00010951 File Offset: 0x0000EB51
		IControllerTemplateButton IHOTASTemplate.stickBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(27);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00010A0E File Offset: 0x0000EC0E
		IControllerTemplateButton IHOTASTemplate.stickBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(161);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00010A1B File Offset: 0x0000EC1B
		IControllerTemplateButton IHOTASTemplate.stickBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(162);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000109AB File Offset: 0x0000EBAB
		IControllerTemplateButton IHOTASTemplate.mode1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(44);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00010A28 File Offset: 0x0000EC28
		IControllerTemplateButton IHOTASTemplate.mode2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(45);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00010A32 File Offset: 0x0000EC32
		IControllerTemplateButton IHOTASTemplate.mode3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(46);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00010A3C File Offset: 0x0000EC3C
		IControllerTemplateButton IHOTASTemplate.throttleButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(50);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00010A46 File Offset: 0x0000EC46
		IControllerTemplateButton IHOTASTemplate.throttleButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(51);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00010A50 File Offset: 0x0000EC50
		IControllerTemplateButton IHOTASTemplate.throttleButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(52);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00010A5A File Offset: 0x0000EC5A
		IControllerTemplateButton IHOTASTemplate.throttleButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(53);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00010A64 File Offset: 0x0000EC64
		IControllerTemplateButton IHOTASTemplate.throttleButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(54);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00010A6E File Offset: 0x0000EC6E
		IControllerTemplateButton IHOTASTemplate.throttleButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(55);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00010A78 File Offset: 0x0000EC78
		IControllerTemplateButton IHOTASTemplate.throttleButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(56);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00010A82 File Offset: 0x0000EC82
		IControllerTemplateButton IHOTASTemplate.throttleButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(57);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00010A8C File Offset: 0x0000EC8C
		IControllerTemplateButton IHOTASTemplate.throttleButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(58);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00010A96 File Offset: 0x0000EC96
		IControllerTemplateButton IHOTASTemplate.throttleButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(59);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton1
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(60);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00010AAA File Offset: 0x0000ECAA
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton2
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(61);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton3
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(62);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00010ABE File Offset: 0x0000ECBE
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton4
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(63);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton5
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(64);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton6
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(65);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x00010ADC File Offset: 0x0000ECDC
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton7
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(66);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00010AE6 File Offset: 0x0000ECE6
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton8
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(67);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton9
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(68);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00010AFA File Offset: 0x0000ECFA
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton10
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(69);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00010B04 File Offset: 0x0000ED04
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton11
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(132);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00010B11 File Offset: 0x0000ED11
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton12
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(133);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00010B1E File Offset: 0x0000ED1E
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton13
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(134);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00010B2B File Offset: 0x0000ED2B
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton14
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(135);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00010B38 File Offset: 0x0000ED38
		IControllerTemplateButton IHOTASTemplate.throttleBaseButton15
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(136);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00010B45 File Offset: 0x0000ED45
		IControllerTemplateAxis IHOTASTemplate.throttleSlider1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(70);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00010B4F File Offset: 0x0000ED4F
		IControllerTemplateAxis IHOTASTemplate.throttleSlider2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(71);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00010B59 File Offset: 0x0000ED59
		IControllerTemplateAxis IHOTASTemplate.throttleSlider3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(72);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00010B63 File Offset: 0x0000ED63
		IControllerTemplateAxis IHOTASTemplate.throttleSlider4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(73);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00010B6D File Offset: 0x0000ED6D
		IControllerTemplateAxis IHOTASTemplate.throttleDial1
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(74);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00010B77 File Offset: 0x0000ED77
		IControllerTemplateAxis IHOTASTemplate.throttleDial2
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(142);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00010B84 File Offset: 0x0000ED84
		IControllerTemplateAxis IHOTASTemplate.throttleDial3
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(143);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00010B91 File Offset: 0x0000ED91
		IControllerTemplateAxis IHOTASTemplate.throttleDial4
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(144);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00010B9E File Offset: 0x0000ED9E
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(145);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00010BAB File Offset: 0x0000EDAB
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(146);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		IControllerTemplateButton IHOTASTemplate.throttleWheel1Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(147);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00010BC5 File Offset: 0x0000EDC5
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(148);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00010BD2 File Offset: 0x0000EDD2
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(149);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00010BDF File Offset: 0x0000EDDF
		IControllerTemplateButton IHOTASTemplate.throttleWheel2Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(150);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00010BEC File Offset: 0x0000EDEC
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Forward
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(151);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00010BF9 File Offset: 0x0000EDF9
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Back
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(152);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00010C06 File Offset: 0x0000EE06
		IControllerTemplateButton IHOTASTemplate.throttleWheel3Press
		{
			get
			{
				return base.GetElement<IControllerTemplateButton>(153);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00010C13 File Offset: 0x0000EE13
		IControllerTemplateAxis IHOTASTemplate.leftPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(168);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00010C20 File Offset: 0x0000EE20
		IControllerTemplateAxis IHOTASTemplate.rightPedal
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(169);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00010C2D File Offset: 0x0000EE2D
		IControllerTemplateAxis IHOTASTemplate.slidePedals
		{
			get
			{
				return base.GetElement<IControllerTemplateAxis>(170);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00010C3A File Offset: 0x0000EE3A
		IControllerTemplateStick IHOTASTemplate.stick
		{
			get
			{
				return base.GetElement<IControllerTemplateStick>(171);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00010C47 File Offset: 0x0000EE47
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick1
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(172);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00010C54 File Offset: 0x0000EE54
		IControllerTemplateThumbStick IHOTASTemplate.stickMiniStick2
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(173);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00010C61 File Offset: 0x0000EE61
		IControllerTemplateHat IHOTASTemplate.stickHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(174);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00010C6E File Offset: 0x0000EE6E
		IControllerTemplateHat IHOTASTemplate.stickHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(175);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00010C7B File Offset: 0x0000EE7B
		IControllerTemplateHat IHOTASTemplate.stickHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(176);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00010C88 File Offset: 0x0000EE88
		IControllerTemplateHat IHOTASTemplate.stickHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(177);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00010C95 File Offset: 0x0000EE95
		IControllerTemplateThrottle IHOTASTemplate.throttle1
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(178);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00010CA2 File Offset: 0x0000EEA2
		IControllerTemplateThrottle IHOTASTemplate.throttle2
		{
			get
			{
				return base.GetElement<IControllerTemplateThrottle>(179);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00010CAF File Offset: 0x0000EEAF
		IControllerTemplateThumbStick IHOTASTemplate.throttleMiniStick
		{
			get
			{
				return base.GetElement<IControllerTemplateThumbStick>(180);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00010CBC File Offset: 0x0000EEBC
		IControllerTemplateHat IHOTASTemplate.throttleHat1
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(181);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00010CC9 File Offset: 0x0000EEC9
		IControllerTemplateHat IHOTASTemplate.throttleHat2
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(182);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00010CD6 File Offset: 0x0000EED6
		IControllerTemplateHat IHOTASTemplate.throttleHat3
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(183);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00010CE3 File Offset: 0x0000EEE3
		IControllerTemplateHat IHOTASTemplate.throttleHat4
		{
			get
			{
				return base.GetElement<IControllerTemplateHat>(184);
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0001089B File Offset: 0x0000EA9B
		public HOTASTemplate(object payload)
			: base(payload)
		{
		}

		// Token: 0x040019A1 RID: 6561
		public static readonly Guid typeGuid = new Guid("061a00cf-d8c2-4f8d-8cb5-a15a010bc53e");

		// Token: 0x040019A2 RID: 6562
		public const int elementId_stickX = 0;

		// Token: 0x040019A3 RID: 6563
		public const int elementId_stickY = 1;

		// Token: 0x040019A4 RID: 6564
		public const int elementId_stickRotate = 2;

		// Token: 0x040019A5 RID: 6565
		public const int elementId_stickMiniStick1X = 78;

		// Token: 0x040019A6 RID: 6566
		public const int elementId_stickMiniStick1Y = 79;

		// Token: 0x040019A7 RID: 6567
		public const int elementId_stickMiniStick1Press = 80;

		// Token: 0x040019A8 RID: 6568
		public const int elementId_stickMiniStick2X = 81;

		// Token: 0x040019A9 RID: 6569
		public const int elementId_stickMiniStick2Y = 82;

		// Token: 0x040019AA RID: 6570
		public const int elementId_stickMiniStick2Press = 83;

		// Token: 0x040019AB RID: 6571
		public const int elementId_stickTrigger = 3;

		// Token: 0x040019AC RID: 6572
		public const int elementId_stickTriggerStage2 = 4;

		// Token: 0x040019AD RID: 6573
		public const int elementId_stickPinkyButton = 5;

		// Token: 0x040019AE RID: 6574
		public const int elementId_stickPinkyTrigger = 154;

		// Token: 0x040019AF RID: 6575
		public const int elementId_stickButton1 = 6;

		// Token: 0x040019B0 RID: 6576
		public const int elementId_stickButton2 = 7;

		// Token: 0x040019B1 RID: 6577
		public const int elementId_stickButton3 = 8;

		// Token: 0x040019B2 RID: 6578
		public const int elementId_stickButton4 = 9;

		// Token: 0x040019B3 RID: 6579
		public const int elementId_stickButton5 = 10;

		// Token: 0x040019B4 RID: 6580
		public const int elementId_stickButton6 = 11;

		// Token: 0x040019B5 RID: 6581
		public const int elementId_stickButton7 = 12;

		// Token: 0x040019B6 RID: 6582
		public const int elementId_stickButton8 = 13;

		// Token: 0x040019B7 RID: 6583
		public const int elementId_stickButton9 = 14;

		// Token: 0x040019B8 RID: 6584
		public const int elementId_stickButton10 = 15;

		// Token: 0x040019B9 RID: 6585
		public const int elementId_stickBaseButton1 = 18;

		// Token: 0x040019BA RID: 6586
		public const int elementId_stickBaseButton2 = 19;

		// Token: 0x040019BB RID: 6587
		public const int elementId_stickBaseButton3 = 20;

		// Token: 0x040019BC RID: 6588
		public const int elementId_stickBaseButton4 = 21;

		// Token: 0x040019BD RID: 6589
		public const int elementId_stickBaseButton5 = 22;

		// Token: 0x040019BE RID: 6590
		public const int elementId_stickBaseButton6 = 23;

		// Token: 0x040019BF RID: 6591
		public const int elementId_stickBaseButton7 = 24;

		// Token: 0x040019C0 RID: 6592
		public const int elementId_stickBaseButton8 = 25;

		// Token: 0x040019C1 RID: 6593
		public const int elementId_stickBaseButton9 = 26;

		// Token: 0x040019C2 RID: 6594
		public const int elementId_stickBaseButton10 = 27;

		// Token: 0x040019C3 RID: 6595
		public const int elementId_stickBaseButton11 = 161;

		// Token: 0x040019C4 RID: 6596
		public const int elementId_stickBaseButton12 = 162;

		// Token: 0x040019C5 RID: 6597
		public const int elementId_stickHat1Up = 28;

		// Token: 0x040019C6 RID: 6598
		public const int elementId_stickHat1UpRight = 29;

		// Token: 0x040019C7 RID: 6599
		public const int elementId_stickHat1Right = 30;

		// Token: 0x040019C8 RID: 6600
		public const int elementId_stickHat1DownRight = 31;

		// Token: 0x040019C9 RID: 6601
		public const int elementId_stickHat1Down = 32;

		// Token: 0x040019CA RID: 6602
		public const int elementId_stickHat1DownLeft = 33;

		// Token: 0x040019CB RID: 6603
		public const int elementId_stickHat1Left = 34;

		// Token: 0x040019CC RID: 6604
		public const int elementId_stickHat1Up_Left = 35;

		// Token: 0x040019CD RID: 6605
		public const int elementId_stickHat2Up = 36;

		// Token: 0x040019CE RID: 6606
		public const int elementId_stickHat2Up_right = 37;

		// Token: 0x040019CF RID: 6607
		public const int elementId_stickHat2Right = 38;

		// Token: 0x040019D0 RID: 6608
		public const int elementId_stickHat2Down_Right = 39;

		// Token: 0x040019D1 RID: 6609
		public const int elementId_stickHat2Down = 40;

		// Token: 0x040019D2 RID: 6610
		public const int elementId_stickHat2Down_Left = 41;

		// Token: 0x040019D3 RID: 6611
		public const int elementId_stickHat2Left = 42;

		// Token: 0x040019D4 RID: 6612
		public const int elementId_stickHat2Up_Left = 43;

		// Token: 0x040019D5 RID: 6613
		public const int elementId_stickHat3Up = 84;

		// Token: 0x040019D6 RID: 6614
		public const int elementId_stickHat3Up_Right = 85;

		// Token: 0x040019D7 RID: 6615
		public const int elementId_stickHat3Right = 86;

		// Token: 0x040019D8 RID: 6616
		public const int elementId_stickHat3Down_Right = 87;

		// Token: 0x040019D9 RID: 6617
		public const int elementId_stickHat3Down = 88;

		// Token: 0x040019DA RID: 6618
		public const int elementId_stickHat3Down_Left = 89;

		// Token: 0x040019DB RID: 6619
		public const int elementId_stickHat3Left = 90;

		// Token: 0x040019DC RID: 6620
		public const int elementId_stickHat3Up_Left = 91;

		// Token: 0x040019DD RID: 6621
		public const int elementId_stickHat4Up = 92;

		// Token: 0x040019DE RID: 6622
		public const int elementId_stickHat4Up_Right = 93;

		// Token: 0x040019DF RID: 6623
		public const int elementId_stickHat4Right = 94;

		// Token: 0x040019E0 RID: 6624
		public const int elementId_stickHat4Down_Right = 95;

		// Token: 0x040019E1 RID: 6625
		public const int elementId_stickHat4Down = 96;

		// Token: 0x040019E2 RID: 6626
		public const int elementId_stickHat4Down_Left = 97;

		// Token: 0x040019E3 RID: 6627
		public const int elementId_stickHat4Left = 98;

		// Token: 0x040019E4 RID: 6628
		public const int elementId_stickHat4Up_Left = 99;

		// Token: 0x040019E5 RID: 6629
		public const int elementId_mode1 = 44;

		// Token: 0x040019E6 RID: 6630
		public const int elementId_mode2 = 45;

		// Token: 0x040019E7 RID: 6631
		public const int elementId_mode3 = 46;

		// Token: 0x040019E8 RID: 6632
		public const int elementId_throttle1Axis = 49;

		// Token: 0x040019E9 RID: 6633
		public const int elementId_throttle2Axis = 155;

		// Token: 0x040019EA RID: 6634
		public const int elementId_throttle1MinDetent = 166;

		// Token: 0x040019EB RID: 6635
		public const int elementId_throttle2MinDetent = 167;

		// Token: 0x040019EC RID: 6636
		public const int elementId_throttleButton1 = 50;

		// Token: 0x040019ED RID: 6637
		public const int elementId_throttleButton2 = 51;

		// Token: 0x040019EE RID: 6638
		public const int elementId_throttleButton3 = 52;

		// Token: 0x040019EF RID: 6639
		public const int elementId_throttleButton4 = 53;

		// Token: 0x040019F0 RID: 6640
		public const int elementId_throttleButton5 = 54;

		// Token: 0x040019F1 RID: 6641
		public const int elementId_throttleButton6 = 55;

		// Token: 0x040019F2 RID: 6642
		public const int elementId_throttleButton7 = 56;

		// Token: 0x040019F3 RID: 6643
		public const int elementId_throttleButton8 = 57;

		// Token: 0x040019F4 RID: 6644
		public const int elementId_throttleButton9 = 58;

		// Token: 0x040019F5 RID: 6645
		public const int elementId_throttleButton10 = 59;

		// Token: 0x040019F6 RID: 6646
		public const int elementId_throttleBaseButton1 = 60;

		// Token: 0x040019F7 RID: 6647
		public const int elementId_throttleBaseButton2 = 61;

		// Token: 0x040019F8 RID: 6648
		public const int elementId_throttleBaseButton3 = 62;

		// Token: 0x040019F9 RID: 6649
		public const int elementId_throttleBaseButton4 = 63;

		// Token: 0x040019FA RID: 6650
		public const int elementId_throttleBaseButton5 = 64;

		// Token: 0x040019FB RID: 6651
		public const int elementId_throttleBaseButton6 = 65;

		// Token: 0x040019FC RID: 6652
		public const int elementId_throttleBaseButton7 = 66;

		// Token: 0x040019FD RID: 6653
		public const int elementId_throttleBaseButton8 = 67;

		// Token: 0x040019FE RID: 6654
		public const int elementId_throttleBaseButton9 = 68;

		// Token: 0x040019FF RID: 6655
		public const int elementId_throttleBaseButton10 = 69;

		// Token: 0x04001A00 RID: 6656
		public const int elementId_throttleBaseButton11 = 132;

		// Token: 0x04001A01 RID: 6657
		public const int elementId_throttleBaseButton12 = 133;

		// Token: 0x04001A02 RID: 6658
		public const int elementId_throttleBaseButton13 = 134;

		// Token: 0x04001A03 RID: 6659
		public const int elementId_throttleBaseButton14 = 135;

		// Token: 0x04001A04 RID: 6660
		public const int elementId_throttleBaseButton15 = 136;

		// Token: 0x04001A05 RID: 6661
		public const int elementId_throttleSlider1 = 70;

		// Token: 0x04001A06 RID: 6662
		public const int elementId_throttleSlider2 = 71;

		// Token: 0x04001A07 RID: 6663
		public const int elementId_throttleSlider3 = 72;

		// Token: 0x04001A08 RID: 6664
		public const int elementId_throttleSlider4 = 73;

		// Token: 0x04001A09 RID: 6665
		public const int elementId_throttleDial1 = 74;

		// Token: 0x04001A0A RID: 6666
		public const int elementId_throttleDial2 = 142;

		// Token: 0x04001A0B RID: 6667
		public const int elementId_throttleDial3 = 143;

		// Token: 0x04001A0C RID: 6668
		public const int elementId_throttleDial4 = 144;

		// Token: 0x04001A0D RID: 6669
		public const int elementId_throttleMiniStickX = 75;

		// Token: 0x04001A0E RID: 6670
		public const int elementId_throttleMiniStickY = 76;

		// Token: 0x04001A0F RID: 6671
		public const int elementId_throttleMiniStickPress = 77;

		// Token: 0x04001A10 RID: 6672
		public const int elementId_throttleWheel1Forward = 145;

		// Token: 0x04001A11 RID: 6673
		public const int elementId_throttleWheel1Back = 146;

		// Token: 0x04001A12 RID: 6674
		public const int elementId_throttleWheel1Press = 147;

		// Token: 0x04001A13 RID: 6675
		public const int elementId_throttleWheel2Forward = 148;

		// Token: 0x04001A14 RID: 6676
		public const int elementId_throttleWheel2Back = 149;

		// Token: 0x04001A15 RID: 6677
		public const int elementId_throttleWheel2Press = 150;

		// Token: 0x04001A16 RID: 6678
		public const int elementId_throttleWheel3Forward = 151;

		// Token: 0x04001A17 RID: 6679
		public const int elementId_throttleWheel3Back = 152;

		// Token: 0x04001A18 RID: 6680
		public const int elementId_throttleWheel3Press = 153;

		// Token: 0x04001A19 RID: 6681
		public const int elementId_throttleHat1Up = 100;

		// Token: 0x04001A1A RID: 6682
		public const int elementId_throttleHat1Up_Right = 101;

		// Token: 0x04001A1B RID: 6683
		public const int elementId_throttleHat1Right = 102;

		// Token: 0x04001A1C RID: 6684
		public const int elementId_throttleHat1Down_Right = 103;

		// Token: 0x04001A1D RID: 6685
		public const int elementId_throttleHat1Down = 104;

		// Token: 0x04001A1E RID: 6686
		public const int elementId_throttleHat1Down_Left = 105;

		// Token: 0x04001A1F RID: 6687
		public const int elementId_throttleHat1Left = 106;

		// Token: 0x04001A20 RID: 6688
		public const int elementId_throttleHat1Up_Left = 107;

		// Token: 0x04001A21 RID: 6689
		public const int elementId_throttleHat2Up = 108;

		// Token: 0x04001A22 RID: 6690
		public const int elementId_throttleHat2Up_Right = 109;

		// Token: 0x04001A23 RID: 6691
		public const int elementId_throttleHat2Right = 110;

		// Token: 0x04001A24 RID: 6692
		public const int elementId_throttleHat2Down_Right = 111;

		// Token: 0x04001A25 RID: 6693
		public const int elementId_throttleHat2Down = 112;

		// Token: 0x04001A26 RID: 6694
		public const int elementId_throttleHat2Down_Left = 113;

		// Token: 0x04001A27 RID: 6695
		public const int elementId_throttleHat2Left = 114;

		// Token: 0x04001A28 RID: 6696
		public const int elementId_throttleHat2Up_Left = 115;

		// Token: 0x04001A29 RID: 6697
		public const int elementId_throttleHat3Up = 116;

		// Token: 0x04001A2A RID: 6698
		public const int elementId_throttleHat3Up_Right = 117;

		// Token: 0x04001A2B RID: 6699
		public const int elementId_throttleHat3Right = 118;

		// Token: 0x04001A2C RID: 6700
		public const int elementId_throttleHat3Down_Right = 119;

		// Token: 0x04001A2D RID: 6701
		public const int elementId_throttleHat3Down = 120;

		// Token: 0x04001A2E RID: 6702
		public const int elementId_throttleHat3Down_Left = 121;

		// Token: 0x04001A2F RID: 6703
		public const int elementId_throttleHat3Left = 122;

		// Token: 0x04001A30 RID: 6704
		public const int elementId_throttleHat3Up_Left = 123;

		// Token: 0x04001A31 RID: 6705
		public const int elementId_throttleHat4Up = 124;

		// Token: 0x04001A32 RID: 6706
		public const int elementId_throttleHat4Up_Right = 125;

		// Token: 0x04001A33 RID: 6707
		public const int elementId_throttleHat4Right = 126;

		// Token: 0x04001A34 RID: 6708
		public const int elementId_throttleHat4Down_Right = 127;

		// Token: 0x04001A35 RID: 6709
		public const int elementId_throttleHat4Down = 128;

		// Token: 0x04001A36 RID: 6710
		public const int elementId_throttleHat4Down_Left = 129;

		// Token: 0x04001A37 RID: 6711
		public const int elementId_throttleHat4Left = 130;

		// Token: 0x04001A38 RID: 6712
		public const int elementId_throttleHat4Up_Left = 131;

		// Token: 0x04001A39 RID: 6713
		public const int elementId_leftPedal = 168;

		// Token: 0x04001A3A RID: 6714
		public const int elementId_rightPedal = 169;

		// Token: 0x04001A3B RID: 6715
		public const int elementId_slidePedals = 170;

		// Token: 0x04001A3C RID: 6716
		public const int elementId_stick = 171;

		// Token: 0x04001A3D RID: 6717
		public const int elementId_stickMiniStick1 = 172;

		// Token: 0x04001A3E RID: 6718
		public const int elementId_stickMiniStick2 = 173;

		// Token: 0x04001A3F RID: 6719
		public const int elementId_stickHat1 = 174;

		// Token: 0x04001A40 RID: 6720
		public const int elementId_stickHat2 = 175;

		// Token: 0x04001A41 RID: 6721
		public const int elementId_stickHat3 = 176;

		// Token: 0x04001A42 RID: 6722
		public const int elementId_stickHat4 = 177;

		// Token: 0x04001A43 RID: 6723
		public const int elementId_throttle1 = 178;

		// Token: 0x04001A44 RID: 6724
		public const int elementId_throttle2 = 179;

		// Token: 0x04001A45 RID: 6725
		public const int elementId_throttleMiniStick = 180;

		// Token: 0x04001A46 RID: 6726
		public const int elementId_throttleHat1 = 181;

		// Token: 0x04001A47 RID: 6727
		public const int elementId_throttleHat2 = 182;

		// Token: 0x04001A48 RID: 6728
		public const int elementId_throttleHat3 = 183;

		// Token: 0x04001A49 RID: 6729
		public const int elementId_throttleHat4 = 184;
	}
}
