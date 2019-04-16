Shader "Unlit/Background"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Factor("Factor",float) = 120
		_Frequency("Frequency",range(0.0,100.0)) = 10
		_Add("Height",Range(0,5.0)) = 0.8
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
			float _Factor;
			fixed _Frequency;
			float _Add;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed a = 0.0;
				fixed2 uv = i.uv;
				fixed max = 20;

				for (int i = 1; i < max; i++)
				{
					fixed iFloat = fixed(i);
					fixed factor = floor(uv.x * _Factor / iFloat + _Frequency * iFloat + _Time.z);

					if (uv.y * (iFloat + _Add) < sin(factor))
						a = iFloat / max;
				}

				return fixed4(a, a, a, 1.0);

				/*
				fixed a = 0.0;
				fixed max = 10;
				fixed2 uv = i.uv;

				for(int i = 1;i<max;i++)
				{
					fixed factor = floor(uv.y < floor(sin(uv.x * _Factor + _Time.y) * _Width) * _Hight);
					if (uv.y * i < factor)
						a = i / max;
				}
				return fixed4(a, a, a, 1.0);*/
			}
			ENDCG
		}
	}
}
