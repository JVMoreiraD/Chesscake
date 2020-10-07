using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Comando GetComponent:acessamos um objeto do jogo e
utilizamos alguma caracter�stica sua,se acessarmos
o script,podemos ent�o modificar algum atributo ou 
realizar algum m�todo da classe,logo o getcomponent � uma 
maneira de modificar o jogo ao vivo pelo script.
*/

/*basicamente o script que possui tudo sobre a pe�a e os 
Moveplates de cada uma*/
public class Chessman : MonoBehaviour {
        

    //Referencias aos objetos na cena
    public GameObject controller;
    public GameObject movePlate;

    //Posi��o da pe�a no tabuleiro
    private int xBoard = -1;
    private int yBoard = -1;

    //Qual jogador a pe�a pertence
    private string player;

    /*Sprites poss�veis que cada pe�a pode receber
    Como esse c�digo serve para todas as pe�as,
    quando o jogo come�a a pe�a espec�fica recebe
    um sprite.*/

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    public void Activate() {
        //Recebe o controlador do jogo
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        /*Fun��o que faz cada pe�a receber seu sprite
         Cada pe�a vai automaticamente ter seu nome 
         por causa de uma fun��o explicada mais tarde
         e o nome da pe�a � um atributo basico de cada
         objeto do unity,logo n � necess�rio colocar 
         como atributo no script*/

        switch (this.name) {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }
    /* Receber as coordenadas da pe�a e transformar em 
    coordenadas aceit�veis pelo Unity */
    public void SetCoords() {

        /*recebe o valor da pe�a no tabuleiro e converte para
        coordenadas x e y*/
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Adiciona constantes (posi��o 0,0)
        x += -2.3f;
        y += -2.3f;

        //Transforma esses valores em valores aceitados pelo unity
        this.transform.position = new Vector3(x, y, -1.0f);
    }
    //Getters e Setters padr�es
    public int GetXBoard() {
        return xBoard;
    }
    
    public int GetYBoard() {
        return yBoard;
    }

    public void SetXBoard(int x) {
        xBoard = x;
    }

    public void SetYBoard(int y) {
        yBoard = y;
    }
    /*Quando apertamos na Pe�a,Deletamos e geramos novos 
    moveplates*/
    private void OnMouseUp() {
        /*se o jogo n�o tiver acabado e o jogador atual
        for o jogado que est� clicando*/
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player) {

            //Remove todas as moveplates
            DestroyMovePlates();

            //Cria novas moveplates
            InitiateMovePlates();
        }
    }
    //Destroi os moveplates atuais
    public void DestroyMovePlates() {
        /*Procura os objetos com tags(ou nome) de "Moveplate"
        e adiciona em um array do tipo GameObject*/
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        //For passando por todos os valores do array
        for (int i = 0; i < movePlates.Length; i++) {
            //Fun��o que destr�i os objetos em cena
            Destroy(movePlates[i]); 
        }
    }
    /*Fun��o para iniciar os MovePlates,resumindo,
     quando apertamos em uma pe�a,recebemos o nome da pe�a
     ent�o fazemos um switch case para cada nome*/
    public void InitiateMovePlates() {

        switch (this.name) {
            //se for uma rainha preta ou branca
            case "black_queen":
            case "white_queen":
                //Moveplates nas seguintes posi��es
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            //se for um cavalo preto ou branco
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            //se for um bispo preto ou branco
            case "black_bishop":
            case "white_bishop":
                //Moveplates nas seguintes posi��es
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            //se for um Rei preto ou branco
            case "black_king":
            case "white_king":
                //Moveplates nas seguintes posi��es
                //Obs:Provavel erro pois ele se move nas diagonais
                SurroundMovePlate();
                break;
            //se for uma torre preta ou branca
            case "black_rook":
            case "white_rook":
                //Moveplates nas seguintes posi��es
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            //se for um pe�o preto
            case "black_pawn":
                //Moveplates nas seguintes posi��es
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
            // se for um pe�o branco
                //Moveplates nas seguintes posi��es
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement) {
        /*A variavel do tipo game "sc" vai receber um componente
        do objeto controller,sendo esse componente o script Game*/
        Game sc = controller.GetComponent<Game>();
        /*recebemos o x e o y da pe�a no tabuleiro e icrementamos
         de acordo com os argumentos do m�todo*/
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        /*enquanto a posi��o no tabuleiro existir,e n�o ouver nada 
          nessa posi��o*/
        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null) {
            //Coloca um moveplate no tabuleiro na posi��o x e y
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        /*se a posi��o existir e a posi��o for de um jogador que 
        n�o � o jogador atual*/
        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player) {
            //Cria um moveplate de ataque
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate() {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate() {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y) {
        Game sc = controller.GetComponent<Game>();
        //se a posi��o existir
        if (sc.PositionOnBoard(x, y)) {
            //recebemos a posi��o
            GameObject cp = sc.GetPosition(x, y);
            //se a posi��o for nula
            if (cp == null) {
                //colocamos uma moveplate
                MovePlateSpawn(x, y);
            }
            //se a pe�a na posi��o for do jogador inimigo
            else if (cp.GetComponent<Chessman>().player != player) {
                //Coloca uma moveplate de ataque
                MovePlateAttackSpawn(x, y);
            }
        }
    }
    //movimento do pe�o
    public void PawnMovePlate(int x, int y) {
        Game sc = controller.GetComponent<Game>();
        //se a posi��o existe
        if (sc.PositionOnBoard(x, y)) {
            //se a posi��o for nula
            if (sc.GetPosition(x, y) == null) {
                //cria um moveplate
                MovePlateSpawn(x, y);
            }
            //
            //se a posi��o x+1 existir no mapa, a posi��o n�o for nula e for uma pe�a do oponente
            //*Pe�o Branco pois � uma posi��o para frente
            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player) {
                MovePlateAttackSpawn(x + 1, y);
            }
            //se a posi��o x-1 existir no mapa, a posi��o n�o for nula e for uma pe�a do oponente
            //*Pe�o branco pois � uma posi��o para tr�s
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player) {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY) {
        /*Recebe os valores do tabuleiro para converter para
        coordenadas x e y*/
        float x = matrixX;
        float y = matrixY;
        
        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Adiciona constantes (posi��o 0,0)
        x += -2.3f;
        y += -2.3f;

        //Objeto mp recebe o "objeto" moveplate no xyz
        //(ainda n�o est� no tabuleiro,e sim possui um x y e z teorico )
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        //Mpscript recebe o Component do moveplate
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        //Coloca a pe�a atual como pe�a ligada ao Moveplate
        mpScript.SetReference(gameObject);
        /*setamos as coordenadas no tabuleiro,logo 
        agora o moveplate existe*/
        mpScript.SetCoords(matrixX, matrixY);
    }
    //Criar moveplate de ataque
    public void MovePlateAttackSpawn(int matrixX, int matrixY) {
        /*Recebe os valores do tabuleiro para converter para
        coordenadas x e y*/
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Objeto mp recebe o "objeto" moveplate no xyz
        //(ainda n�o est� no tabuleiro,e sim possui um x y e z teorico )
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        //Moveplate recebe o atributo de attack como true
        mpScript.attack = true;
        //Coloca a pe�a atual como pe�a ligada ao Moveplate
        mpScript.SetReference(gameObject);
        /*setamos as coordenadas no tabuleiro,logo 
        agora o moveplate existe*/
        mpScript.SetCoords(matrixX, matrixY);
    }
}