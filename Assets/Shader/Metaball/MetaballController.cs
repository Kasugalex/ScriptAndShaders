using UnityEngine;

public class MetaballController : MonoBehaviour
{
    [Header("Metaball 材质 (Shader: Custom/2DMetaball)")]
    public Material metaballMaterial;

    [Header("元球数量（本例使用 4 个）")]
    public int ballCount = 4;

    [Header("阈值，控制 Metaball 内外边界")]
    [Range(0, 2)]
    public float threshold = 0.5f;

    // 内部存储元球数据，使用 UV 坐标 (0~1 范围)
    private Vector4[] ballPositions;
    private float[] ballRadii;

    void Start()
    {
        // 初始化数组（此处 ballCount 固定为 4，与 Shader 中定义保持一致）
        ballPositions = new Vector4[ballCount];
        ballRadii = new float[ballCount];

        // 初始设置四个元球的位置（使用 UV 坐标：x,y 范围 0 ~ 1）
        ballPositions[0] = new Vector4(0.3f, 0.3f, 0, 0);
        ballPositions[1] = new Vector4(0.7f, 0.3f, 0, 0);
        ballPositions[2] = new Vector4(0.3f, 0.7f, 0, 0);
        ballPositions[3] = new Vector4(0.7f, 0.7f, 0, 0);

        // 设置每个元球的半径，控制影响范围（可根据需要调整）
        for (int i = 0; i < ballCount; i++)
        {
            ballRadii[i] = 0.15f;
        }

        // 设置初始阈值到材质
        metaballMaterial.SetFloat("_Threshold", threshold);
    }

    void Update()
    {
        // 示例：对部分元球做简单动画，让它们轻微移动
        float t = Time.time;
        ballPositions[0].x = 0.3f + 0.1f * Mathf.Sin(t);
        ballPositions[0].y = 0.3f + 0.1f * Mathf.Cos(t);
        ballPositions[1].x = 0.7f + 0.1f * Mathf.Cos(t);
        ballPositions[1].y = 0.3f + 0.1f * Mathf.Sin(t);
        ballPositions[2].x = 0.3f + 0.1f * Mathf.Cos(t * 1.1f);
        ballPositions[2].y = 0.7f + 0.1f * Mathf.Sin(t * 1.1f);
        ballPositions[3].x = 0.7f + 0.1f * Mathf.Sin(t * 1.1f);
        ballPositions[3].y = 0.7f + 0.1f * Mathf.Cos(t * 1.1f);

        // 将更新后的元球数据传递给材质 Shader
        metaballMaterial.SetVectorArray("_BallPositions", ballPositions);
        metaballMaterial.SetFloatArray("_BallRadii", ballRadii);

        // 若阈值在 Inspector 中调整，也可以实时更新
        metaballMaterial.SetFloat("_Threshold", threshold);
    }
}
