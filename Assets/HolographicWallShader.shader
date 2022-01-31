Shader "Unlit/HolographicWallShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius("Radius", float) = 0.0
        _CellSize("Cell Size", float) = 10.0
        _CentreX("CentreX", float) = 0.
        _CentreY("CentreY", float) = 0.
        _Falloff("Falloff", float) = 2.
        _HexColour("Hex Colour", Color) = (1,1,1,1)
        _HexIntensity("Hex Intensity", float) = 1.
        _EdgeColour("Edge Colour", Color) = (1,1,1,1)
        _EdgeIntensity("Edge Intensity", float) = 1.
    }
    SubShader
    {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;
            float _CellSize;
            float _CentreX;
            float _CentreY;
            float _Falloff;
            float4 _EdgeColour;
            float _EdgeIntensity;
            float4 _HexColour;
            float _HexIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

#define mod(x,y) (x-y *floor(x/y))

            float HexDist(float2 p) {
                p = abs(p);


                float c = dot(p, normalize(float2(1, 1.73)));
                c = max(c, p.x);
                return c;
            }

            float CircleDist(float2 uv, float radius, float2 centre) {


                float d = length(centre - uv);
                // Generates a smooth falloff for the circle mask
                float c = smoothstep(radius, radius - 2., d);

                return c;
            }

            float4 HexCoord(float2 uv) {
                // Generates a grid of Hexagons
                float2 r = float2(1.f, 1.73f);
                float2 h = r * 0.5;
                float2 a = fmod(uv, r) - h;
                float2 b = fmod(uv - h, r) - h;

                float2 gv;
                if (length(a) < length(b))
                    gv = a;
                else
                    gv = b;

                float x = atan2(gv.y, gv.x);
                float y = 0.5 - HexDist(gv);
                // Makes an ID for each grid cell
                float2 id = uv - gv;
                return float4(x, y, id.x, id.y);
            }

            float HexColour(float2 pos, float2 maskCentre, float radius, float mask) {
                float4 coord = HexCoord(pos);
                float dist = distance(coord.zw, maskCentre);
                if (dist < radius)
                    // If the distance is less than radius than the mask has passed this cell
                    // I want it to be able to start shrinking at this point so
                    // the size will be the distance to the radius normalised between 0-1 
                    return ((radius - dist) * mask) / 5.;
                else
                    return 0.;
            }

            fixed4 frag(v2f i) : SV_Target
            {
            float radius = _Radius;
            float2 centre = float2(_CentreX, _CentreY);

            // Normalized pixel coordinates (from 0 to 1)
            float2 uv = i.uv;
            // Controls size of grid cells
            uv *= _CellSize;

            // Time varying pixel color
            float4 col = float4(0,0,0,0);



            float4 hc = HexCoord(uv);
            float mask = CircleDist(uv, radius, centre);

            float outer = HexColour(uv, centre, radius, mask);
            float inner = HexColour(uv, centre, radius - _Falloff, mask);
			float edgeMask = HexColour(uv, centre, radius + 5., min(1. - inner, outer));

			float edges = 1. - smoothstep(0.01, 0.05, hc.y);
			edges *= edgeMask;

            edges *= _EdgeColour * _EdgeIntensity;


            //col += smoothstep(0.05, 0.2, hc.y * clamp(
            //min(1. - inner, outer) * 2., 0., 1.5));

            float4 c = min(smoothstep(0.05, 0.2, hc.y * clamp(
                min(1. - inner, outer) * 2., 0., 1.5)) * _HexColour * _HexIntensity, outer);

            

            col = lerp(c, edges, min(1. - inner, outer));
            //col = c;

                
            clip(1. - outer);

            // Output to screen
            return col;

            }
        ENDCG
        }

    }
}




//col += smoothstep(0.05, 0.2, hc.y * clamp(
//min(1. - inner, outer) * 2., 0., 1.5));
