Shader "Custom/Flat Wireframe" {

	Properties {
		_Color ("Tint", Color) = (1, 1, 1, 1)
		_WireframeColor ("Wireframe Color", Color) = (0, 0, 0)
		_WireframeSmoothing ("Wireframe Smoothing", Range(0, 10)) = 1
		_WireframeThickness ("Wireframe Thickness", Range(0, 10)) = 1
	}

	CGINCLUDE

	ENDCG

	SubShader {

		Pass {
			Tags {
				"LightMode" = "Always"
			}

			CGPROGRAM

			#pragma target 4.0

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram
			#pragma geometry MyGeometryProgram

			#include "MyFlatWireframe.cginc"

			ENDCG
		}


	}

	CustomEditor "MyLightingShaderGUI"
}