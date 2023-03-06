using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D Rig2D;//cria uma variavel do tipo da classe
    float xAxis; // variavel responsavel em definir a direção horizontal do player.
    [SerializeField] float playerSpeed, jumpForce; // Define a velocidade do player e a força do pulo do jogador.

    void Awake()
    {
        QualitySettings.vSyncCount = 0;//desativa o vSync
        Application.targetFrameRate = 60;//tenta estabiliza o jogo em 60 frames por segundo
    }
    void Start()
    {
        Rig2D = GetComponent<Rigidbody2D>();//puxa os componentes do Rigidbody2d
    }

    void Update()
    {
        MyInput();
        Flip();
    }
    void MyInput()//Metodo responsavel em guardar os controles do player
    {
        //retorna um valor float -1 quando aperta o botoão que vai para esquedar e 1 quando aperta o botão que vai para a direita
        //Time.deltaTime garante que o objeto se mova suavemente, independentemente da taxa de quadros (FPS) do jogo.
        xAxis = Input.GetAxisRaw("Horizontal") * Time.deltaTime * playerSpeed;
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector2(xAxis,0));//altera a posição do player frame por frame causando uma sensaçãod e movimentação
        //Rig2D.velocity = new Vector2(xAxis * playerSpeed, Rig2D.velocity.y);//codigo que vai aplicar uma força a o player dando a sensação de movimentação
    }
    void Flip()
    {
        if (xAxis < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if(xAxis > 0 )
            transform.localScale = new Vector3(1, 1, 1);
    }
}
