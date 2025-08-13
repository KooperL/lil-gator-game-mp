using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class SharedAudioManager : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060001FE RID: 510 RVA: 0x0000AE56 File Offset: 0x00009056
	private void OnEnable()
	{
		SharedAudioManager.nearbyAudioSources = new List<ISharedAudioSource>();
		this.audioListener = Object.FindObjectOfType<AudioListener>();
		this.activeSources = new Dictionary<SharedAudioProfile, SharedAudioManager.ActiveAudioSource>();
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000AE83 File Offset: 0x00009083
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000AE94 File Offset: 0x00009094
	public void ManagedUpdate()
	{
		Vector3 position = base.transform.position;
		this.outOfRangeList.Clear();
		foreach (SharedAudioManager.ActiveAudioSource activeAudioSource in this.activeSources.Values)
		{
			activeAudioSource.hasSource = false;
			activeAudioSource.directions.Clear();
			activeAudioSource.strengths.Clear();
		}
		foreach (ISharedAudioSource sharedAudioSource in SharedAudioManager.nearbyAudioSources)
		{
			if (sharedAudioSource == null)
			{
				this.outOfRangeList.Add(sharedAudioSource);
			}
			else
			{
				SharedAudioProfile sharedAudioProfile;
				Vector3 vector;
				float num;
				sharedAudioSource.GetAudioData(position, out sharedAudioProfile, out vector, out num);
				if (num == 0f)
				{
					this.outOfRangeList.Add(sharedAudioSource);
				}
				else
				{
					if (!this.activeSources.ContainsKey(sharedAudioProfile))
					{
						this.AddActiveSource(sharedAudioProfile);
					}
					this.activeSources[sharedAudioProfile].hasSource = true;
					this.activeSources[sharedAudioProfile].directions.Add(vector);
					this.activeSources[sharedAudioProfile].strengths.Add(num);
				}
			}
		}
		foreach (ISharedAudioSource sharedAudioSource2 in this.outOfRangeList)
		{
			SharedAudioManager.nearbyAudioSources.Remove(sharedAudioSource2);
			if (sharedAudioSource2 != null)
			{
				sharedAudioSource2.WasRemoved();
			}
		}
		this.activeSourcesToRemove.Clear();
		foreach (KeyValuePair<SharedAudioProfile, SharedAudioManager.ActiveAudioSource> keyValuePair in this.activeSources)
		{
			if (keyValuePair.Key != null && keyValuePair.Value != null && keyValuePair.Value.audioSource != null && (keyValuePair.Value.hasSource || keyValuePair.Value.audioSource.volume > 0f))
			{
				Vector3 vector2 = Vector3.zero;
				float num2 = 0f;
				float num3 = 0f;
				for (int i = 0; i < keyValuePair.Value.directions.Count; i++)
				{
					vector2 += keyValuePair.Value.strengths[i] * keyValuePair.Value.directions[i];
					num2 += keyValuePair.Value.strengths[i];
					num3 = Mathf.Max(num3, keyValuePair.Value.strengths[i]);
				}
				if (num2 > 0f)
				{
					vector2 /= num2;
				}
				float num4 = 1f - vector2.magnitude;
				keyValuePair.Value.audioSource.volume = Mathf.MoveTowards(keyValuePair.Value.audioSource.volume, keyValuePair.Key.volume * Mathf.Pow(num3, 3f), 1.5f * Time.deltaTime * 4f);
				keyValuePair.Value.audioSource.spatialBlend = Mathf.Max(keyValuePair.Key.spacialBlend * (1f - num3), num4);
				keyValuePair.Value.transform.position = base.transform.position + 5f * vector2;
			}
			else
			{
				this.activeSourcesToRemove.Add(keyValuePair.Key);
			}
		}
		foreach (SharedAudioProfile sharedAudioProfile2 in this.activeSourcesToRemove)
		{
			Object.Destroy(this.activeSources[sharedAudioProfile2].gameObject);
			this.activeSources.Remove(sharedAudioProfile2);
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000B30C File Offset: 0x0000950C
	private void AddActiveSource(SharedAudioProfile profile)
	{
		SharedAudioManager.ActiveAudioSource activeAudioSource = new SharedAudioManager.ActiveAudioSource();
		activeAudioSource.gameObject = Object.Instantiate<GameObject>(this.audioSourcePrefab);
		activeAudioSource.gameObject.name = string.Format("Shared Audio Source ({0})", profile.name);
		activeAudioSource.transform = activeAudioSource.gameObject.transform;
		activeAudioSource.audioSource = activeAudioSource.gameObject.GetComponent<AudioSource>();
		activeAudioSource.directions = new List<Vector3>();
		activeAudioSource.strengths = new List<float>();
		activeAudioSource.audioSource.clip = profile.audioClip;
		activeAudioSource.audioSource.pitch = profile.pitch;
		activeAudioSource.audioSource.volume = 0f;
		activeAudioSource.audioSource.Play();
		this.activeSources.Add(profile, activeAudioSource);
	}

	// Token: 0x0400029F RID: 671
	public static List<ISharedAudioSource> nearbyAudioSources = new List<ISharedAudioSource>();

	// Token: 0x040002A0 RID: 672
	public GameObject audioSourcePrefab;

	// Token: 0x040002A1 RID: 673
	private Dictionary<SharedAudioProfile, SharedAudioManager.ActiveAudioSource> activeSources;

	// Token: 0x040002A2 RID: 674
	private AudioListener audioListener;

	// Token: 0x040002A3 RID: 675
	private List<ISharedAudioSource> outOfRangeList = new List<ISharedAudioSource>();

	// Token: 0x040002A4 RID: 676
	private List<SharedAudioProfile> activeSourcesToRemove = new List<SharedAudioProfile>();

	// Token: 0x02000372 RID: 882
	private class ActiveAudioSource
	{
		// Token: 0x04001A64 RID: 6756
		public GameObject gameObject;

		// Token: 0x04001A65 RID: 6757
		public Transform transform;

		// Token: 0x04001A66 RID: 6758
		public AudioSource audioSource;

		// Token: 0x04001A67 RID: 6759
		public bool hasSource;

		// Token: 0x04001A68 RID: 6760
		public List<Vector3> directions;

		// Token: 0x04001A69 RID: 6761
		public List<float> strengths;
	}
}
