#ifndef MYRP_UNLIT_INCLUDED
#define MYRP_UNLIT_INCLUDED

float4x4 unity_MatrixVP;
float4x4 unity_ObjectToWorld;

struct VertexInput {
	float4 pos : POSITION;
};

struct VertexOutput {
	float4 clipPos :SV_POSITION;
};


VertexOutput UnlitPassVertex(VertexInput input) {
	VertexOutput output;
	float4 worldPos = mul(unity_ObjectToWorld,input.pos);
	output.clipPos = mul(unity_MatrixVP, worldPos);
	return output;
}

float4 UnlitPassFragment(VertexOutput input) : SV_Target{
	return 1;
}





#endif

