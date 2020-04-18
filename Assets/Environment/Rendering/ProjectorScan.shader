// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'

Shader "Projector/Light" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_PaternTex ("Patern", 2D) = "" {}
		_ShapeTex ("Shape", 2D) = "" {}
		_PaternTiling("Patern Tiling", Float) = 1
		_Reveal("Reveal", Range(0,1)) = 1
	}
	
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend DstColor One
			Offset -1, -1
	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				UNITY_FOG_COORDS(2)
				float4 pos : SV_POSITION;
			};
			
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul (unity_Projector, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			fixed4 _Color;
			sampler2D _PaternTex;
			sampler2D _ShapeTex;
			half _PaternTiling;
			uniform half _Reveal;
			
			fixed4 frag (v2f i) : SV_Target
			{
				clip(i.uvShadow.y -1 + _Reveal);
				
				fixed4 patern = tex2D (_PaternTex, i.uvShadow * _PaternTiling);
				patern.rgb *= _Color.rgb;
				patern.a = 1.0-patern.a;
				
				float shape = tex2Dproj (_ShapeTex, UNITY_PROJ_COORD(i.uvShadow)).r * 10;

				fixed4 res = saturate(patern * shape);
				res.r *=-1;
				res.gb*=3;

				UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(0,0,0,0));
				return res;
			}
			ENDCG
		}
	}
}
