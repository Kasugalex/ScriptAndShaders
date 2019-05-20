// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Kasug/Depth/MotionBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		
		Pass {      
			
			CGPROGRAM  
			
			#pragma vertex vert  
			#pragma fragment frag  
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float4x4 _CurrentViewProjectionInverseMatrix;
			float4x4 _PreviousViewProjectionMatrix;
			half _BlurSize;
			half _SampleFactor;
			half _SampleTimes;
			struct v2f {
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
				half2 uv_depth : TEXCOORD1;
			};
			
			v2f vert(appdata_img v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				o.uv = v.texcoord;
				o.uv_depth = v.texcoord;		
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target {

				float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv_depth);
				float4 ndc = float4(i.uv.x * 2 - 1, i.uv.y * 2 - 1, depth * 2 - 1, 1);
				float4 D = mul(_CurrentViewProjectionInverseMatrix, ndc);
				
				float4 currentPos = ndc;

				float4 previousPos = mul(_PreviousViewProjectionMatrix, D / D.w);
				previousPos /= previousPos.w;
				
				float2 velocity = (currentPos.xy - previousPos.xy)/_SampleFactor;
				
				float2 uv = i.uv;
				float4 c = tex2D(_MainTex, uv);
				uv += velocity * _BlurSize;
				for (int it = 1; it < _SampleTimes; it++, uv += velocity * _BlurSize) {
					float4 currentColor = tex2D(_MainTex, uv);
					c += currentColor;
				}
				c /= _SampleTimes;
				
				return fixed4(c.rgb, 1.0);
			}
			
			ENDCG  
		}
	} 
}
