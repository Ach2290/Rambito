using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Velocidad de la bala
    public float Speed;

    // Sonido que se reproduce al instanciar la bala
    public AudioClip Sound;

    // Referencia al componente Rigidbody2D
    private Rigidbody2D Rigidbody2D;

    // Direcci�n en la que se mover� la bala
    private Vector3 Direction;

    // M�todo llamado al inicio del script
    private void Start()
    {
        // Obtiene la referencia al componente Rigidbody2D adjunto al objeto
        Rigidbody2D = GetComponent<Rigidbody2D>();

        // Reproduce el sonido de la bala usando el componente de audio de la c�mara principal
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    // M�todo llamado en cada frame de la f�sica del juego
    private void FixedUpdate()
    {
        // Establece la velocidad de la bala en la direcci�n especificada
        Rigidbody2D.velocity = Direction * Speed;
    }

    // Establece la direcci�n de la bala
    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    // Destruye la bala
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // M�todo llamado cuando la bala colisiona con otro objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Intenta obtener el script GruntScript adjunto al objeto colisionado
        GruntScript grunt = other.GetComponent<GruntScript>();

        // Intenta obtener el script JohnMovement adjunto al objeto colisionado
        JohnMovement john = other.GetComponent<JohnMovement>();

        // Si se encuentra el script GruntScript, llama al m�todo Hit() para procesar el impacto
        if (grunt != null)
        {
            grunt.Hit();
        }

        // Si se encuentra el script JohnMovement, llama al m�todo Hit() para procesar el impacto
        if (john != null)
        {
            john.Hit();
        }

        // Destruye la bala despu�s de colisionar con cualquier objeto
        DestroyBullet();
    }
}
