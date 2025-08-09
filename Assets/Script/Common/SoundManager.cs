using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private List<AudioDirectory> audioClips;


   public void PlaySound(string clip)
   {
      var clip_instance=audioClips.Where(x=>x.clipname==clip).FirstOrDefault();
      audioSource.PlayOneShot(clip_instance.clip);
   }
}

[System.Serializable]
public class AudioDirectory
{
   public string clipname;
   public AudioClip clip;
}
