using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAudioPlayer : MonoBehaviour
{
  [Header("---Coins---")]
  [SerializeField] AudioClip coinClip;
  [SerializeField] [Range(0f, 1f)] float coinVolume = 1f;

  static NewAudioPlayer instance;

  AudioSource audioSource;

public NewAudioPlayer GetInstance()
{
    return instance;
}

void Awake()
{
    audioSource = GetComponent<AudioSource>();
    ManageSingleton();
}

void ManageSingleton()
{
    //int instanceCount = FindObjectsOfType(GetType()).Length;
    //if(instanceCount > 1)
    if(instance != null)
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    else
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

  public void PlayCoinClip()
  {
      PlayClip(coinClip, coinVolume);
  }

  void PlayClip(AudioClip clip, float volume)
  {
      if(clip != null)
      {
          audioSource.PlayOneShot(clip, volume);
      }
  }
}
