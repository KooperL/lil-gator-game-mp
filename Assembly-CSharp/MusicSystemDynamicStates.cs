using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000026 RID: 38
[AddComponentMenu("Music/MusicSystem Dynamic States")]
public class MusicSystemDynamicStates : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000091 RID: 145 RVA: 0x00004C48 File Offset: 0x00002E48
	private void OnValidate()
	{
		if (this.musicSystem == null)
		{
			this.musicSystem = base.GetComponent<MusicSystem>();
		}
		if (this.dynamicStates != null && this.dynamicStates.Length != 0)
		{
			this.currentState = Mathf.Clamp(this.currentState, 0, this.dynamicStates.Length);
			for (int i = 0; i < this.dynamicStates.Length; i++)
			{
				if (this.dynamicStates[i].variants != null && this.dynamicStates[i].variants.Length != 0)
				{
					for (int j = 0; j < this.dynamicStates[i].variants.Length; j++)
					{
						this.dynamicStates[i].variants[j].name = this.dynamicStates[i].variants[j].variant.ToString();
					}
				}
			}
		}
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00004D20 File Offset: 0x00002F20
	private void Awake()
	{
		MusicSystemDynamicStates.DynamicState[] array = this.dynamicStates;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Initialize(this.musicSystem);
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00004D50 File Offset: 0x00002F50
	private void OnEnable()
	{
		this.isPlayerActiveSmooth = 1f;
		this.currentVariant = MusicSystemDynamicStates.VariantStates.Dialogue;
		for (int i = 0; i < this.dynamicStates.Length; i++)
		{
			this.dynamicStates[i].lastEligibleTime = -100f;
		}
		this.UpdateDynamicState(this.currentState, this.currentVariant);
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00004DB2 File Offset: 0x00002FB2
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00004DC0 File Offset: 0x00002FC0
	public void ManagedUpdate()
	{
		MusicSystemDynamicStates.VariantStates variantStates = this.currentVariant;
		int num = this.currentState;
		int num2 = -100;
		for (int i = 0; i < this.dynamicStates.Length; i++)
		{
			MusicSystemDynamicStates.DynamicState dynamicState = this.dynamicStates[i];
			if (dynamicState.IsEligible && dynamicState.priority >= num2)
			{
				num2 = dynamicState.priority;
				num = i;
			}
		}
		if (this.forceUpdate || num != this.currentState || variantStates != this.currentVariant)
		{
			this.UpdateDynamicState(num, variantStates);
		}
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00004E3C File Offset: 0x0000303C
	private void UpdateDynamicState(int newState, MusicSystemDynamicStates.VariantStates newVariant)
	{
		this.currentState = newState;
		this.currentVariant = newVariant;
		if (this.ignoreMusicLayers)
		{
			return;
		}
		this.musicSystem.ClearLayerWeights();
		MusicSystemDynamicStates.DynamicState dynamicState = this.dynamicStates[this.currentState];
		MusicSystemDynamicStates.DynamicState.LayerSetting[] array = dynamicState.layerSettings;
		foreach (MusicSystemDynamicStates.DynamicState.DynamicStateVariant dynamicStateVariant in dynamicState.variants)
		{
			if (dynamicStateVariant.variant == this.currentVariant)
			{
				array = dynamicStateVariant.layerSettings;
				break;
			}
		}
		foreach (MusicSystemDynamicStates.DynamicState.LayerSetting layerSetting in array)
		{
			this.musicSystem.layers[layerSetting.layerIndex].weight = layerSetting.volume;
		}
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00004EE8 File Offset: 0x000030E8
	public int GetStateIndex(string stateName)
	{
		for (int i = 0; i < this.dynamicStates.Length; i++)
		{
			if (this.dynamicStates[i].name == stateName)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00004F20 File Offset: 0x00003120
	public void MarkStateEligible(int index)
	{
		this.dynamicStates[index].lastEligibleTime = Time.time;
	}

	// Token: 0x040000C6 RID: 198
	public MusicSystem musicSystem;

	// Token: 0x040000C7 RID: 199
	public bool ignoreMusicLayers;

	// Token: 0x040000C8 RID: 200
	public MusicSystemDynamicStates.DynamicState[] dynamicStates;

	// Token: 0x040000C9 RID: 201
	public int currentState;

	// Token: 0x040000CA RID: 202
	public MusicSystemDynamicStates.VariantStates currentVariant = MusicSystemDynamicStates.VariantStates.Active;

	// Token: 0x040000CB RID: 203
	private float isPlayerActiveSmooth = 1f;

	// Token: 0x040000CC RID: 204
	public bool forceUpdate;

	// Token: 0x040000CD RID: 205
	private const float stateEligibilityDelay = 1f;

	// Token: 0x02000356 RID: 854
	public enum VariantStates
	{
		// Token: 0x040019EB RID: 6635
		Stationary,
		// Token: 0x040019EC RID: 6636
		Active,
		// Token: 0x040019ED RID: 6637
		Dialogue = 10
	}

	// Token: 0x02000357 RID: 855
	[Serializable]
	public class DynamicState
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00066A34 File Offset: 0x00064C34
		public bool IsEligible
		{
			get
			{
				return this.isLocked || this.lastEligibleTime + 1f > Time.time;
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00066A53 File Offset: 0x00064C53
		public int[] GetNeededLayers()
		{
			return this.neededLayers;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00066A5C File Offset: 0x00064C5C
		public void Initialize(MusicSystem musicSystem)
		{
			List<int> list = new List<int>();
			foreach (MusicSystemDynamicStates.DynamicState.LayerSetting layerSetting in this.layerSettings)
			{
				layerSetting.FindLayerIndex(musicSystem);
				if (layerSetting.layerIndex != -1 && !list.Contains(layerSetting.layerIndex))
				{
					list.Add(layerSetting.layerIndex);
				}
			}
			MusicSystemDynamicStates.DynamicState.DynamicStateVariant[] array2 = this.variants;
			for (int i = 0; i < array2.Length; i++)
			{
				foreach (MusicSystemDynamicStates.DynamicState.LayerSetting layerSetting2 in array2[i].layerSettings)
				{
					layerSetting2.FindLayerIndex(musicSystem);
					if (layerSetting2.layerIndex != -1 && !list.Contains(layerSetting2.layerIndex))
					{
						list.Add(layerSetting2.layerIndex);
					}
				}
			}
			this.neededLayers = list.ToArray();
		}

		// Token: 0x040019EE RID: 6638
		public string name;

		// Token: 0x040019EF RID: 6639
		[Tooltip("Highest wins")]
		public int priority;

		// Token: 0x040019F0 RID: 6640
		[ReadOnly]
		public float lastEligibleTime;

		// Token: 0x040019F1 RID: 6641
		public bool isLocked;

		// Token: 0x040019F2 RID: 6642
		public MusicSystemDynamicStates.DynamicState.LayerSetting[] layerSettings;

		// Token: 0x040019F3 RID: 6643
		public MusicSystemDynamicStates.DynamicState.DynamicStateVariant[] variants;

		// Token: 0x040019F4 RID: 6644
		private int[] neededLayers;

		// Token: 0x020004B9 RID: 1209
		[Serializable]
		public class LayerSetting
		{
			// Token: 0x06001E27 RID: 7719 RVA: 0x00079430 File Offset: 0x00077630
			public void FindLayerIndex(MusicSystem musicSystem)
			{
				for (int i = 0; i < musicSystem.layers.Length; i++)
				{
					if (musicSystem.layers[i].name == this.layerName)
					{
						this.layerIndex = i;
						return;
					}
				}
				this.layerIndex = -1;
			}

			// Token: 0x04001FA0 RID: 8096
			[MusicLayerLookup("musicSystem")]
			public string layerName;

			// Token: 0x04001FA1 RID: 8097
			[ReadOnly]
			public int layerIndex;

			// Token: 0x04001FA2 RID: 8098
			[Range(0f, 1f)]
			public float volume = 1f;
		}

		// Token: 0x020004BA RID: 1210
		[Serializable]
		public class DynamicStateVariant
		{
			// Token: 0x04001FA3 RID: 8099
			[HideInInspector]
			public string name;

			// Token: 0x04001FA4 RID: 8100
			public MusicSystemDynamicStates.VariantStates variant;

			// Token: 0x04001FA5 RID: 8101
			public MusicSystemDynamicStates.DynamicState.LayerSetting[] layerSettings;
		}
	}
}
