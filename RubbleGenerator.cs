using UnityEngine;

public class RubbleGenerator : MonoBehaviour
{
    public GameObject rubblePrefab; // 3D model prefab
    public Shader rubbleShader;     // Rubble shader
    public int numberOfInstances = 10; // Number of rubble instances to generate
    public float spreadRadius = 5f; // Radius for spreading out the rubble

    void Start()
    {
        GenerateRubble();
    }

    void GenerateRubble()
    {
        if (rubblePrefab == null)
        {
            Debug.LogError("Rubble prefab not assigned!");
            return;
        }

        if (rubbleShader == null)
        {
            Debug.LogError("Rubble shader not assigned!");
            return;
        }

        // Applies the shader to the material
        Material rubbleMaterial = new Material(rubbleShader);

        for (int i = 0; i < numberOfInstances; i++)
        {
            // Randomizes position within the spread radius in both X and Z axes
            float offsetX = Random.Range(-spreadRadius, spreadRadius);
            float offsetZ = Random.Range(-spreadRadius, spreadRadius);

            // Duplicates the prefab with the randomized offset
            GameObject rubbleInstance = Instantiate(rubblePrefab, new Vector3(transform.position.x + offsetX, transform.position.y, transform.position.z + offsetZ), Quaternion.identity);

            // Applies the material with the shader
            Renderer renderer = rubbleInstance.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = rubbleMaterial;
            }

            // Assigns a Mesh Collider to the rubble instances
            MeshCollider meshCollider = rubbleInstance.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = rubbleInstance.GetComponent<MeshFilter>().sharedMesh;

            // Randomly rotate the instances
            rubbleInstance.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualizes the spread radius in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spreadRadius);
    }
}
