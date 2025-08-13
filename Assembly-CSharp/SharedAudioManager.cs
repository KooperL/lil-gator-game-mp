using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class SharedAudioManager : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000235 RID: 565 RVA: 0x00003CE4 File Offset: 0x00001EE4
	private void OnEnable()
	{
		SharedAudioManager.nearbyAudioSources = new List<ISharedAudioSource>();
		this.audioListener = Object.FindObjectOfType<AudioListener>();
		this.activeSources = new Dictionary<SharedAudioProfile, SharedAudioManager.ActiveAudioSource>();
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000266A File Offset: 0x0000086A
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0001EA4C File Offset: 0x0001CC4C
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

	// Token: 0x06000238 RID: 568 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
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

	// Token: 0x04000325 RID: 805
	public static List<ISharedAudioSource> nearbyAudioSources = new List<ISharedAudioSource>();

	// Token: 0x04000326 RID: 806
	public GameObject audioSourcePrefab;

	// Token: 0x04000327 RID: 807
	private Dictionary<SharedAudioProfile, SharedAudioManager.ActiveAudioSource> activeSources;

	// Token: 0x04000328 RID: 808
	private AudioListener audioListener;

	// Token: 0x04000329 RID: 809
	private List<ISharedAudioSource> outOfRangeList = new List<ISharedAudioSource>();

	// Token: 0x0400032A RID: 810
	private List<SharedAudioProfile> activeSourcesToRemove = new List<SharedAudioProfile>();

	// Token: 0x0200009E RID: 158
	private class ActiveAudioSource
	{
		// Token: 0x0400032B RID: 811
		public GameObject gameObject;

		// Token: 0x0400032C RID: 812
		public Transform transform;

		// Token: 0x0400032D RID: 813
		public AudioSource audioSource;

		// Token: 0x0400032E RID: 814
		public bool hasSource;

		// Token: 0x0400032F RID: 815
		public List<Vector3> directions;

		// Token: 0x04000330 RID: 816
		public List<float> strengths;
	}
}
