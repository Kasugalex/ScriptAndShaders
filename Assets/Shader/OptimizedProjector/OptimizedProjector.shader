// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Kasug/OptimizedProjector"
{
	Properties{
		_ShadowTex("Cookie", 2D) = "gray" {}
	}
	Subshader{
		Tags {"Queue" = "Transparent"}
		Pass {
			blend srcAlpha oneMinusSrcAlpha
			cull back
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"

			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 pos : SV_POSITION;
				float3 worldNormal:NORMAL;
				float3 worldPos :TEXCOORD1;
			};

			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;

			v2f vert(appdata_full v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uvShadow = mul(unity_Projector, v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex);
				return o;
			}

			sampler2D _ShadowTex;
			float3 _ProjectorWorldPos;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 texS;

				float dotValue = dot(_ProjectorWorldPos - i.worldPos ,normalize(i.worldNormal));

				if(dotValue > 0){
					if (i.uvShadow.x >= 0.01 && i.uvShadow.x <= 0.99
					&& i.uvShadow.y >= 0.01 && i.uvShadow.y <= 0.99) {
						texS = tex2Dproj(_ShadowTex,UNITY_PROJ_COORD(i.uvShadow));
					}
					else {
						texS = fixed4(1,1,1,0);
					}
				}else{
					texS = fixed4(1,1,1,0);
				}
				return texS;

			}
			ENDCG
		}
	}
}
