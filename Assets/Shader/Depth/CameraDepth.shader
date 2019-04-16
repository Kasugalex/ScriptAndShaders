// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/CameraDepth"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass{
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag  
			#include "UnityCG.cginc"  

			sampler2D _CameraDepthTexture;

			struct v2f {
			   float4 pos : SV_POSITION;
			   float4 scrPos:TEXCOORD1;
			};

			v2f vert(appdata_base v) {
			   v2f o;
			   o.pos = UnityObjectToClipPos(v.vertex);
			   o.scrPos = ComputeScreenPos(o.pos);
			   return o;
			}

			half4 frag(v2f i) : COLOR{
			   float depthValue = 1 - Linear01Depth(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(i.scrPos)).r);
			   return half4(depthValue,depthValue,depthValue,1);
			}
			ENDCG
		}
	}
}
