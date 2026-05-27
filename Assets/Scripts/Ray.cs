using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastsController : MonoBehaviour
{
    public float distancia = 100f;
    private LineRenderer lineRenderer;
    public float tempoExibicao = 0.1f; 
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 0;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        Vector3 origem = Camera.main.transform.position;
        Vector3 direcao = Camera.main.transform.forward;

        if (Input.GetMouseButtonDown(0))
        {
            ProcessarRaycast(origem, direcao, "destruir");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ProcessarRaycast(origem, direcao, "construir");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ProcessarRaycast(origem, direcao, "pintar");
        }
    }

    void ProcessarRaycast(Vector3 origem, Vector3 direcao, string acao)
    {
        RaycastHit hit;

        if (Physics.Raycast(origem, direcao, out hit, distancia))
        {
            DesenharLaser(origem, hit.point);

            switch (acao)
            {
                case "destruir":
                    Destroy(hit.collider.gameObject);
                    break;

                case "construir":
                    GameObject novoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    novoCubo.transform.position = hit.point + hit.normal * 0.5f;
                    break;

                case "pintar":
                    Renderer rend = hit.collider.GetComponent<Renderer>();
                    if (rend != null) rend.material.color = Color.blue;
                    break;
            }
        }
        else
        {
            DesenharLaser(origem, origem + direcao * distancia);
        }
    }

    void DesenharLaser(Vector3 inicio, Vector3 fim)
    {
        lineRenderer.positionCount = 3;
        lineRenderer.SetPosition(0, inicio);
        lineRenderer.SetPosition(1, fim);

        CancelInvoke(nameof(ApagarLaser));
        Invoke(nameof(ApagarLaser), tempoExibicao);
    }

    void ApagarLaser()
    {
        lineRenderer.positionCount = 0;
    }
}