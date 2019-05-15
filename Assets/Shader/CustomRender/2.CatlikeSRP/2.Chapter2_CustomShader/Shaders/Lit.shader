Shader "Kasug/My Pipeline/Lit"
{
    Properties
    {
		_Color("Color",Color) = (1,1,1,1)
    }
    SubShader
    {

        Pass
        {
            HLSLPROGRAM
			//because Unlit.hlsl use the Common.hlsl,this shader fails to compile for OpenGL ES2 
			#pragma target 3.5	
			//GPU Instancing is enabled but pipeline doesn't mean that objects are automatically instanced.So add this directive
			#pragma multi_compile_instancing
			//support non-uniform scaling
			#pragma instancing_options assumeuniformscaling
			#include "../ShaderLibrary/Lit.hlsl"	
			#pragma vertex LitPassVertex
			#pragma fragment LitPassFragment


			ENDHLSL
        }
    }
}
