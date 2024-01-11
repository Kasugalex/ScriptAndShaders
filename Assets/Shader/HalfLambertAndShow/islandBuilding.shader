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
            #pragma multi_compile_fwdbase   // ����PassType.ForwardBase��������б��塣���崦��ͬ�Ĺ�����ͼ���ͣ������û��������������Ӱ
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
                float4 vertex : SV_POSITION;
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
                o.vertex = UnityObjectToClipPos(v.vertex);
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

                UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos); //����˥��+������Ӱ��
                return fixed4(diffuse * atten, _Alpha);
			}
			ENDCG
		}
    }
}
