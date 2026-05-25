using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastsController : MonoBehaviour
{
    public float distancia = 100f;
    private LineRenderer lineRenderer;
    public float tempoExibicao = 0.2f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        // 1. CLIQUE DO MOUSE (Destrói)
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, distancia))
            {
                DesenharLaser(ray.origin, hit.point);
                Destroy(hit.collider.gameObject);
            }
            else
            {
                DesenharLaser(ray.origin, ray.origin + ray.direction * distancia);
            }
        }

        // 2. TECLA T (MODIFICADO: Cria um cubo novo no ponto de impacto)
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distancia))
            {
                DesenharLaser(transform.position, hit.point);

                // Cria o cubo nativo da Unity
                GameObject novoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);

                // Posiciona o cubo ligeiramente acima do ponto de impacto para não afundar no chão
                novoCubo.transform.position = hit.point + hit.normal * 0.5f;

                Debug.Log("Cubo criado em: " + novoCubo.transform.position);
            }
            else
            {
                DesenharLaser(transform.position, transform.position + transform.forward * distancia);

                // Se o raio não bater em nada, cria o cubo no ar no limite da distância
                Vector3 posicaoFinal = transform.position + transform.forward * distancia;
                GameObject novoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                novoCubo.transform.position = posicaoFinal;
            }
        }

        // 3. TECLA F (Mantém o Destruir)
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distancia))
            {
                DesenharLaser(transform.position, hit.point);
                Destroy(hit.collider.gameObject);
            }
            else
            {
                DesenharLaser(transform.position, transform.position + transform.forward * distancia);
            }
        }
    }

    void DesenharLaser(Vector3 inicio, Vector3 fim)
    {
        lineRenderer.positionCount = 2;
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
