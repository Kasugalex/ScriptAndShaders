Shader "Custom/MobileMetaball"
{
    Properties
    {
        // 主贴图（这里可以不使用，仅用作 Pass 的占位）
        _MainTex("Texture", 2D) = "white" {}
        // Metaball 颜色
        _MetaballColor("Metaball Color", Color) = (1,1,1,1)
        // 阈值，决定哪个像素被认为在 Metaball 内部
        _Threshold("Threshold", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // 定义元球数量（本例中使用 4 个）
            #define BALL_COUNT 4

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _MetaballColor;
            float _Threshold;

            // 数组传递元球数据
            // _BallPositions: 每个元球的中心坐标（使用 UV 坐标，xy 分量有效）
            float4 _BallPositions[BALL_COUNT];
            // _BallRadii: 每个元球的半径（控制高斯衰减范围）
            float _BallRadii[BALL_COUNT];

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // 计算给定 UV 坐标与元球中心的高斯衰减影响
            float Influence(float2 uv, float2 center, float radius)
            {
                // 计算距离
                float dist = distance(uv, center);
                // 使用高斯函数，保证距离越远影响越小
                // 参数 radius 控制扩散程度，radius 越大，衰减越慢
                return exp(- (dist * dist) / (radius * radius));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算当前像素的场值（累加所有元球的影响）
                float field = 0.0;
                for (int j = 0; j < BALL_COUNT; j++)
                {
                    field += Influence(i.uv, _BallPositions[j].xy, _BallRadii[j]);
                }

                // 根据阈值判断该像素是否处于 Metaball 内部
                if (field > _Threshold)
                {
                    // 可以根据 field 调整颜色强度，此处直接使用设定颜色
                    return _MetaballColor;
                }
                else
                {
                    // 不满足阈值条件则输出透明（注意：需要材质支持透明混合模式）
                    return fixed4(0, 0, 0, 0);
                }
            }
            ENDCG
        }
    }
}
