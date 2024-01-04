using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [System.Serializable]
    public struct NameAndAudioSource
    {
        public string name;
        public AudioClip soundSource;
    }

    public List<NameAndAudioSource> soundPack = new List<NameAndAudioSource>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        MakeParticle(Vector2.one, "1Stage", false, 0f, 0.4f);
    }

    

    public AudioSource MakeParticle(Vector2 pitch,string name = "", bool isAutoDestroy = false, float destroyTime = 2f, float volume = 0.5f
        )
    {

        var particles = soundPack.Where(x => x.name == name).ToArray();

        if (particles.Length >= 1)
        {
            GameObject audio = new GameObject(name);
            AudioSource source = audio.AddComponent<AudioSource>();

            source.volume = 0.5f;
            source.clip = particles[0].soundSource;
            source.pitch = Random.Range(pitch.x, pitch.y);
            source.Play();
            if (isAutoDestroy)
                Destroy(audio, destroyTime);

            return source;
        }

        return null;
    }




}
