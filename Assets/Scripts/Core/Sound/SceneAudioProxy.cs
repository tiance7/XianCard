using UnityEngine;

/// <summary>
/// 场景声音代理
/// </summary>
public class SceneAudioProxy : MonoBehaviour
{

    public static SceneAudioProxy Inst { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        Inst = this;
        audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path"></param>
    /// <param name="source"></param>
    public void PlaySoundEffect(string path)
    {
        if (audioSource == null)
            return;
        AudioClip clip = AssetManager.Load<AudioClip>(path);
        if (clip == null)
            return;
        audioSource.PlayOneShot(clip);
    }
}
