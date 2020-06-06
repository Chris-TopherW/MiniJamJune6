Shader "Unlit/outlineexperiment"
{
    Properties
    {
		_ColorLight("Light Color", Color) = (1,1,1,1)
		_ColorDark("Dark Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(.001, 0.03)) = .005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            uniform float4 _ColorLight;
            uniform float4 _ColorDark;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color.b;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				return i.color > 0.5 ? _ColorLight : _ColorDark;
            }
            ENDCG
        }

		Pass
		{
			Cull Front 

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			uniform float _Outline;
			uniform float4 _ColorDark;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
				float2 offset = TransformViewToProjection(norm.xy);
				
				o.vertex.xy += offset * _Outline * v.color.r;
				o.vertex.z -= v.color.g * v.color.r * 0.00015;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _ColorDark;
			}
			ENDCG
		}

		Pass
		{
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 color : COLOR;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			uniform float _Outline;
			uniform float4 _ColorLight;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
				float2 offset = TransformViewToProjection(norm.xy);

				o.vertex.xy += offset * _Outline * 2.0 * v.color.r;
				o.vertex.z -= v.color.g * v.color.r * 0.001;

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _ColorLight;
			}
			ENDCG
		}
    }
}
