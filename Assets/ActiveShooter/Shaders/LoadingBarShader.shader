Shader "Custom/LoadingBarShader"
{
    Properties
    {
        _MainTex("Loading Bar Texture", 2D) = "white" {}
        _Progress("Progress", Range(0, 1)) = 0
    }

        SubShader
        {
            Tags { "Queue" = "Transparent" }
            Pass
            {
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
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _Progress;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, i.uv);

                // Calculate a grayscale intensity
                float grayIntensity = dot(texColor.rgb, float3(0.299, 0.587, 0.114));

                // Define a threshold for black pixels (adjust as needed)
                float blackThreshold = 0.1; // Adjust this value based on your texture

                // Calculate the final alpha value based on the loading progress and black pixel threshold
                float alpha = (grayIntensity < blackThreshold) ? 0 : 1 - step(i.uv.y, _Progress);

                // Combine the original texture color with calculated alpha
                return texColor * alpha;
            }
            ENDCG
        }
        }
}
