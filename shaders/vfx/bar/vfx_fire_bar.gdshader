shader_type canvas_item;
render_mode blend_add;   // blend_add

uniform sampler2D noise_tex : repeat_enable;
uniform sampler2D gradient_tex;
uniform float intensity = 0.6;
uniform float speed = 0.5;

void fragment() {
    vec2 uv = SCREEN_UV;
    
    uv.y += TIME * speed;
    
    float scrollOffset1 = texture(noise_tex, uv * 0.1 + TIME * 0.0005 + 0.24).r;
    float col1 = texture(noise_tex, uv + TIME * 0.0005 + scrollOffset1).r;
    float scrollOffset2 = texture(noise_tex, uv * 0.1 + TIME * 0.0005 + 0.24).r;
    float col2 = texture(noise_tex, uv + TIME * -0.0007 + scrollOffset2).r;
    
    float final_val = col1 * 0.75 + col2 * 0.35;
    final_val = smoothstep(0.4, 0.6, final_val);
    
    vec3 flame_color = texture(gradient_tex, vec2(final_val, 0.0)).rgb;
    
    float alpha = final_val * intensity;
    COLOR = vec4(flame_color, alpha);
}