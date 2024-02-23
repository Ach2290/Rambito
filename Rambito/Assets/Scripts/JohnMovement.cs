using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    // Velocidad de movimiento de John
    public float Speed;

    // Fuerza de salto de John
    public float JumpForce;

    // Prefab de la bala que John disparará
    public GameObject BulletPrefab;

    // Componente Rigidbody2D de John para el movimiento físico
    private Rigidbody2D Rigidbody2D;

    // Componente Animator de John para controlar las animaciones
    private Animator Animator;

    // Input horizontal (izquierda o derecha)
    private float Horizontal;

    // Estado de si John está en el suelo
    private bool Grounded;

    // Último momento en que John disparó
    public float LastShoot;

    // Salud actual de John
    public int Health = 5;

    // Método llamado al inicio del script
    public void Start()
    {
        // Obtiene los componentes Rigidbody2D y Animator de John
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Método llamado en cada frame de la lógica del juego
    public void Update()
    {
        // Movimiento horizontal
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Voltea la escala de John para mirar en la dirección correcta
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Configura la animación de correr en base al input horizontal
        Animator.SetBool("running", Horizontal != 0.0f);

        // Detecta si John está en el suelo usando un rayo hacia abajo
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Disparo
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    // Método llamado en cada frame de la física del juego
    public void FixedUpdate()
    {
        // Aplica la velocidad horizontal al Rigidbody2D para el movimiento
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    // Método para realizar el salto de John
    public void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    // Método para que John dispare una bala
    public void Shoot()
    {
        // Determina la dirección de la bala según la escala de John
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        // Instancia la bala y establece su dirección usando el script BulletScript
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    // Método para manejar el daño recibido por John
    public void Hit()
    {
        Health -= 1;

        // Destruye a John si su salud llega a cero
        if (Health == 0) Destroy(gameObject);
    }
}
