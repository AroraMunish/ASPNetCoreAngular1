// How we write in javascript
let log =function log(message){
    console.log(message);
}

// In typescript, there is shorter way
let dolog =(msg) =>{
    console.log(msg);
}

// In typescript, there is shorter way if there is only one line
let dologaltway =(msg) =>console.log(msg);

dologaltway('Hello');
// If there is no parameter
let dologaltway2 =() =>{
                        let msg='welcome';
                        console.log(msg);
                        }

dologaltway2();
