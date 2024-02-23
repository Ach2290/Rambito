using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Referencia al objeto transform de John (objeto que seguirá la cámara)
    public Transform John;

    // Método llamado en cada frame de la lógica del juego
    void Update()
    {
        // Verifica si la referencia a John no es nula (para evitar errores si John no está asignado)
        if (John != null)
        {
            // Obtiene la posición actual de la cámara
            Vector3 position = transform.position;

            // Actualiza la posición x de la cámara para que coincida con la posición x de John
            position.x = John.position.x;

            // Actualiza la posición de la cámara con la nueva posición x
            transform.position = position;
        }
    }
}
