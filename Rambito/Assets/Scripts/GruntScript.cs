using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    // Referencia al objeto transform de John (objetivo para el Grunt)
    public Transform John;

    // Prefab del proyectil que el Grunt dispara
    public GameObject BulletPrefab;

    // Salud inicial del Grunt
    private int Health = 3;

    // �ltimo momento en que el Grunt dispar�
    private float LastShoot;

    // M�todo llamado en cada frame de la l�gica del juego
    void Update()
    {
        // Verifica si la referencia a John no es nula (para evitar errores si John no est� asignado)
        if (John == null) return;

        // Calcula la direcci�n desde el Grunt hacia John
        Vector3 direction = John.position - transform.position;

        // Voltea la escala del Grunt en el eje x seg�n la direcci�n para que mire hacia John
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        // Calcula la distancia entre el Grunt y John en el eje x
        float distance = Mathf.Abs(John.position.x - transform.position.x);

        // Dispara si la distancia es menor que 1.0f y ha pasado al menos 0.25 segundos desde el �ltimo disparo
        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    // M�todo que dispara un proyectil desde el Grunt
    private void Shoot()
    {
        // Calcula la direcci�n del disparo seg�n la escala del Grunt en el eje x
        Vector3 direction = new Vector3(transform.localScale.x, 0.0f, 0.0f);

        // Instancia un proyectil en la posici�n del Grunt desplazada ligeramente en la direcci�n del disparo
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);

        // Establece la direcci�n del proyectil usando el script BulletScript
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    // M�todo llamado cuando el Grunt recibe un impacto
    public void Hit()
    {
        // Reduce la salud del Grunt
        Health -= 1;

        // Destruye el Grunt si su salud llega a cero
        if (Health == 0)
            Destroy(gameObject);
    }
}
