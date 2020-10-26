using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject player;

    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public Text wintext;

    public Text livestext;

    private int livesValue = 0;
    private int check;
    private int check2;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    private SpriteRenderer mySpriteRenderer;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        // Start position of player object 
        Vector3 start = new Vector3(-3.69f,3.6f,0);
        player.transform.position = start;
        score.text = "Score: " + scoreValue.ToString();
        wintext.text = "";
        livesValue = 3;
        livestext.text = "Lives: " + livesValue.ToString();
        check = 0;
        check2 = 0;
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    void Update(){
         if (Input.GetKeyDown(KeyCode.A)){
                anim.SetInteger("State", 1);
                mySpriteRenderer.flipX = true;
            }
            if (Input.GetKeyDown(KeyCode.D)){
                anim.SetInteger("State", 1);
                mySpriteRenderer.flipX = false;
            }
            if (Input.GetKeyUp(KeyCode.A)){
                 anim.SetInteger("State", 0);
                 mySpriteRenderer.flipX = true;
            }
            if (Input.GetKeyUp(KeyCode.D)){
                 anim.SetInteger("State", 0);
                 mySpriteRenderer.flipX = false;
            }
            if (Input.GetKeyDown(KeyCode.W)){
                anim.SetInteger("State", 2);
            }
            if (Input.GetKeyUp(KeyCode.W)){
                anim.SetInteger("State", 0);
            }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(livesValue > 0){
            float hozMovement = Input.GetAxis("Horizontal");
            float vertMovement = Input.GetAxis("Vertical");
            rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
           
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (scoreValue == 4 && check == 0){
            Vector3 stage2 = new Vector3(63.06f,2.14f,0);
            player.transform.position = stage2;
            check = 1;
            livesValue = 3;
            livestext.text = "Lives: " + livesValue.ToString();
        }
        if (scoreValue == 8 && check2 == 0){
            wintext.text = "You win! Game created by Jonathan Murray";
            musicSource.loop = false;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            check2 = 1;
        }       
        if (livesValue == 0){
            wintext.text = "Your lives have reached 0, Game Over...";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livestext.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W) && livesValue > 0)
            {
                
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    
}