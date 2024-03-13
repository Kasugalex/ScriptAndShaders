Shader "Comic/ComicStandardSprite" 
{
    Properties 
    {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        
        //Surface Option
        [Header(Surface Option)][Space(5)]
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull Mode", Float) = 2
        
        [Space(5)]
        _Stencil ("Stencil ID", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)]_StencilComp ("Stencil Comparison", Float) = 8
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilOp ("Stencil Operation", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilOpFail ("Stencil Fail Operation", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
        
        //溶解
        [Header(Dissolve)][Space(5)]
        [Toggle(_DISSOLVE_ON)]_UseDissolve("Use Dissolve ?", Float) = 0
        [KeywordEnum(LegacyBurn, Dissolve)]_DissoleType("Dissolve Mode", Float) = 0

        [HideDisabled(_DISSOLVE_ON)][NoScaleOffset]_SliceGuide("Dissolve NoiseMap", 2D) = "bump" {} //A=溶解值
        [HideDisabled(_DISSOLVE_ON)]_SliceAmount("Dissolve Level", Range(0, 1)) = 0 //溶解程度
        [HideDisabled(_DISSOLVE_ON)][NoScaleOffset]_BurnRamp("Burn Edge RampMap", 2D) = "white" {} //软溶解边缘过渡
        [HideDisabled(_DISSOLVE_ON)]_BurnSize("Burn Edge Size", Float) = 0.15
        //  LegacyBurn模式
        [HideDisabled(_DISSOLVE_ON, _DISSOLETYPE_LEGACYBURN)]_BurnColor("Burn Edge Color", Color) = (1,1,1,1)
        [HideDisabled(_DISSOLVE_ON, _DISSOLETYPE_LEGACYBURN)]_BurnEmission("Burn Edge Emission", Float) = 1
        //  Dissolve模式
        [HideDisabled(_DISSOLVE_ON, _DISSOLETYPE_DISSOLVE)]_EdgeAroundPower("Edge Power",Range(1,5)) = 1
        [HideDisabled(_DISSOLVE_ON, _DISSOLETYPE_DISSOLVE)]_EdgeAroundHDR("Edge Color HDR",Range(1,5)) = 1
        
        [HideDisabled(_DISSOLVE_ON, _DISSOLETYPE_DISSOLVE)]_EdgeDistortion("Edge Distortion",Range(0,1)) = 0 //扭曲

        //扫光
        [Header(Flow Lighting)][Space(5)]
        [Toggle(_FLOW_LIGHTING_ON)]_UseFlowLighting("加流光 ?", Float) = 0
        [HideDisabled(_FLOW_LIGHTING_ON)]_FlowLightingMap("Flow Lighting Map", 2D) = "white" {}
        [HideDisabled(_FLOW_LIGHTING_ON)]_FlowLightingColor("Flow Lighting Color", Color) = (1,1,1,1)
        [HideDisabled(_FLOW_LIGHTING_ON)]_FlowLightingSpeedX("Flow Lighting Speed X", Float) = 0
        [HideDisabled(_FLOW_LIGHTING_ON)]_FlowLightingSpeedY("Flow Lighting Speed Y", Float) = 0
        
        //置灰
        [Header(Gray)][Space(5)]
        [Toggle(_GRAY_ON)] _EnableGray("Use Gray ?", Float) = 0
        [HideDisabled(_GRAY_ON)]_Grayness("Grayness", Range(0, 1)) = 0
        
        //描边
        [Header(Outline)][Space(5)]
        [Toggle(_OUTLINE_ON)] _EnableOutline("Use Outline ? (目前不兼容溶解)", Float) = 0
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth("Outline Width", Float) = 1
    }
    
    SubShader 
    {
        Tags 
        {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass 
        {
            Name "FORWARD"
            Tags 
            {
                "LightMode"="ForwardBase"
            }
            
            //Blend One OneMinusSrcAlpha
            Blend SrcAlpha OneMinusSrcAlpha
            Cull [_Cull]
            ZWrite Off
            
            Stencil {
                Ref [_Stencil]
                ReadMask [_StencilReadMask]
                //WriteMask [_StencilWriteMask]
                Comp [_StencilComp]
                Pass [_StencilOp]
                Fail [_StencilOpFail]
                ZFail [_StencilOpZFail]
            }
            
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma shader_feature_local_fragment _ _DISSOLVE_ON
            #pragma shader_feature_local_fragment _DISSOLETYPE_LEGACYBURN _DISSOLETYPE_DISSOLVE
            #pragma shader_feature_local_fragment _ _GRAY_ON
            #pragma shader_feature_local_fragment _ _FLOW_LIGHTING_ON
            #pragma shader_feature_local_fragment _ _OUTLINE_ON
            
            #include "UnityCG.cginc"

            uniform sampler2D _MainTex; 
            uniform float4 _MainTex_ST;
            uniform float4 _MainTex_TexelSize;
            uniform float4 _Color;
            
            uniform float _Grayness;

            #ifdef _DISSOLVE_ON
            sampler2D _SliceGuide;
            float4 _SliceGuide_ST;
            float _SliceAmount;
            sampler2D _BurnRamp;
            float _BurnSize;
            
            float4 _BurnColor;
            float _BurnEmission;

            float _EdgeAroundHDR;
            float _EdgeAroundPower;
            half _EdgeDistortion;
            #endif

            #ifdef _FLOW_LIGHTING_ON
            sampler2D _FlowLightingMap;
            half4 _FlowLightingMap_ST;
            half4 _FlowLightingColor;
            half _FlowLightingSpeedX;
            half _FlowLightingSpeedY;
            #endif
            
            #ifdef _OUTLINE_ON
            half4 _OutlineColor;
            half _OutlineWidth;
            #endif
            
            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            
            struct VertexOutput 
            {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            
            VertexOutput vert (VertexInput v) 
            {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                return o;
            }

            half4 frag(VertexOutput i, float facing : VFACE) : COLOR 
            {
               float2 uv = TRANSFORM_TEX(i.uv0, _MainTex);
                half4 finalColor = half4(0,0,0,0);
                
                //溶解
#ifdef _DISSOLVE_ON
    #if _DISSOLETYPE_DISSOLVE
                //  Dissolve模式（边缘用smoothstep做渐变）
                float2 sliceUV = TRANSFORM_TEX(i.uv0, _SliceGuide);
                half dissolveValue = tex2D(_SliceGuide, sliceUV);

                //  边缘计算
                half tempDissolve = lerp(dissolveValue+_BurnSize, dissolveValue-_BurnSize, _SliceAmount);//对edge作修正，为了在0,1值时alpha分别为0,1
                dissolveValue = smoothstep(_SliceAmount-_BurnSize, _SliceAmount+_BurnSize, tempDissolve);//dissolveValue限制在[dissolveLevel-EdgeSize, dissolveLevel+EdgeSize]
                // return alpha.xxxx;
                
                //  扭曲（alpha越大 扭曲越大）
                half avoid = 0.15f;
                half distort = dissolveValue * dissolveValue * avoid;
                uv = lerp(uv, uv + distort - avoid, float(_SliceAmount * _EdgeDistortion));

                half4 mainColor = tex2D(_MainTex, uv);
                
                half rampX = pow(1 - dissolveValue, _EdgeAroundPower);
                half3 edgeColor = tex2D(_BurnRamp, half2(rampX, 0)).rgb;
                edgeColor = (mainColor.rgb + edgeColor) * edgeColor * _EdgeAroundHDR;//边缘颜色
                
                mainColor.rgb = lerp(edgeColor, mainColor.rgb, dissolveValue);//rgb
                
                half premultiAlpha = smoothstep(_SliceAmount-_BurnSize*1.2, _SliceAmount-_BurnSize, tempDissolve);
                premultiAlpha *= mainColor.a * i.vertexColor.a * _Color.a;
                mainColor.rgb *= premultiAlpha;
                mainColor.rgb *= i.vertexColor.rgb * _Color.rgb;

                mainColor.a *= dissolveValue;//alpha
                mainColor.a *= i.vertexColor.a * _Color.a;

                finalColor = mainColor;
    #elif _DISSOLETYPE_LEGACYBURN
                //  Legacy Burn模式
                half4 mainColor = tex2D(_MainTex, i.uv0);
                half4 dissolveNoiseMap = tex2D(_SliceGuide, i.uv0);
                float dissolveValue = dissolveNoiseMap.r - _SliceAmount;//减一个数
                // fixed dissolveValue01 = saturate(sign(0.0001+dissolveValue));
                half dissolveValue01 = saturate(sign(dissolveValue));//正数1 负数-1 =》0 (不需要加0.0001 否则会溶解不干净）
                
                half2 node_7280 = float2(dissolveValue/_BurnSize, 0.0);//除一个数
                half4 _BurnEdgeRampMap = tex2D(_BurnRamp, node_7280);
                
                finalColor.a = mainColor.a * _Color.a * i.vertexColor.a * dissolveValue01;//alpha
                finalColor.rgb = mainColor.rgb * _Color.rgb * i.vertexColor.rgb;//rgb
                //  计算溶解边缘色
                half dissolveEdge = dissolveValue01 * saturate(sign(dissolveValue - _BurnSize*saturate(_SliceAmount*10.0))*-1.0);
                half3 dissolveEdgeColor = dissolveEdge * _BurnEdgeRampMap.rgb * _BurnColor.rgb * _BurnEmission;
                
                finalColor.rgb += dissolveEdgeColor;
                //finalColor.rgb *= finalColor.a;
    #endif
#else
                half4 mainColor = tex2D(_MainTex, uv);
                finalColor = mainColor;
                
                #ifdef _OUTLINE_ON
                //内描边
                half2 up_uv = uv + float2(0,1) * _OutlineWidth * _MainTex_TexelSize.xy;
                half2 down_uv = uv + float2(0,-1) * _OutlineWidth * _MainTex_TexelSize.xy;
                half2 left_uv = uv + float2(-1,0) * _OutlineWidth * _MainTex_TexelSize.xy;
                half2 right_uv = uv + float2(1,0) * _OutlineWidth * _MainTex_TexelSize.xy;
                half w = tex2D(_MainTex, up_uv).a * tex2D(_MainTex, down_uv).a
                            * tex2D(_MainTex, left_uv).a * tex2D(_MainTex, right_uv).a;
                
                //mainColor.rgb = w == 0 ? _OutlineColor.rgb : mainColor.rgb;//锯齿较严重
                finalColor.rgb = lerp(_OutlineColor.rgb, mainColor.rgb, w);
                #endif
#endif
                
#if !(_DISSOLVE_ON && _DISSOLETYPE_LEGACYBURN)
                finalColor *= i.vertexColor * _Color;
#endif

                //扫光
                #ifdef _FLOW_LIGHTING_ON
                half2 uv_flowLighting = TRANSFORM_TEX(uv, _FlowLightingMap);
                uv_flowLighting = half2(uv_flowLighting.x + _Time.y*_FlowLightingSpeedX,
                                        uv_flowLighting.y + _Time.y*_FlowLightingSpeedY);
                uv_flowLighting = frac(uv_flowLighting);

                half4 flowLightingColor = tex2D(_FlowLightingMap, uv_flowLighting);
                finalColor.rgb += flowLightingColor.rgb * _FlowLightingColor.rgb * _FlowLightingColor.a;
                #endif
               
                //置灰
                #if _GRAY_ON
                fixed3 grayColor = dot(finalColor.rbg, float3(0.299, 0.587, 0.114));
                finalColor.rgb = lerp(finalColor.rgb, grayColor, _Grayness);
                #endif

//#if !(_DISSOLVE_ON && _DISSOLETYPE_LEGACYBURN)
                //Blend One OneMinusSrcAlpha, 所以需要预乘alpha
                //finalColor.rgb *= finalColor.a;
//#endif
                return finalColor;
            }

            
            ENDCG
        }
    }
    FallBack "Custom/FallBackDefault"
}
