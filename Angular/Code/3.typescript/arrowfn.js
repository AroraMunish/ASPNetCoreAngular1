// How we write in javascript
var log = function log(message) {
    console.log(message);
};
// In typescript, there is shorter way
var dolog = function (msg) {
    console.log(msg);
};
// In typescript, there is shorter way if there is only one line
var dologaltway = function (msg) { return console.log(msg); };
dologaltway('Hello');
// If there is no parameter
var dologaltway2 = function () {
    var msg = 'welcome';
    console.log(msg);
};
dologaltway2();
