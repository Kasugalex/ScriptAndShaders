Shader "Unlit/SkyBoxShader"
{
	Properties
	{
		_SkyBoxTexture ("SkyBox Texture", Cube) = "white" {}
	}
	SubShader
	{
		Cull off ZWrite off 
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 worldPos:TEXCOORD1;
				float4 vertex : SV_POSITION;
			};
			float4 _Corner[4];
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = v.vertex;
				o.worldPos = _Corner[v.uv.x + v.uv.y * 2].xyz;
				return o;
			}

			TextureCube _SkyBoxTexture; 
			SamplerState sampler_SkyBoxTexture;

			fixed4 frag (v2f i) : SV_Target
			{
				float3 viewDir = normalize(i.worldPos - _WorldSpaceCameraPos);
				return _SkyBoxTexture.Sample(sampler_SkyBoxTexture,viewDir);
			}
			ENDCG
		}
	}
}
