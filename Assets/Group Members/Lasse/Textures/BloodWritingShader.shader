Shader "Custom/BloodWritingShader"
{
    Properties
    {
        _MainTex ("Blood Writing Texture", 2D) = "white" {}
        _RevealTex ("Reveal Mask", 2D) = "white" {}
        _RevealProgress ("Reveal Progress", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _RevealTex;
            float _RevealProgress;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                fixed4 revealMask = tex2D(_RevealTex, i.uv);

                // Check if the reveal mask allows this pixel to show
                if (revealMask.r < _RevealProgress)
                    mainColor.a = 0; // Hide this pixel

                return mainColor;
            }
            ENDCG
        }
    }
}
