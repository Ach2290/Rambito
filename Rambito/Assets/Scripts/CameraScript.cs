using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Referencia al objeto transform de John (objeto que seguir� la c�mara)
    public Transform John;

    // M�todo llamado en cada frame de la l�gica del juego
    void Update()
    {
        // Verifica si la referencia a John no es nula (para evitar errores si John no est� asignado)
        if (John != null)
        {
            // Obtiene la posici�n actual de la c�mara
            Vector3 position = transform.position;

            // Actualiza la posici�n x de la c�mara para que coincida con la posici�n x de John
            position.x = John.position.x;

            // Actualiza la posici�n de la c�mara con la nueva posici�n x
            transform.position = position;
        }
    }
}
