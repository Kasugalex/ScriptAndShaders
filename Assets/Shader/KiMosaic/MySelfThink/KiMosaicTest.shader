Shader "Unlit/KiMosaicTest"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Level ("Level",Range(1,100)) = 10
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

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
			float4 _MainTex_TexelSize;
			fixed _Level;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				float2 uv = i.uv * _MainTex_TexelSize.zw;
				uv = (floor(uv / _Level) + fixed2(0.5,0.5)) * _Level * _MainTex_TexelSize.xy;		
				fixed3 col = tex2D(_MainTex, uv);
				return fixed4(col,1);
			}
			ENDCG
		}
	}
}
