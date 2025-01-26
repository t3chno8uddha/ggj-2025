using UnityEngine;

public class PieceRandomizer : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;
    [SerializeField] Vector2 scaleRange = Vector2.one;
    [SerializeField] bool randomRotation = true;
    
    void Start()
    {
        GameObject piece = pieces[Random.Range(0, pieces.Length)];
        foreach (GameObject obj in pieces)
        {
            if (piece != obj) obj.SetActive(false);
        }

        float newScale = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        if (randomRotation)
        {
            var rotation = transform.eulerAngles;
            var random = new Vector3 (rotation.x, Random.Range(0,360), rotation.z);
            
            transform.eulerAngles = random;
        }
    }
}