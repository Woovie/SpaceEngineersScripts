public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update1;
}
void Main(string argument)
{
    string ERR_TXT = "";
    string[] variables = Me.CustomData.Split();
    string[] groups = variables[0].Split(variables[0][0]);
    int timeScale = Int32.Parse(variables[1]);
    if (groups.Count() == 0) {
        ERR_TXT += "Groups total is 0, please check your Custom Data values.\n";
    }
    if (timeScale > 255 | timeScale < 1) {
        ERR_TXT += "Time scale is too low, please increase or decrease your time scale to be between 1 and 255, integers only.\n";
    }
    foreach (string group in groups) {
        if (group.Length > 0) {
            List<IMyTerminalBlock> lightBlocks = new List<IMyTerminalBlock>();
            if(GridTerminalSystem.GetBlockGroupWithName(group) != null) {
                GridTerminalSystem.GetBlockGroupWithName(group).GetBlocksOfType<IMyLightingBlock>(lightBlocks, filterThis);
                            if (lightBlocks.Count == 0) {
                ERR_TXT += "No lighting blocks found in group "+group+"\n";
                } else if (lightBlocks.Count > 0) {//Actual lighting loop
                    Color lightColor = lightBlocks[0].GetValueColor("Color");
                    int r = lightColor.R;
                    int g = lightColor.G;
                    int b = lightColor.B;
                    if(r == 255 & g >= 0 & g < 255 & b == 0){
                        g=g+timeScale;
                    }
                    if(r <= 255 & r > 0 & g == 255 & b == 0){
                        r=r-timeScale;
                    }
                    if(r == 0 & g == 255 & b >= 0 & b < 255){
                        b=b+timeScale;
                    }
                    if(r == 0 & g <= 255 & g > 0 & b == 255){
                        g=g-timeScale;
                    }
                    if(r >= 0 & r < 255 & g == 0 & b == 255){
                        r=r+timeScale;
                    }
                    if(r == 255 & g == 0 & b <= 255 & b > 0){
                        b=b-timeScale;
                    }
                    for(int i = 0; i < lightBlocks.Count; i++) {
                        lightBlocks[i].SetValueColor("Color", new Color(r, g, b, 255));
                    }
                }
            } else {
                ERR_TXT += "Group "+group+" not found.\n";
            }
        }
    }
    if(ERR_TXT != "") {
        Echo("Script Errors:\n"+ERR_TXT+"\n(make sure block ownership is set correctly)");
        return;
    }
}

bool filterThis(IMyTerminalBlock block) {
    return block.CubeGrid == Me.CubeGrid;
}
