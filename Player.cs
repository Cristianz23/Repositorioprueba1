using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] private float FuerzaVertical = 400f;
    [SerializeField] private float restartDelay = 1f;

    [SerializeField] private ParticleSystem playerParticles;

    [SerializeField] private Color ColorNaranja;
    [SerializeField] private Color ColorNegra;
    [SerializeField] private Color ColorAzul;
    [SerializeField] private Color ColorRoja;
    private string colorActual;
    bool isUp = false;




    // REFERENCIAS 
    Rigidbody2D playerRb;
    SpriteRenderer PlayerSR;
    private SpriteRenderer rend;

    void Start()
    {
       playerRb = GetComponent<Rigidbody2D>();
        
       PlayerSR = GetComponent<SpriteRenderer>();

       CambiarColor();

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(0, FuerzaVertical));
        }

        if (isUp) 
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(0, FuerzaVertical));
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log("XD: " + collision.gameObject.name);
    //     collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("ColorChanger")){
            CambiarColor();
            // DESAPARECE DE LA ESCENA AL OBJECTO
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("Meta")) {
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);
            Invoke("LoadNextScene", restartDelay);
            return ;
        }


        if(!collision.gameObject.CompareTag(colorActual)) {
            gameObject.SetActive(false); // Desactiva el personaje
            Instantiate(playerParticles, transform.position, Quaternion.identity); // Llama efecto particulas 
            Invoke("ReIniciarEscena", restartDelay); // Reinicia la escena cuando perdemos
        }
    }

    /// <summary>
    /// FUNCION QUE PERMITE CARGAR LA SIGUIENTE ESCENA
    /// </summary>
    void LoadNextScene() {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 2);
    }
    /// <summary>
    /// Reinicia la escena cuando perdemos 
    /// </summary>
    void ReIniciarEscena(){
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(activeSceneIndex);
    }
    /// <summary>
    /// Funcion utilizada para cambiar el color cada vez
    /// que se colisiona con la rueda pequeñita de colores
    /// </summary>
    void CambiarColor()
    {
        // Genera un numero aleatorio
        int aleatorio = Random.Range(0, 4);
        Debug.Log(aleatorio);

        // Asigna color 
        if (aleatorio == 0)
        {
            PlayerSR.color = ColorNaranja;
            colorActual = "Naranja";
        } else if (aleatorio == 1)
        {
            PlayerSR.color = ColorNegra;
            colorActual = "Negra";
        } else if (aleatorio == 2)
        {
            PlayerSR.color = ColorAzul;
            colorActual = "Azul";
        } else {
            PlayerSR.color = ColorRoja;
            colorActual = "Roja";
        }

    }

/// <summary>
/// Utilizada para capturar cuando el boton de ui es presionado
/// </summary>
    public void ClickUp(){
        isUp = true;
    }
/// <summary>
/// Utilizada para capturar cuando se suelta el boton de la UI (usado para saltar)
/// </summary>
    public void ReleaseUp(){
        isUp = false;
    }

}
