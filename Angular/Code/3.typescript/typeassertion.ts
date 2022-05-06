let fname;
fname = 'abc';

let log =function log(message){
    console.log(message);
}
let endsWithcfn =function endsWithcfn(name){
    return name +':' +(<string>fname).endsWith('c');
}

let alwayEndsWithcfn =function alwayEndsWithcfn(name){
    return name +':' +(name as string).endsWith('c');
}

//Both approaches are ok, but widely used across industry is 
let endsWithcAgain =fname +':' + (<string>fname).endsWith('c');

log('endswithc:' + endsWithcfn(fname));

fname='Turmeric';

log('alwayEndsWithc:' +alwayEndsWithcfn(fname));

fname='Munish';

log('alwayEndsWithc:' +alwayEndsWithcfn(fname));
