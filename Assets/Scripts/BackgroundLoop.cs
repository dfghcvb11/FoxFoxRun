using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경의 가로 길이

    private void Awake() {
        // 가로 길이를 측정하는 처리
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        Transform backgroundTransform = GetComponent<Transform>();
        width = (backgroundCollider.size.x)*(backgroundTransform.localScale.x);
        // 참조형식을 이용해야하니까 한번 이렇게 뺴줘야해.
        //잊지말자 컴포넌트를 쓰려면 참조형식 변수로 먼저 선언해주고 써야한다.
    }

    private void Update() {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋
        if(transform.position.x <= -width)
        {Reposition();}
    }

    // 위치를 리셋하는 메서드
    private void Reposition() {
        //왜 새로운 벡터를 선언해야하는가?
        Vector2 offset1 = new Vector2(width*2f, 0);
        transform.position = (Vector2)transform.position + offset1;
    }
}