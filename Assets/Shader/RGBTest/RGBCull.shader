Shader "Kasug/RGBCull"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_CullFactor("Factor",range(0.5,3.0)) = 0.95
		_Color("Color",color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue" = "Transparent"}
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _CullFactor;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a = step(col.r + col.g + col.b,_CullFactor);
				/*if (col.r >= _CullFactor && col.g >= _CullFactor && col.b >= _CullFactor)
					col.a = 0;
				else
					col.a = 1;*/
				return col;
			}
			ENDCG
		}
	}
}
