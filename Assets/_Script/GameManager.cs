using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : SingletonMonobehavior<GameManager>
{
    public float PlayerDmgMod { get; private set; }
    public float EnemyHPScale { get; private set; }

    public int Score { get; private set; }
    [SerializeField]
    private PlayerMovement PlayerCursor;
    [SerializeField]
    List<EnemyMob> EnemyMobList;
    [SerializeField]
    List<EnemyObstacle> EnemyObsList;
    [SerializeField]
    List<BGObject> BGObjects;
    [SerializeField]
    GameObject Boss;
    [SerializeField]
    PlayerStatus PlayerStatus;
    [SerializeField]
    private Slider HpSlide;
    [SerializeField]
    private Text ScoreText;
    private float MobSpawnCD = 3f;
    private float ObsSpawnCD = 2.5f;
    private float BGSpawnCD = 10f;
    private bool MobSwitch;
    protected override void Awake()
    {
        base.Awake();
        SetInitialScale();
    }
    private void Start()
    {
        Score = 0;
        ScoreText.text = "0";
        //SpawnBoss();
        CountDownBoss();
    }
    // Update is called once per frame
    void Update()
    {
        if(MobSwitch)
        {
            SpawnMob();
            SpawnObstacle(); 
        }
        SpawningBGPlanet();
    }
    private void SetInitialScale()
    {
        PlayerDmgMod = 1.0f;
        EnemyHPScale = 1.0f;
        MobSwitch = true;
    }
    private void SpawnMob()
    {
        if(MobSpawnCD <= 0f)
        {
            int side = Random.Range(0,2);
            if(side == 0)
            {
                Vector3 spawnPos1 = new Vector3(10f, Random.Range(-5f, 5f), 0);
                EnemyMob mob1 = EnemyMobList[0];
                Spawner.instance.SpawnObj(mob1.gameObject, spawnPos1, Quaternion.identity);
            }
            else
            {
                Vector3 spawnPos2 = new Vector3(-10f, Random.Range(-5f, 5f), 0);
                EnemyMob mob2 = EnemyMobList[1];
                Spawner.instance.SpawnObj(mob2.gameObject, spawnPos2, Quaternion.identity);
            }
            MobSpawnCD = Random.Range(1.5f, 3f);

        }
        MobSpawnCD -= Time.deltaTime;
    }
    private void SpawnObstacle()
    {
        if (ObsSpawnCD <= 0f)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f,8f),10,0);
            EnemyObstacle obsToSpawn = EnemyObsList[Random.Range(0,EnemyObsList.Count)];
            Spawner.instance.SpawnObj(obsToSpawn.gameObject, spawnPos, Quaternion.identity);

            ObsSpawnCD = Random.Range(0.5f, 1.5f);
        }
        ObsSpawnCD -= Time.deltaTime;
    }

    private void SpawningBGPlanet()
    {
        if (BGSpawnCD <= 0f)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 10, 0);
            BGObject objToSpawn = BGObjects[Random.Range(0, EnemyObsList.Count)];
            Spawner.instance.SpawnObj(objToSpawn.gameObject, spawnPos, Quaternion.identity);

            BGSpawnCD = Random.Range(15f, 40f);
        }
        BGSpawnCD -= Time.deltaTime;
    }
    private void SpawnBoss()
    {
        Spawner.instance.SpawnObj(Boss,new Vector3(0,6,0),Quaternion.identity);
        StartCoroutine(ChangingControl());
        EnemyHPScale *= 1.5f;
    }
    public void EndingGame()
    {
        Destroy(PowerUpPanel.instance.gameObject);
        MobSwitch = false;
        StartCoroutine(Endgame());
    }
    public void CountDownBoss()
    {
        StartCoroutine(SpawningBoss());
    }
    IEnumerator Endgame()
    {
        yield return new WaitForSeconds(1.5f);
        
        MySceneManager.instance.GameOver();
    }
    public void AddScore(int  score)
    {
        Score += score;
        //Debug.Log(Score.ToString());
        ScoreText.text = Score.ToString();
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }
    public void ToggleMob()
    {
        MobSwitch = !MobSwitch;
    }
    IEnumerator SpawningBoss()
    {
        yield return new WaitForSeconds(25f);
        SpawnBoss();
    }
    public void HealPowerUp()
    {
        PlayerStatus.FullyHeal();
    }
    public void MaxHpUpPowerUp()
    {
        PlayerStatus.MaxHPUp(1);
    }
    public void AttackIncrease()
    {
        PlayerDmgMod += 0.25f;
    }
    public void SetHpBar(int curHP,int maxHP)
    {
        float hpratio = (float)curHP / (float)maxHP;
        HpSlide.value = hpratio;
    }
    IEnumerator ChangingControl()
    {
        yield return new WaitForSeconds(2f);
        PlayerCursor.ToggleMovement(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
