using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Comando GetComponent:acessamos um objeto do jogo e
utilizamos alguma caracter�stica sua,se acessarmos
o script,podemos ent�o modificar algum atributo ou 
realizar algum m�todo da classe,logo o getcomponent � uma 
maneira de modificar o jogo ao vivo pelo script.
*/
public class MovePlate : MonoBehaviour {
    //O Script que Controla o Moveplate

    ////Referencias ao controller
    public GameObject controller;

    //A pe�a que tem liga��o com essa moveplate
    GameObject reference = null;

    //localiza��o no tabuleiro
    int matrixX;
    int matrixY;

    //falso:Movimento;True:Attack
    public bool attack = false;

    //Quando o Codigo come�a a rodar
    public void Start() {
        if (attack) {
            //Muda a cor MovePlate para Vermelho
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    //Quando apertamos no moveplate
    public void OnMouseUp() {
        //Encontramos a pe�a com o nome de moveplate
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Se o Moveplate tiver attack = true
        if (attack) {
            //pegamos a posi��o onde o moveplate est�
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            //se a pe�a onde o moveplate est� for o rei ou rainha preto ou branco,o jogo acaba
            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");
            //destr�i a pe�a
            Destroy(cp);
        }

      
        //Deixa a posi��o da pe�a atual em branco -- SetpositionEmpty(pe�a atual.x().y())
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        //Move a pe�a de acordo com o moveplate clicado
        //Atribui novos valores a pe�a de acordo com o x e y do moveplate atual.
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        //Coloca a A pe�a na posi��o real do tabuleiro de acordo com o novo X e Y
        controller.GetComponent<Game>().SetPosition(reference);

        //Troca de jogador
        controller.GetComponent<Game>().NextTurn();

        //Destroi as moveplates existentes,incluindo a atual
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }
    
    public void SetCoords(int x, int y) {
        matrixX = x;
        matrixY = y;
    }
    //A pe�a que tem rela��o com essa moveplate
    public void SetReference(GameObject obj) {
        reference = obj;
    }

    public GameObject GetReference() {
        return reference;
    }
}