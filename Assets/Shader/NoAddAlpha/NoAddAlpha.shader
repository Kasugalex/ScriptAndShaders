// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Test"
{
Properties   
    {   
		_MainTex("MainTex",2d) = "white"{}
        _Color("Color",Color) = (1,1,1,1)
    }  
      
    SubShader   
    {  
        
        LOD 100  
  		Tags { "Queue" = "Transparent" "RenderType"="Opaque" } 
		stencil
		{
			Ref 2
			Comp Greater
			Pass Replace
		}
        Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
 
			float4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			struct appdata_t {
				float4 vertex : POSITION;
				float4 color:COLOR;
				float2 uv:TEXCOORD0;
			};
 
			struct v2f {
				float4  pos : SV_POSITION;
				float4	color:COLOR;
				float2 uv:TEXCOORD0;
			} ;
			v2f vert (appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv,_MainTex);
				return o;
			}
			float4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex,i.uv);
				return _Color * col; 
			}
			ENDCG
		}
    } 
    FallBack "Diffuse" 
}
