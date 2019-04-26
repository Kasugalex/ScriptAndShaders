Shader "Unlit/InteractiveWater"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			sampler2D _NoiseTex;

			float _WindForce;

			float2 _Direction;

			fixed _NoisexScroll;
			fixed _NoiseyScroll;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv += float2 (_Direction.x, _Direction.y) * _Time.x * _WindForce;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{		
				fixed4 noise = tex2D(_NoiseTex,fixed2(i.uv.x + _Time.x * _NoisexScroll ,i.uv.y + _Time.x * _NoiseyScroll));
				fixed4 col = tex2D(_MainTex, i.uv + noise.xy * min(_WindForce,0.25));
				return col;
			}
			ENDCG
		}
	}
}
