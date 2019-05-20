Shader "Kasug/Noise/WaterDistrub"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Noise("Noise",2D) = "white"{}
		_XFactor("X_Speed",Range(-1.0,1.0)) = 0.1
		_YFactor("Y_Speed",Range(-1.0,1.0)) = 0.1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
			sampler2D _Noise;
			fixed _XFactor;
			fixed _YFactor;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed4 noise = tex2D(_Noise,fixed2(i.uv.x + _Time.x * _XFactor,i.uv.y + _Time.x * _YFactor));
				fixed4 col = tex2D(_MainTex, i.uv + noise.xy);
				
				return col;
			}
			ENDCG
		}
	}
}
