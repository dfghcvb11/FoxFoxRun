using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들
    //배열로 주고 받을거야
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {
        // 발판을 리셋하는 처리
    stepped = false;
    for(int i = 0; i <obstacles.Length; i ++)
    {
        if(Random.Range(0, 3) == 0)
        {   //Random.Range(0, 3)은 0~2까지의 숫자를 내뱉는다.
            // 0~3이라고 착각하기 너무 쉬워보인다. 착각하지 말자.
            obstacles[i].SetActive(true);
        }
        else
        {
            obstacles[i].SetActive(false);
            //켜지고 꺼지고를 관리하는 방식입니다.
        }
    }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if(collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;
            GameManager.instance.AddScore(100);
        }
    }
}