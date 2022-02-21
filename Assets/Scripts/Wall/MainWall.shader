Shader "Custom/MainWall"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _global("Global Mask", 2D) = "black"{}
        _numOfBands("Number Of Bands", int) = 3
        _falloff("Falloff", Range(0,1)) = 0.01



    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        //sampler2D _nf;
        //sampler2D _st;
        sampler2D _global;


		


        struct Input
        {
            float2 uv_MainTex;
            //float2 uv_nf;
            //float2 uv_st;
            float2 uv_global;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        int _numOfBands;
        float _falloff;

        float4 determineColour(Input IN) {
            float4 c = lerp(0, 1, tex2D(_global, IN.uv_global));


        }
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)



        void surf (Input IN, inout SurfaceOutputStandard o)
        {


            //half amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0)).r;
            //fixed4 c = lerp(tex2D(_GroundTex, IN.uv_GroundTex) * _GroundColor,
            //    tex2D(_SoilTex, IN.uv_SoilTex) * _Color, amount);

            //fixed4 c = lerp(tex2D(_nf, IN.uv_nf), tex2D(_st, IN.uv_st), tex2D(_global, IN.uv_global));
            fixed4 c = lerp(0, 1, tex2D(_global, IN.uv_global));

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
