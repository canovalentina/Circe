Shader "SergeyIwanski/GemShader"
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Emission ("Emission", Range(0.0,2.0)) = 0.0
		[NoScaleOffset] _RefractTex ("Refraction Texture", Cube) = "" {}
	}

	SubShader 
	{
		Tags 
		{
			"Queue" = "Transparent"
		}
		
		Pass 
		{
			Cull Front
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
        
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float3 uv : TEXCOORD0;
			};

			v2f vert (float4 v : POSITION, float3 n : NORMAL)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v);

				float3 viewDir = normalize(ObjSpaceViewDir(v));
				o.uv = -reflect(viewDir, n);
				o.uv = mul(unity_ObjectToWorld, float4(o.uv,0));
				return o;
			}

			fixed4 _Color;
			samplerCUBE _RefractTex;
			half _Emission;
			half4 frag (v2f i) : SV_Target
			{
				half3 refraction = texCUBE(_RefractTex, i.uv).rgb * _Color.rgb;
				half4 reflection = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, i.uv);
				reflection.rgb = DecodeHDR (reflection, unity_SpecCube0_HDR);
				half3 multiplier = reflection.rgb + _Emission;
				return half4(refraction.rgb * multiplier.rgb, 1.0f);
			}
			ENDCG 
		}

		Pass 
		{
			ZWrite On
			Blend One One
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
        
			struct v2f 
			{
				float4 pos : SV_POSITION;
				float3 uv : TEXCOORD0;
				half fresnel : TEXCOORD1;
			};

			v2f vert (float4 v : POSITION, float3 n : NORMAL)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v);

				float3 viewDir = normalize(ObjSpaceViewDir(v));
				o.uv = -reflect(viewDir, n);
				o.uv = mul(unity_ObjectToWorld, float4(o.uv,0));
				o.fresnel = 1.0 - saturate(dot(n,viewDir));
				return o;
			}

			fixed4 _Color;
			samplerCUBE _RefractTex;
			half _Emission;
			half4 frag (v2f i) : SV_Target
			{
				half3 refraction = texCUBE(_RefractTex, i.uv).rgb * _Color.rgb;
				half4 reflection = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, i.uv);
				reflection.rgb = DecodeHDR (reflection, unity_SpecCube0_HDR);
				half3 reflection2 = reflection * i.fresnel;
				half3 multiplier = reflection.rgb + _Emission;
				return fixed4(reflection2 + refraction.rgb * multiplier, 1.0f);
			}
			ENDCG
		}

        UsePass "VertexLit/SHADOWCASTER"
	}
}
