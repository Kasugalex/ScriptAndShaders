Shader "Kasug/RimFade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Range("Fade Range",Range(0.0,0.5)) = 0.125
		_Mask ("Mask",2d) = "white"{}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
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
			fixed _Range;
			sampler2D _Mask;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv= v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 mask = tex2D(_Mask,i.uv);
				fixed a = mask.a - _Range;
				a = a >=0 ? a :0.01;
				col.a *= a;

				return col;
			}
			ENDCG
		}
	}
}
