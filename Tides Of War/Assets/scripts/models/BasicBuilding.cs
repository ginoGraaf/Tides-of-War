
    public abstract partial class BuildingModel
    {
        public class BasicBuilding : BuildingModel
        {
            public enum Use { NUTRAL,RED,BLUE,GREEN,YELLOW}
            public Use useState = 0;
            public override BuildingModel BuildProcess(BuildingModel model)
            {
                BasicBuilding bb= new BasicBuilding {ObjectSprite=model.ObjectSprite, ObjectType = model.ObjectType, Width = model.Width, Height = model.Height, IsBlocked = model.IsBlocked, useState=Use.NUTRAL };
                return bb;
            }
            public override void UpdateState(int newState)
            {
                useState = (Use)newState;
            }
            public override int GetStateInt()
            {
                return (int)useState;
            }
        }
    }


