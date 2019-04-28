Shader "Unlit/Flood"
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
				float4 frustumDir:TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			sampler2D _CameraDepthTexture;
			float4x4 _FrustumDir;
			fixed4 _WaterColor;
			fixed _WaterHeight;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = float4(v.uv,v.uv);
				//左下角uv (0,0) bottomLeft 对应矩阵第一行
				//右下角uv (1,0) bottomRight对应矩阵第二行
				//左上角uv (0,1) topLeft	对应矩阵第三行
				//右上角uv (1,1) topRight	对应矩阵第四行
				int ix = (int)o.uv.z;
				int iy = (int)o.uv.w;
				o.frustumDir = _FrustumDir[ix + 2 * iy];
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv.xy);
				float4 depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv.zw));
				float linearEyeDepth = LinearEyeDepth(depth);
				float3 worldPos = _WorldSpaceCameraPos.xyz + i.frustumDir * linearEyeDepth;
				if (worldPos.y < _WaterHeight)
					return lerp(col,_WaterColor,_WaterColor.a);

				return col;
			}
			ENDCG
		}
	}
}
