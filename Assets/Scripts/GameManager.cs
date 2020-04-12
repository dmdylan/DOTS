using System.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics;
using TMPro;
using Unity.Transforms;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ballPrefab;
    public TextMeshProUGUI scoreText;

    private int curScore;
    private Entity ballEntityPrefab;
    private EntityManager manager;
    private BlobAssetStore blobAssetStore;

    public int maxScore;
    public int cubesPerFrame;
    public float cubeSpeed = 3f;
    public GameObject cubePrefab;
    private bool insanemode;
    private Entity cubeEntityPrefab;


    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ballPrefab, settings);

        cubeEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(cubePrefab, settings);
    }

    private void OnDestroy()
    {
        blobAssetStore.Dispose();
    }

    private void Start()
    {
        insanemode = false;
        curScore = 0;
        DisplayScore();
        SpawnBall();
    }

    private void Update()
    {
        if(!insanemode && curScore >= maxScore)
        {
            insanemode = true;
            StartCoroutine(SpawnLotsOfCubes());
        }    
    }

    IEnumerator SpawnLotsOfCubes()
    {
        while (insanemode)
        {
            for(int i = 0; i < cubesPerFrame; i++)
            {
                SpawnNewCube();
            }
            yield return null;
        }
    }

    void SpawnNewCube()
    {
        Entity newCubeEntity = manager.Instantiate(cubeEntityPrefab);

        Vector3 direction = Vector3.up;
        Vector3 speed = direction * cubeSpeed;

        PhysicsVelocity velocity = new PhysicsVelocity()
        {
            Linear = speed,
            Angular = float3.zero
        };

        manager.AddComponentData(newCubeEntity, velocity);
    }

    public void IncreaseScore()
    {
        curScore++;
        DisplayScore();
    }

    private void SpawnBall()
    {
        Entity newBallEntity = manager.Instantiate(ballEntityPrefab);

        Translation ballTrans = new Translation
        {
            Value = new float3(0f, 0.5f, 0f)
        };

        manager.AddComponentData(newBallEntity, ballTrans);
        CameraFollow.instance.ballEntity = newBallEntity;
    }

    private void DisplayScore()
    {
        scoreText.text = "Score: " + curScore;
    }
}
