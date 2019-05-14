Shader "My Pipeline/Unlit"
{
    Properties
    {

    }
    SubShader
    {

        Pass
        {
            HLSLPROGRAM
			#include "Unlit.hlsl"	
			#pragma vertex UnlitPassVertex
			#pragma fragment UnlitPassFragment


			ENDHLSL
        }
    }
}
