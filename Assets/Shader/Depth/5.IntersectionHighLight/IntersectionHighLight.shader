Shader "Unlit/IntersectionHighLight"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_IntersectionColor("Color",color) = (1,1,1,1)
		_IntersectionWidth("Width",Range(0.0,1.0)) = 0.1
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
				float4 screenPos:TEXCOORD1;
				float eyeZ : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _IntersectionColor;
			fixed _IntersectionWidth;
			sampler2D _CameraDepthTexture;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.eyeZ);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float screenZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
				float halfWidth = _IntersectionWidth * 0.5;
				float diff = saturate(abs(i.eyeZ - screenZ) / halfWidth);

				
				return fixed4(lerp(_IntersectionColor,col,diff));
			}
			ENDCG
		}
	}
}
