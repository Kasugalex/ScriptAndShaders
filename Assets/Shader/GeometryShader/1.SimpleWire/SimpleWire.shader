Shader "Unlit/SimpleWire"
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
            //-------声明几何着色器 
            #pragma geometry geom 
            #pragma fragment frag 
      			 
            #include "UnityCG.cginc" 
 
            struct appdata 
            { 
                float4 vertex : POSITION; 
                float2 uv : TEXCOORD0; 
            }; 
 
            //-------顶点向几何阶段传递数据 
            struct v2g{ 
                float4 vertex:SV_POSITION; 
                float2 uv:TEXCOORD0; 
            }; 
 
            //-------几何阶段向片元阶段传递数据 
            struct g2f 
            { 
                float2 uv : TEXCOORD0; 
                float4 vertex : SV_POSITION; 
            }; 
 
            sampler2D _MainTex; 
            float4 _MainTex_ST; 
             
            v2g vert (appdata v) 
            { 
                v2g o; 
                o.vertex = UnityObjectToClipPos(v.vertex); 
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); 
                return o; 
            } 
 
            //-------静态制定单个调用的最大顶点个数 
            [maxvertexcount(3)] 
            //使用三角面作为输入，线段作为输出我们得到完整的线框 
            void geom(triangle v2g input[3],inout TriangleStream<g2f> triangleStream){ 

                g2f o=(g2f)0; 

                o.vertex=input[0].vertex; 
                o.uv=input[0].uv;           
                triangleStream.Append(o); 
 
                o.vertex=input[1].vertex; 
                o.uv=input[1].uv;                 
                triangleStream.Append(o); 
 
                o.vertex=input[2].vertex; 
                o.uv=input[2].uv; 
                triangleStream.Append(o); 
 
            } 
             
            fixed4 frag (g2f i) : SV_Target 
            { 
                // sample the texture 
                fixed4 col = tex2D(_MainTex, i.uv); 
                return col; 
            } 
            ENDCG 
        } 
    } 
}
