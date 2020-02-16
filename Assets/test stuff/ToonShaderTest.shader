Shader "FurFighter/ToonShaderTest"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", Float) = 32
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
		_DepthMap("Depth Map", 2D) = "white" {}
	}
		SubShader
	{
		Pass
		{
		Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float3 tangent : TANGENT;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
				float3 tangentViewDir : TEXCOORD3;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _AmbientColor;
			float _Glossiness;
			float4 _SpecularColor;
			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;
			sampler2D _DepthMap;
			float height_scale;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				TRANSFER_SHADOW(o)

					float3 worldVertexPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					float3 worldViewDir = worldVertexPos - _WorldSpaceCameraPos;

					float3 worldNormal = UnityObjectToWorldNormal(v.normal);
					float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
					float3 worldBitangent = cross(worldNormal, worldTangent) * v.tangent * unity_WorldTransformParams.w;

					//Use dot products instead of building the matrix
					o.tangentViewDir = float3(
						dot(worldViewDir, worldTangent),
						dot(worldViewDir, worldNormal),
						dot(worldViewDir, worldBitangent)
						);
				return o;
			}

			float4 _Color;

			float2 ParallaxMapping(float2 texCoords, float3 viewDir)
			{
				float height = tex2D(_DepthMap, texCoords).r;
				float2 p = viewDir.xy / viewDir.z * (height * height_scale);
				return texCoords - p;
			}

			float4 frag(v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0, 0.01, NdotL* shadow);
				float4 light = lightIntensity * _LightColor0;



				//float3 viewDir = normalize(fs_in.TangentViewPos - fs_in.TangentFragPos);
				//float3 viewDir = normalize(fs_in.TangentViewPos - fs_in.TangentFragPos);
				//float2 texCoords = ParallaxMapping(i.uv, viewDir);
			float2 texCoords = ParallaxMapping(i.uv, i.tangentViewDir);
				float4 sample = tex2D(_MainTex, texCoords);
				float3 viewDir = normalize(i.viewDir);

				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);

				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;

				float4 rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				return _Color * sample * (_AmbientColor + light + specular + rim);
			}
			ENDCG
		}

			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"

	}
}
