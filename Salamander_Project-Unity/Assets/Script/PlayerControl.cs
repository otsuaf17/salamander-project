using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D Rig2D;//Cria uma variavel do tipo da classe
    [Header("Controls variable")]
    [SerializeField] float playerSpeed; // Define a velocidade do player.
    [SerializeField] float jumpForce; // Define a for�a do pulo do player.
    public float xAxis { get; set; } // Variavel responsavel em definir a dire��o horizontal do player.

    [Header("Variable Jump System")]
    [SerializeField] LayerMask groundLayer;//Variavel para selecionar uma Layer especifica
    [SerializeField] Transform groundCheck;//Variavel responsavel para pegar a posição do objeto de colisão
    public bool jumpPressed { get; set; }//Variavel que verifica se o espaço ta precionado ou não

    [Header("Variable Slope System")]
    [SerializeField] Transform slopeCheck;//variavel que pega a posição do objeto "SlopeCheck"
    [SerializeField] float slopCheckPosition;//Variavel responsavel para definir a distancia maxima do "hitSlope"
    [SerializeField] PhysicsMaterial2D noFrictionMaterial;//Variavel que define o material que não tem fricção ao Rigidbody2D
    [SerializeField] PhysicsMaterial2D frictionMaterial;//Variavel que define o material que tem fricção ao Rigidbody2D
    Vector2 perpendicularSpeed;//Variavel pra definir a velocidade perpendicular
    float slopeAngle;//Variavel que vai guarda o valor do angulo da rampa
    bool isOnSlope;//Variavel responsavel pra ver se estão em uma rampa

    void Start()//Metodo que é executado uma vez quando o jogo ou a cena for iniciada
    {
        QualitySettings.vSyncCount = 0;//Desativa o vSync
        Application.targetFrameRate = 60;//Tenta estabiliza o jogo em 60 frames por segundo
        Rig2D = GetComponent<Rigidbody2D>();//Puxa os componentes do Rigidbody2d
    }

    void Update()//Puxa as funções que precisam ser gerados a todo momento do jogo
    {
        MyInput();//Metodo responsavel em guardar os controles do player
        Flip();//Metodo responsavel em fazer o player virar de acordo com a direção dele
        Sloping();//Metodo para verificar se o player esta em uma inclinação
        Move();//Metodo responsavel na movimentação do player
    }

    void MyInput()
    {
        //Retorna um valor float -1 quando aperta o boto�o que vai para esquedar e 1 quando aperta o bot�o que vai para a direita
        //Time.deltaTime garante que o objeto se mova suavemente, independentemente da taxa de quadros (FPS) do jogo.
        xAxis = Input.GetAxisRaw("Horizontal") * Time.deltaTime * playerSpeed;//⤴⤴

        if (Input.GetKeyDown(KeyCode.Space) && Grounded())//Se o player pressionar o "Space" e ele estiver no chão
        {
            Rig2D.velocity = new Vector2(Rig2D.velocity.x, jumpForce);//vai executar o codigo de pulo
            jumpPressed = true;//A variavel sera verdadeira enquanto estiver segurando o espaço
        }
        else if(Input.GetKeyUp(KeyCode.Space) && !Grounded())//Se o player soltar o "Space"
        {
            Rig2D.velocity = new Vector2(Rig2D.velocity.x, Rig2D.velocity.y * 0.2f);//Acelera a queda do player
            jumpPressed = false;//A variavel sera falsa quando o player soltar o botão de espaço
        }
    }

    void Move()
    {
        if(isOnSlope && !jumpPressed && Grounded())//Se ele estiver em uma rampa, não estiver pulando e estiver no chão 
        {
            //ele vai executar a condição de andar na rampa ⤵⤵
            Rig2D.velocity = new Vector2(-xAxis * playerSpeed * perpendicularSpeed.x, -xAxis * playerSpeed * perpendicularSpeed.y);
        }
        else
        {
        Rig2D.velocity = new Vector2(xAxis * playerSpeed, Rig2D.velocity.y);//codigo que vai aplicar uma for�a a o player dando a sensa��o de movimenta��o
        }
    }

    void Flip()
    {
        if (xAxis < 0)
            transform.localScale = new Vector3(-1, 1, 1);//Faz o sprite virar para a esquerda
        else if (xAxis > 0)
            transform.localScale = new Vector3(1, 1, 1);//Faz o sprite virar para a direita
    }
  
    public bool Grounded()//metodo boleano responsavel em verificar se o player esta no chão
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    void Sloping()
    {
        RaycastHit2D hitSlope = Physics2D.Raycast(slopeCheck.position, Vector2.down, slopCheckPosition, groundLayer);//Variavel que cria um raio do ponto de ancoragem para baixo 

        if (hitSlope)//se ele estiver em uma rampa
        {
            //vai guardar o valor da velocidade perpendicular de acordo com o angulo perpendicular do "hitSlope"
            perpendicularSpeed = Vector2.Perpendicular(hitSlope.normal).normalized;//⤴⤴

            slopeAngle = Vector2.Angle(hitSlope.normal, Vector2.up);//Variavel para pegar a possição angular do "hitSlope"
            isOnSlope = slopeAngle != 0;//Variavel que é verdadeira quando o player esta em uma ranpa
        }

        if(isOnSlope && xAxis == 0)//Condição para o "isOnSlope" for verdadeiro e quando o player estiver parado 
        {
            Rig2D.sharedMaterial = frictionMaterial;//Caso a condição for verdadeira o player não vai deslizar em um rampa
        }
        else//Se não a condição de cima não for verdadeiro 
        {
            Rig2D.sharedMaterial = noFrictionMaterial;//O player vai poder se movimentar em uma rampa
        }
    }

}