Shader "Comic/CBB/HalfLambert"
{
    Properties
    {
        _MainTex("Albedo", 2D) = "white" {}
        _Color("Color",Color) =(1,1,1,1)
	    _Alpha("Alpha", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" "RenderType"="Transparent" "LightMode"="ForwardBase" }
	    LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
            #pragma multi_compile_fwdbase   // 编译PassType.ForwardBase所需的所有变体。变体处理不同的光照贴图类型，并启用或禁用主方向光的阴影
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION; 
                float4 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION; //用TRANSFER_SHADOW方法，v2f的顶点位置变量名一定要是pos，如果用了其他名字，会报错：invalid subscript ‘pos’ ‘ComputeScreenPos’: no matching 1 parameter function (on d3d11)
                fixed3 worldNormal : TEXCOORD0; 
                float2 uv : TEXCOORD1;
                float3 worldPos: TEXCOORD2;
                SHADOW_COORDS(3)
            };

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			fixed _Alpha;
			v2f vert (appdata v)
			{
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                TRANSFER_SHADOW(o);
                return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                fixed3 texColor = tex2D(_MainTex, i.uv) * _Color.rgb;

                //fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 diffuse = texColor * (dot(worldLight, worldNormal) * 0.5 + 0.5);

                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos); //光照衰减+接收阴影宏
                return fixed4(diffuse * atten, _Alpha);
			}
			ENDCG
		}
    }
}
