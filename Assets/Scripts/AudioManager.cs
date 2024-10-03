using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
  //  public AudioMixer audioMixer;  // Tham chiếu tới AudioMixer

    public AudioClip theme; // Âm thanh nền
    public AudioClip jump;  // Hiệu ứng âm thanh nhảy
    public AudioClip run;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip shoot;  // Hiệu ứng âm thanh nhảy
    public AudioClip bomup;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip bom;  // Hiệu ứng âm thanh nhảy
    public AudioClip shield;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip heal;  // Hiệu ứng âm thanh nhảy
    public AudioClip highJump;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip coin;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip hurt;  // Hiệu ứng âm thanh nhảy
    public AudioClip dead;  // Hiệu ứng âm thanh nhảy
    public AudioClip enrage;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip eAttack;  // Hiệu ứng âm thanh nhảy
    public AudioClip eShoot;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip eHurt;  // Hiệu ứng âm thanh nhảy
    public AudioClip eDie;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip scene;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip teleport;  // Hiệu ứng âm thanh nhặt đồng xu
    public AudioClip UFO;
    public AudioClip bossJump;

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
    [Range(0f, 1f)] public float masterVolume = 1f; // Âm lượng tổng
    [Range(0f, 1f)] public float backgroundVolume = 0.5f; // Âm lượng nền
    [Range(0f, 1f)] public float efxVolume = 0.75f; // Âm lượng hiệu ứng

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject); // Đảm bảo AudioManager không bị hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //themeSource = gameObject.AddComponent<AudioSource>();
        //jumpSource = gameObject.AddComponent<AudioSource>();
        //runSource = gameObject.AddComponent<AudioSource>();
        //shootSource = gameObject.AddComponent<AudioSource>();
        //bomSource = gameObject.AddComponent<AudioSource>();
        //bomupSource = gameObject.AddComponent<AudioSource>();
        //shieldSource = gameObject.AddComponent<AudioSource>();
        //coinSource = gameObject.AddComponent<AudioSource>();
        //highJumpSource = gameObject.AddComponent<AudioSource>();
        //healSource = gameObject.AddComponent<AudioSource>();
        //hurtSource = gameObject.AddComponent<AudioSource>();
        //deadSource = gameObject.AddComponent<AudioSource>();
        //enrageSource = gameObject.AddComponent<AudioSource>();
        //eAttackSource = gameObject.AddComponent<AudioSource>();
        //eShootSource = gameObject.AddComponent<AudioSource>();
        //eHurtSource = gameObject.AddComponent<AudioSource>();
        //eDieSource = gameObject.AddComponent<AudioSource>();
        //sceneSource = gameObject.AddComponent<AudioSource>();
        //teleportSource = gameObject.AddComponent<AudioSource>();
        //UFOSource = gameObject.AddComponent<AudioSource>();
        
        //themeSource.loop = true;
        //PlayBackground(theme);
        //Khởi tạo các nguồn âm thanh(AudioSource)
        backgroundSource = CreateAudioSource(theme, true); // Nhạc nền lặp lại
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
        //PlayBomUp();
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
