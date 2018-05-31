public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update1;
    List<IMyTerminalBlock> v0 = new List<IMyTerminalBlock>();
    for(int i = 0; i < v0.Count; i++) {
        v0[i].SetValueColor("Color", new Color(255,0,0, 255));
    }
}
void Main(string argument)
{
    string ERR_TXT = "";
    List<IMyTerminalBlock> v0 = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>(v0, filterThis);
    if(v0.Count == 0) {
        ERR_TXT += "no Lighting Block blocks found\n";
    }
    if(ERR_TXT != "") {
        Echo("Script Errors:\n"+ERR_TXT+"(make sure block ownership is set correctly)");
        return;
    } else {
        Echo("");
    }
    if (v0.Count > 0) {
        Color lightColor = v0[0].GetValueColor("Color");
        int r = lightColor.R;
        int g = lightColor.G;
        int b = lightColor.B;
        int adjustBy = 8;
        if(r == 255 & g >= 0 & g < 255 & b == 0){
            g=g+adjustBy;
        }
        if(r <= 255 & r > 0 & g == 255 & b == 0){
            r=r-adjustBy;
        }
        if(r == 0 & g == 255 & b >= 0 & b < 255){
            b=b+adjustBy;
        }
        if(r == 0 & g <= 255 & g > 0 & b == 255){
            g=g-adjustBy;
        }
        if(r >= 0 & r < 255 & g == 0 & b == 255){
            r=r+adjustBy;
        }
        if(r == 255 & g == 0 & b <= 255 & b > 0){
            b=b-adjustBy;
        }
        for(int i = 0; i < v0.Count; i++) {
            v0[i].SetValueColor("Color", new Color(r, g, b, 255));
        }
    }
}
bool filterThis(IMyTerminalBlock block) {
    return block.CubeGrid == Me.CubeGrid;
}
