#ifndef MYRP_UNLIT_INCLUDED
#define MYRP_UNLIT_INCLUDED

/*cbuffer UnityPerFrame {
	float4x4 unity_MatrixVP;
};
cbuffer UnityPerDraw {
	float4x4 unity_ObjectToWorld;
};*/

//use Common.hlsl to make CBUFFER_START correct
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
//cbuffer don't benefit all platforms,so I use macros(宏)
CBUFFER_START(UnityPerFrame)
float4x4 unity_MatrixVP;
CBUFFER_END

CBUFFER_START(UnityPerDraw)
float4x4 unity_ObjectToWorld;
CBUFFER_END
/* normal Color
CBUFFER_START(UnityPerMaterial)
float4 _Color;
CBUFFER_END
*/

#define UNITY_MATRIX_M unity_ObjectToWorld

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
//Instance Color
UNITY_INSTANCING_BUFFER_START(PerInstance)
UNITY_DEFINE_INSTANCED_PROP(float4,_Color)
UNITY_INSTANCING_BUFFER_END(PerInstance)


struct VertexInput {
	float4 pos : POSITION;
	//UNITY_MATRIX_M relies on the index,so add it to the VertexInput.
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct VertexOutput {
	float4 clipPos :SV_POSITION;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};


VertexOutput UnlitPassVertex(VertexInput input) {
	VertexOutput output;
	//should make the index avaliable before using UNITY_MATRIX_M
	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_TRANSFER_INSTANCE_ID(input,output);
	float4 worldPos = mul(UNITY_MATRIX_M, float4(input.pos.xyz, 1.0));
	output.clipPos = mul(unity_MatrixVP, worldPos);
	return output;
}

float4 UnlitPassFragment(VertexOutput input) : SV_Target{
	UNITY_SETUP_INSTANCE_ID(input);
	return UNITY_ACCESS_INSTANCED_PROP(PerInstance, _Color);
}





#endif

