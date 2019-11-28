using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesOnRadius : MonoBehaviour
{
	public static IDictionary <string, ArrayList> enemies; // Todos os inimigos dentro do círculo
    public int[] nearestEnemy; // Inimigo mais próximo do centro
    public IDictionary<string, string[]> melodias; // Todas as melodias dos inimigos
    public float timer; // Timer para input do jogador
    private string[][] melodia; // Array de notas = uma melodia
    public bool[][] pressed; // Array das notas pressionadas
    public int notaAtual; // Indicador da nota atual
    public Text textScore;
    public int  score = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new Dictionary<string, ArrayList>(); // Inicializando dicionário dos inimigos
        melodias = new Dictionary<string, string[]>(); // Inicializando dicionário das melodias
        //timer = Mathf.Infinity; // Setando o tempo para o infinito
        timer = 0;

        string[] aux = new string[4];
        nearestEnemy = new int[4];
        pressed = new bool[4][];
        melodia = new string[4][];
        melodia[0] = new string[4] { "right", "right", "right", "right"}; // Uma melodia qualquer para o inimigo
        melodia[1] = new string[4] { "left", "left", "left", "left" };
        melodia[2] = new string[4] { "up", "up", "up", "up" };
        melodia[3] = new string[4] { "down", "down", "down", "down" };
        for (int i = 0; i < 4; i++)
        {
            pressed[i] = new bool[4]; // Todas as notas começam sem terem sido pressionadas
            for(int j = 0; j < 4; j++)
            {
                pressed[i][j] = false;
            }
            notaAtual = 0; // Nota atual é a primeira
        }
        melodias.Add("Enemy1", melodia[0]); // Adiciona a melodia ao dicionário de melodias
        melodias.Add("Enemy2", melodia[1]);
        melodias.Add("Enemy3", melodia[2]);
        melodias.Add("Enemy4", melodia[3]);
    }

    void calculaMaisProximo()
    {
        for (int i = 1; i < 3; i++)
        {
            try
            { // Tenta:
                ArrayList aux = enemies["Enemy"+i]; // Pegar a lista de inimigos com a tag do inimigo que entrou no círcul

                nearestEnemy[i - 1] = 0; // O inimigo mais próximo é o primeiro, por padrão

                for (int j = 0; j < aux.Count; j++)
                { // Percorre a lista de inimigos, procurando o mais próximo do centro
                    GameObject atual = (GameObject)aux[j]; // O inimigo atual
                    GameObject nearest = (GameObject)aux[nearestEnemy[i - 1]];

                    // Distância do inimigo atual ao centro (*** CHECAR SE ESTÁ CORRETO ***)
                    float distancia1 = Mathf.Sqrt(Mathf.Pow(atual.GetComponent<Transform>().position.x, 2) + Mathf.Pow(atual.GetComponent<Transform>().position.y, 2));
                    // Distância do inimigo "mais próximo" ao centro (*** CHECAR SE ESTÁ CORRETO ***)
                    float distancia2 = Mathf.Sqrt(Mathf.Pow(nearest.GetComponent<Transform>().position.x, 2) + Mathf.Pow(nearest.GetComponent<Transform>().position.y, 2));

                    if (distancia1 < distancia2)
                    { // Se encontrou um novo inimigo mais próximo, atualiza
                        nearestEnemy[i - 1] = j;
                    }
                }

            }
            catch (ArgumentOutOfRangeException e)
            {
                //Debug.Log("foraa");
            }
            catch (ArgumentException)
            {
                Debug.Log("Argument");
            }
        }
    }

    public void remove(int enemy) // Remove o inimigo mais próximo do centro do tipo n
    {
        Debug.Log("Remover Enemy" + enemy);
        try
        {
            ArrayList aux = enemies["Enemy"+enemy]; // Um Arraylist para auxiliar a remoção do inimigo

            GameObject lixo = (GameObject)aux[nearestEnemy[enemy-1]];

            aux.RemoveAt(nearestEnemy[enemy-1]); // Remove o inimigo mais próximo
            Debug.Log("Score ++");
            score++;
            textScore.text = "Score:" + score;
            enemies.Remove("Enemy"+enemy); // Remove o Arraylist do dicionário de inimigos
            enemies.Add("Enemy"+enemy, aux); // Adiciona o Arraylist com o mais próximo já removido

            notaAtual = 0; // Volta a nota atual pra zero
            Destroy(lixo); // Finalmente, destroy o gameObject
            timer = 0;

            calculaMaisProximo();
        } catch (ArgumentOutOfRangeException e)
        {
            //Debug.Log("forraaa22");
        }
        catch (KeyNotFoundException e)
        {
            //Debug.Log("Não há inimigos para essa nota no raio");
        }
        catch (ArgumentException)
        {
            Debug.Log("Argument");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Aqui adicionamos novos inimigos ao dicionário, assim que entram no círculo maior
    { // COLOCAR AQUI AS PARADAS DE COLISÂO
          //Debug.Log("colisão!"); // DEBUG
            if (!collision.gameObject.tag.Contains("Enemy"))
            {
                return;
            }
            try
            { // Tenta:
                ArrayList aux = enemies[collision.gameObject.tag]; // Pegar a lista de inimigos com a tag do inimigo que entrou no círculo

                aux.Add(collision.gameObject); // Adiciona à lista o novo inimigo que chegou
                // lenEnemy = aux.Count; 
                enemies.Remove(collision.gameObject.tag); // Remove do dicionário a lista antiga
                enemies.Add(collision.gameObject.tag, aux); // Adiciona ao dicionário a lista atualizada, com o novo inimigo

                calculaMaisProximo();

            }
            catch (KeyNotFoundException e)
            { // Chegamos aqui quando ainda não há no dicionário inimigos com a tag procurada
              //Debug.Log("aaaa"); // DEBUG
                Debug.Log("Adicionando ao dic Enemy: " + collision.gameObject.tag);
                ArrayList aux = new ArrayList(); // Criamos um Arraylist vazio
                aux.Add(collision.gameObject); // Adicionamos o inimigo
                enemies.Add(collision.gameObject.tag, aux); // Adicionamos o Arraylist ao dicionário
                return;
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Debug.Log("foraa");
            }
            catch(MissingReferenceException e)
            {
                Debug.Log("Erro no atual");
            }
            catch (ArgumentException)
            {
                Debug.Log("Argument");
            }
    }

    void preencheMelodia(string tecla) // Tenta preencher a melodia com uma determinada nota
    {
        for (int i = 1; i <= 4; i++)
        {
            string[] atual = melodias["Enemy" + i];
            //Debug.Log("Melodia Enemy" + i);
                if (pressed[i - 1][notaAtual] == false && atual[notaAtual] == tecla) // Se a próxima nota estiver correta
                {
                    pressed[i - 1][notaAtual] = true; // Confirma a nota e passa para a próxima
                    notaAtual++;
                if (notaAtual == 4) notaAtual = 0;
                try
                {
                    if (melodiaPreenchida(i - 1))
                    {
                        remove(i);
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    /*Debug.Log("Exception");
                    Debug.Log("pressed" + (i - 1) + "," + notaAtual);
                    Debug.Log("atual" + notaAtual);*/
                }
                return;
                }
        }
    }

    bool melodiaPreenchida(int enemy) // Checa se a melodia foi totalmente preenchida
    {
        for(int j = 0; j < 4; j++)
        {
            if (pressed[enemy][j] == false)
            {
                return false;
            }
        }
        notaAtual = 0;
        timer = 0;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyUp("left") || Input.GetKeyUp("right") || Input.GetKeyUp("up") || Input.GetKeyUp("down")) && (timer > 6 || timer <= 0)) // Se houve input do jogador e o timer está fora de uso
        {
            timer = 6; // O timer volta pra 6s
            notaAtual = 0; // A nota atual volta pra zero
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    pressed[i][j] = false; // Todas as notas voltam a não terem sido pressionadas
                }
            }
        }
        if (Input.GetKeyUp("left")) // Se o jogador apertou Z
        {
            preencheMelodia("left"); // Tenta preencher a melodia com Z
        }
        else if (Input.GetKeyUp("right")) // Se o jogador apertou X
        {
            preencheMelodia("right"); // Tenta preencher a melodia com X
        }
        else if (Input.GetKeyUp("up")) // Se o jogador apertou X
        {
            preencheMelodia("up"); // Tenta preencher a melodia com X
        }
        else if (Input.GetKeyUp("down")) // Se o jogador apertou X
        {
            preencheMelodia("down"); // Tenta preencher a melodia com X
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime; // Decrementa o contador de tempo
        }
    }
}
