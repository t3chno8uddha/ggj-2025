using UnityEngine;
using DG.Tweening;

public class EnemyRenderer : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 targetDir;

    [SerializeField] float angle;

    MaterialPropertyBlock matPropBlock;
    [SerializeField] Renderer enemyRenderer;

    [SerializeField] UnitHealth unit;

    void Start()
    {
        matPropBlock = new MaterialPropertyBlock();
        if (!enemyRenderer) enemyRenderer = GetComponent<Renderer>();
    
        unit.OnDamageReceived += Hit;
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

    public void Hit()
    {
        float hitValue = 0;

        // Tween hitValue from 0 to 1
        DOTween.To(() => hitValue, x => 
        {
            hitValue = x;
            matPropBlock.SetFloat("_Hit", hitValue);
            enemyRenderer.SetPropertyBlock(matPropBlock);
        }, 1f, 0.2f) // Adjust duration (0.5f) as needed
        .OnComplete(() => 
        {
            // Optionally, tween it back to 0 after reaching 1
            DOTween.To(() => hitValue, x => 
            {
                hitValue = x;
                matPropBlock.SetFloat("_Hit", hitValue);
                enemyRenderer.SetPropertyBlock(matPropBlock);
            }, 0f, 0.5f); // Adjust duration as needed
        });
    }
}