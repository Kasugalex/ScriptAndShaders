Shader "Unlit/UVScroll"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise",2D) = "white"{}
		_Factor("Factor",float) = 0.1
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
			float4 _MainTex_ST;
			sampler2D _NoiseTex;
			float _Factor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv.x += _Time.x ;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed4 noise = tex2D(_NoiseTex,fixed2(i.uv.x + _Time.x,i.uv.y));
				fixed4 col = tex2D(_MainTex, i.uv + noise.xy * _Factor);
				return col;
			}
			ENDCG
		}
	}
}
