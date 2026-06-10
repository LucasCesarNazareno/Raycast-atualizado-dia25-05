using UnityEngine;

public class RaycastsController : MonoBehaviour
{
    private float distancia = 100f;
    void Start()
    {

    }

    void Update()
    {
        Vector3 origem = Camera.main.transform.position;
        Vector3 direcao = Camera.main.transform.forward;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(origem, direcao, out hit, distancia))
            {
                if (hit.collider.CompareTag("alvo"))
                {
                Destroy(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;

            if (Physics.Raycast(origem, direcao, out hit, distancia))
            {
                GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubo.transform.position = hit.point + hit.normal * 0.5f;
                cubo.tag = "alvo";
                Renderer objetoRenderer = cubo.GetComponent<Renderer>();
                if (objetoRenderer != null)
                {
                    objetoRenderer.material.color = Color.red;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            RaycastHit hit;

            if (Physics.Raycast(origem, direcao, out hit, distancia))
            {
                Renderer objeto = hit.collider.GetComponent<Renderer>();

                if (objeto != null)
                {
                    objeto.material.color = Color.blue;
                }
            }
        }
    }
}