using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public JohnMovement John; // Asigna el objeto John desde el Inspector

    private Slider healthSlider;

    private void Start()
    {
        // Obtén la referencia al componente Slider
        healthSlider = GetComponent<Slider>();

        // Asegúrate de que se ha asignado el objeto John
        if (John == null)
        {
            Debug.LogError("No se ha asignado el objeto John al script de la barra de vida.");
            return;
        }

        // Establece el valor máximo del Slider como la salud inicial de John
        healthSlider.maxValue = John.Health;
    }

    private void Update()
    {
        // Actualiza el valor actual del Slider con la salud actual de John
        if (John != null)
        {
            healthSlider.value = John.Health;
        }
    }
}
