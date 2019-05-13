Shader "Unlit/CPUParticleHelloWorld"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			StructuredBuffer<float3> pointsBuffer;
			StructuredBuffer<float3> colorBuffer;

            struct v2f
            {
                float4 vertex : SV_POSITION;
				fixed3 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (uint id:SV_VertexID)
            {
                v2f o;
				float3 pos = pointsBuffer[id];
				o.color = colorBuffer[id];
				o.vertex = mul(UNITY_MATRIX_VP, float4(pos, 1));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color,1);
            }
            ENDCG
        }
    }
}
