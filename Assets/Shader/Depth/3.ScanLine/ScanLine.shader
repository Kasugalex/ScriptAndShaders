Shader "Kasug/Depth/ScanLine"
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
				float4 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float4 _MainTex_ST;
			half _CurValue;
			half _LineWidth;
			fixed4 _LineColor;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = float4(v.uv, v.uv);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv.xy);
				float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv.zw);
				float linear01Depth = Linear01Depth(depth);
				float halfWidth = _LineWidth * 0.5;
				float v = saturate(abs(_CurValue - linear01Depth) / halfWidth);
				return lerp(_LineColor,col,v);
			}
			ENDCG
		}
	}
}
