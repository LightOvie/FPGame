Shader "Custom/FogShader"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.2, 0.2, 0.2, 1)
        _FogDensity ("Fog Density", Range(0, 1)) = 0.05
        _FogStart ("Fog Start", Float) = 10
        _FogEnd ("Fog End", Float) = 50
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            float4 _FogColor;
            float _FogDensity;
            float _FogStart;
            float _FogEnd;

           v2f vert (appdata_t v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
    o.uv = v.uv;  
    return o;
}

            half4 frag (v2f i) : SV_Target
            {
                float distance = length(i.worldPos - _WorldSpaceCameraPos);
                float fogFactor = saturate((distance - _FogStart) / (_FogEnd - _FogStart));
                fogFactor = 1.0 - exp(-_FogDensity * distance);
                return lerp(half4(0, 0, 0, 0), _FogColor, fogFactor);
            }
            ENDCG
        }
    }
}
