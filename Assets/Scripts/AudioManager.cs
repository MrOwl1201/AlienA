using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("AudioClip")]
    public AudioClip theme;
    public AudioClip jump; 
    public AudioClip run; 
    public AudioClip shoot; 
    public AudioClip bomup; 
    public AudioClip bom; 
    public AudioClip shield;
    public AudioClip heal;
    public AudioClip highJump;
    public AudioClip coin; 
    public AudioClip hurt; 
    public AudioClip dead; 
    public AudioClip enrage;
    public AudioClip eAttack; 
    public AudioClip eShoot; 
    public AudioClip eHurt; 
    public AudioClip eDie; 
    public AudioClip scene; 
    public AudioClip teleport; 
    public AudioClip UFO;
    public AudioClip bossJump;

    [Header("AudioSource")]
    private AudioSource backgroundSource;
    private AudioSource jumpSource;
    private AudioSource runSource;
    private AudioSource shootSource;
    private AudioSource bomupSource;
    private AudioSource bomSource;
    private AudioSource shieldSource;
    private AudioSource healSource;
    private AudioSource highJumpSource;
    private AudioSource coinSource;
    private AudioSource hurtSource;
    private AudioSource deadSource;
    private AudioSource enrageSource;
    private AudioSource eAttackSource;
    private AudioSource eShootSource;
    private AudioSource eHurtSource;
    private AudioSource eDieSource;
    private AudioSource sceneSource;
    private AudioSource teleportSource;
    private AudioSource UFOSource;
    private AudioSource bossJumpSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f; 
    [Range(0f, 1f)] public float backgroundVolume = 0.5f;
    [Range(0f, 1f)] public float efxVolume = 0.75f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        backgroundSource = CreateAudioSource(theme, true);
        jumpSource = CreateAudioSource(jump, false);
        runSource = CreateAudioSource(run, false);
        shootSource = CreateAudioSource(shoot, false);
        bomSource = CreateAudioSource(bom, false);
        bomupSource = CreateAudioSource(bomup, false);
        shieldSource = CreateAudioSource(shield, false);
        healSource = CreateAudioSource(heal, false);
        highJumpSource = CreateAudioSource(highJump, false);
        hurtSource = CreateAudioSource(hurt, false);
        coinSource = CreateAudioSource(coin, false);
        deadSource = CreateAudioSource(dead, false);
        enrageSource = CreateAudioSource(enrage, false);
        eAttackSource = CreateAudioSource(eAttack, false);
        eShootSource = CreateAudioSource(eShoot, false);
        eHurtSource = CreateAudioSource(eHurt, false);
        eDieSource = CreateAudioSource(eDie, false);
        sceneSource = CreateAudioSource(scene, false);
        teleportSource = CreateAudioSource(teleport, false);
        UFOSource = CreateAudioSource(teleport, false);
        bossJumpSource= CreateAudioSource(bossJump, false);

        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        backgroundVolume = PlayerPrefs.GetFloat("BackgroundVolume", 0.5f);
        efxVolume = PlayerPrefs.GetFloat("EfxVolume", 0.75f);
        backgroundSource.volume = 1;
        PlayBackground(theme);
        UpdateVolumeLevels();
    }
    public void PlayBackground(AudioClip clip)
    {
        backgroundSource.clip = clip;
        backgroundSource.volume = backgroundVolume * masterVolume;
        backgroundSource.Play();
    }
    private AudioSource CreateAudioSource(AudioClip clip, bool loop)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        return newSource;
    }

    public void PlayJump()
    {    
        jumpSource.volume = efxVolume * masterVolume;
        jumpSource.Play();
    }
    public void PlayCoin()
    {
        coinSource.volume = efxVolume * masterVolume;
        coinSource.Play();
    }
    public void PlayRun()
    {
        runSource.volume = efxVolume * masterVolume;
        runSource.Play();
    }
    public void PlayShoot()
    {
        shootSource.volume = efxVolume * masterVolume;
        shootSource.Play();
    }
    public void PlayBom()
    {
        bomSource.volume = efxVolume * masterVolume;
        bomSource.Play();
    }
    public void PlayBomUp()
    {
        bomupSource.volume = efxVolume * masterVolume;
        bomupSource.Play();
    }
    public void PlayShield()
    {
        shieldSource.volume = efxVolume * masterVolume;
        shieldSource.Play();
    }
    public void PlayHeal()
    {
        healSource.volume = efxVolume * masterVolume;
        healSource.Play();
    }
    public void PlayHighJump()
    {
        highJumpSource.volume = efxVolume * masterVolume;
        highJumpSource.Play();
    }
    public void PlayHurt()
    {
        hurtSource.volume = efxVolume * masterVolume;
        hurtSource.Play();
    }
    public void PlayDead()
    {
        deadSource.volume = efxVolume * masterVolume;
        deadSource.Play();
    }
    public void PlayEnrage()
    {
        enrageSource.volume = efxVolume * masterVolume;
        enrageSource.Play();
    }
    public void PlayEnemyAttack()
    {
        eAttackSource.volume = efxVolume * masterVolume;
        eAttackSource.Play();
    }
    public void PlayEnemyShoot()
    {
        eShootSource.volume = efxVolume * masterVolume;
        eShootSource.Play();
    }
    public void PlayEnemyHurt()
    {
        eHurtSource.volume = efxVolume * masterVolume;
        eHurtSource.Play();
    }
    public void PlayEnemyDie()
    {
        eDieSource.volume = efxVolume * masterVolume;
        eDieSource.Play();
    }
    public void PlayScene()
    {
        sceneSource.volume = efxVolume * masterVolume;
        sceneSource.Play();
    }
    public void PlayTeleport()
    {
        teleportSource.volume = efxVolume * masterVolume;
        teleportSource.Play();
    }
    public void PlayUFO()
    {
        UFOSource.volume = efxVolume * masterVolume;
        UFOSource.Play();
    }
    public void PlayBossJump()
    {
        bossJumpSource.volume = efxVolume * masterVolume;
        bossJumpSource.Play();
    }
    public void StopTheme()
    {
        backgroundSource.Stop();
    }
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
        UpdateVolumeLevels();

    }    
    public void SetBackgroundVolume(float volume)
    {
        PlayBackground(theme);
        backgroundVolume = volume;
        PlayerPrefs.SetFloat("BackgroundVolume", backgroundVolume);
        PlayerPrefs.Save();
        UpdateVolumeLevels();
    }    
    public void SetEfxVolume(float volume)
    {
        PlayEnrage();
        efxVolume = volume;
        PlayerPrefs.SetFloat("EfxVolume", efxVolume);
        PlayerPrefs.Save();
        jumpSource.volume *= efxVolume;
        runSource.volume *= efxVolume;
        shootSource.volume *= efxVolume;
        bomSource.volume *= efxVolume;
        bomupSource.volume *= efxVolume;
        shieldSource.volume *= efxVolume;
        hurtSource.volume *= efxVolume;
        deadSource.volume *= efxVolume;
        highJumpSource.volume *= efxVolume;
        coinSource.volume *= efxVolume;
        healSource.volume *= efxVolume;
        enrageSource.volume *= efxVolume;
        eShootSource.volume *= efxVolume;
        eAttackSource.volume *= efxVolume;
        eHurtSource.volume *= efxVolume;
        eDieSource.volume *= efxVolume;
        sceneSource.volume *= efxVolume;
        teleportSource.volume *= efxVolume;
        jumpSource.volume *= efxVolume;
        coinSource.volume *= efxVolume;
        UFOSource.volume*= efxVolume;
    }

    private void UpdateVolumeLevels()
    {
        backgroundSource.volume = backgroundVolume * masterVolume;
        efxVolume = efxVolume * masterVolume;
        SetEfxVolume( efxVolume );
        
    }
}
