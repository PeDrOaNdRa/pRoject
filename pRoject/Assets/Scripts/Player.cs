using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Conf Player")]
    public float velocidade;
    private float movimentoHorizontal;
    private Rigidbody2D rbPlayer;
    public float forcaPulo;
    public Transform posicaoSensor;
    public bool sensor;
    private Animator anim;
    public bool verifdirecao;

    public GameObject municao;
    public Transform posicaoTiro;
    public float speedtiro;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        verificar();
        movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(movimentoHorizontal * velocidade, rbPlayer.velocity.y);
        if (Input.GetButtonDown("Jump") && sensor == true)
        {
            rbPlayer.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
        }

        anim.SetInteger("run", (int)movimentoHorizontal);
        anim.SetBool("jump", sensor);
        if (movimentoHorizontal > 0 && verifdirecao == true)
        {
            direcao();
            speedtiro = speedtiro * -1;
        }
        if (movimentoHorizontal < 0 && verifdirecao == false)
        {
            direcao();
            speedtiro = speedtiro * -1;
        }
        if (Input.GetMouseButtonDown(0))
        {
            atira();
        }
    }

    public void verificar()
    {
        sensor = Physics2D.OverlapCircle(posicaoSensor.position, 0.2f);
    }
    public void direcao()
    {
        verifdirecao = !verifdirecao;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    public void atira()
    {
        GameObject temp = Instantiate(municao);
        temp.transform.position = posicaoTiro.position;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedtiro, 0);
    }
}
