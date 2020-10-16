using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
// 상태 관리 1. 달리기ㅇ 2. 뛰기ㅇ 3. 죽음ㅇ 별로 애니매이이션 재생
// 애니매이터 할당
// 점프 1. 점프 세기ㅇ 2. 횟수ㅇ
// 죽음 1. 죽음 오디오클립 할당ㅇ 2. 죽음상태
// 리지드바디 잊지마!!  
public class PlayerController : MonoBehaviour {
    
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public GameObject backgroundAudio;
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   //항상 false네.. 점프할때만 발동? 이해가 안 간다.
   private bool isDead = false; // 사망 상태

    //플레이어의 컴포넌트들을 할당할 변수들이 선언되어있음.
   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private CircleCollider2D playerCollider;
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트 
    
    private void Start() {
       // 초기화 하자마자 컴포넌트 부터 할당 떄려버리기
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update() {
       // 사용자 입력을 감지하고 점프하는 처리
        if(isDead)
        {
            return;
           //그냥 리턴 때리면 되는거?
           //업데이트 자페를 나가버리라는거지.
        }

        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
           //정말 아름다운 코드
            playerAudio.Play();
           //미쳤어
        }

        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
           playerRigidbody.velocity *= 0.5f;
           //너무나도 아름다운 처리. 손가락을 때는 순간 속도를 낮춘다 = 누르지 않으면 속도를 유지한다 이마리야.
        }

        animator.SetBool("Grounded", isGrounded);
       //이런거는 계속 갱신해줘야하나?
       //애니메이터의 파라미터 그라운디드를 이스그라운드 값으로 계속해서 갱신해준다.
       //애니메이터와 
    }

    private void Die() {
       // 사망 처리
        animator.SetTrigger("Die");
       //뭐 재생한다는거임? 그런거 같음.
       //파라미터를 향하는 것이다!!

        playerAudio.clip = deathClip;
        playerAudio.Play();
       // 아 오디오 소스에 할당된 오디오 클립을 바꿔주는거군요.
       // 미리 여러 클립을 할당해놓고, 오디오 소스의 자리를 계속 대체해주는거임.
       // 내 생각이 맞아 원래는 점프가 할당되어있자너.
        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
        playerCollider.enabled = false ;
        backgroundAudio.SetActive(false);
        GameManager.instance.OnPlayerDead();
        //이건 UI를 위한 것.
    }

    private void OnTriggerEnter2D(Collider2D other) {
       // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
       // 낙사영역과 장애물에다가 Dead 태그를 할당한 후 트리거 콜라이더를 달아 줄거래요.
       if(other.tag == "Dead" && !isDead ) //죽지 않았을 떄.
        {
            playerRigidbody.AddForce(new Vector2(0, (0.5f)*jumpForce));
            Die();
        }   
    }

    private void OnCollisionEnter2D(Collision2D collision) {
       // 바닥에 닿았음을 감지하는 처리
        if(collision.contacts[0].normal.y > 0.7f )
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
       // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}