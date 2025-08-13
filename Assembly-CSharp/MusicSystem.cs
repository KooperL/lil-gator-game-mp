using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

// Token: 0x02000025 RID: 37
[AddComponentMenu("Music/MusicSystem")]
public class MusicSystem : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000078 RID: 120 RVA: 0x000026EC File Offset: 0x000008EC
	public bool IsEligible
	{
		get
		{
			return this.isLocked || this.eligibleTime + 0.5f > Time.time;
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000270B File Offset: 0x0000090B
	private void OnValidate()
	{
		this.loopEndTime = 60.0 / (double)this.bpm * (double)this.loopEndBeats;
		this.loopSkipTime = 60.0 / (double)this.bpm * (double)this.loopStartBeats;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000274B File Offset: 0x0000094B
	private void Awake()
	{
		base.transform.parent = null;
		Object.DontDestroyOnLoad(base.gameObject);
		this.nativeScene = SceneManager.GetActiveScene();
		SceneManager.sceneUnloaded += new UnityAction<Scene>(this.OnSceneUnload);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00002780 File Offset: 0x00000980
	private void Start()
	{
		this.nativeScene = SceneManager.GetActiveScene();
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000278D File Offset: 0x0000098D
	private void OnSceneUnload(Scene unloadedScene)
	{
		if (unloadedScene == this.nativeScene)
		{
			this.isUnloading = true;
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00018478 File Offset: 0x00016678
	private void OnEnable()
	{
		if (this.oldSources == null)
		{
			this.oldSources = new List<AudioSource>();
		}
		this.loopEndTime = 60.0 / (double)this.bpm * (double)this.loopEndBeats;
		this.loopStartTime = (double)(Time.time + 0.25f);
		this.masterWeight = (this.masterWeightSmooth = 0f);
		if (!MusicSystem.current.Contains(this))
		{
			MusicSystem.current.Add(this);
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000027A4 File Offset: 0x000009A4
	private void OnDestroy()
	{
		if (MusicSystem.current.Contains(this))
		{
			MusicSystem.current.Remove(this);
		}
		SceneManager.sceneUnloaded -= new UnityAction<Scene>(this.OnSceneUnload);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000184F8 File Offset: 0x000166F8
	public void CueStartPlaying(float cueDelay)
	{
		if (this.isPlaying)
		{
			return;
		}
		this.isPlaying = true;
		this.isReadyToPlay = true;
		this.rightBeforePlaying.Invoke();
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].smoothWeight = this.layers[i].GetWeight();
			this.layers[i].weightVelocity = 0f;
			if (this.layers[i].smoothWeight > 0f && !this.layers[i].IsReadyToPlay())
			{
				this.isReadyToPlay = false;
			}
		}
		double dspTime = AudioSettings.dspTime;
		this.masterWeightSmooth = this.masterWeight;
		this.loopStartTime = dspTime + (double)cueDelay;
		this.nextLoopTime = this.loopStartTime + this.loopEndTime;
		this.isFirstLoop = true;
		for (int j = 0; j < this.layers.Length; j++)
		{
			this.layers[j].smoothWeight = this.layers[j].GetWeight();
			this.layers[j].weightVelocity = 0f;
			if (this.layers[j].smoothWeight > 0f)
			{
				if (this.stopClipsAtEndOfLoop)
				{
					this.layers[j].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, this.loopEndTime);
				}
				else
				{
					this.layers[j].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, 0.0);
				}
			}
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00018678 File Offset: 0x00016878
	public void StartPlaying()
	{
		if (this.isPlaying)
		{
			return;
		}
		this.isPlaying = true;
		this.isReadyToPlay = true;
		this.rightBeforePlaying.Invoke();
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].smoothWeight = this.layers[i].GetWeight();
			this.layers[i].weightVelocity = 0f;
			if (this.layers[i].smoothWeight > 0f && !this.layers[i].IsReadyToPlay())
			{
				this.isReadyToPlay = false;
			}
		}
		if (this.isReadyToPlay)
		{
			this.StartPlayingNow();
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00018720 File Offset: 0x00016920
	private void StartPlayingNow()
	{
		double dspTime = AudioSettings.dspTime;
		this.masterWeightSmooth = this.masterWeight;
		this.loopStartTime = dspTime + 0.25;
		this.nextLoopTime = this.loopStartTime + this.loopEndTime;
		this.isFirstLoop = true;
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].smoothWeight = this.layers[i].GetWeight();
			this.layers[i].weightVelocity = 0f;
			if (this.layers[i].smoothWeight > 0f)
			{
				if (this.stopClipsAtEndOfLoop)
				{
					this.layers[i].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, this.loopEndTime);
				}
				else
				{
					this.layers[i].StartMusic(this.musicSourcePrefab, base.transform, this.loopStartTime, 0.0);
				}
			}
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0001881C File Offset: 0x00016A1C
	private void TryToUnload()
	{
		if (this.masterWeightSmooth > 1E-05f)
		{
			return;
		}
		foreach (MusicSystem.MusicLayer musicLayer in this.layers)
		{
			if (musicLayer.layerSource != null && musicLayer.layerSource.volume > 1E-05f)
			{
				return;
			}
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0001887C File Offset: 0x00016A7C
	private void Update()
	{
		float num = Mathf.Min(Time.unscaledDeltaTime, 0.05f);
		if (this.isUnloading)
		{
			this.masterWeight = 0f;
			this.TryToUnload();
		}
		double dspTime = AudioSettings.dspTime;
		for (int i = 0; i < this.oldSources.Count; i++)
		{
			if (this.oldSources[i] == null || !this.oldSources[i].isPlaying)
			{
				if (this.oldSources[i] != null)
				{
					Object.Destroy(this.oldSources[i].gameObject);
				}
				this.oldSources.RemoveAt(i);
				i--;
			}
		}
		if (this.masterWeightSmooth > 1E-05f || this.masterWeight > 1E-05f)
		{
			this.masterWeightSmooth = Mathf.MoveTowards(this.masterWeightSmooth, this.masterWeight, 0.2f * num / (this.masterWeightSmoothTimeMultiplier * this.masterWeightSmoothTime));
			if (this.masterWeight == 0f && this.masterWeightSmooth < 1E-05f)
			{
				this.masterWeightSmooth = 0f;
				this.masterWeightVelocity = 0f;
			}
			if (!this.isPlaying && this.masterWeight > 0f && this.masterWeightSmooth > this.masterWeight * 0.5f && dspTime > this.loopStartTime)
			{
				this.StartPlaying();
			}
		}
		if (!this.isPlaying)
		{
			return;
		}
		if (!this.isReadyToPlay)
		{
			this.isReadyToPlay = true;
			for (int j = 0; j < this.layers.Length; j++)
			{
				if (this.layers[j].smoothWeight > 0f && !this.layers[j].IsReadyToPlay())
				{
					this.isReadyToPlay = false;
				}
			}
			if (!this.isReadyToPlay)
			{
				return;
			}
			this.StartPlayingNow();
		}
		if (dspTime > this.nextLoopTime)
		{
			this.loopStartTime = this.nextLoopTime;
			this.nextLoopTime += this.loopEndTime - this.loopSkipTime;
			this.isFirstLoop = false;
			for (int k = 0; k < this.layers.Length; k++)
			{
				if (this.layers[k].layerSource != null)
				{
					this.oldSources.Add(this.layers[k].layerSource);
				}
				if (this.layers[k].cuedLayerSource != null)
				{
					this.layers[k].layerSource = this.layers[k].cuedLayerSource;
					this.layers[k].cuedLayerSource = null;
				}
			}
		}
		bool flag = false;
		foreach (MusicSystem.MusicLayer musicLayer in this.layers)
		{
			if ((musicLayer.weight > 0f || musicLayer.smoothWeight > 0f) && !musicLayer.IsReadyToPlay())
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			MusicSystem.MusicLayer[] array = this.layers;
			int l = 0;
			while (l < array.Length && array[l].IsReadyToPlay())
			{
				l++;
			}
		}
		for (int m = 0; m < this.layers.Length; m++)
		{
			if (this.layers[m].isPlaying && !flag)
			{
				this.layers[m].smoothWeight = Mathf.MoveTowards(this.layers[m].smoothWeight, this.layers[m].GetWeight(), num / this.weightSmoothTime);
			}
			if (!this.layers[m].isPlaying && dspTime > this.layers[m].scheduledTime && this.layers[m].layerSource != null)
			{
				this.layers[m].isPlaying = true;
			}
			if (this.layers[m].smoothWeight > 5E-06f || this.layers[m].weight > 0f)
			{
				if (this.layers[m].IsReadyToPlay())
				{
					if (dspTime + 0.25 > this.nextLoopTime)
					{
						if (this.layers[m].cuedLayerSource == null)
						{
							if (this.stopClipsAtEndOfLoop)
							{
								this.layers[m].CueMusic(this.musicSourcePrefab, base.transform, this.nextLoopTime, this.loopSkipTime, this.loopEndTime);
							}
							else
							{
								this.layers[m].CueMusic(this.musicSourcePrefab, base.transform, this.nextLoopTime, this.loopSkipTime, 0.0);
							}
						}
					}
					else if (this.allowClipsAddedMidloop && this.layers[m].layerSource == null && this.layers[m].audioClip.loadState == 2)
					{
						this.layers[m].layerSource = Object.Instantiate<GameObject>(this.musicSourcePrefab, base.transform).GetComponent<AudioSource>();
						Object.DontDestroyOnLoad(this.layers[m].layerSource.gameObject);
						this.layers[m].layerSource.clip = this.layers[m].audioClip;
						double num2 = dspTime + 0.25 - this.loopStartTime;
						if (!this.isFirstLoop)
						{
							num2 += this.loopSkipTime;
						}
						this.layers[m].layerSource.timeSamples = (int)(num2 * (double)this.layers[m].audioClip.frequency);
						this.layers[m].layerSource.PlayScheduled(dspTime + 0.25);
						this.layers[m].scheduledTime = dspTime + 0.25;
					}
					float num3 = this.masterWeightSmooth * this.layers[m].smoothWeight * this.systemVolume;
					if (this.layers[m].layerSource != null)
					{
						this.layers[m].layerSource.volume = num3;
					}
					if (this.layers[m].cuedLayerSource != null)
					{
						this.layers[m].cuedLayerSource.volume = num3;
					}
				}
			}
			else
			{
				this.layers[m].ClearSource();
				if (this.layers[m].cuedLayerSource != null)
				{
					Object.Destroy(this.layers[m].cuedLayerSource.gameObject);
				}
				this.layers[m].isPlaying = false;
			}
		}
		if (this.masterWeightSmooth == 0f && this.masterWeight == 0f)
		{
			for (int n = 0; n < this.layers.Length; n++)
			{
				this.layers[n].ClearSource();
				this.layers[n].Unload();
				if (this.layers[n].cuedLayerSource != null)
				{
					Object.Destroy(this.layers[n].cuedLayerSource.gameObject);
				}
			}
			this.isPlaying = false;
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00018F78 File Offset: 0x00017178
	public float GetCurrentBeatT()
	{
		double dspTime = AudioSettings.dspTime;
		if (!this.isPlaying || dspTime < this.loopStartTime)
		{
			return -1f;
		}
		return (float)((dspTime - this.loopStartTime) / (60.0 / (double)this.bpm) % 1.0);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00018FC8 File Offset: 0x000171C8
	public bool PrepareStateChange(int[] neededLayers)
	{
		bool flag = true;
		foreach (int num in neededLayers)
		{
			if (this.layers[num].IsReadyToPlay())
			{
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00019000 File Offset: 0x00017200
	public void ClearLayerWeights()
	{
		for (int i = 0; i < this.layers.Length; i++)
		{
			this.layers[i].weight = 0f;
		}
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000027D0 File Offset: 0x000009D0
	public void MarkEligible()
	{
		this.eligibleTime = Time.time;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00019034 File Offset: 0x00017234
	public void ChangeLoop(int newStartBeats, int newEndBeats, MusicSystem.LoopChangeBehavior changeBehavior)
	{
		double num = 60.0 / (double)this.bpm * (double)newEndBeats;
		double num2 = 60.0 / (double)this.bpm * (double)newStartBeats;
		double dspTime = AudioSettings.dspTime;
		double num3 = this.nextLoopTime;
		switch (changeBehavior)
		{
		case MusicSystem.LoopChangeBehavior.Immediate:
			num3 = dspTime + 0.25;
			break;
		case MusicSystem.LoopChangeBehavior.EndOfMeasure:
		{
			double num4 = 60.0 / (double)this.bpm * (double)this.beatsPerMeasure;
			double num5 = (dspTime - this.loopStartTime) % num4;
			num3 = dspTime + (num4 - num5);
			break;
		}
		case MusicSystem.LoopChangeBehavior.Continue:
			num3 = this.nextLoopTime - this.loopEndTime + num;
			break;
		}
		this.loopEndBeats = newEndBeats;
		this.loopEndTime = num;
		this.loopStartBeats = newStartBeats;
		this.loopSkipTime = num2;
		foreach (MusicSystem.MusicLayer musicLayer in this.layers)
		{
			if (musicLayer.cuedLayerSource != null)
			{
				musicLayer.CueMusic(this.musicSourcePrefab, base.transform, num3, this.loopStartTime, this.loopEndTime);
			}
		}
		if (Math.Abs(this.nextLoopTime - num3) > 0.01)
		{
			this.nextLoopTime = num3;
			MusicSystem.MusicLayer[] array = this.layers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UpdateEndTime(this.nextLoopTime);
			}
		}
	}

	// Token: 0x04000096 RID: 150
	public static List<MusicSystem> current = new List<MusicSystem>();

	// Token: 0x04000097 RID: 151
	public int priority;

	// Token: 0x04000098 RID: 152
	public float eligibleTime = -100f;

	// Token: 0x04000099 RID: 153
	public bool isLocked;

	// Token: 0x0400009A RID: 154
	private Scene nativeScene;

	// Token: 0x0400009B RID: 155
	[ReadOnly]
	public bool isUnloading;

	// Token: 0x0400009C RID: 156
	public MusicSystem.MusicLayer[] layers;

	// Token: 0x0400009D RID: 157
	public GameObject musicSourcePrefab;

	// Token: 0x0400009E RID: 158
	public float bpm = 160f;

	// Token: 0x0400009F RID: 159
	public int beatsPerMeasure = 4;

	// Token: 0x040000A0 RID: 160
	[FormerlySerializedAs("totalBeats")]
	public int loopEndBeats = 64;

	// Token: 0x040000A1 RID: 161
	[FormerlySerializedAs("loopSkipBeats")]
	public int loopStartBeats;

	// Token: 0x040000A2 RID: 162
	[ReadOnly]
	public double loopEndTime;

	// Token: 0x040000A3 RID: 163
	[ReadOnly]
	public double loopSkipTime;

	// Token: 0x040000A4 RID: 164
	public bool stopClipsAtEndOfLoop;

	// Token: 0x040000A5 RID: 165
	public float beatSyncMultiplier = 1f;

	// Token: 0x040000A6 RID: 166
	private bool isFirstLoop;

	// Token: 0x040000A7 RID: 167
	private double loopStartTime;

	// Token: 0x040000A8 RID: 168
	private double nextLoopTime;

	// Token: 0x040000A9 RID: 169
	public bool isPlaying;

	// Token: 0x040000AA RID: 170
	[Range(0f, 1f)]
	public float systemVolume = 1f;

	// Token: 0x040000AB RID: 171
	[Range(0f, 1f)]
	public float masterWeight;

	// Token: 0x040000AC RID: 172
	[ReadOnly]
	public float masterWeightSmooth;

	// Token: 0x040000AD RID: 173
	[ReadOnly]
	public float masterWeightVelocity;

	// Token: 0x040000AE RID: 174
	[Range(0.0001f, 5f)]
	public float weightSmoothTime = 1f;

	// Token: 0x040000AF RID: 175
	[Range(0.0001f, 5f)]
	public float masterWeightSmoothTime = 1f;

	// Token: 0x040000B0 RID: 176
	public float masterWeightSmoothTimeMultiplier = 1f;

	// Token: 0x040000B1 RID: 177
	public List<AudioSource> oldSources;

	// Token: 0x040000B2 RID: 178
	private const float cueTime = 0.25f;

	// Token: 0x040000B3 RID: 179
	public bool allowClipsAddedMidloop = true;

	// Token: 0x040000B4 RID: 180
	public bool isSilence;

	// Token: 0x040000B5 RID: 181
	public UnityEvent rightBeforePlaying;

	// Token: 0x040000B6 RID: 182
	private bool isReadyToPlay;

	// Token: 0x040000B7 RID: 183
	private const float volumeCutoff = 1E-05f;

	// Token: 0x02000026 RID: 38
	[Serializable]
	public class MusicLayer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000027E9 File Offset: 0x000009E9
		private static bool CanLoad
		{
			get
			{
				return !MusicSystem.MusicLayer.isSomethingLoading;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000027F3 File Offset: 0x000009F3
		public float GetWeight()
		{
			return this.weight;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000027FB File Offset: 0x000009FB
		public void ClearSource()
		{
			if (this.layerSource != null)
			{
				Object.Destroy(this.layerSource.gameObject);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00019210 File Offset: 0x00017410
		public void Unload()
		{
			if (this.isLoadingAudioClip)
			{
				MusicSystem.MusicLayer.isSomethingLoading = false;
			}
			if (this.isLoadingAudioClip || this.isLoadDependent)
			{
				this.audioClip.UnloadAudioData();
			}
			if (this.isAudioClipAddressable)
			{
				this.audioClip = null;
				this.addressableClip.ReleaseAsset();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00019264 File Offset: 0x00017464
		public void StartMusic(GameObject musicSourcePrefab, Transform parent, double cuedTime, double endTime = 0.0)
		{
			this.layerSource = Object.Instantiate<GameObject>(musicSourcePrefab, parent).GetComponent<AudioSource>();
			Object.DontDestroyOnLoad(this.layerSource.gameObject);
			this.layerSource.clip = this.audioClip;
			this.layerSource.PlayScheduled(cuedTime);
			if (endTime != 0.0)
			{
				this.layerSource.SetScheduledEndTime(cuedTime + endTime + 0.05);
			}
			this.isPlaying = true;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000192E0 File Offset: 0x000174E0
		public void CueMusic(GameObject musicSourcePrefab, Transform parent, double cuedTime, double startTime = 0.0, double endTime = 0.0)
		{
			if (this.cuedLayerSource == null)
			{
				this.cuedLayerSource = Object.Instantiate<GameObject>(musicSourcePrefab, parent).GetComponent<AudioSource>();
				Object.DontDestroyOnLoad(this.cuedLayerSource.gameObject);
				this.cuedLayerSource.clip = this.audioClip;
				this.cuedLayerSource.timeSamples = (int)(startTime * (double)this.audioClip.frequency);
				this.cuedLayerSource.PlayScheduled(cuedTime);
				if (endTime != 0.0)
				{
					this.cuedLayerSource.SetScheduledEndTime(cuedTime + (endTime - startTime) + 0.05);
					return;
				}
			}
			else
			{
				this.cuedLayerSource.timeSamples = (int)(startTime * (double)this.audioClip.frequency);
				this.cuedLayerSource.SetScheduledStartTime(cuedTime);
				if (endTime != 0.0)
				{
					this.cuedLayerSource.SetScheduledEndTime(cuedTime + (endTime - startTime) + 0.05);
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000281B File Offset: 0x00000A1B
		public void UpdateEndTime(double absoluteEndTime)
		{
			this.layerSource.SetScheduledEndTime(absoluteEndTime + 0.05);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000193D4 File Offset: 0x000175D4
		public bool IsReadyToPlay()
		{
			if (this.audioClip == null && !this.isLoadingAsset && this.addressableClip.RuntimeKeyIsValid())
			{
				this.isAudioClipAddressable = true;
				this.isLoadingAsset = true;
				this.addressableClip.LoadAssetAsync<AudioClip>().Completed += this.OnLoadFinished;
			}
			if (this.isLoadingAsset)
			{
				return false;
			}
			if (this.audioClip == null)
			{
				return true;
			}
			switch (this.audioClip.loadState)
			{
			case 0:
				if (MusicSystem.MusicLayer.CanLoad)
				{
					MusicSystem.MusicLayer.isSomethingLoading = true;
					this.isLoadingAudioClip = true;
					this.isLoadDependent = true;
					this.audioClip.LoadAudioData();
				}
				return false;
			case 1:
				return false;
			case 2:
				if (this.isLoadingAudioClip)
				{
					this.isLoadingAudioClip = false;
					MusicSystem.MusicLayer.isSomethingLoading = false;
				}
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002833 File Offset: 0x00000A33
		private void OnLoadFinished(AsyncOperationHandle<AudioClip> obj)
		{
			if (this.isLoadingAsset)
			{
				this.isLoadingAsset = false;
				this.audioClip = obj.Result;
				return;
			}
			this.addressableClip.ReleaseAsset();
		}

		// Token: 0x040000B8 RID: 184
		private static bool isSomethingLoading;

		// Token: 0x040000B9 RID: 185
		private const bool allowMultipleLoading = false;

		// Token: 0x040000BA RID: 186
		public string name;

		// Token: 0x040000BB RID: 187
		[ReadOnly]
		public bool isPlaying;

		// Token: 0x040000BC RID: 188
		public AudioClip audioClip;

		// Token: 0x040000BD RID: 189
		public AssetReferenceT<AudioClip> addressableClip;

		// Token: 0x040000BE RID: 190
		[Range(0f, 1f)]
		public float weight;

		// Token: 0x040000BF RID: 191
		[HideInInspector]
		public float smoothWeight;

		// Token: 0x040000C0 RID: 192
		[HideInInspector]
		public float weightVelocity;

		// Token: 0x040000C1 RID: 193
		[HideInInspector]
		public AudioSource layerSource;

		// Token: 0x040000C2 RID: 194
		[HideInInspector]
		public AudioSource cuedLayerSource;

		// Token: 0x040000C3 RID: 195
		[HideInInspector]
		public double scheduledTime;

		// Token: 0x040000C4 RID: 196
		[ReadOnly]
		public bool isLoadDependent;

		// Token: 0x040000C5 RID: 197
		private const double endTimeBuffer = 0.05;

		// Token: 0x040000C6 RID: 198
		private bool isLoadingAudioClip;

		// Token: 0x040000C7 RID: 199
		private bool isAudioClipAddressable;

		// Token: 0x040000C8 RID: 200
		private bool isLoadingAsset;
	}

	// Token: 0x02000027 RID: 39
	public enum LoadingBehavior
	{
		// Token: 0x040000CA RID: 202
		LoadAll,
		// Token: 0x040000CB RID: 203
		LoadNeededFirst,
		// Token: 0x040000CC RID: 204
		LoadOnlyNeeded,
		// Token: 0x040000CD RID: 205
		PlayAsLoaded
	}

	// Token: 0x02000028 RID: 40
	public enum UnloadingBehavior
	{
		// Token: 0x040000CF RID: 207
		DontUnload,
		// Token: 0x040000D0 RID: 208
		UnloadNotNeeded
	}

	// Token: 0x02000029 RID: 41
	public enum LoopChangeBehavior
	{
		// Token: 0x040000D2 RID: 210
		Immediate,
		// Token: 0x040000D3 RID: 211
		EndOfLoop,
		// Token: 0x040000D4 RID: 212
		EndOfMeasure,
		// Token: 0x040000D5 RID: 213
		Continue
	}
}
