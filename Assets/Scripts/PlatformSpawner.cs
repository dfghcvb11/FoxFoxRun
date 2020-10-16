using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 4; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값 (숨겨놨지롱)

    private GameObject[] platforms; // 미리 생성한 발판들
    // 그저 배열을 만들어 준 것 뿐임. 여기다가 새로운 오브젝트드를 넣어줘야함.
    private int currentIndex = 0; // 사용할 현재 순번의 발판
    //아 그냥 순서대로 가는갑다.

    private Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점
    //재배치를 위한 녀석 얘는 왜 또 따로 있는걸까?

    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];
        
        for(int i= 0 ;i<count ;i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치
        if(GameManager.instance.isGameover)
        {
            return;
        }

        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);
            //여기서만 새로 정의했네? 랜덤이라 그런거 같아. 나가서는 절대 안 쓰거든
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            currentIndex++;

            if(currentIndex >= count)
            {
                currentIndex = 0;
            }

        }

    }
}