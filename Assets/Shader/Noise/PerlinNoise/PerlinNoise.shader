Shader "Kasug/Noise/PerlinNoise"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			#include "Assets/CommonResources/Shaders/Noise/ClassicNoise2D.cginc"
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
			/*//实现一
			float2 hash22(float2 p)
			{
				p = float2 (dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)));
				return -1.0 + 2.0 * frac(sin(p) * 43758.5453123);
			}

			float noise(float2 p)
			{
				float2 pi = float2(floor(p.x),floor(p.y));
				float2 pf = p - pi;
				float2 w = pf * pf * (3.0 - 2.0 * pf);

				return lerp(lerp(dot(hash22(pi + float2(0.0, 0.0)), pf - float2(0.0, 0.0)),
					dot(hash22(pi + float2(1.0, 0.0)), pf - float2(1.0, 0.0)), w.x),
					lerp(dot(hash22(pi + float2(0.0, 1.0)), pf - float2(0.0, 1.0)),
						dot(hash22(pi + float2(1.0, 1.0)), pf - float2(1.0, 1.0)), w.x),
					w.y);
			}*/
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				float4 col = cnoise(fixed2(i.uv.x ,i.uv.y));

				return col;
			}
			ENDCG
		}
	}
}
