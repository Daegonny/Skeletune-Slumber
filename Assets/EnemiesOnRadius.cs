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
    public int notaAtual; // Indicador da nota atual
    public Text textScore;
    public int  score;

    private string[] melodiaAtual;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new Dictionary<string, ArrayList>(); // Inicializando dicionário dos inimigos
        melodias = new Dictionary<string, string[]>(); // Inicializando dicionário das melodias
        //timer = Mathf.Infinity; // Setando o tempo para o infinito
        timer = 0;
        melodiaAtual = new string[4]{"","","",""};
        string[] aux = new string[4];
        nearestEnemy = new int[4];
        melodia = new string[4][];
        melodia[0] = new string[4] { "left", "down", "up", "left"}; // Uma melodia qualquer para o inimigo
        melodia[1] = new string[4] { "up", "left", "right", "up" };
        melodia[2] = new string[4] { "right", "up", "right", "down" };
        melodia[3] = new string[4] { "down", "down", "left", "right" };
        
        enemies.Add("Enemy1", new ArrayList());
        enemies.Add("Enemy2", new ArrayList());
        enemies.Add("Enemy3", new ArrayList());
        enemies.Add("Enemy4", new ArrayList());

        notaAtual = 0; // Nota atual é a primeira
        melodias.Add("Enemy1", melodia[0]); // Adiciona a melodia ao dicionário de melodias
        melodias.Add("Enemy2", melodia[1]);
        melodias.Add("Enemy3", melodia[2]);
        melodias.Add("Enemy4", melodia[3]);

        score = PlayerPrefs.GetInt ("highscore");
        textScore.text = score.ToString();
    }

    void calculaMaisProximo()
    {
        for (int i = 1; i <= 4; i++)
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
                    float distancia1 = Mathf.Sqrt(Mathf.Pow(atual.GetComponent<Transform>().position.x - transform.position.x, 2) + Mathf.Pow(atual.GetComponent<Transform>().position.y - transform.position.y, 2));
                    // Distância do inimigo "mais próximo" ao centro (*** CHECAR SE ESTÁ CORRETO ***)
                    float distancia2 = Mathf.Sqrt(Mathf.Pow(nearest.GetComponent<Transform>().position.x - transform.position.x, 2) + Mathf.Pow(nearest.GetComponent<Transform>().position.y - transform.position.y, 2));

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
        try
        {
            ArrayList aux = enemies["Enemy"+enemy]; // Um Arraylist para auxiliar a remoção do inimigo

            GameObject lixo = (GameObject)aux[nearestEnemy[enemy-1]];

            aux.RemoveAt(nearestEnemy[enemy-1]); // Remove o inimigo mais próximo
            score++;
            textScore.text = score.ToString();
            //enemies.Remove("Enemy"+enemy); // Remove o Arraylist do dicionário de inimigos
            //enemies.Add("Enemy"+enemy, aux); // Adiciona o Arraylist com o mais próximo já removido

            notaAtual = 0; // Volta a nota atual pra zero
            Destroy(lixo); // Finalmente, destroy o gameObject
            playSound("SkeletonDead");

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
                //enemies.Remove(collision.gameObject.tag); // Remove do dicionário a lista antiga
                //enemies.Add(collision.gameObject.tag, aux); // Adiciona ao dicionário a lista atualizada, com o novo inimigo

                calculaMaisProximo();

            }
            catch (KeyNotFoundException e)
            { // Chegamos aqui quando ainda não há no dicionário inimigos com a tag procurada
              //Debug.Log("aaaa"); // DEBUG
                //Debug.Log("Adicionando ao dic Enemy: " + collision.gameObject.tag);
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


    int checkMelodia(string[] m){
        int output = -1;
        for (int i = 1; i <= 4; i++){
            if (melodias["Enemy"+i][0] == m[0] && melodias["Enemy"+i][1] == m[1] && melodias["Enemy"+i][2] == m[2] && melodias["Enemy"+i][3] == m[3]){ 
                output = i;
                break;
            }
        }
        return output;
    }

    void preencheMelodia(string tecla) // Tenta preencher a melodia com uma determinada nota
    {
        playSoundNote(tecla);
        melodiaAtual[notaAtual] = tecla;
        notaAtual++;
        if(notaAtual == 4){
            notaAtual = 0;
            int enemyType = checkMelodia(melodiaAtual); 
            if(enemyType != -1){
                try{
                    timer = 0;
                    remove(enemyType);
                }
                catch (IndexOutOfRangeException e)
                {
                   
                }
            }
            else{
                //TODO: TOCAR SOM DE MELODIA ERRADA
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
         PlayerPrefs.SetInt ("highscore", score);
         Debug.Log(PlayerPrefs.GetInt ("highscore").ToString());

        if((Input.GetKeyUp("left") || Input.GetKeyUp("right") || Input.GetKeyUp("up") || Input.GetKeyUp("down")) && (timer > 6 || timer <= 0)) // Se houve input do jogador e o timer está fora de uso
        {
            timer = 3; // O timer volta pra 6s
            notaAtual = 0; // A nota atual volta pra zero
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
        if (timer == 0){
            notaAtual = 0;
        }
    }

    private void playSound(string sound) {
        FindObjectOfType<AudioManager>().Play(sound);
    }

    private void playSoundNote(string key) {
        switch (key)
        {
            case "left":
                FindObjectOfType<AudioManager>().Play("Note1");
                break;
            case "right":
                FindObjectOfType<AudioManager>().Play("Note2");
                break;
            case "up":
                FindObjectOfType<AudioManager>().Play("Note3");
                break;
            case "down":
                FindObjectOfType<AudioManager>().Play("Note4");
                break;
            default:
                break;
        }
    }
}
