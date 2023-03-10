using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    // Declaração de duas variáveis ​​do tipo Animator e PlayerControl
    Animator Anim;
    PlayerControl Player;

    // Declaração de quatro variáveis ​​constantes do tipo string que contêm os nomes das animações do personagem.
    string currentState;
    const string Idle = "Idle";
    const string Run = "Run";
    const string Jump = "Jump";
    const string Fall = "Fall";

    
    void Start()// Função chamada quando o script começa a ser executado.
    {
        // Obtém os componentes Animator e PlayerControl do GameObject ao qual este script é anexado.
        Anim = GetComponent<Animator>();
        Player = GetComponent<PlayerControl>();
    }

    
    void Update()// Função chamada a cada quadro de atualização.
    {
        Moving();
    }

    
    void Moving()// Função para verificar o movimento do personagem e alterar a animação de acordo.
    {
        
        if (Player.Grounded())// Verifica se o personagem está no chão.
        {
            // Verifica se o personagem está se movendo na horizontal e altera a animação de acordo.⤵⤵
            if (Player.xAxis != 0)
                AnimationState(Run);
            else
                AnimationState(Idle);
        }
        
        else if (!Player.Grounded())// Verifica se o personagem está no ar.
        {
            // Verifica se o personagem pressionou o botão de pulo e altera a animação de acordo.⤵⤵
            if (Player.jumpPressed)
                AnimationState(Jump);
            else if (!Player.jumpPressed)
                AnimationState(Fall);
        }
    }

    
    void AnimationState(string animState)// Função para alterar o estado da animação atual do personagem.
    {
        // Verifica se a animação atual é a mesma que a animação a ser reproduzida e retorna se forem iguais.
        if (currentState == animState) return;
        
        // Altera a animação do personagem para a animação especificada e atualiza o estado atual da animação.
        Anim.Play(animState);
        currentState = animState;
    }
}
