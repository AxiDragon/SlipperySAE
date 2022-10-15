using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Soundtrack : MonoBehaviour
{
    AudioSource source;
    float targetVolume = 0f;
    float baseVolume = 0f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        baseVolume = source.volume;
        SceneManager.sceneLoaded += NewSceneUnmute;
    }

    private void NewSceneUnmute(Scene arg0, LoadSceneMode arg1)
    {
        UnMute();
    }

    void Start()
    {
        source.volume = 0f;
        targetVolume = baseVolume;
    }

    void Update()
    {
        source.volume = Mathf.MoveTowards(source.volume, targetVolume, Time.deltaTime / 8f);
    }

    [ContextMenu("Mute")]
    public void Mute()
    {
        targetVolume = 0f;
    }

    [ContextMenu("Unmute")]
    public void UnMute()
    {
        targetVolume = baseVolume;
    }
}
