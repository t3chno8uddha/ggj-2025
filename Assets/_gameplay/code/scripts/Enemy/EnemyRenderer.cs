using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 targetDir;

    [SerializeField] float angle;

    MaterialPropertyBlock matPropBlock;
    [SerializeField] Renderer enemyRenderer;

    void Start()
    {
        matPropBlock = new MaterialPropertyBlock();
        if (!enemyRenderer) enemyRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        var target = Camera.main.transform;

        targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        if (angle > 135  || angle < -135) { matPropBlock.SetFloat("Facing", 0); }
        if (angle > -135 && angle < -45) { matPropBlock.SetFloat("Facing", -0.25f); }
        if (angle > -45 && angle < 45) { matPropBlock.SetFloat("Facing", -0.5f); }
        if (angle > 45 && angle < 135) { matPropBlock.SetFloat("Facing", -0.75f); }

        enemyRenderer.SetPropertyBlock(matPropBlock);
    }
}