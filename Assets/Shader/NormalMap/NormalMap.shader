
Shader "Unlit/NormalMap"
{
	//属性  
    Properties{  
        _Diffuse("Diffuse", Color) = (1,1,1,1)  
        _MainTex("Base 2D", 2D) = "white"{}  
        _BumpMap("Bump Map", 2D) = "bump"{}  
    }  
  
    //子着色器    
    SubShader  
    {  
        Pass  
        {  
            //定义Tags  
            Tags{ "RenderType" = "Opaque" }  
  
            CGPROGRAM  
			#pragma vertex vert  
            #pragma fragment frag    
            #include "Lighting.cginc"  

            fixed4 _Diffuse;  
            sampler2D _MainTex;  
            float4 _MainTex_ST;  
            sampler2D _BumpMap;  
            struct v2f  
            {  
                float4 pos : SV_POSITION;  
                float2 uv : TEXCOORD0;  
                float3 lightDir : TEXCOORD1;  
            };  
  
            v2f vert(appdata_tan v)  
            {  
                v2f o;  
                o.pos = UnityObjectToClipPos(v.vertex);  
                //模型空间到切线空间的转换矩阵rotation
                TANGENT_SPACE_ROTATION;  
                //光线方向转到模型空间，通过rotation再转到切线空间  
                o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex));  
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);  
                return o;  
            }  
  
            
            fixed4 frag(v2f i) : SV_Target  
            {  
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * _Diffuse.xyz;  
               
                float3 tangentNormal = UnpackNormal(tex2D(_BumpMap , i.uv));  
               
                float3 tangentLight = normalize(i.lightDir);  
               
                fixed3 lambert = 0.5 * dot(tangentNormal, tangentLight) + 0.5;  
               
                fixed3 diffuse = lambert * _Diffuse.xyz * _LightColor0.xyz + ambient;  
                fixed4 color = tex2D(_MainTex, i.uv);  
                return fixed4(diffuse * color.rgb, 1.0);  
            }  
  
            ENDCG  
        }  
  
    }  
}
