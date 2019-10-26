using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesOnRadius : MonoBehaviour
{
	public IDictionary <string, ArrayList> enemies; // Todos os inimigos dentro do círculo
    public GameObject nearestEnemy; // Inimigo mais próximo do centro
    public IDictionary<string, string[]> melodias; // Todas as melodias dos inimigos
    public float timer; // Timer para input do jogador
    private string[] melodia; // Array de notas = uma melodia
    public bool[] pressed; // Array das notas pressionadas
    public int notaAtual; // Indicador da nota atual
    // Start is called before the first frame update
    void Start()
    {
        enemies = new Dictionary<string, ArrayList>(); // Inicializando dicionário dos inimigos
        melodias = new Dictionary<string, string[]>(); // Inicializando dicionário das melodias
        timer = Mathf.Infinity; // Setando o tempo para o infinito

        string[] aux = new string[4];
        pressed = new bool[4];
        melodia = new string[4] { "z", "x", "z", "x"}; // Uma melodia qualquer para o inimigo
        for (int i = 0; i < 4; i++)
        {
            pressed[i] = false; // Todas as notas começam sem terem sido pressionadas
            notaAtual = 0; // Nota atual é a primeira
        }
        melodias.Add("Enemy", melodia); // Adiciona a melodia ao dicionário de melodias
    }

    public void remove() // Remove o inimigo mais próximo do centro
    {
        ArrayList aux = enemies[nearestEnemy.tag]; // Um Arraylist para auxiliar a remoção do inimigo

        aux.Remove(nearestEnemy); // Remove o inimigo mais próximo

        enemies.Remove(nearestEnemy.gameObject.tag); // Remove o Arraylist do dicionário de inimigos
        enemies.Add(nearestEnemy.gameObject.tag, aux); // Adiciona o Arraylist com o mais próximo já removido

        notaAtual = 0; // Volta a nota atual pra zero
        Destroy(nearestEnemy); // Finalmente, destroy o gameObject
    }

    private void OnTriggerEnter2D(Collider2D collision) // Aqui adicionamos novos inimigos ao dicionário, assim que entram no círculo maior
    { // COLOCAR AQUI AS PARADAS DE COLISÂO
    	Debug.Log("colisão!"); // DEBUG
    	try{ // Tenta:
			ArrayList aux = enemies[collision.gameObject.tag]; // Pegar a lista de inimigos com a tag do inimigo que entrou no círculo

			aux.Add(collision.gameObject); // Adiciona à lista o novo inimigo que chegou
	    	enemies.Remove(collision.gameObject.tag); // Remove do dicionário a lista antiga
	    	enemies.Add(collision.gameObject.tag, aux); // Adiciona ao dicionário a lista atualizada, com o novo inimigo

            nearestEnemy = (GameObject) aux[0]; // O inimigo mais próximo é o primeiro, por padrão

            for (int i = 0; i < aux.Count; i++){ // Percorre a lista de inimigos, procurando o mais próximo do centro
                GameObject atual = (GameObject) aux[i]; // O inimigo atual

                // Distância do inimigo atual ao centro (*** CHECAR SE ESTÁ CORRETO ***)
                float distancia1 = Mathf.Sqrt(Mathf.Pow(atual.GetComponent<Transform>().position.x,2)+Mathf.Pow(atual.GetComponent<Transform>().position.y,2));
                // Distância do inimigo "mais próximo" ao centro (*** CHECAR SE ESTÁ CORRETO ***)
                float distancia2 = Mathf.Sqrt(Mathf.Pow(nearestEnemy.GetComponent<Transform>().position.x, 2) + Mathf.Pow(nearestEnemy.GetComponent<Transform>().position.y, 2));

                if(distancia1 < distancia2) { // Se encontrou um novo inimigo mais próximo, atualiza
                    nearestEnemy = atual;
                }
            }

		} catch (KeyNotFoundException e){ // Chegamos aqui quando ainda não há no dicionário inimigos com a tag procurada
			Debug.Log("aaaa"); // DEBUG
	    	ArrayList aux = new ArrayList(); // Criamos um Arraylist vazio
	    	aux.Add(collision.gameObject); // Adicionamos o inimigo
            enemies.Add(collision.gameObject.tag, aux); // Adicionamos o Arraylist ao dicionário
		}
    }

    void preencheMelodia(string tecla) // Tenta preencher a melodia com uma determinada nota
    {
        if (pressed[notaAtual] == false && melodias["Enemy"][notaAtual] == tecla) // Se a próxima nota estiver correta
        {
            pressed[notaAtual] = true; // Confirma a nota e passa para a próxima
            notaAtual++;
            return;
        }
    }

    bool melodiaPreenchida() // Checa se a melodia foi totalmente preenchida
    {
        for (int i = 0; i < 4; i++)
        {
            if (pressed[i] == false)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKey("z") || Input.GetKey("x")) && (timer > 6 || timer <= 0)) // Se ouve input do jogador e o timer está fora de uso
        {
            timer = 6; // O timer volta pra 6s
            notaAtual = 0; // A nota atual volta pra zero
            for (int i = 0; i < 4; i++)
            {
                pressed[i] = false; // Todas as notas voltam a não terem sido pressionadas
            }
        }
        if (Input.GetKey("z")) // Se o jogador apertou Z
        {
            preencheMelodia("z"); // Tenta preencher a melodia com Z
            if (melodiaPreenchida()) // Se a melodia tiver sido preenchida
            {
                remove(); // Remove o inimigo adequado
            }
        }
        else if (Input.GetKey("x")) // Se o jogador apertou X
        {
            preencheMelodia("x"); // Tenta preencher a melodia com X
            if (melodiaPreenchida()) // Se a melodia tiver sido preenchida
            {
                remove(); // Remove o inimigo adequado
            }
        }
        timer -= Time.deltaTime; // Decrementa o contador de tempo
    }
}
