Shader "Kasug/Advanced Rendering/FlatWireframe"
{
    Properties
    {
    
    }
    SubShader
    {
        
        void InitializeFragmentNormal(inout Interpolators i){
            // partial derivative of the world position
            float3 dpdx = ddx(i.worldPos);
            float3 dpdy = ddy(i.worldPos);
            i.normal = normalize(cross(dpdx,dpdy));

            
        }
    }
}
