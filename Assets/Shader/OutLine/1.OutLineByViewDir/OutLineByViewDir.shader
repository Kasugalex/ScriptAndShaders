Shader "Kasug/OutLineByViewDir"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color",Color) =(1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal:NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 viewDir:TEXCOORD1;
				float3 normal:NORMAl;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.viewDir = ObjSpaceViewDir(v.vertex);
				o.normal = v.normal;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 viewDir = i.viewDir;
				float normal = i.normal;
				float alpha = 1 - saturate(dot(i.viewDir,i.normal));
				fixed3 col = tex2D(_MainTex, i.uv);

				return fixed4(col * _Color,alpha);
			}
			ENDCG
		}
	}
}
