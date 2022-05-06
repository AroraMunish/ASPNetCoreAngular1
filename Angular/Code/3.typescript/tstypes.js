var a;
var b;
var c;
var d;
// Defining array of numbers, and we may initialize it with values
var e = [1, 2, 3];
// Defining array of any datatypes, and we may initialize it with values
var f = [1, true, 'a', false];
// Defining Constants
var colorRed = 0;
var colorBlue = 1;
var colorGreen = 2;
// Defining enums
var Color;
(function (Color) {
    Color[Color["Red"] = 0] = "Red";
    Color[Color["Blue"] = 1] = "Blue";
    Color[Color["Green"] = 2] = "Green";
})(Color || (Color = {}));
;
//Using Enum
var backGroundColor = Color.Red;
