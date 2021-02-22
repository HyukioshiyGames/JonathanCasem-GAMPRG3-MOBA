//////////////////////////////////////////////////////////////////////////
//
//      DO NOT EDIT THIS FILE!! THIS IS AUTOMATICALLY GENERATED!!
//      DO NOT EDIT THIS FILE!! THIS IS AUTOMATICALLY GENERATED!!
//      DO NOT EDIT THIS FILE!! THIS IS AUTOMATICALLY GENERATED!!
//
//////////////////////////////////////////////////////////////////////////

    Shader "Hidden/TerrainTools/NoiseFill/NoiseFillNone"
    {

        Properties
        {
            _MainTex ("Texture", any) = "" {}
        }

        SubShader
        {

            ZTest Always Cull Off ZWrite Off

            HLSLINCLUDE

            #include "UnityCG.cginc"
            #include "Packages/com.unity.terrain-tools/Shaders/TerrainTools.hlsl"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;      // 1/width, 1/height, width, height

            sampler2D _BrushTex;

            float4 _BrushParams;
            #define BRUSH_STRENGTH      (_BrushParams[0])
            #define BRUSH_TARGETHEIGHT  (_BrushParams[1])
            #define BRUSH_PINCHAMOUNT   (_BrushParams[2])
            #define BRUSH_ROTATION      (_BrushParams[3])

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 pcUV : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 pcUV : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.pcUV = v.pcUV;
                return o;
            }

            ENDHLSL

            
            
            Pass // Billow Noise Fill
            {
                Name "Billow Noise Fill"

                HLSLPROGRAM
                
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.terrain-tools/Shaders/NoiseLib/None/Billow.hlsl"

                float4 _TerrainXform;
                float4 _TerrainScale;

                float2 TransformPosition( float2 brushUV )
                {
                    return _TerrainXform.xz + brushUV * _TerrainScale.xz;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float h = UnpackHeightmap( tex2D( _MainTex, i.pcUV ) );
                    
                    float2 pos = TransformPosition( i.pcUV );

                    float n = noise_NoneBillow( pos );
                    
                    return PackHeightmap( n );
                    // return PackHeightmap( lerp( h + n, n, OVERWRITE_HEIGHT ) );
                }

                ENDHLSL
            }

            

            
            Pass // Perlin Noise Fill
            {
                Name "Perlin Noise Fill"

                HLSLPROGRAM
                
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.terrain-tools/Shaders/NoiseLib/None/Perlin.hlsl"

                float4 _TerrainXform;
                float4 _TerrainScale;

                float2 TransformPosition( float2 brushUV )
                {
                    return _TerrainXform.xz + brushUV * _TerrainScale.xz;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float h = UnpackHeightmap( tex2D( _MainTex, i.pcUV ) );
                    
                    float2 pos = TransformPosition( i.pcUV );

                    float n = noise_NonePerlin( pos );
                    
                    return PackHeightmap( n );
                    // return PackHeightmap( lerp( h + n, n, OVERWRITE_HEIGHT ) );
                }

                ENDHLSL
            }

            

            
            Pass // Ridge Noise Fill
            {
                Name "Ridge Noise Fill"

                HLSLPROGRAM
                
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.terrain-tools/Shaders/NoiseLib/None/Ridge.hlsl"

                float4 _TerrainXform;
                float4 _TerrainScale;

                float2 TransformPosition( float2 brushUV )
                {
                    return _TerrainXform.xz + brushUV * _TerrainScale.xz;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float h = UnpackHeightmap( tex2D( _MainTex, i.pcUV ) );
                    
                    float2 pos = TransformPosition( i.pcUV );

                    float n = noise_NoneRidge( pos );
                    
                    return PackHeightmap( n );
                    // return PackHeightmap( lerp( h + n, n, OVERWRITE_HEIGHT ) );
                }

                ENDHLSL
            }

            

            
            Pass // Value Noise Fill
            {
                Name "Value Noise Fill"

                HLSLPROGRAM
                
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.terrain-tools/Shaders/NoiseLib/None/Value.hlsl"

                float4 _TerrainXform;
                float4 _TerrainScale;

                float2 TransformPosition( float2 brushUV )
                {
                    return _TerrainXform.xz + brushUV * _TerrainScale.xz;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float h = UnpackHeightmap( tex2D( _MainTex, i.pcUV ) );
                    
                    float2 pos = TransformPosition( i.pcUV );

                    float n = noise_NoneValue( pos );
                    
                    return PackHeightmap( n );
                    // return PackHeightmap( lerp( h + n, n, OVERWRITE_HEIGHT ) );
                }

                ENDHLSL
            }

            

            
            Pass // Voronoi Noise Fill
            {
                Name "Voronoi Noise Fill"

                HLSLPROGRAM
                
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.terrain-tools/Shaders/NoiseLib/None/Voronoi.hlsl"

                float4 _TerrainXform;
                float4 _TerrainScale;

                float2 TransformPosition( float2 brushUV )
                {
                    return _TerrainXform.xz + brushUV * _TerrainScale.xz;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float h = UnpackHeightmap( tex2D( _MainTex, i.pcUV ) );
                    
                    float2 pos = TransformPosition( i.pcUV );

                    float n = noise_NoneVoronoi( pos );
                    
                    return PackHeightmap( n );
                    // return PackHeightmap( lerp( h + n, n, OVERWRITE_HEIGHT ) );
                }

                ENDHLSL
            }

            

        }

    Fallback Off
}
