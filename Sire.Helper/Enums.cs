using System.ComponentModel;

namespace Sire.Helper
{
    public enum Operator : short
    {
        [Description("Not Null")] NotNull = 1,
        [Description("=")] Equal = 2,
        [Description("!=")] NotEqual = 3,
        [Description(">")] Greater = 4,
        [Description(">=")] GreaterEqual = 5,
        [Description("<")] Lessthen = 6,
        [Description("<=")] LessthenEqual = 7,
        [Description("+")] Plus = 8,
        [Description("Diff")] Different = 9,
        [Description("%")] Percentage = 10,
        //[Description("BMI")] Bmi = 11,
        [Description("Null")] Null = 12,
        [Description("Soft Fetch")] SoftFetch = 13,
        [Description("Hard Fetch")] HardFetch = 14,
        [Description("Required")] Required = 15,
        [Description("Optional")] Optional = 16,
        [Description("Enable")] Enable = 17,
        [Description("Warning")] Warning = 18,
        [Description("Visible")] Visible = 19,
        [Description("Between")] Between = 20,
        [Description("Not Between")] NotBetween = 21,
        [Description("Filter")] Filter = 22,
        [Description("In")] In = 23,
        [Description("Not In")] NotIn = 24,
        [Description("-")] Minus = 25,
        [Description("/")] Divide = 26,
        [Description("*")] Multiplication = 27,
        [Description("^")] Power = 28,
        [Description("√")] SquareRoot = 29,
    }

    public enum FolderType
    {
        [Description("Logo")] Logo = 1,
       
    }
}