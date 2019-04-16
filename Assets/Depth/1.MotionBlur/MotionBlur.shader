Shader "Unlit/MotionBlur"
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

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				half2 uv_depth:TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv_depth = v.uv;
				return o;
			}
			
			float4x4 _CurrentProjectionInverseMatrix;
			float4x4 _PreviousProjectionMatrix;
			fixed _BlurSize;
			sampler2D _CameraDepthTexture;
			fixed4 frag (v2f i) : SV_Target
			{
				
				float d = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,i.uv_depth);

				float4 H = float4(i.uv.x * 2 - 1 ,i.uv.y * 2 - 1,d * 2 - 1,1);
				float4 NDC = mul(_CurrentProjectionInverseMatrix,H);
				float4 worldPos = NDC / NDC.w;

				float4 currentPos = H;
				float4 previousPos = mul(_PreviousProjectionMatrix,worldPos);
				previousPos /= previousPos.w;

				float2 velocity = (currentPos.xy - previousPos.xy) / 2.0f;

				float2 uv = i.uv;
				fixed4 c = tex2D(_MainTex, i.uv);

				uv+= velocity * _BlurSize;
				for(int it = 1;it<3;it++,uv += velocity * _BlurSize){
					float4 currentColor = tex2D(_MainTex,uv);
					c += currentColor;
				}

				c /= 3;

		
				return c;
			}
			ENDCG
		}
	}
}
