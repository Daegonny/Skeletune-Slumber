using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class djScript : MonoBehaviour{

    private CircleCollider2D cc2d;
    public int lives = 3;
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        lives = 3;
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.tag.StartsWith("Enemy"))
        {
            return;
        }
        Debug.Log("colisão com " + collision.gameObject.tag);
        lives--;
        playSound("GettingHit");
        if(lives == 0)
        {
            SceneManager.LoadSceneAsync("GameOver");
        }
        try
        {
            ArrayList aux = EnemiesOnRadius.enemies[collision.gameObject.tag]; // Um Arraylist para auxiliar a remoção do inimigo
            
            GameObject lixo = (GameObject) collision.gameObject;

            aux.Remove(lixo); // Remove o inimigo mais próximo

            EnemiesOnRadius.enemies.Remove(collision.gameObject.tag); // Remove o Arraylist do dicionário de inimigos
            EnemiesOnRadius.enemies.Add(collision.gameObject.tag, aux); // Adiciona o Arraylist com o mais próximo já removido

            Destroy(lixo); // Finalmente, destroy o gameObject
            playSound("SkeletonDead");
        }
        catch (KeyNotFoundException e)
        {
            //Debug.Log("Não há inimigos para essa nota no raio");
        }
    }

    private void playSound(string sound) {
        FindObjectOfType<AudioManager>().Play(sound);
    }

    private void FixedUpdate()
    {
        
    }
}
