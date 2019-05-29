Shader "Kasug/Advanced Rendering/My First Lighting Shader"
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

            struct VertexData
            {
                float4 vertex : POSITION;
                float3 normal:NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal:TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Interpolators vert (VertexData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = mul(transpose(unity_ObjectToWorld), float4(v.normal,0));
                o.normal = normalize(o.normal);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                // sample the texture
                fixed4 col = float4(i.normal * 0.5 + 0.5,1);
                return col;
            }
            ENDCG
        }
    }
}
