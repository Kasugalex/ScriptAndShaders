Shader "Kasug/GPUParticle/ParticleShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag				
			#include "UnityCG.cginc"

			StructuredBuffer<float3> buf_Points;
			StructuredBuffer<float3> buf_Colors;

			struct ps_input {
				float4 pos:SV_POSITION;
				half3 color:COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			ps_input vert (uint id :SV_VertexID)
			{
				ps_input o;
				float3 pos = buf_Points[id]; //存储点的位置，buf_Points在compute shader已放置了点位置
				o.color = buf_Colors[id];	//
				o.pos = mul(UNITY_MATRIX_VP, float4(pos, 1));
				return o;
			}
			
			fixed4 frag (ps_input i) : SV_Target
			{
				return fixed4(i.color,1);
			}
			ENDCG
		}
	}
}
