// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Kasug/Sprites/Default"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
		_NoiseTex("Noise",2D) = "white"{}
		_Factor("Factor",float) = 0.1
		_SwingRange("Swing Range",Range(0,0.2)) = 0.1
		_SwingHeight("Height",Range(0,1)) = 0.1
		_Frequency("Frequency",Range(0,100)) = 10
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off


		Pass
		{
			Blend srcalpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _NoiseTex;
			float _Factor;
			fixed _SwingRange;
			sampler2D _MainTex;
			fixed _SwingHeight;
			half _Frequency;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				
				float offset = sin(3.1416 * _Time.y  * _Frequency * clamp(v.uv.y - _SwingHeight, 0, 1))  * _SwingRange;		
				o.vertex.x +=  offset;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed4 noise = tex2D(_NoiseTex,fixed2(i.uv.x + _Time.x,i.uv.y));
				fixed4 col = tex2D(_MainTex, i.uv + noise.xy * _Factor);
				return col;
			}
		ENDCG
		}
	}
}
